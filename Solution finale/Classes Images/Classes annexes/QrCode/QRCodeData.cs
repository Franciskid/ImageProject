using System;
using System.Linq;
using System.Text;
using ReedSol = ReedSolomon.ReedSolomonAlgorithm;
using EncoderMode = Photoshop3000.Barcode.QRCode.EncoderMode;
using EciMode = Photoshop3000.Barcode.QRCode.EciMode;
using Ecl = Photoshop3000.Barcode.QRCode.ErrorCorrectionLevel;
using Photoshop3000.QrCode.MyReedSol;
using System.Runtime.CompilerServices;

namespace Photoshop3000.Barcode
{
    /// <summary>
    /// Classe pour analyser et traiter les données de QR Codes.
    /// </summary>
    class QRCodeData
    {
        //Champs

        /// <summary>
        /// Version du qrcode. 1-40
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// Niveau de correction.
        /// </summary>
        public Ecl Level { get; private set; }

        /// <summary>
        /// Mode d'encodage des données.
        /// </summary>
        public EncoderMode Mode { get; private set; } = EncoderMode.Auto;

        /// <summary>
        /// Mode eci.
        /// </summary>
        public EciMode Eci { get; private set; } = EciMode.Auto;

        /// <summary>
        /// Qr code encodé.
        /// </summary>
        public string CodeQR { get; private set; }

        /// <summary>
        /// Qr code décodé ou code de l'utilisateur
        /// </summary>
        public string CodeUser { get; private set; }


        //Constructeur

        /// <summary>
        /// Initialise un <see cref="QRCodeData"/> à partir d'un niveau de correction <see cref="Ecl"/>
        /// et d'une version. La laisser à -1 si on est pas sûr de la version qu'on veut.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="version"></param>
        public QRCodeData(Ecl level, int version)
        {
            this.Version = version;

            this.Level = level;
        }



        //Méthodes publics

        /// <summary>
        /// Transforme un code lisible en code uniquement composé des donnés dessinées sur le qrcode sans les pattern.
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="mode"></param>
        /// <param name="eci"></param>
        public void Encode(string userCode, EncoderMode mode, EciMode eci)
        {
            bool useEci = mode.HasFlag(EncoderMode.Eci) && eci != EciMode.Auto; //Auto aura l'eci mode activé

            this.Mode = !mode.IsDefined() || mode == EncoderMode.Auto || useEci ? FindBestSuitedMode(userCode) : mode;

            this.CodeUser = TransformStringToMode(userCode); //Conversion du texte dans le mode donné

            bool boost = (this.Level & Ecl.Boost) == Ecl.Boost;
            this.Level = !this.Level.IsDefined() && !boost? Ecl.L : !(this.Level &= ~Ecl.Boost).IsDefined() ? Ecl.L : this.Level;

            this.Version = this.Version > 40 ? 40 : 
                this.Version <= 0 ? SmallestVersion(this.CodeUser) : this.Version;

            if (boost)
            {
                while ((this.Level + 1) <= Ecl.H && GetCapacity(this.Version, this.Level + 1) >= this.CodeUser.Length)
                    this.Level++;
            }

            int maxLength = GetCapacity(this.Version, this.Level) - EciActivatedLessCapacity(useEci); //nbr de charactère max pour cette version (et correction et encodage)

            if (maxLength < this.CodeUser.Length) //On rogne le texte s'il est trop long, on pourrait throw une exception à la place
                this.CodeUser = this.CodeUser.Substring(0, maxLength);


            string dataBase2 = ""; 
            if (useEci)
            {
                dataBase2 += Formats.ValToStrBase2(7, 4); //mode eci
                dataBase2 += Formats.ValToStrBase2((int)eci, 8); //eci
            }
            dataBase2 += GetModeIndicator(this.Mode);//Mode

            dataBase2 += GetCharacterCountIndicator(this.CodeUser.Length); //Indique le nb de char encodés
            switch (this.Mode)  //Encodage
            {
                case EncoderMode.Numeric:
                    dataBase2 += NumericEncoder(this.CodeUser);
                    break;

                case EncoderMode.AlphaNumeric:
                    dataBase2 += AlphaNumericEncoder(this.CodeUser);
                    break;

                case EncoderMode.Byte:
                    dataBase2 += ByteEncoder(this.CodeUser);
                    break;

                case EncoderMode.Kanji:
                    dataBase2 += KanjiEncoder(this.CodeUser);
                    break;
            }
            dataBase2 = AddPadBytes(dataBase2); //Padding

            var s_DataCodeWords = CreateGroupsString(true);
            var s_ErrorCodeWords = CreateGroupsString(false);

            int indexData = 0;
            for (int i = 0; i < s_DataCodeWords.Length; i++)
            {
                for (int j = 0; j < s_DataCodeWords[i].Length; j++)
                {
                    string codeword = "";
                    for (int k = 0; k < s_DataCodeWords[i][j].Length; k++, indexData += 8)
                    {
                        codeword += s_DataCodeWords[i][j][k] = dataBase2.Substring(indexData, 8);
                    }

                    byte[] errors = ReedSol.Encode(StrBase2ToBuff(codeword), GetErrorCWCount());

                    for (int k = 0; k < s_ErrorCodeWords[i][j].Length; k++)
                    {
                        s_ErrorCodeWords[i][j][k] = Formats.ValToStrBase2(errors[k], 8);
                    }
                }
            }

            this.CodeQR = DisassembleGroupsString(s_DataCodeWords) + DisassembleGroupsString(s_ErrorCodeWords); //On assemble les tableaux.
        }

        /// <summary>
        /// Transforme un code uniquement composé des donnés dessinées sur le qrcode sans les pattern en code lisible.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <param name="qrCode"></param>
        public void Decode(string qrCode)
        {
            this.CodeQR = qrCode.Substring(0, qrCode.Length - qrCode.Length % 8); //on élimine le surplus d'info.

            string[][][] s_dataCodeWords = CreateGroupsString(true);
            string[][][] s_errorCodeWords = CreateGroupsString(false);

            int dataBitsTotalLength = GetDataCodeWordsTotalCount() * 8;
            Assert(this.CodeQR.Length >= dataBitsTotalLength, "Impossible de lire l'image !");

            string dataTotalCode = this.CodeQR.Substring(0, GetDataCodeWordsTotalCount() * 8);
            string errorTotalCode = this.CodeQR.Substring(dataBitsTotalLength, this.CodeQR.Length - dataBitsTotalLength);

            AssembleGroupsString(s_dataCodeWords, dataTotalCode);
            AssembleGroupsString(s_errorCodeWords, errorTotalCode);

            string codeBase2 = null;
            for (int i = 0; i < s_dataCodeWords.Length; i++)
            {
                for (int j = 0; j < s_dataCodeWords[i].Length; j++)
                {
                    string dataCW = "", errorCW = "";
                    for (int k = 0; k < s_dataCodeWords[i][j].Length; k++)
                    {
                        dataCW += s_dataCodeWords[i][j][k];
                    }

                    for (int k = 0; k < s_errorCodeWords[i][j].Length; k++)
                    {
                        errorCW += s_errorCodeWords[i][j][k];
                    }

                    if (errorCW != null)
                    {
                        string messDecodé = BuffToStrBase2(ReedSol.Decode(StrBase2ToBuff(dataCW), StrBase2ToBuff(errorCW)));

                        codeBase2 += messDecodé == null || messDecodé == "" ? dataCW : messDecodé;
                    }
                }
            }

            Assert((codeBase2?.Length ?? 0) > 4 && SetMode(codeBase2.Substring(0, 4)), "Impossible de lire l'image (mode d'encodage non reconnu)!");

            int startIndex = 4;

            if (this.Mode.HasFlag(EncoderMode.Eci))
            {
                startIndex = 16;

                this.Eci = (EciMode)Convert.ToInt32(codeBase2.Substring(4, 8), 2);
                Assert(SetMode(codeBase2.Substring(12, 4)), "Impossible de lire l'image (mode d'encodage non reconnu)!");
            }

            int charactCountLength = charactCountIndicator[4 * (this.Version <= 9 ? 0 : this.Version <= 26 ? 1 : 2) + (int)this.Mode];

            int lengthUsrData = Convert.ToInt32(codeBase2.Substring(startIndex, charactCountLength), 2);

            int dataLength = GetDataLength(lengthUsrData);
            Assert(codeBase2.Length >= dataLength + startIndex + charactCountLength, "Impossible de lire l'image");

            string dataCode = codeBase2.Substring(startIndex + charactCountLength, dataLength);
            switch (this.Mode)
            {
                case EncoderMode.Numeric:
                    this.CodeUser = NumericDecoder(dataCode);
                    break;

                case EncoderMode.AlphaNumeric:
                    this.CodeUser = AlphaNumericDecoder(dataCode);
                    break;

                case EncoderMode.Byte:
                    this.CodeUser = ByteDecoder(dataCode);
                    break;

                case EncoderMode.Kanji:
                    this.CodeUser = KanjiDecoder(dataCode);
                    break;
            }
        }


        //Méthodes privées

        #region Encodage

        /// <summary>
        /// Renvoie le meilleur mode pour encoder ces données.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static EncoderMode FindBestSuitedMode(string message)
        {
            if (message.All(c => char.IsDigit(c)))
                return EncoderMode.Numeric;

            if (message.All(c => alphaNumChars.Contains(c)))
                return EncoderMode.AlphaNumeric;

            if (message.All(c => (c >= 0x8140 && c <= 0x9FFC) || (c >= 0xE040 && c <= 0xEBBF)))
                return EncoderMode.Kanji;

            return EncoderMode.Byte;
        }

        /// <summary>
        /// Convertit ce string dans le mode donné.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string TransformStringToMode(string message)
        {
            switch (this.Mode)
            {
                case EncoderMode.Numeric:
                    return new string(message.Where(c => char.IsDigit(c)).ToArray()); 

                case EncoderMode.AlphaNumeric:
                    return new string(message.Select(c => alphaNumChars.Contains(c) ? c : '*').ToArray());

                case EncoderMode.Byte:
                    return new string(message.Select(c => c < 256 ? c : '?').ToArray());

                case EncoderMode.Kanji:
                    Encoding shiftEnc = Encoding.GetEncoding(932);
                    Encoding currentEnc = Encoding.Unicode;

                    byte[] current = currentEnc.GetBytes(message);
                    byte[] shift = Encoding.Convert(currentEnc, shiftEnc, current);

                    string s = "";
                    for (int i = 0; i < shift.Length; i += 2)
                    {
                        s += (char)(shift[i] | (i + 1 < shift.Length ? (shift[i + 1] << 8) : 0));
                    }
                    return new string(s.Where(c => (c >= 0x8140 && c <= 0x9FFC) || (c >= 0xE040 && c <= 0xEBBF)).ToArray());
            }

            return null;
        }

        /// <summary>
        /// Détermine la version la plus petite possible pour encoder ce string alphaNum.
        /// </summary>
        /// <returns></returns>
        private int SmallestVersion(string codeAlpha)
        {
            int version = 40;

            while (version > 0 && GetCapacity(version, this.Level) >= codeAlpha.Length)
                version--;

            return Math.Min(40, Math.Max(1, version + 1));
        }

        /// <summary>
        /// Renvoie le nombre de charactère à supprimer en plus si le mode eci est activé.
        /// </summary>
        /// <returns></returns>
        private int EciActivatedLessCapacity(bool eci)
        {
            if (eci)
            {
                switch (this.Mode)
                {
                    case EncoderMode.Numeric:
                        return 4;

                    case EncoderMode.AlphaNumeric:
                        return 3;

                    case EncoderMode.Byte:
                        return 2;

                    case EncoderMode.Kanji:
                        return 1;
                }
            }

            return 0;
        }


        /// <summary>
        /// Ajoute le padding selon l'encodage.
        /// </summary>
        /// <returns></returns>
        private string AddPadBytes(string code)
        {
            int maxLength = GetDataCodeWordsTotalCount() * 8;

            code += new string('0', Math.Min(maxLength - code.Length, 4));

            if (code.Length % 8 != 0)
                code += new string('0', 8 - code.Length % 8);

            int padByteRemaining = (maxLength - code.Length) / 8; //Erreur dans le poly : -GetEcCW();

            for (int i = 0; i < padByteRemaining; i++)
                code += Formats.ValToStrBase2(i % 2 == 0 ? 236 : 17, 8);
            
            return code;
        }


        /// <summary>
        /// Encode le texte en base2 selon la méthode d'encodage numérique.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string NumericEncoder(string code)
        {
            string encoded = "";

            for (int i = 0; i < code.Length; i += 3)
            {
                int length = code.Length - i;
                int val = Convert.ToInt32(code.Substring(i, Math.Min(3, length)));
                encoded += Formats.ValToStrBase2(val, length >= 3 ? 10 : length >= 2 ? 7 : 4);
            }

            return encoded;
        }

        /// <summary>
        /// Encode le texte en base2 selon la méthode des séparations par paires.
        /// </summary>
        /// <param name="alpha"></param>
        /// <returns></returns>
        private static string AlphaNumericEncoder(string alpha)
        {
            string code = "";

            for (int i = 0; i < alpha.Length; i += 2)
            {
                if (i + 1 >= alpha.Length) //le dernier char si impair
                {
                    code += Formats.ValToStrBase2(alphaNumChars.IndexOf(alpha[i]), 6);
                }
                else //Par paire
                {
                    int val = alphaNumChars.IndexOf(alpha[i]);
                    int val2 = alphaNumChars.IndexOf(alpha[i + 1]);

                    code += Formats.ValToStrBase2(45 * val + val2, 11);
                }
            }

            return code;
        }

        /// <summary>
        /// Encode le texte en base2.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string ByteEncoder(string code)
        {
            return code.Aggregate("", (str, c) => str += Formats.ValToStrBase2(c, 8));
        }

        /// <summary>
        /// Encode le texte en base2 selon le mode d'encodage kanji.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string KanjiEncoder(string code)
        {
            string base2 = "";

            foreach (var c in code)
            {
                char realC;
                if (c >= 0x8140 && c <= 0x9FFC)
                {
                    realC = (char)(c - 0x8140);
                }
                else if (c >= 0xE040 && c <= 0xEBBF)
                {
                    realC = (char)(c - 0xC140);
                }
                else continue;

                base2 += Formats.ValToStrBase2((((byte)(realC >> 8)) * 0xC0) + ((byte)realC), 13);
            }

            return base2;
        }

        #endregion


        #region Décodage

        /// <summary>
        /// Décode un code numérique
        /// </summary>
        /// <param name="code"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string NumericDecoder(string code)
        {
            int adjustedLength = code.Length - code.Length % 10; // = code.Length / 10 * 10;

            string decoded = "";

            for (int i = 0; i < adjustedLength; i += 10)
            {
                int val = Convert.ToInt32(code.Substring(i, 10), 2);
                decoded += val / 100 + "";
                decoded += val % 100 / 10 + "";
                decoded += val % 10 + "";
            }

            if (code.Length - adjustedLength == 7)
            {
                int val = Convert.ToInt32(code.Substring(adjustedLength, 7), 2);
                decoded += val % 100 / 10 + "";
                decoded += val % 10 + "";
            }
            else if (code.Length - adjustedLength == 4)
            {
                int val = Convert.ToInt32(code.Substring(adjustedLength, 4), 2);
                decoded += val % 10 + "";
            }

            return decoded;
        }

        /// <summary>
        /// Décode un code alphanumérique
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string AlphaNumericDecoder(string code)
        {
            string alphaCode = "";

            for (int i = 0; i < code.Length - 6; i += 11)
            {
                alphaCode += alphaNumChars[Convert.ToInt32(code.Substring(i, 11), 2) / 45];
                alphaCode += alphaNumChars[Convert.ToInt32(code.Substring(i, 11), 2) % 45];
            }

            if (code.Length % 11 != 0)
                alphaCode += alphaNumChars[Convert.ToInt32(code.Substring(code.Length - 6, 6), 2)];

            return alphaCode;
        }

        /// <summary>
        /// Décode un code en byte mode (8 bit)
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string ByteDecoder(string code)
        {
            return Encoding.Default.GetString(StrBase2ToBuff(code));
        }

        /// <summary>
        /// Décode un code en kanji mode.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string KanjiDecoder(string code)
        {
            string decoded = "";

            for (int i = 0; i < code.Length; i += 13)
            {
                string charact = code.Substring(i, 13);

                byte least = Convert.ToByte(charact.Substring(7), 2);
                byte most = (byte)(Convert.ToInt32(charact.Substring(0, 7) + new string('0', 6), 2) / 0xC0);

                int result = most << 8 | least;

                result += result <= 0x9FFC - 0x8140 ? 0x8140 : 0xC140;

                decoded += (char)result;
            }

            return decoded;
        }


        /// <summary>
        /// Initialise le mode d'encodage.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private bool SetMode(string code)
        {
            for (int i = 0; i < 5; i++)
            {
                if (GetModeIndicator((EncoderMode)i) == code)
                {
                    this.Mode = (EncoderMode)i;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Renvoie le nombre de bits associés à cette longueur et au mode d'encodage.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private int GetDataLength(int length)
        {
            switch (this.Mode)
            {
                case EncoderMode.Numeric:
                    return (length * 10 + 2) / 3;

                case EncoderMode.AlphaNumeric:
                    return (length / 2 * 11) + (length % 2 * 6);

                case EncoderMode.Byte:
                    return length * 8;

                case EncoderMode.Kanji:
                    return length * 13;
            }
            return -1;
        }

        #endregion 


        /// <summary>
        /// Crée un tab de string en fonction de la taille des 2 groupes, du nombre et des tailles des blocks de ces groupes.
        /// </summary>
        /// <param name="Level"></param>
        /// <param name="Version"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string[][][] CreateGroupsString(bool data)
        {
            string[][][] groups = new string[2][][];
            for (int i = 0; i < 2; i++)
            {
                groups[i] = new string[GetBlockCount(i)][];

                for (int j = 0; j < groups[i].Length; j++)
                    groups[i][j] = new string[data ? GetDataCWCount(i) : GetErrorCWCount()];
            }

            return groups;
        }

        /// <summary>
        /// Assemble le tab de string selon la méthode inverse de <see cref="DisassembleGroupsString(string[][][])"/>. Chaque premiers 8 char sont
        /// mis dans le 1er block puis le 2ème byte est mis dans le 2ème block et ainsi de suite.
        /// </summary>
        /// <param name="cw"></param>
        /// <param name="code"></param>
        private static void AssembleGroupsString(string[][][] cw, string code)
        {
            int max = Math.Max(cw[0].FirstOrDefault()?.Length ?? 0, cw[1].FirstOrDefault()?.Length ?? 0);

            int indexCode = 0;
            for (int i = 0; i < max; i++)
            {
                for (int group = 0; group < 2; group++)
                {
                    for (int block = 0; block < cw[group].Length; block++)
                    {
                        if (i < cw[group][block].Length)
                        {
                            cw[group][block][i] = code.Substring(indexCode, 8);
                            indexCode += 8;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Renvoie un désassemblage de ce tab de string selon la méthode inverse de <see cref="AssembleGroupsString(string[][][], string)"/>: 1ers de chaque bloc et ainsi de suite.
        /// </summary>
        /// <param name="cw"></param>
        /// <returns></returns>
        private static string DisassembleGroupsString(string[][][] cw)
        {
            int max = Math.Max(cw[0].FirstOrDefault()?.Length ?? 0, cw[1].FirstOrDefault()?.Length ?? 0);
            string totalCode = "";
            for (int i = 0; i < max; i++)
            {
                for (int group = 0; group < 2; group++)
                {
                    for (int k = 0; k < cw[group].Length; k++)
                    {
                        if (i < cw[group][k].Length) //Les blocs n'auront pas la meme taille entre les groupes
                        {
                            totalCode += cw[group][k][i];
                        }
                    }
                }
            }

            return totalCode;
        }


        /// <summary>
        /// Sépare le string par octet et le convertit en tab de byte.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static byte[] StrBase2ToBuff(string str)
        {
            byte[] b = new byte[(str?.Length ?? 0) / 8];

            for (int i = 0; i < b.Length; i++)
            {
                b[i] = Convert.ToByte(str.Substring(i * 8, Math.Min(8, str.Length - i * 8)), 2);
            }

            return b;
        }

        /// <summary>
        /// Renvoie le string en base 2.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private static string BuffToStrBase2(byte[] array)
        {
            return array?.Aggregate("", (str, val) => str += Formats.ValToStrBase2(val, 8)) ?? "";
        }

        /// <summary>
        /// Lève une exception si une condition n'est pas vérifiée.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="error"></param>
        private static void Assert(bool condition, string error)
        {
            if (!condition)
                throw new Exception(error);
        }


        #region Données et récupération des données

        /// <summary>
        /// Renvoie le nombre total d'octet des données utilisateurs pour cette version et ce niveau.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private int GetDataCodeWordsTotalCount() => GetBlockCount(0) * GetDataCWCount(0) + GetBlockCount(1) * GetDataCWCount(1);

        /// <summary>
        /// Renvoie le nombre de codewords de la correction d'erreur par bloc
        /// </summary>
        private int GetErrorCWCount() => dataAndErrorCW[5 * ((this.Version - 1) * 4 + (int)this.Level)];

        /// <summary>
        /// Renvoie le nombre de blocs par groupe.
        /// </summary>
        /// <param name="group">0 (1er groupe) ou 1 (2ème groupe)</param>
        /// <returns></returns>
        private int GetBlockCount(int group) => dataAndErrorCW[5 * ((this.Version - 1) * 4 + (int)this.Level) + 1 + (group * 2)];

        /// <summary>
        /// Renvoie le nombre de codewords des données par bloc et par groupe
        /// </summary>
        /// <param name="group">0 (1er groupe) ou 1 (2ème groupe)</param>
        private int GetDataCWCount(int group) => dataAndErrorCW[5 * ((this.Version - 1) * 4 + (int)this.Level) + 1 + (group * 2) + 1];

        /// <summary>
        /// Renvoie le nombre de charactère encodable maximal pour cette version et ce niveau de correction
        /// </summary>
        /// <param name="version"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private int GetCapacity(int version, Ecl level) => capacity[4 * ((version - 1) * 4 + (int)level) + (int)this.Mode];

        /// <summary>
        /// Renvoie le code à 4 chiffre qui correspond à l'indicateur de mode d'encodage utilisé.
        /// </summary>
        private static string GetModeIndicator(EncoderMode mode) => Formats.ValToStrBase2(!mode.HasFlag(EncoderMode.Eci) ? 1 << (int)mode : 7, 4);


        /// <summary>
        /// Renvoie la longueur en base 2
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string GetCharacterCountIndicator(int length)
        {
            int index = this.Version <= 9 ? 0 : this.Version <= 26 ? 1 : 2;
            return Formats.ValToStrBase2(length, charactCountIndicator[4 * index + (int)this.Mode]);
        }


        //Constantes

        private static readonly int[] charactCountIndicator = { 10, 9, 8, 8, 12, 11, 16, 10, 14, 13, 16, 12 }; //Mode[1-9, 10-26, 27-40]

        private static readonly int[] capacity = { 41, 25, 17, 10, 34, 20, 14, 8, 27, 16, 11, 7, 17, 10, 7, 4, 77, 47, 32, 20, 63, 38, 26, 16, 48, 29, 20, 12, 34, 20, 14, 8, 127, 77, 53, 32, 101, 61, 42, 26, 77, 47, 32, 20, 58, 35, 24, 15, 187, 114, 78, 48, 149, 90, 62, 38, 111, 67, 46, 28, 82, 50, 34, 21, 255, 154, 106, 65, 202, 122, 84, 52, 144, 87, 60, 37, 106, 64, 44, 27, 322, 195, 134, 82, 255, 154, 106, 65, 178, 108, 74, 45, 139, 84, 58, 36, 370, 224, 154, 95, 293, 178, 122, 75, 207, 125, 86, 53, 154, 93, 64, 39, 461, 279, 192, 118, 365, 221, 152, 93, 259, 157, 108, 66, 202, 122, 84, 52, 552, 335, 230, 141, 432, 262, 180, 111, 312, 189, 130, 80, 235, 143, 98, 60, 652, 395, 271, 167, 513, 311, 213, 131, 364, 221, 151, 93, 288, 174, 119, 74, 772, 468, 321, 198, 604, 366, 251, 155, 427, 259, 177, 109, 331, 200, 137, 85, 883, 535, 367, 226, 691, 419, 287, 177, 489, 296, 203, 125, 374, 227, 155, 96, 1022, 619, 425, 262, 796, 483, 331, 204, 580, 352, 241, 149, 427, 259, 177, 109, 1101, 667, 458, 282, 871, 528, 362, 223, 621, 376, 258, 159, 468, 283, 194, 120, 1250, 758, 520, 320, 991, 600, 412, 254, 703, 426, 292, 180, 530, 321, 220, 136, 1408, 854, 586, 361, 1082, 656, 450, 277, 775, 470, 322, 198, 602, 365, 250, 154, 1548, 938, 644, 397, 1212, 734, 504, 310, 876, 531, 364, 224, 674, 408, 280, 173, 1725, 1046, 718, 442, 1346, 816, 560, 345, 948, 574, 394, 243, 746, 452, 310, 191, 1903, 1153, 792, 488, 1500, 909, 624, 384, 1063, 644, 442, 272, 813, 493, 338, 208, 2061, 1249, 858, 528, 1600, 970, 666, 410, 1159, 702, 482, 297, 919, 557, 382, 235, 2232, 1352, 929, 572, 1708, 1035, 711, 438, 1224, 742, 509, 314, 969, 587, 403, 248, 2409, 1460, 1003, 618, 1872, 1134, 779, 480, 1358, 823, 565, 348, 1056, 640, 439, 270, 2620, 1588, 1091, 672, 2059, 1248, 857, 528, 1468, 890, 611, 376, 1108, 672, 461, 284, 2812, 1704, 1171, 721, 2188, 1326, 911, 561, 1588, 963, 661, 407, 1228, 744, 511, 315, 3057, 1853, 1273, 784, 2395, 1451, 997, 614, 1718, 1041, 715, 440, 1286, 779, 535, 330, 3283, 1990, 1367, 842, 2544, 1542, 1059, 652, 1804, 1094, 751, 462, 1425, 864, 593, 365, 3517, 2132, 1465, 902, 2701, 1637, 1125, 692, 1933, 1172, 805, 496, 1501, 910, 625, 385, 3669, 2223, 1528, 940, 2857, 1732, 1190, 732, 2085, 1263, 868, 534, 1581, 958, 658, 405, 3909, 2369, 1628, 1002, 3035, 1839, 1264, 778, 2181, 1322, 908, 559, 1677, 1016, 698, 430, 4158, 2520, 1732, 1066, 3289, 1994, 1370, 843, 2358, 1429, 982, 604, 1782, 1080, 742, 457, 4417, 2677, 1840, 1132, 3486, 2113, 1452, 894, 2473, 1499, 1030, 634, 1897, 1150, 790, 486, 4686, 2840, 1952, 1201, 3693, 2238, 1538, 947, 2670, 1618, 1112, 684, 2022, 1226, 842, 518, 4965, 3009, 2068, 1273, 3909, 2369, 1628, 1002, 2805, 1700, 1168, 719, 2157, 1307, 898, 553, 5253, 3183, 2188, 1347, 4134, 2506, 1722, 1060, 2949, 1787, 1228, 756, 2301, 1394, 958, 590, 5529, 3351, 2303, 1417, 4343, 2632, 1809, 1113, 3081, 1867, 1283, 790, 2361, 1431, 983, 605, 5836, 3537, 2431, 1496, 4588, 2780, 1911, 1176, 3244, 1966, 1351, 832, 2524, 1530, 1051, 647, 6153, 3729, 2563, 1577, 4775, 2894, 1989, 1224, 3417, 2071, 1423, 876, 2625, 1591, 1093, 673, 6479, 3927, 2699, 1661, 5039, 3054, 2099, 1292, 3599, 2181, 1499, 923, 2735, 1658, 1139, 701, 6743, 4087, 2809, 1729, 5313, 3220, 2213, 1362, 3791, 2298, 1579, 972, 2927, 1774, 1219, 750, 7089, 4296, 2953, 1817, 5596, 3391, 2331, 1435, 3993, 2420, 1663, 1024, 3057, 1852, 1273, 784 }; //Version[1..40]Error[LMQH]Mode[]

        private static readonly int[] dataAndErrorCW = { 7, 1, 19, 0, 0, 10, 1, 16, 0, 0, 13, 1, 13, 0, 0, 17, 1, 9, 0, 0, 10, 1, 34, 0, 0, 16, 1, 28, 0, 0, 22, 1, 22, 0, 0, 28, 1, 16, 0, 0, 15, 1, 55, 0, 0, 26, 1, 44, 0, 0, 18, 2, 17, 0, 0, 22, 2, 13, 0, 0, 20, 1, 80, 0, 0, 18, 2, 32, 0, 0, 26, 2, 24, 0, 0, 16, 4, 9, 0, 0, 26, 1, 108, 0, 0, 24, 2, 43, 0, 0, 18, 2, 15, 2, 16, 22, 2, 11, 2, 12, 18, 2, 68, 0, 0, 16, 4, 27, 0, 0, 24, 4, 19, 0, 0, 28, 4, 15, 0, 0, 20, 2, 78, 0, 0, 18, 4, 31, 0, 0, 18, 2, 14, 4, 15, 26, 4, 13, 1, 14, 24, 2, 97, 0, 0, 22, 2, 38, 2, 39, 22, 4, 18, 2, 19, 26, 4, 14, 2, 15, 30, 2, 116, 0, 0, 22, 3, 36, 2, 37, 20, 4, 16, 4, 17, 24, 4, 12, 4, 13, 18, 2, 68, 2, 69, 26, 4, 43, 1, 44, 24, 6, 19, 2, 20, 28, 6, 15, 2, 16, 20, 4, 81, 0, 0, 30, 1, 50, 4, 51, 30, 1, 50, 4, 51, 30, 1, 50, 4, 51, 24, 2, 92, 2, 93, 22, 6, 36, 2, 37, 26, 4, 20, 6, 21, 28, 7, 14, 4, 15, 26, 4, 107, 0, 0, 22, 8, 37, 1, 38, 24, 8, 20, 4, 21, 22, 12, 11, 4, 12, 30, 3, 115, 1, 116, 24, 4, 40, 5, 41, 20, 11, 16, 5, 17, 24, 11, 12, 5, 13, 22, 5, 87, 1, 88, 24, 5, 41, 5, 42, 30, 5, 24, 7, 25, 24, 11, 12, 7, 13, 24, 5, 98, 1, 99, 28, 7, 45, 3, 46, 24, 15, 19, 2, 20, 30, 3, 15, 13, 16, 28, 1, 107, 5, 108, 28, 10, 46, 1, 47, 28, 1, 22, 15, 23, 28, 2, 14, 17, 15, 30, 5, 120, 1, 121, 26, 9, 43, 4, 44, 28, 17, 22, 1, 23, 28, 2, 14, 19, 15, 28, 3, 113, 4, 114, 26, 3, 44, 11, 45, 26, 17, 21, 4, 22, 26, 9, 13, 16, 14, 28, 3, 107, 5, 108, 26, 3, 41, 13, 42, 30, 15, 24, 5, 25, 28, 15, 15, 10, 16, 28, 4, 116, 4, 117, 26, 17, 42, 0, 0, 28, 17, 22, 6, 23, 30, 19, 16, 6, 17, 28, 2, 111, 7, 112, 28, 17, 46, 0, 0, 30, 7, 24, 16, 25, 24, 34, 13, 0, 0, 30, 4, 121, 5, 122, 28, 4, 47, 14, 48, 30, 11, 24, 14, 25, 30, 16, 15, 14, 16, 30, 6, 117, 4, 118, 28, 6, 45, 14, 46, 30, 11, 24, 16, 25, 30, 30, 16, 2, 17, 26, 8, 106, 4, 107, 28, 8, 47, 13, 48, 30, 7, 24, 22, 25, 30, 22, 15, 13, 16, 28, 10, 114, 2, 115, 28, 19, 46, 4, 47, 28, 28, 22, 6, 23, 30, 33, 16, 4, 17, 30, 8, 122, 4, 123, 28, 22, 45, 3, 46, 30, 8, 23, 26, 24, 30, 12, 15, 28, 16, 30, 3, 117, 10, 118, 28, 3, 45, 23, 46, 30, 4, 24, 31, 25, 30, 11, 15, 31, 16, 30, 7, 116, 7, 117, 28, 21, 45, 7, 46, 30, 1, 23, 37, 24, 30, 19, 15, 26, 16, 30, 5, 115, 10, 116, 28, 19, 47, 10, 48, 30, 15, 24, 25, 25, 30, 23, 15, 25, 16, 30, 13, 115, 3, 116, 28, 2, 46, 29, 47, 30, 42, 24, 1, 25, 30, 23, 15, 28, 16, 30, 17, 115, 0, 0, 28, 10, 46, 23, 47, 30, 10, 24, 35, 25, 30, 19, 15, 35, 16, 30, 17, 115, 1, 116, 28, 14, 46, 21, 47, 30, 29, 24, 19, 25, 30, 11, 15, 46, 16, 30, 13, 115, 6, 116, 28, 14, 46, 23, 47, 30, 44, 24, 7, 25, 30, 59, 16, 1, 17, 30, 12, 121, 7, 122, 28, 12, 47, 26, 48, 30, 39, 24, 14, 25, 30, 22, 15, 41, 16, 30, 6, 121, 14, 122, 28, 6, 47, 34, 48, 30, 46, 24, 10, 25, 30, 2, 15, 64, 16, 30, 17, 122, 4, 123, 28, 29, 46, 14, 47, 30, 49, 24, 10, 25, 30, 24, 15, 46, 16, 30, 4, 122, 18, 123, 28, 13, 46, 32, 47, 30, 48, 24, 14, 25, 30, 42, 15, 32, 16, 30, 20, 117, 4, 118, 28, 40, 47, 7, 48, 30, 43, 24, 22, 25, 30, 10, 15, 67, 16, 30, 19, 118, 6, 119, 28, 18, 47, 31, 48, 30, 34, 24, 34, 25, 30, 20, 15, 61, 16, }; //Version[1..40]ErrorLevel[LMQH][ErrorCW, Group1SizeBlock, Group1BlockDataCW, Group2SizeBlock, Group2BlockDataCW]

        private const string alphaNumChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ $%*+-./:";

        #endregion

    }
}
