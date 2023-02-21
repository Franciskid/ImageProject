using System;
using System.Collections.Generic;
using Mask = System.Func<int, int, bool>;
using Ecl = Photoshop3000.Barcode.QRCode.ErrorCorrectionLevel;

namespace Photoshop3000.Barcode
{
    /// <summary>
    /// Classe pour dessiner des QR Codes de niveau 1 à 40.
    /// </summary>
    class DrawQRCode
    {
        //Champs

        private QRCode qrCode;

        private bool makeNull = false;

        private int mask = 0;


        //Constructeurs

        /// <summary>
        /// Renvoie un <see cref="QRCode"/> dessiné à partir d'un <see cref="QRCodeData"/> et d'un mask à laisser à -1 si on est pas sûr
        /// </summary>
        /// <param name="data">Données du QRCode</param>
        /// <param name="mask">On utilise le mask indiqué ou on choisi le meilleur mask possible</param>
        public static QRCode GenerateQRCode(QRCodeData data, int mask = -1)
        {
            DrawQRCode draw = new DrawQRCode
            {
                qrCode = new QRCode(data),
                mask = mask
            };

            draw.Draw();

            return draw.qrCode;
        }


        /// <summary>
        /// Prend en paramètre un QrCode rempli et met tous les modules de pattern à null.
        /// </summary>
        /// <param name="qrCode"></param>
        public static void ResetModules(QRCode qrCode)
        {
            DrawQRCode draw = new DrawQRCode
            {
                qrCode = qrCode,
                makeNull = true
            };

            draw.DrawModules();
        } //Nécessaire pour le scan


        //Méthodes privées

        /// <summary>
        /// Dessine le qr code et lui applique un masque.
        /// </summary>
        private void Draw()
        {
            Dictionary<int, Mask> modèles = new Dictionary<int, Mask>(8)
            {
                { 0, MaskModèles.Mask0 }, { 1, MaskModèles.Mask1 }, { 2, MaskModèles.Mask2 }, { 3, MaskModèles.Mask3 },
                { 4, MaskModèles.Mask4 }, { 5, MaskModèles.Mask5 }, { 6, MaskModèles.Mask6 }, { 7, MaskModèles.Mask7 },
            };
            int minScore = int.MaxValue;
            int bestMask = this.mask <= 7 ? this.mask : -1;

            if (bestMask < 0) //On choisit le meilleur mask
            {
                foreach (var modele in modèles)
                {
                    this.mask = modele.Key;

                    this.qrCode.Reset();

                    DrawModules();

                    DrawDataAndMask(modele.Value);

                    int score = MaskModèles.GetPenaltyScore(this.qrCode);
                    if (score < minScore)
                    {
                        bestMask = modele.Key;
                        minScore = score;
                    }
                }
            }

            this.mask = bestMask;

            this.qrCode.Reset();
            DrawModules();

            DrawDataAndMask(modèles[this.mask]);
        }

        /// <summary>
        /// Dessine les modèles de modules sur ce qrcode.
        /// </summary>
        private void DrawModules()
        {
            DrawFinderPatternsAndSeperators();

            if (this.qrCode.Data.Version >= 2)
                DrawAlignmentsPatterns();

            DrawTimingPatterns();

            DrawFormatInformation();

            DrawDarkModule();

            if (this.qrCode.Data.Version >= 7)
                DrawVersionInformation();
        }

        /// <summary>
        /// Place les données et ajoute le mask spécifié.
        /// </summary>
        private void DrawDataAndMask(Mask mask)
        {
            int indexCode = 0;
            int indexY = this.qrCode.Size - 1;
            int directionY = -1;

            for (int i = this.qrCode.Size - 1; i >= 0; i -= 2) //Colonnes en partant de la dernière
            {
                if (i == 6) //Skip le timing pattern : position du timing pattern = 6
                    i--;

                for (; indexY >= 0 && indexY < this.qrCode.Size; indexY += directionY) //alternance haut bas
                {
                    for (int k = i; k > i - 2; k--) //droite gauche
                    {
                        if (this.qrCode[k, indexY] == null) //Null = case vide donc libre pour y mettre les données
                        {
                            if (indexCode < this.qrCode.Data.CodeQR.Length)
                            {
                                this.qrCode[k, indexY] = this.qrCode.Data.CodeQR[indexCode++] == '1';
                            }
                            else //On remplit la matrice.
                            {
                                this.qrCode[k, indexY] = false;
                            }

                            if (mask(k, indexY)) //Mask
                                this.qrCode[k, indexY] = !this.qrCode[k, indexY];
                        }
                    }
                }

                directionY = directionY == -1 ? 1 : -1;

                indexY += directionY; //Comme on a dépassé les bords, on s'y remet.
            }
        }


        #region module placement

        private void DrawFinderPatternsAndSeperators()
        {
            //Finder patterns
            DrawFinderPattern(0, 0);
            DrawFinderPattern(this.qrCode.Size - 7, 0);
            DrawFinderPattern(0, this.qrCode.Size - 7);

            //Séparateurs
            DrawStraightLineHorizontal(7, 0, 8, this.makeNull ? (bool?)null : false);//gauche
            DrawStraightLineHorizontal(7, this.qrCode.Size - 8, 8, this.makeNull ? (bool?)null : false); //droite
            DrawStraightLineHorizontal(this.qrCode.Size - 8, 0, 8, this.makeNull ? (bool?)null : false); //bas

            DrawStraightLineVertical(0, 7, 8, this.makeNull ? (bool?)null : false);//gauche
            DrawStraightLineVertical(this.qrCode.Size - 8, 7, 8, this.makeNull ? (bool?)null : false); //droite
            DrawStraightLineVertical(0, this.qrCode.Size - 8, 8, this.makeNull ? (bool?)null : false); //bas
        }

        /// <summary>
        /// Module 7x7
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void DrawFinderPattern(int x, int y)
        {
            //Centre noir
            for (int i = x + 2; i <= x + 5; i++)
            {
                for (int j = y + 2; j <= y + 5; j++)
                {
                    this.qrCode[i, j] = this.makeNull ? (bool?)null : true;
                }
            }

            //1er Contour blanc intérieur
            DrawStraightLineHorizontal(y + 1, x + 1, 5, this.makeNull ? (bool?)null : false);
            DrawStraightLineHorizontal(y + 5, x + 1, 5, this.makeNull ? (bool?)null : false);

            DrawStraightLineVertical(y + 1, x + 1, 5, this.makeNull ? (bool?)null : false);
            DrawStraightLineVertical(y + 1, x + 5, 5, this.makeNull ? (bool?)null : false);

            //2eme Contour noir ext
            DrawStraightLineHorizontal(y, x, 7, this.makeNull ? (bool?)null : true);
            DrawStraightLineHorizontal(y + 6, x, 7, this.makeNull ? (bool?)null : true);

            DrawStraightLineVertical(y, x, 7, this.makeNull ? (bool?)null : true);
            DrawStraightLineVertical(y, x + 6, 7, this.makeNull ? (bool?)null : true);
        }


        private void DrawTimingPatterns()
        {
            for (int i = 6; i < this.qrCode.Size - 6; i++)
                this.qrCode[i, 6] = this.qrCode[6, i] = this.makeNull ? (bool?)null : i % 2 == 0;
        }

        private void DrawDarkModule()
        {
            this.qrCode[8, 4 * this.qrCode.Data.Version + 9] = this.makeNull ? (bool?)null : true;
        }


        private void DrawFormatInformation()
        {
            //string val = QRCodeData.masksAndErrorInfo[(int)this.qrCode.Data.Level * 8 + this.mask];
            string val = GenerateFormatString(this.qrCode.Data.Level, this.mask);

            int indexVal = 0;
            for (int i = 0; i < 9; i++) //gauche -> horizontal
            {
                if (i == 6)
                    i++;
                this.qrCode[i, 8] = this.makeNull ? (bool?)null : val[indexVal++] == '1';
            }
            for (int i = 7; i >= 0; i--) //gauche -> vertical
            {
                if (i == 6)
                    i--;
                this.qrCode[8, i] = this.makeNull ? (bool?)null : val[indexVal++] == '1';
            }

            indexVal = 0;
            for (int i = this.qrCode.Size - 1; i > this.qrCode.Size - 1 - 7; i--) //bas vertical
            {
                this.qrCode[8, i] = this.makeNull ? (bool?)null : val[indexVal++] == '1';
            }

            for (int i = this.qrCode.Size - 1 - 7; i < this.qrCode.Size; i++) //droite horizontal
            {
                this.qrCode[i, 8] = this.makeNull ? (bool?)null : val[indexVal++] == '1';
            }
        }

        private void DrawVersionInformation()
        {
            string code = GenerateVersionString(this.qrCode.Data.Version);
            int indexCode = code.Length - 1;
            for (int i = 0; i < 6; i++)
            {
                for (int j = this.qrCode.Size - 11; j < this.qrCode.Size - 11 + 3; j++)
                {
                    this.qrCode[i, j] = makeNull ? (bool?)null : code[indexCode--] == '1';
                }
            }

            indexCode = code.Length - 1;
            for (int i = 0; i < 6; i++)
            {
                for (int j = this.qrCode.Size - 11; j < this.qrCode.Size - 11 + 3; j++)
                {
                    this.qrCode[j, i] = makeNull ? (bool?)null : code[indexCode--] == '1';
                }
            }
        }


        private void DrawAlignmentsPatterns()
        {
            if (this.qrCode.Data.Version >= 2)
            {
                int index = (this.qrCode.Data.Version - 2) * 7;
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        int xPos = alignmentPatternPosition[index + i];
                        int yPos = alignmentPatternPosition[index + j];

                        if (IsAlignmentPatternDrawable(xPos, yPos))
                            DrawAlignmentPattern(xPos, yPos);
                    }
                }
            }
        }

        /// <summary>
        /// Check si on peut placer l'alignment pattern ou non.
        /// </summary>
        /// <param name="xCenter"></param>
        /// <param name="yCenter"></param>
        /// <returns></returns>
        private bool IsAlignmentPatternDrawable(int xCenter, int yCenter)
        {
            if (xCenter < 2 || yCenter < 2 || xCenter >= this.qrCode.Size - 2 || yCenter >= this.qrCode.Size - 2)
                return false;

            for (int i = xCenter - 2; i <= xCenter + 2; i++)
                for (int j = yCenter - 2; j <= yCenter + 2; j++)
                    if ((!makeNull && this.qrCode[i, j] != null) || (makeNull && this.qrCode[i, j] == null))
                        return false;
            return true;
        }

        /// <summary>
        /// Module 5x5
        /// </summary>
        /// <param name="xCenter"></param>
        /// <param name="yCenter"></param>
        private void DrawAlignmentPattern(int xCenter, int yCenter)
        {
            for (int i = xCenter - 2; i <= xCenter + 2; i++)
                for (int j = yCenter - 2; j <= yCenter + 2; j++)
                    this.qrCode[i, j] = this.makeNull ? (bool?)null : true;

            for (int i = xCenter - 1; i <= xCenter + 1; i++)
                for (int j = yCenter - 1; j <= yCenter + 1; j++)
                    this.qrCode[i, j] = this.makeNull ? (bool?)null : false;

            this.qrCode[xCenter, yCenter] = this.makeNull ? (bool?)null : true;
        }


        private void DrawStraightLineHorizontal(int y, int x, int length, bool? val)
        {
            for (int i = x; i < x + length; i++)
                this.qrCode[i, y] = val;
        }

        private void DrawStraightLineVertical(int y, int x, int length, bool? val)
        {
            for (int i = y; i < y + length; i++)
                this.qrCode[x, i] = val;
        }

        #endregion


        /// <summary>
        /// Génère un string de longueur 15 avec l'erreur et le mask encodés.
        /// </summary>
        /// <param name="error">L, M, Q, H</param>
        /// <param name="mask">0-7</param>
        /// <returns></returns>
        private static string GenerateFormatString(Ecl error, int mask)
        {
            string bitFormat = error == Ecl.M ? "00" : error == Ecl.L ? "01" : error == Ecl.H ? "10" : "11";
            bitFormat += Formats.ValToStrBase2(mask, 3);

            const string poly = "10100110111";

            string divide = bitFormat.PadRight(15, '0').TrimStart('0');

            while (divide.Length > 10)
            {
                string currForm = poly.PadRight(divide.Length, '0');

                divide = Formats.ValToStrBase2(Convert.ToInt32(currForm, 2) ^ Convert.ToInt32(divide, 2), divide.Length).TrimStart('0');
            }
            divide = divide.PadLeft(10, '0');

            string total = bitFormat + divide;

            return Formats.ValToStrBase2(Convert.ToInt32(total, 2) ^ 0b_101010000010010, 15);
        }

        /// <summary>
        /// Génère un string de longueur 18 avec la version encodée.
        /// </summary>
        /// <param name="version">1-40</param>
        /// <returns></returns>
        private static string GenerateVersionString(int version)
        {
            const string poly = "1111100100101";

            string versionForm = Formats.ValToStrBase2(version, 6);

            string versionNumb = versionForm.PadRight(18, '0').TrimStart('0');

            while (versionNumb.Length > 12)
            {
                string polyPadd = poly.PadRight(versionNumb.Length, '0');

                versionNumb = Formats.ValToStrBase2(Convert.ToInt32(polyPadd, 2) ^ Convert.ToInt32(versionNumb, 2), versionNumb.Length).TrimStart('0');
            }

            versionNumb = versionNumb.PadLeft(12, '0');

            return versionForm + versionNumb;
        }


        private static readonly int[] alignmentPatternPosition = { 6, 18, -1, -1, -1, -1, -1, 6, 22, -1, -1, -1, -1, -1, 6, 26, -1, -1, -1, -1, -1, 6, 30, -1, -1, -1, -1, -1, 6, 34, -1, -1, -1, -1, -1, 6, 22, 38, -1, -1, -1, -1, 6, 24, 42, -1, -1, -1, -1, 6, 26, 46, -1, -1, -1, -1, 6, 28, 50, -1, -1, -1, -1, 6, 30, 54, -1, -1, -1, -1, 6, 32, 58, -1, -1, -1, -1, 6, 34, 62, -1, -1, -1, -1, 6, 26, 46, 66, -1, -1, -1, 6, 26, 48, 70, -1, -1, -1, 6, 26, 50, 74, -1, -1, -1, 6, 30, 54, 78, -1, -1, -1, 6, 30, 56, 82, -1, -1, -1, 6, 30, 58, 86, -1, -1, -1, 6, 34, 62, 90, -1, -1, -1, 6, 28, 50, 72, 94, -1, -1, 6, 26, 50, 74, 98, -1, -1, 6, 30, 54, 78, 102, -1, -1, 6, 28, 54, 80, 106, -1, -1, 6, 32, 58, 84, 110, -1, -1, 6, 30, 58, 86, 114, -1, -1, 6, 34, 62, 90, 118, -1, -1, 6, 26, 50, 74, 98, 122, -1, 6, 30, 54, 78, 102, 126, -1, 6, 26, 52, 78, 104, 130, -1, 6, 30, 56, 82, 108, 134, -1, 6, 34, 60, 86, 112, 138, -1, 6, 30, 58, 86, 114, 142, -1, 6, 34, 62, 90, 118, 146, -1, 6, 30, 54, 78, 102, 126, 150, 6, 24, 50, 76, 102, 128, 154, 6, 28, 54, 80, 106, 132, 158, 6, 32, 58, 84, 110, 136, 162, 6, 26, 54, 82, 110, 138, 166, 6, 30, 58, 86, 114, 142, 170 };

    }
}