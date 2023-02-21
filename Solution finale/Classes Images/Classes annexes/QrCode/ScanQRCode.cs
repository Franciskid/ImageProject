using System;
using System.Linq;
using Mask = System.Func<int, int, bool>;
using Ecl = Photoshop3000.Barcode.QRCode.ErrorCorrectionLevel;
using System.Collections.Generic;
using Photoshop3000.Annexes;

namespace Photoshop3000.Barcode
{
    /// <summary>
    /// Classe pour scanner des QR Codes de niveau 1 à 40.
    /// </summary>
    class ScanQRCode
    {
        //Champs

        private QRCode qrCode;

        /// <summary>
        /// Indique s'il faut prendre en compte la couleur qui revient le plus lors du décodage du QrCode. 
        /// Peut ajouter un temps non négligeable pour les grosses images (>10M pixels)
        /// </summary>
        public static bool FindMostOcurrentClr = false;

        private const int divergenceMAX = 15;

        private int version, mask;

        /// <summary>
        /// Image scannée.
        /// </summary>
        public MyImage Image { get; private set; }

        /// <summary>
        /// Message issu du scan de l'image.
        /// </summary>
        public string Message => this.qrCode.Data.CodeUser;


        //Constructeurs

        /// <summary>
        /// Scan une image. L'image doit être grande (> 1000) si le niveau du qrcode est > 7.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <param name="qrCode"></param>
        public ScanQRCode(MyImage qrCode)
        {
            this.Image = qrCode;

            InitializeQrCodeFromImage();

            InitializeQrCodeData();

            DecodeData();
        }


        //Méthodes privées

        /// <summary>
        /// Initialise le qrcode à partir d'une image contenant un qrcode.
        /// </summary>
        private void InitializeQrCodeFromImage()
        {
            int y = FindYAxisFirstModule();

            if (y < 0)
                throw new ArgumentException("Impossible de lire l'image");

            int size = GetSize(y, out int x, out float sizeMod);

            if (size == 0)
                throw new ArgumentException("Impossible de lire l'image");

            this.qrCode = new QRCode(size);
            this.version = QRCode.SizeToVersion(size);

            FillQrCode(y, x, sizeMod);
        }

        #region Initialisation Matrice QrCode

        /// <summary>
        /// Renvoie la taille en module de ce QrCode ainsi que la taille d'un module en pixel et la colonne du 1er module.
        /// </summary>
        /// <param name="yAxisFirstModule"></param>
        /// <param name="xAxisFirstModule"></param>
        /// <param name="sizeMod"></param>
        /// <returns></returns>
        private int GetSize(int yAxisFirstModule, out int xAxisFirstModule, out float sizeMod)
        {
            int lengthAlignmentPattern = 0;

            xAxisFirstModule = 0;
            bool inAlignmentPattern = false;

            Pixel brgndClr = this.Image[yAxisFirstModule, 0];
            Pixel frgndClr = Pixel.Zero;
            for (int x = this.Image.Width * 3 / 100; x < this.Image.Width * 97 / 100; x++)
            {
                if (!inAlignmentPattern && Pixel.Divergence(brgndClr, this.Image[yAxisFirstModule, x]) > divergenceMAX) //20%
                {
                    frgndClr = this.Image[yAxisFirstModule, x];
                    inAlignmentPattern = true;
                    xAxisFirstModule = x;
                }
                if (inAlignmentPattern && Pixel.Divergence(frgndClr, this.Image[yAxisFirstModule, x]) <= divergenceMAX)
                {
                    lengthAlignmentPattern++;
                }
                else if (inAlignmentPattern)
                {
                    break;
                }
            }
            int endModules = 0;

            for (int x = (this.Image.Width - 1) * 97 / 100; x >= 0; x--) //on part de la fin de l'image.
            {
                if (Pixel.Divergence(brgndClr, this.Image[yAxisFirstModule, x]) > divergenceMAX) //20%
                {
                    endModules = x;
                    break;
                }
            }

            sizeMod = (float)lengthAlignmentPattern / 7;

            int length = endModules - xAxisFirstModule;
            float size = length / sizeMod;

            int[] taillesPossibles = new int[40]; //On prend la taille qui se rapproche le plus de ce qu'on a trouvé.
            for (int i = 0; i < taillesPossibles.Length; i++)
                taillesPossibles[i] = QRCode.VersionToSize(i + 1);

            int realSize = taillesPossibles.OrderBy(x => Math.Abs((double)x - size)).First(); //vraie taille du qrcode

            sizeMod = (float)length / realSize; //taille moyenne d'un module

            if (QRCode.SizeToVersion(realSize) >= 7) //On va checker le module de taille pour plus de précision
            {
                int taille = GetVersion(sizeMod, xAxisFirstModule, yAxisFirstModule + length, frgndClr);
                if (Math.Abs(100 - (realSize * 100 / taille)) < divergenceMAX)
                    realSize = taille;
            }

            sizeMod = (float)length / realSize; //taille moyenne d'un module

            return realSize;
        }


        /// <summary>
        /// Initialise la version de ce QrCode et renvoie la taille en module.
        /// </summary>
        /// <returns></returns>
        private int GetVersion(float moduleSize, float _xAxis, float _yAxis, Pixel f)
        {
            for (int k = 0; k < 10 && this.version == 0; k++)
            {
                string version = "";

                float xAxis = _xAxis + moduleSize / ((float)(k + 3) / 2);
                float yAxis = _yAxis - moduleSize * (10 + k / 3);

                for (int i = 0; i < 6; i++, xAxis += moduleSize)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        version += Pixel.Divergence(this.Image[(int)(yAxis + moduleSize * j), (int)xAxis], f) >= divergenceMAX ? "1" : "0";
                    }
                }

                version = new string(((IEnumerable<char>)version).Reverse().ToArray()); //On inverse le string

                this.version = DecodeVersion(version);
            }

            return QRCode.VersionToSize(this.version);
        }

        /// <summary>
        /// Décode la version encodé, string de longueur 18
        /// </summary>
        /// <param name="versionInfo"></param>
        /// <returns></returns>
        private static int DecodeVersion(string versionInfo)
        {
            return Convert.ToInt32(versionInfo.Substring(0, 6), 2);
        }


        /// <summary>
        /// Renvoie la ligne du 1er module détecté dans l'image
        /// </summary>
        /// <returns></returns>
        private int FindYAxisFirstModule()
        {
            Pixel bckgrndClr = FindMostOcurrentClr ? MyImageStats.GetMostOccurentPixel(this.Image) : this.Image[0, 0]; //Couleur du 1er pixel

            for (int pos = this.Image.Height * 3 / 100; pos < this.Image.Height * 97 / 100; pos++)
            {
                for (int x = this.Image.Width * 3 / 100; x < this.Image.Width * 97 / 100; x++)
                {
                    if (Pixel.Divergence(this.Image[pos, x], bckgrndClr) >= divergenceMAX)
                        return pos;
                }
            }

            return -1;
        }

        /// <summary>
        /// Rempli le <see cref="QRCode"/> à partir de l'image et de la position du 1er module dans l'image ainsi que de de sa taille.
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <param name="sizeMod"></param>
        private void FillQrCode(int y, int x, float sizeMod)
        {
            //Les modules n'auront presque jamais un entier pour taille, on ne peut pas prendre l'entier le plus proche
            //pour taille finale car au fur et à mesure qu'on lit le qrcode on prendrait du retard ou de l'avance sur les
            //modules qu'on lit. Pour que cette méthode fonctionne il faut néanmoins des modules de taille > 2 pixels ou = 1, 2.

            float fy = y + sizeMod / 2; //on essaie de se placer un peu plus vers l'intérieur du module.

            Pixel fgrndClr = this.Image[y, x];
            for (int i = 0; i < this.qrCode.Size; i++, fy += sizeMod)
            {
                float fx = x + sizeMod / 2;

                for (int j = 0; j < this.qrCode.Size; j++, fx += sizeMod)
                {
                    this.qrCode[j, i] = Pixel.Divergence(this.Image[(int)fy, (int)fx], fgrndClr) <= divergenceMAX;
                }

            }
        }

        #endregion


        /// <summary>
        /// Initialise le <see cref="QRCodeData"/> du <see cref="QRCode"/> de cette instance.
        /// </summary>
        private void InitializeQrCodeData()
        {
            string format1 = "", format2 = "";

            for (int i = 0; i < 9; i++) //gauche -> horizontal
            {
                if (i == 6)
                    i++;
                format1 += this.qrCode[i, 8] == true ? "1" : "0";
            }
            for (int i = 7; i >= 0; i--) //gauche -> vertical
            {
                if (i == 6)
                    i--;
                format1 += this.qrCode[8, i] == true ? "1" : "0";
            }


            for (int i = this.qrCode.Size - 1; i > this.qrCode.Size - 1 - 7; i--) //bas vertical
            {
                format2 += this.qrCode[8, i] == true ? "1" : "0";
            }
            for (int i = this.qrCode.Size - 1 - 7; i < this.qrCode.Size; i++) //droite horizontal
            {
                format2 += this.qrCode[i, 8] == true ? "1" : "0";
            }

            bool setData = GetEcLAndMask(format1, format2, out Ecl error, out this.mask);

            if (!setData)
            {
                throw new ArgumentException("Impossible de lire l'image");
            }
            else
            {
                QRCodeData data = new QRCodeData(error, this.version);

                this.qrCode.SetData(data);
            }
        }

        /// <summary>
        /// Décode le niveau de correction d'erreur et le mask encodés, de longueur 15.
        /// </summary>
        private static bool GetEcLAndMask(string format1, string format2, out Ecl error, out int mask)
        {
            string xorFormat = Formats.ValToStrBase2(Convert.ToInt32(format1, 2) ^ Convert.ToInt32("101010000010010", 2), format1.Length);

            int valError = Convert.ToInt32(xorFormat.Substring(0, 2), 2);
            error = valError == 0 ? Ecl.M : valError == 1 ? Ecl.L : valError == 2 ? Ecl.H : Ecl.Q;

            mask = Convert.ToInt32(xorFormat.Substring(2, 3), 2);

            return true;
        }



        /// <summary>
        /// Décode les données de ce <see cref="QRCode"/> et met le résultat dans <see cref="QRCodeData.CodeUser"/>.
        /// </summary>
        private void DecodeData()
        {
            DrawQRCode.ResetModules(this.qrCode); //On met à null tout ce qui n'appartient pas aux données.

            RemoveMask(); //On enlève le masque sur les données

            this.qrCode.Data.Decode(GetData()); //On lit les données puis les décode.
        }

        /// <summary>
        /// Elimine le mask.
        /// </summary>
        private void RemoveMask()
        {
            Mask mask = new Mask[]
            {
                MaskModèles.Mask0, MaskModèles.Mask1, MaskModèles.Mask2, MaskModèles.Mask3,
                MaskModèles.Mask4, MaskModèles.Mask5, MaskModèles.Mask6, MaskModèles.Mask7,
            }[this.mask];

            //Alternativement, on pourrait utiliser Reflection: (Mask)typeof(MaskModèles).GetMethod($"Mask{this.mask}").CreateDelegate(typeof(Mask));
            for (int i = 0; i < this.qrCode.Size; i++)
            {
                for (int j = 0; j < this.qrCode.Size; j++)
                {
                    if (mask(i, j))
                        this.qrCode[i, j] = !this.qrCode[i, j];
                }
            }
        }

        /// <summary>
        /// Renvoie les données sur le QrCode.
        /// </summary>
        /// <returns></returns>
        private string GetData()
        {
            int indexY = this.qrCode.Size - 1;
            int directionY = -1;

            string code = "";

            for (int i = this.qrCode.Size - 1; i >= 0; i -= 2) //Colonnes en partant de la dernière
            {
                if (i == 6) //Skip le timing patern : position du timing pattern = 6
                    i--;

                for (; indexY >= 0 && indexY < this.qrCode.Size; indexY += directionY) //alternance haut bas
                {
                    for (int k = i; k > i - 2; k--) //droite gauche
                    {
                        if (this.qrCode[k, indexY] != null) //Null = case vide donc n'appartient pas aux données
                        {
                            code += (bool)this.qrCode[k, indexY] ? "1" : "0";
                        }
                    }
                }

                directionY = directionY == -1 ? 1 : -1;

                indexY += directionY; //Comme on a dépassé les bords, on s'y remet.
            }

            return code;
        }



        public override string ToString()
        {
            return $@"Message : ""{this.Message}""" + "\r\n" +
                $"Version : {this.qrCode.Data.Version}\r\n" +
                $"Mask : {this.mask}\r\n" +
                $"Niveau d'erreur : {this.qrCode.Data.Level}\r\n" +
                $"Mode d'encodage : {this.qrCode.Data.Mode}\r\n" +
                $"Mode ECI : {this.qrCode.Data.Eci}\r\n";
        }
    }
}
