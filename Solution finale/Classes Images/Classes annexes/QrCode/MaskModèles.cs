using System;

namespace Photoshop3000.Barcode
{
    /// <summary>
    /// Masks
    /// </summary>
    static class MaskModèles
    {
        public static bool Mask0(int x, int y) => (x + y) % 2 == 0;

        public static bool Mask1(int x, int y) => y % 2 == 0;

        public static bool Mask2(int x, int y) => x % 3 == 0;

        public static bool Mask3(int x, int y) => (x + y) % 3 == 0;

        public static bool Mask4(int x, int y) => ((y / 2) + (x / 3)) % 2 == 0;

        public static bool Mask5(int x, int y) => ((x * y) % 2) + ((x * y) % 3) == 0;

        public static bool Mask6(int x, int y) => (((x * y) % 2) + ((x * y) % 3)) % 2 == 0;

        public static bool Mask7(int x, int y) => (((x + y) % 2) + ((x * y) % 3)) % 2 == 0;

        public static bool NoMask(int x, int y) => false; //Si on veut placer les données sans mask


        /// <summary>
        /// Renvoie le score de pénalité généré par ce QrCode.
        /// </summary>
        /// <param name="qrCode"></param>
        /// <returns></returns>
        public static int GetPenaltyScore(QRCode qrCode)
        {
            int score1 = 0, score2 = 0, score3 = 0, score4 = 0;

            //Penalité 1
            for (int i = 0; i < qrCode.Size; i++)
            {
                bool valeurEnCours = (bool)qrCode[0, i];
                int totalEnCours = 1;

                for (int j = 1; j < qrCode.Size; j++) //colonnes
                {
                    if (qrCode[j, i] == valeurEnCours)
                        totalEnCours++;
                    else
                    {
                        valeurEnCours = !valeurEnCours;
                        totalEnCours = 1;
                    }
                    if (totalEnCours == 5)
                        score1 += 3;
                    else if (totalEnCours > 5)
                        score1++;
                }

                valeurEnCours = (bool)qrCode[i, 0];
                totalEnCours = 1;

                for (int j = 1; j < qrCode.Size; j++) //lignes
                {
                    if (qrCode[i, j] == valeurEnCours)
                        totalEnCours++;
                    else
                    {
                        valeurEnCours = !valeurEnCours;
                        totalEnCours = 1;
                    }
                    if (totalEnCours == 5)
                        score1 += 3;
                    else if (totalEnCours > 5)
                        score1++;
                }
            }


            //Penalité 2
            for (var x = 0; x < qrCode.Size - 1; x++)
            {
                for (var y = 0; y < qrCode.Size - 1; y++)
                {
                    if (qrCode[x, y] == qrCode[x, y + 1] && qrCode[x, y] == qrCode[x + 1, y] && qrCode[x, y] == qrCode[x + 1, y + 1])
                        score2 += 3;
                }
            }

            //Penalité 3
            for (var i = 0; i < qrCode.Size; i++)
            {
                for (var j = 0; j < qrCode.Size - 6; j++)
                {
                    bool patternFoundHoriz = (bool)qrCode[i, j] && !(bool)qrCode[i, j + 1] && (bool)qrCode[i, j + 2]
                        && (bool)qrCode[i, j + 3] && (bool)qrCode[i, j + 4] && !(bool)qrCode[i, j + 5] && (bool)qrCode[i, j + 6];

                    bool patternBlancHoriz = false;
                    if (j - 4 >= 0)
                        patternBlancHoriz = !(bool)qrCode[i, j - 4] && !(bool)qrCode[i, j - 3] && !(bool)qrCode[i, j - 2] && !(bool)qrCode[i, j - 1];

                    if (j + 4 < qrCode.Size && !patternBlancHoriz)
                        patternBlancHoriz = !(bool)qrCode[i, j + 4] && !(bool)qrCode[i, j + 3] && !(bool)qrCode[i, j + 2] && !(bool)qrCode[i, j + 1];

                    if (patternFoundHoriz & patternBlancHoriz)
                        score3 += 40;


                    bool patternFoundVerti = (bool)qrCode[j, i] && !(bool)qrCode[j + 1, i] && (bool)qrCode[j + 2, i]
                        && (bool)qrCode[j + 3, i] && (bool)qrCode[j + 4, i] && !(bool)qrCode[j + 5, i] && (bool)qrCode[j + 6, i];

                    bool patternBlancVerti = false;
                    if (j - 4 >= 0)
                        patternBlancVerti = !(bool)qrCode[j - 4, i] && !(bool)qrCode[j - 3, i] && !(bool)qrCode[j - 2, i] && !(bool)qrCode[j - 1, i];

                    if (j + 4 < qrCode.Size && !patternBlancVerti)
                        patternBlancVerti = !(bool)qrCode[j + 4, i] && !(bool)qrCode[j + 3, i] && !(bool)qrCode[j + 2, i] && !(bool)qrCode[j + 1, i];

                    if (patternFoundVerti & patternBlancVerti)
                        score3 += 40;
                }
            }

            //Penalité 4
            int blackModules = 0;
            for (var i = 0; i < qrCode.Size; i++)
            {
                for (var j = 0; j < qrCode.Size; j++)
                {
                    if ((bool)qrCode[i, j])
                        blackModules++;
                }
            }

            double proportion = (double)blackModules / (qrCode.Size * qrCode.Size) * 100;
            int multiple5prec = Math.Abs((int)Math.Floor(proportion / 5) * 5 - 50) / 5;
            int multiple5suiv = Math.Abs((int)Math.Ceiling(proportion / 5) * 5 - 50) / 5;
            score4 = Math.Min(multiple5prec, multiple5suiv) * 10;

            return score1 + score2 + score3 + score4;
        }

    }
}
