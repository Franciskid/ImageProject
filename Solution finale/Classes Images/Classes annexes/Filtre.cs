using System;
using System.Diagnostics;

namespace Photoshop3000.Annexes
{
    /// <summary>
    /// Classe effectuant des opérations de filtrage sur des copies d'instances de <see cref="Photoshop3000.MyImage"/>
    /// </summary>
    internal class Filtre
    {
        //Champs et propriétés

        private MyImage imageFiltree;

        /// <summary>
        /// Addition du total des éléments dans la matrice de convolution de cette instance
        /// </summary>
        private float sommeTotale;

        /// <summary>
        /// Valeur ajoutée à chaque valeur de pixel lors du calcul matriciel
        /// </summary>
        private int correctif;

        /// <summary>
        /// Récupère l'instance <see cref="Photoshop3000.MyImage"/> résultante du filtrage
        /// </summary>
        public MyImage MyImage => imageFiltree;


        /// <summary>
        /// Renvoie la somme de tous les éléments du tableau
        /// </summary>
        /// <param name="convMat"></param>
        /// <returns></returns>
        private float GetTotal(float[][] convMat)
        {
            float value = 0;
            for (int i = 0; i < convMat.Length; i++)
            {
                for (int j = 0; j < convMat[i].Length; j++)
                {
                    value += convMat[i][j];
                }
            }

            return value == 0 ? 1 : value;
        }


        //Constructeur

        /// <summary>
        /// Applique à une copie d'une instance <see cref="Photoshop3000.MyImage"/> le filtre <see cref="ConvolutionMatrix"/> spécifié
        /// </summary>
        /// <param name="imageToFilter">Instance <see cref="Photoshop3000.MyImage"/> contenant les informations liées à l'image</param>
        /// <param name="convMat">Matrice de convolution</param>
        public Filtre(MyImage imageToFilter, ConvolutionMatrix convMat)
        {
            var matrice = convMat.GetMatrix();

            this.sommeTotale = GetTotal(matrice);
            this.correctif = convMat.Correctif;

            this.imageFiltree = new MyImage(imageToFilter.Height, imageToFilter.Width, imageToFilter.PixelFormat);

            if (convMat.MethodeFiltrage == 0)
            {
                ApplicationFiltreBords(imageToFilter, matrice);
            }
            else if (convMat.MethodeFiltrage == 1)
            {
                ApplicationFiltre(imageToFilter, matrice);
            }
            else
            {
                ApplicationFiltreRapide(imageToFilter, matrice);
            }
        }


        //Méthodes de calculs, pour la vitesse de calcul, il y a 3 méthodes distinctes

        /// <summary>
        /// Multiplie chaque pixel d'une matrice et les pixels autours de ce pixel par une matrice de convolution
        /// Les bords ne sont pas ignorés mais calculés comme les autres pixels, leurs pixels voisins sont considérés comme étant noirs.
        /// </summary>
        /// <param name="imageToFilter"></param>
        /// <param name="convMatrix"></param>
        private void ApplicationFiltre(MyImage imageToFilter, float[][] convMatrix)
        {
            //~10x fois plus rapide que la methode ancienne

            if (!this.MyImage.Validité)
                return;

            byte[] data = this.MyImage.ToBGRArray();
            byte[] oldData = imageToFilter.ToBGRArray();

            bool alphaExiste = this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Argb32;

            int lengthPixel = alphaExiste ? 4 : 3;

            int indexH = 0;
            int maxW = this.MyImage.Stride - this.MyImage.GetPadding();
            int décalage = 0;

            int autourIndex = convMatrix.Length / 2;  //Nombre de cases à prendre en compte autour de la case, 1 si la mat est de taille 3, 2 si de taille 5

            while (indexH < this.MyImage.Height)
            {
                int indexW = 0;
                int indexImageW = 0;

                while (indexW < maxW)
                {
                    int pos = décalage + indexW;

                    float valueB = 0;
                    float valueG = 0;
                    float valueR = 0;

                    int maxY = Math.Min(indexH + autourIndex, this.MyImage.Height - 1);
                    int maxX = Math.Min(indexImageW + autourIndex, this.MyImage.Width - 1);

                    int minY = Math.Max(indexH - autourIndex, 0);
                    int minX = Math.Max(indexImageW - autourIndex, 0);

                    int newLengthLine = (maxX - minX) * lengthPixel;

                    int startDataIndex = this.MyImage.Stride * minY + minX * lengthPixel;

                    int startHeight = minY;

                    while (startHeight <= maxY)
                    {
                        int indexWW = 0;
                        int indexImageX = minX;

                        while (indexWW <= newLengthLine)
                        {
                            valueB += oldData[startDataIndex + indexWW] * convMatrix[startHeight - minY][indexImageX - minX];
                            valueG += oldData[startDataIndex + indexWW + 1] * convMatrix[startHeight - minY][indexImageX - minX];
                            valueR += oldData[startDataIndex + indexWW + 2] * convMatrix[startHeight - minY][indexImageX - minX];

                            indexWW += lengthPixel;

                            indexImageX++;
                        }

                        startDataIndex += this.MyImage.Stride;
                        startHeight++;
                    }

                    data[pos] = (byte)Math.Min(Math.Max(valueB / this.sommeTotale + this.correctif, 0), 255);
                    data[pos + 1] = (byte)Math.Min(Math.Max(valueG / this.sommeTotale + this.correctif, 0), 255);
                    data[pos + 2] = (byte)Math.Min(Math.Max(valueR / this.sommeTotale + this.correctif, 0), 255);
                    if (alphaExiste) data[pos + 3] = oldData[pos + 3];

                    indexW += lengthPixel;
                    indexImageW++;
                }
                indexH++;
                décalage += this.MyImage.Stride;
            }
        }


        /// <summary>
        /// Multiplie chaque pixel d'une matrice et les pixels autours de ce pixel par une matrice de convolution
        /// Les bords ne sont pas ignorés et laissés tels quels.
        /// </summary>
        /// <param name="imageToFilter"></param>
        /// <param name="convMatrix"></param>
        private void ApplicationFiltreRapide(MyImage imageToFilter, float[][] convMatrix)
        {
            //~10x fois plus rapide que la methode classique

            if (!this.MyImage.Validité)
                return;

            byte[] data = this.MyImage.ToBGRArray();
            byte[] oldData = imageToFilter.ToBGRArray();

            bool alphaExiste = this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Argb32;

            int lengthPixel = alphaExiste ? 4 : 3;

            int autourIndex = convMatrix.Length / 2;  //Nombre de cases à prendre en compte autour de la case, 1 si la mat est de taille 3, 2 si de taille 5

            int indexH = autourIndex;
            int startIndexW = autourIndex * lengthPixel;

            int maxW = this.MyImage.Stride - this.MyImage.GetPadding() - autourIndex * lengthPixel;
            int maxH = this.MyImage.Height - autourIndex;

            int décalage = 0;

            while (indexH < maxH)
            {
                int indexW = startIndexW;
                int indexImageW = autourIndex;

                while (indexW < maxW)
                {
                    int pos = décalage + indexW;

                    float valueB = 0;
                    float valueG = 0;
                    float valueR = 0;

                    int maxY = indexH + autourIndex;
                    int maxX = indexImageW + autourIndex;

                    int minY = indexH - autourIndex;
                    int minX = indexImageW - autourIndex;

                    int newLengthLine = (maxX - minX) * lengthPixel;

                    int startDataIndex = this.MyImage.Stride * minY + minX * lengthPixel;

                    int startHeight = minY;

                    while (startHeight <= maxY)
                    {
                        int indexWW = 0;
                        int indexImageX = minX;
                        int indexMatY = startHeight - minY;

                        while (indexWW <= newLengthLine)
                        {
                            int indexMatX = indexImageX - minX;
                            int indexOldData = startDataIndex + indexWW;

                            valueB += oldData[indexOldData] * convMatrix[indexMatY][indexMatX];
                            valueG += oldData[indexOldData + 1] * convMatrix[indexMatY][indexMatX];
                            valueR += oldData[indexOldData + 2] * convMatrix[indexMatY][indexMatX];

                            indexWW += lengthPixel;

                            indexImageX++;
                        }

                        startDataIndex += this.MyImage.Stride;
                        startHeight++;
                    }

                    data[pos] = (byte)Math.Min(Math.Max(valueB / this.sommeTotale + this.correctif, 0), 255);
                    data[pos + 1] = (byte)Math.Min(Math.Max(valueG / this.sommeTotale + this.correctif, 0), 255);
                    data[pos + 2] = (byte)Math.Min(Math.Max(valueR / this.sommeTotale + this.correctif, 0), 255);
                    if (alphaExiste) data[pos + 3] = oldData[pos + 3];

                    indexW += lengthPixel;
                    indexImageW++;
                }
                indexH++;
                décalage += this.MyImage.Stride;
            }
        }


        /// <summary>
        /// Multiplie chaque pixel d'une matrice et les pixels autours de ce pixel par une matrice de convolution.
        /// On prend les pixels à l'intérieur, "facon miroir", pour le calcul des bords.
        /// </summary>
        /// <param name="imageToFilter"></param>
        /// <param name="convMatrix"></param>
        private void ApplicationFiltreBords(MyImage imageToFilter, float[][] convMatrix)
        {
            if (!this.MyImage.Validité)
                return;

            byte[] data = this.MyImage.ToBGRArray();
            byte[] originalData = imageToFilter.ToBGRArray();

            bool alphaExiste = this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Argb32;

            int lengthPixel = alphaExiste ? 4 : 3;

            int indexH = 0;
            int maxW = this.MyImage.Stride - this.MyImage.GetPadding();
            int maxH = this.MyImage.Height;

            int autourIndex = convMatrix.Length / 2;  //Nombre de cases à prendre en compte autour de la case position[], 1 si la mat est de taille 3, 2 si de taille 5

            int[][] convMatrixInt = new int[convMatrix.Length][];

            for (int i = 0; i < convMatrixInt.Length; i++)
            {
                convMatrixInt[i] = new int[convMatrix[i].Length];
                for (int j = 0; j < convMatrix[i].Length; j++)
                    convMatrixInt[i][j] = (int)(convMatrix[i][j] * (1 << 16));
            }

            while (indexH < maxH)
            {
                int décalage = indexH * this.MyImage.Stride;

                for (int indexW = 0; indexW < maxW;)
                {
                    int valueB = 0;
                    int valueG = 0;
                    int valueR = 0;
                    //float valueB = 0;
                    //float valueG = 0;
                    //float valueR = 0;

                    int startY = (indexH - autourIndex) * this.MyImage.Stride;
                    int startX = indexW - autourIndex * lengthPixel;

                    //On applique aux pixels voisins la matrice de convolution
                    int bordsIgnorésY = indexH;
                    for (int Y = -autourIndex; Y <= autourIndex; ++Y)
                    {
                        int bordsIgnorésX = startX;
                        int bordsConsidérésIndex = Math.Abs(startY) + Math.Abs(startX);

                        int indexMatriceY = Y + autourIndex;

                        for (int X = -autourIndex; X <= autourIndex; ++X)
                        {
                            int indexMatriceX = X + autourIndex;

                            //valueB += originalData[bordsConsidérésIndex] * convMatrix[indexMatriceY][indexMatriceX];
                            //valueG += originalData[bordsConsidérésIndex + 1] * convMatrix[indexMatriceY][indexMatriceX];
                            //valueR += originalData[bordsConsidérésIndex + 2] * convMatrix[indexMatriceY][indexMatriceX];

                            valueB += (originalData[bordsConsidérésIndex] * convMatrixInt[indexMatriceY][indexMatriceX]) >> 16;
                            valueG += (originalData[bordsConsidérésIndex + 1] * convMatrixInt[indexMatriceY][indexMatriceX]) >> 16;
                            valueR += (originalData[bordsConsidérésIndex + 2] * convMatrixInt[indexMatriceY][indexMatriceX]) >> 16;

                            bordsIgnorésX += lengthPixel;

                            bordsConsidérésIndex += bordsIgnorésX < 0 || bordsIgnorésX >= maxW ? -lengthPixel : lengthPixel;
                        }

                        startY += ++bordsIgnorésY > maxH ? -this.MyImage.Stride : this.MyImage.Stride; //bords bas, on cherche vers l'intérieur
                    }

                    data[décalage + indexW] = (byte)Math.Min(Math.Max(valueB / this.sommeTotale + this.correctif, 0), 255);
                    data[décalage + indexW + 1] = (byte)Math.Min(Math.Max(valueG / this.sommeTotale + this.correctif, 0), 255);
                    data[décalage + indexW + 2] = (byte)Math.Min(Math.Max(valueR / this.sommeTotale + this.correctif, 0), 255);
                    if (alphaExiste) data[décalage + indexW + 3] = originalData[décalage + indexW + 3];

                    indexW += lengthPixel;
                }

                indexH++;
            }
        }



        /// <summary>
        /// Lent, ne pas utiliser. Laissé pour la postérité.
        /// </summary>
        /// <param name="imageToFilter"></param>
        /// <param name="convMatrix"></param>
        private void ApplicationFiltreBords_Old(MyImage imageToFilter, float[][] convMatrix)
        {
            for (int i = 0; i < imageToFilter.Height; i++) //ancien algorithme
            {
                for (int j = 0; j < imageToFilter.Width; j++)
                {
                    int argb = 255 << 24;
                    for (int k = 0; k < 3; k++)
                    {
                        argb |= GetValue(imageToFilter, convMatrix, j, i, k) << (8 * k);
                    }

                    this.imageFiltree[i, j] = Pixel.FromArgb(argb);
                }
            }
        }

        private byte GetValue(MyImage imageToFilter, float[][] convMatrix, int X, int Y, int couleur)
        {
            int autourIndex = convMatrix.Length / 2;  //Nombre de case à prendre en compte autour de la case position[], 1 si la mat est de taille 3, 2 si de taille 5

            int minY = Y - autourIndex;
            int minX = X - autourIndex;

            int maxY = Y + autourIndex;
            int maxX = X + autourIndex;

            double value = 0;

            for (int i = minY; i <= maxY; i++)
            {
                for (int j = minX; j <= maxX; j++)
                {
                    int x = j, y = i;

                    //On gère les contours en prenant les pixels miroirs à l'intérieur de l'image. 
                    //Par ex pour un pixel situé à 2 unités de distances en dehors de l'image on prendra le pixel à 2 unités à l'intérieur de l'image

                    if (x >= imageToFilter.Width)
                        x = imageToFilter.Width - 1 - (x - imageToFilter.Width);
                    else if (x < 0)
                    {
                        x = -x;
                    }
                    if (y >= imageToFilter.Height)
                        y = imageToFilter.Height - 1 - (y - imageToFilter.Height);
                    else if (y < 0)
                    {
                        y = -y;
                    }

                    byte color = couleur == 0 ? imageToFilter[y, x].B : couleur == 1 ? imageToFilter[y, x].G : imageToFilter[y, x].R;

                    value += color * convMatrix[i - minY][j - minX];
                }
            }
            return (byte)Math.Min(Math.Max(value / this.sommeTotale + this.correctif, 0), 255);

        }

    }
}