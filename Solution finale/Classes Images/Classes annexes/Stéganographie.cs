using System;
using System.Security.Cryptography;
using System.Text;

namespace Photoshop3000.Annexes
{
    /// <summary>
    /// Contient des méthodes pour cacher une image dans une image et la retrouver ainsi que 
    /// cacher du texte de manière pseudo-aléatoire à partir d'un mdp dans une image et le retrouver
    /// </summary>
    internal static class Stéganographie
    {

        /// <summary>
        /// Cache un <see cref="MyImage"/> dans un <see cref="MyImage"/> visible à partir d'un nombre de bits à cacher.<para/>
        /// L'image à cacher l'est au centre de l'image visible, si elle est plus grande que l'image visible alors ses bords sont rognés pour rentrer dans l'image visible en largeur ou en hauteur<para/>
        /// </summary>
        /// <param name="imageVisible">Image visible</param>
        /// <param name="imageToHide">Image à cacher</param>
        /// <param name="nbBitsToHide">Nombre de bits à cacher sur 1 pixel</param>
        public static MyImage CacherImage(MyImage imageVisible, MyImage imageToHide, int nbBitsToHide = 4)
        {
            int startHeight = (imageVisible.Height - imageToHide.Height) / 2;
            int startWidth = (imageVisible.Width - imageToHide.Width) / 2;

            return CacherImage(imageVisible, imageToHide, nbBitsToHide, startWidth, startHeight);
        }

        /// <summary>
        /// Cache un <see cref="MyImage"/> dans un <see cref="MyImage"/> visible à partir d'un nombre de bits à cacher.<para/>
        /// L'image à cacher l'est au point indiqué dans l'image visible, si ce point est en dehors de l'image, alors ses bords sont rognés.<para/>
        /// </summary>
        /// <param name="imageVisible">Image visible</param>
        /// <param name="imageCachée">Image à cacher</param>
        /// <param name="nbBitsToHide">Nombre de bits à cacher sur 1 pixel</param>
        /// <param name="origineX">Nombre de bits à cacher sur 1 pixel</param>
        /// <param name="origineY">Nombre de bits à cacher sur 1 pixel</param>
        public static MyImage CacherImage(MyImage imageVisible, MyImage imageCachée, int nbBitsToHide, int origineX, int origineY)
        {
            //Reprend la logique de DrawImage de MyGraphics

            if (!imageVisible.Validité)
                return null;

            nbBitsToHide = nbBitsToHide < 0 ? 0 : nbBitsToHide > 8 ? 8 : nbBitsToHide;

            MyImage imageTotale = imageVisible.Clone();

            if (nbBitsToHide == 0 || !imageCachée.Validité)
                return imageTotale;

            byte décalage = (byte)(8 - nbBitsToHide);

            byte bits_visibles = (byte)(0b1111_1111 << nbBitsToHide);


            int offSetX = 0;
            int offSetY = 0;

            if (origineX < 0)
            {
                offSetX = -origineX;
                origineX = 0;
            }
            if (origineY < 0)
            {
                offSetY = -origineY;
                origineY = 0;
            }


            int maxX = Math.Min(origineX + imageCachée.Width - offSetX, imageTotale.Width); //limites hautes non comprises
            int maxY = Math.Min(origineY + imageCachée.Height - offSetY, imageTotale.Height);

            int srcLengthPixel = imageTotale.PixelFormat.GetPixelLength();
            int copyLengthPixel = imageCachée.PixelFormat.GetPixelLength();

            int lengthDrawDataSkipStart = offSetX * copyLengthPixel;

            int lengthDrawDataSkipEnd = origineX + imageCachée.Width - offSetX - imageTotale.Width;
            if (lengthDrawDataSkipEnd < 0)
            {
                lengthDrawDataSkipEnd = imageCachée.GetPadding();
            }
            else
            {
                lengthDrawDataSkipEnd = lengthDrawDataSkipEnd * copyLengthPixel + imageCachée.GetPadding();
            }


            int totalDrawDataSkip = lengthDrawDataSkipStart + lengthDrawDataSkipEnd;

            int drawDataIndex = lengthDrawDataSkipStart + offSetY * imageCachée.Stride;
            int dataIndex = imageTotale.Stride * origineY + origineX * srcLengthPixel;
            int endDrawDataLine = (maxX - origineX) * srcLengthPixel;

            int startHeight = origineY;

            unsafe
            {
                fixed (byte* visibleDataF = imageVisible.ToBGRArray()) //Image source visible
                fixed (byte* totalDataF = imageTotale.ToBGRArray())  //Image résultante de l'opération
                fixed (byte* hiddenDataF = imageCachée.ToBGRArray())   //Image à cacher
                {
                    byte* visibleData = visibleDataF + dataIndex; //additionD et visibleD ont tjrs le meme index
                    byte* totalData = totalDataF + dataIndex;
                    byte* hiddenData = hiddenDataF;

                    while (startHeight++ < maxY)
                    {
                        for (int indexW = 0; indexW < endDrawDataLine; indexW += srcLengthPixel)
                        {
                            totalData[indexW] = (byte)((*(visibleData + indexW) & bits_visibles) | (*hiddenData >> décalage));
                            totalData[indexW + 1] = (byte)((*(visibleData + indexW + 1) & bits_visibles) | (*(hiddenData + 1) >> décalage));
                            totalData[indexW + 2] = (byte)((*(visibleData + indexW + 2) & bits_visibles) | (*(hiddenData + 2) >> décalage));

                            hiddenData += copyLengthPixel;
                        }

                        hiddenData += totalDrawDataSkip;
                        totalData += imageTotale.Stride; //les im total et visible ont le meme stride
                        visibleData += imageVisible.Stride;
                    }
                }
            }

            return imageTotale;
        }


        /// <summary>
        /// Renvoie une image cachée dans une autre
        /// </summary>
        /// <param name="imageVisible">Image à décoder</param>
        /// <param name="nbBitsHidden">Nombre de bits cachés sur 1 pixel</param>
        /// <returns></returns>
        public static MyImage GetImageCachée(MyImage imageVisible, int nbBitsHidden = 4)
        {
            //optimisation done

            if (!imageVisible.Validité)
                return null;

            nbBitsHidden = nbBitsHidden < 0 ? 0 : nbBitsHidden > 8 ? 8 : nbBitsHidden;

            if (nbBitsHidden == 0 || nbBitsHidden == 8)
                return imageVisible.Clone();

            MyImage imageCachée = new MyImage(imageVisible.Height, imageVisible.Width, imageVisible.PixelFormat);
            new MyGraphics(imageCachée).ModifyComponentValue(3, 255);//Si jamais 32bpp, car l'image est initialisée à 0 par défaut.

            byte décalage = (byte)(8 - nbBitsHidden);

            byte bits_cachés = (byte)(0b1111_1111 >> décalage);

            bool alphaExiste = imageVisible.PixelFormat == Formats.PixelFormat.BMP_Argb32;

            int skipAlpha = alphaExiste ? 2 : 1;

            int indexH = 0;
            int maxW = imageVisible.Stride - imageVisible.GetPadding();

            byte[] dataVisible = imageVisible.ToBGRArray();
            byte[] dataHidden = imageCachée.ToBGRArray();

            while (indexH < imageVisible.Height)
            {
                int décalageIndex = indexH * imageVisible.Stride;
                int indexW = 0;

                while (indexW < maxW)
                {
                    dataHidden[décalageIndex + indexW] = (byte)((dataVisible[décalageIndex + indexW] & bits_cachés) << décalage);

                    dataHidden[décalageIndex + ++indexW] = (byte)((dataVisible[décalageIndex + indexW] & bits_cachés) << décalage);

                    dataHidden[décalageIndex + ++indexW] = (byte)((dataVisible[décalageIndex + indexW] & bits_cachés) << décalage);

                    indexW += skipAlpha;
                }
                indexH++;
            }



            return imageCachée;
        }



        /// <summary>
        /// Cache du texte au format 8 bits (256 premiers chars du format UTF-8) dans les bits les moins importants d'une copie d'une image et renvoie le résultat. <para/>
        /// Les pixels sont choisis pseudo-aléatoirement à partir d'un mot de passe. Les charactères ne faisant pas partie du format 8 bits sont ignorés. <para/>
        /// On peut rajouter <see langword="\u0003"/> à la fin du texte pour signifier sa fin lors de la récupération.<para/>
        /// Ne pas utiliser la même image pour cacher 2 textes car l'un pourrait empiéter sur l'autre et corrompre certaines parties du texte. <para/>
        /// Ne pas sauvegarder l'image dans un format de compression susceptible de supprimer des données (exemple : JPEG) <para/>
        /// La longueur du texte par rapport à la taille de l'image n'influe pas sur le temps de calcul
        /// </summary>
        /// <param name="image">Image dans laquelle cacher du texte</param>
        /// <param name="textToHide">Le texte à cacher au format 8 bits. Si la longueur du texte sera inconnue lors du décodage, lui rajouter à la fin \u0003 </param>
        /// <param name="password">Mot de passe pour retrouver le texte par la suite. Pas de format particulier à respecter</param>
        /// <param name="nbreBitsCachés">Nombre de bits sur lesquels cacher le texte sur une couleur (rgb) d'un pixel. 1, 2, 4 ou 8</param>
        /// <returns></returns>
        public static MyImage CacherTexte(MyImage image, string textToHide, string password, int nbreBitsCachés = 2)
        {
            MyImage copieImage = image.Clone();

            byte décalage = (byte)nbreBitsCachés;
            if (décalage != 1 && décalage != 2 && décalage != 4 && décalage != 8) //Seuls décalages supportés
                return copieImage;

            byte bitsVisibles = (byte)(0b1111_1111 << décalage);

            byte bitsInvisibles = (byte)~bitsVisibles;

            byte[] textByte = GetByteArrayFrom8BitsString(Get8BitsString(textToHide));

            int numberOfChangesToDo = textByte.Length * 8 / décalage;
            int totalCoord = image.Width * image.Height * 3;

            if (numberOfChangesToDo > totalCoord)
            {
                return copieImage;
            }

            //Init tabs des coordonnées
            int[] coordonnéesExistentes = new int[totalCoord];
            int[] indexDesCoordonnéesDispo = new int[totalCoord];

            for (int k = 0; k < totalCoord; k++)
            {
                coordonnéesExistentes[k] = k;
                indexDesCoordonnéesDispo[k] = k;
            }

            int indexChar = 0;
            int indexDansChar = 0;

            Random rand = new Random(SecureHashString(password)); //On génère une classe Rand à partir du hashcode du mdp. On pourrait générer le hashcode à la main pour plus de sécu

            for (int i = 0; i < numberOfChangesToDo; i++)
            {
                int index = rand.Next(coordonnéesExistentes.Length - i); //Index excluant la fin du tab d'index, index déjà utilisés

                int position = coordonnéesExistentes[indexDesCoordonnéesDispo[index]];
                int temp = indexDesCoordonnéesDispo[coordonnéesExistentes.Length - 1 - i];
                indexDesCoordonnéesDispo[coordonnéesExistentes.Length - 1 - i] = index;
                indexDesCoordonnéesDispo[index] = temp;

                int shiftARGB = (2 - (position % 3 == 0 ? 0 : position % 2 == 0 ? 1 : 2)) * 8; //0, 8 ou 16 (alpha ignoré pour diverses raisons)

                position /= 3;
                int X = position % image.Width;
                int Y = position / image.Width;

                byte bytesTexte = (byte)((textByte[indexChar] & (bitsInvisibles << indexDansChar)) >> indexDansChar);
                int valueP = copieImage[Y, X].ToArgb();

                copieImage[Y, X] = Pixel.FromArgb(valueP & ~(byte.MaxValue << shiftARGB) | ((((valueP >> shiftARGB) & bitsVisibles) | bytesTexte) << shiftARGB));

                if (indexDansChar + décalage >= 8) //On change de char à cacher
                {
                    indexChar++;
                    indexDansChar = 0;
                }
                else
                {
                    indexDansChar += décalage;  //On change les bits à cacher dans le char
                }
            }

            return copieImage;
        }

        /// <summary>
        /// Récupère un texte caché dans un <see cref="MyImage"/> à partir d'un mot de passe et de la longueur du texte à trouver (pas obligé mais le texte doit alors finir par \u0003 pour signifier la fin).
        /// </summary>
        /// <param name="image">Image dans laquelle retrouver le message caché</param>
        /// <param name="password">Mot de passe</param>
        /// <param name="tailleMessage">Taille du message à retrouver. Si inconnue, laisser à 0, la longueur de texte maximale cherchée sera alors de 10 000 char sauf si on trouve un '\u0003' entre temps.</param>
        /// <param name="nbreBitsCachés">Nombre de bits utilisés pour cacher le message sur 1 pixel. 1, 2, 4 ou 8</param>
        /// <returns></returns>
        public static string GetTexteCaché(MyImage image, string password, int tailleMessage = 0, int nbreBitsCachés = 2)
        {
            const int maxAmountPixelsToLookFor = 10000;

            byte décalage = (byte)nbreBitsCachés;
            if (décalage != 1 && décalage != 2 && décalage != 4 && décalage != 8) //Seuls décalages supportés
                return string.Empty;

            byte bitsInvisibles = (byte)(0b1111_1111 >> (8 - décalage));

            int maxPixelsAvailableInImage = image.Width * image.Height * 3; //Nb max de char à cacher possible

            int amountPixelsToLookFor; //Quantité de pixels dans lesquels chercher un char ou une partie de char caché
            if (tailleMessage <= 0) //Si on connait pas la taille du texte à récupérer
            {
                amountPixelsToLookFor = maxAmountPixelsToLookFor > maxPixelsAvailableInImage ? maxPixelsAvailableInImage : maxAmountPixelsToLookFor;
            }
            else
            {
                amountPixelsToLookFor = tailleMessage * (int)Math.Ceiling((double)8 / décalage);
                amountPixelsToLookFor = amountPixelsToLookFor > maxPixelsAvailableInImage ? maxPixelsAvailableInImage : amountPixelsToLookFor;
            }


            byte[] chars = tailleMessage <= 0 ? new byte[maxAmountPixelsToLookFor / (int)Math.Ceiling((double)8 / décalage)]
                : new byte[tailleMessage];

            //Init tabs des coordonnées
            int[] positionsAvailable = new int[maxPixelsAvailableInImage];
            int[] positionsUsed = new int[maxPixelsAvailableInImage];

            for (int k = 0; k < maxPixelsAvailableInImage; k++)
            {
                positionsAvailable[k] = k;
                positionsUsed[k] = k;
            }

            int tempChar = 0; //Variable dans laquelle on enregistre les bits récupérés pour 1 char
            int indexChar = 0;   //Position dans le tableau de char
            int indexDansChar = 0;  //Position dans le byte du char du tableau de char (0 à 8)

            Random rand = new Random(SecureHashString(password)); //On génère une classe Rand à partir du hashcode du mdp. On pourrait générer le hashcode à la main pour plus de sécu

            for (int i = 0; i < amountPixelsToLookFor; i++)
            {
                int index = rand.Next(positionsAvailable.Length - i);

                int position = positionsAvailable[positionsUsed[index]];
                int temp = positionsUsed[positionsAvailable.Length - 1 - i];
                positionsUsed[positionsAvailable.Length - 1 - i] = index;
                positionsUsed[index] = temp;

                //Une position prend 3 espaces dans le tableau pour chaque composante du pixel : les nb aux index 0, 1, 2 rpz la même pos
                //L'ordre des couleurs n'importe pas tant que le même algo a été utilisé pour cacher le texte
                int shiftARGB = (2 - (position % 3 == 0 ? 0 : position % 2 == 0 ? 1 : 2)) * 8; //rgb shift, 0, 8 ou 16

                position /= 3;
                int X = position % image.Width;
                int Y = position / image.Width;

                tempChar |= (byte)(((image[Y, X].ToArgb() >> shiftARGB) & bitsInvisibles) << indexDansChar); //+= marche aussi

                if (indexDansChar + décalage >= 8)
                {
                    if (tempChar == '\u0003') //Fin du texte si on ne connaissait pas la taille du texte
                    {
                        byte[] realSizeChars = new byte[indexChar + 1];
                        for (int k = 0; k < realSizeChars.Length; k++)
                        {
                            realSizeChars[k] = chars[k];
                        }
                        return Get8BitsStringFromByteArray(realSizeChars);
                    }

                    chars[indexChar++] = (byte)tempChar;
                    tempChar = indexDansChar = 0;
                }
                else
                {
                    indexDansChar += décalage;
                }
            }

            return Get8BitsStringFromByteArray(chars);
        }



        private static int SecureHashString(string mdp)
        {
            return Formats.ConvertLittleEndianToInt(MD5.Create().ComputeHash(Encoding.Unicode.GetBytes(mdp)));
        }


        private static byte[] GetByteArrayFrom8BitsString(string text)
        {
            byte[] bytes = new byte[text.Length];

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)text[i];
            }

            return bytes;
        }

        private static string Get8BitsStringFromByteArray(byte[] array)
        {
            string txt = string.Empty;
            for (int i = 0; i < array.Length; i++)
            {
                txt += ((char)array[i]).ToString();
            }
            return txt;
        }


        private static string Get8BitsString(string text)
        {
            string realText = "";
            for (int i = 0; i < text.Length; i++)
            {
                realText += Is8bits(text[i]) ? text[i] : '?';
            }

            return realText;
        }

        private static bool Is8bits(char c)
        {
            return c < 256;
        }

    }
}
