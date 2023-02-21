using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Drawing2D;
using System.Drawing;
using Photoshop3000.Annexes;

namespace Photoshop3000
{
    /// <summary>
    /// Spécifie les méthodes d'interpolation utilisées pour la mise à l'échelle et la rotation d'un <see cref="MyImage"/>.
    /// <para/>Les méthodes d'interpolation dont les algorithmes sont ceux de GDI+ sont précisées de la mention "Windows_".
    /// </summary>
    public enum InterpolationMode
    {
        /// <summary>
        /// Interpolation par le voisin le plus proche. Avantage : aucune perte d'information lorsqu'on augmente la taille, rapide à process. Peut prendre en compte la transparence.
        /// </summary>
        NearestNeighbour = 0,

        /// <summary>
        /// On réalise une moyenne des pixels les plus proche (4) du pixel d'arrivée en fonction de leur distance à celui-ci. 
        /// Avantage : précis, résultat plus proche de ce qui est attendu en général lorsqu'on redimensionne une image. Peut prendre en compte la transparence.
        /// </summary>
        Bilineaire = 1,

        /// <summary>
        /// Interpolation bicubique, prend en compte les 16 pixels les plus proches. Plus précis que l'interpolation bilinéaire. Peut prendre en compte la transparence.
        /// </summary>
        Bicubique = 2,


        /// <summary>
        /// Comme bilineaire sauf que les bords sont accentués pour avoir plus d'importance, sont de la couleur du <see cref="MyGraphics.PixelRemplissage"/>
        /// </summary>
        Bilineaire_BordsAccentuésStyle1 = 3, //Plus proche de la méthode bilinéaire gdi+ (qui utilise un pixel = 0 comme remplissage)

        /// <summary>
        /// Comme bilineaire sauf que les bords sont accentués pour avoir plus d'importance, sont de la couleur du pixel le plus proche
        /// </summary>
        Bilineaire_BordsAccentuésStyle2 = 4, //Plus proche de ce qui est attendu

        /// <summary>
        /// Interpolation bicubique mais les couleurs sur les bords sont accentuées.
        /// </summary>
        Bicubique_BordsAccentués = 5, //Fonctionne pas comme escompté => il faut "dézoomer" de 1 pixel sur les bords


        /// <summary>
        /// Interpolation bilinéaire, algorithmie gérée par gdi+.
        /// </summary>
        Windows_Bilinear,

        /// <summary>
        /// Interpolation bicubique, algorithmie gérée par gdi+.
        /// </summary>
        Windows_Bicubic,

        /// <summary>
        /// Interpolation par le voisin le plus proche, algorithmie gérée par gdi+.
        /// </summary>
        Windows_NearestNeighbour,

        /// <summary>
        /// Interpolation bilinéaire de haute qualité, algorithmie gérée par gdi+.
        /// </summary>
        Windows_HighQualityBilinear,

        /// <summary>
        /// Interpolation bicubique de haute qualité, algorithmie gérée par gdi+.
        /// </summary>
        Windows_HighQualityBicubic,
    }


    /// <summary>
    /// Fournit des méthodes pour modifier un <see cref="Photoshop3000.MyImage"/>. Les changements sont effectués directement sur l'instance du <see cref="Photoshop3000.MyImage"/> entré en paramètre.
    /// <para/>Les méthodes qui demandent des objets référencés (autres qu'une struct) utiliseront toujours des copies de ceux-ci.
    /// </summary>
    public class MyGraphics
    {
        /// <summary>
        /// Instance <see cref="Random"/> initialisée à partir d'un seed ne dépendant pas uniquement du temps, unique (guid).
        /// </summary>
        private static Random Rand { get; } = new Random(Guid.NewGuid().GetHashCode());

        /// <summary>
        /// Récupère le <see cref="Photoshop3000.MyImage"/> de ce <see cref="MyGraphics"/>, la même instance que celle utilisée pour créer ce <see cref="MyGraphics"/>.
        /// </summary>
        public MyImage MyImage { get; }

        /// <summary>
        /// Spécifie le mode d'interpolation <see cref="Photoshop3000.InterpolationMode"/> de ce <see cref="MyGraphics"/>
        /// </summary>
        public InterpolationMode InterpolationMode { get; set; }

        /// <summary>
        /// Lors d'un changement de taille de l'image, contrôle le respect ou non du ratio Hauteur/Largeur de l'image d'origine. Les bords vides sont alors remplis par <see cref="PixelRemplissage"/><para/>
        /// Lors d'une rotation de l'image, contrôle le respect de la taille de l'image d'origine. <para/>
        /// Lors d'une pixélisation d'une image, adapte ou non la taille d'un nouveau pixel pour que les bords n'aient pas de "petits" pixels
        /// </summary>
        public bool KeepAspectRatio { get; set; }

        /// <summary>
        /// Lorsque une image est dessinée sur cette image, spécifie quel canal est utilisé sur l'image finale.
        /// Par défaut on utilise le canal de l'image source : <see langword="true"/>.
        /// </summary>
        public bool SourceAlpha { get; set; }

        /// <summary>
        /// Pixel qui occasionnellement remplira les trous laissés dans une image.
        /// </summary>
        public Pixel PixelRemplissage { get; set; }

        /// <summary>
        /// Région de ce Graphics
        /// </summary>
        public Rectangle Clip { get; set; }

        //Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="MyGraphics"/>. Réalise des opérations sur l'instance du <see cref="Photoshop3000.MyImage"/> entré en paramètre
        /// </summary>Instance sur laquelle les opérations sont effectuées
        /// <param name="image">Image sur laquelle réaliser des opérations</param>
        public MyGraphics(MyImage image)
        {
            //Update : plus d'exception n'est jetée, on gère ça dans les méthodes. Si on entre une image valide, on peut
            //très bien la mettre à null une fois le MyG initialisé, contraire à l'objectif initiale.

            this.MyImage = image;

            this.InterpolationMode = InterpolationMode.Windows_NearestNeighbour; //Le plus rapide
            this.KeepAspectRatio = false;   //On ne garde pas l'aspect de l'image
            this.SourceAlpha = true;
            this.PixelRemplissage = Pixel.Zero; //Noir
            if (image != null)
                this.Clip = new Rectangle(0, 0, image.Width, image.Height);
        }


        //Autres

        /// <summary>
        /// Clone les pixels d'un <see cref="Photoshop3000.MyImage"/> dans le <see cref="Photoshop3000.MyImage"/> de cette instance.
        /// </summary>
        /// <param name="toClone"></param>
        private void ReplaceImage(MyImage toClone)
        {
            if (this.MyImage as object != toClone as object && toClone?.Validité == true) //meme ref, si on réinitialise le tab de pixel de l'un on réinitialise aussi l'autre, donc crash
            {
                this.MyImage.ConvertPixelFormat(toClone.PixelFormat); //Conversion si besoin du format

                this.MyImage.ChangeSize(toClone.Height, toClone.Width); //Redimensionnement

                this.MyImage.ChangeData(toClone.ToBGRArray()); //Copie des données dans le nouveau tableau
            }
        }



        /// <summary>
        /// Inverse une ligne de pixels. Ne fonctionne uniquement que pour des pixels de longueur 3 ou 4.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="lengthPixel"></param>
        /// <param name="padding"></param>
        private static unsafe void ReversePixelsLine(byte[] line, int lengthPixel, int padding)
        {
            fixed (byte* bufferLine = line)
            {
                ReversePixelsLine(bufferLine, lengthPixel, line.Length - padding);
            }
        }


        /// <summary>
        /// Inverse une ligne de pixels à partir d'un pointeur. Fonctionne uniquement pour des pixels de longueur 3 ou 4.
        /// </summary>
        /// <param name="dataBuffer"></param>
        /// <param name="lengthPixel"></param>
        /// <param name="lengthData"></param>
        private static unsafe void ReversePixelsLine(byte* dataBuffer, int lengthPixel, int lengthData)
        {
            if (lengthPixel == 3)
            {
                int halfW = lengthData / 2;

                for (int i = 0; i < halfW; i += lengthPixel)
                {
                    int index = lengthData - i - lengthPixel;

                    //int argb = *(int*)&dataBuffer[i]; //Bug au milieu comme les pixels font 3 de longueur et un int fait 4

                    //*(int*)&dataBuffer[i] = *(int*)&dataBuffer[index];

                    //*(int*)&dataBuffer[index] = argb;

                    byte b = dataBuffer[i];
                    byte g = dataBuffer[i + 1];
                    byte r = dataBuffer[i + 2];

                    dataBuffer[i] = dataBuffer[index];
                    dataBuffer[i + 1] = dataBuffer[index + 1];
                    dataBuffer[i + 2] = dataBuffer[index + 2];

                    dataBuffer[index] = b;
                    dataBuffer[index + 1] = g;
                    dataBuffer[index + 2] = r;
                }
            }
            else if (lengthPixel == 4)
            {
                int maxW = lengthData / 4;
                int halfW = maxW / 2;

                int* bufferInt = (int*)dataBuffer;

                for (int i = 0; i < halfW; i += 1)
                {
                    int index = maxW - i - 1;

                    int argb = bufferInt[i];

                    bufferInt[i] = bufferInt[index];

                    bufferInt[index] = argb;
                }
            }
        }


        /// <summary>
        /// Copie un certain nombre de lignes de données à la longueur indiquée entre 2 pointeurs.
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="src"></param>
        /// <param name="copyLength">Nombre de bytes</param>
        /// <param name="height"></param>
        /// <param name="strideDestIncr"></param>
        /// <param name="strideSrcIncr"></param>
        private static unsafe void CopyLines(byte* dest, byte* src, uint copyLength, int height, int strideDestIncr, int strideSrcIncr)
        {
            //RtlMoveMemory gère auto les problèmes de superposition de mémoire donc pas besoin de checker si dest + copyL > src.
            for (int i = 0; i < height; ++i)
            {
                Formats.SafeMemCpy((IntPtr)dest, (IntPtr)src, copyLength);

                dest += strideDestIncr;
                src += strideSrcIncr;
            }
        }



        //Rotation et redimensionnement

        #region Rotation + Redimensionnement

        /// <summary>
        /// Tourne le <see cref="Photoshop3000.MyImage"/> avec l'angle en degré spécifié et le mode d'interpolation prédéfini. 
        /// <para/>La taille de l'image est adaptée ou non selon <see cref="KeepAspectRatio"/>.<para/>
        /// La couleur des bords est définie par <see cref="PixelRemplissage"/>.
        /// </summary>
        /// <param name="degré">Angle de rotation en degré, sens inverse du sens trigo</param>
        /// <param name="bordsCouleurIm">Gère la couleur des trous laissés par l'image. <see langword="false"/> = remplissage par <see cref="PixelRemplissage"/>.
        /// Sinon remplissage par la couleur des bords de l'image (sauf si interpolation windows ou bicubique).</param>
        public void Rotation(double degré, bool bordsCouleurIm = false)
        {
            if (this.InterpolationMode >= InterpolationMode.Windows_Bilinear) //Windows
            {
                ReplaceImage(Rotation_Windows(this.MyImage, degré, this.KeepAspectRatio, true, this.InterpolationMode, this.PixelRemplissage));
                return;
            }

            if (this.InterpolationMode != InterpolationMode.Bicubique && bordsCouleurIm)
                bordsCouleurIm = false;


            double rad = Math.PI * (degré % 360.0) / 180.0;

            double sin = Math.Sin(rad);
            double cos = Math.Cos(rad);


            int targetHeight;
            int targetWidth;

            if (this.KeepAspectRatio)
            {
                targetHeight = this.MyImage.Height;
                targetWidth = this.MyImage.Width;
            }
            else
            {
                targetHeight = (int)(Math.Abs(this.MyImage.Height * cos) + Math.Abs(this.MyImage.Width * sin));
                targetWidth = (int)(Math.Abs(this.MyImage.Width * cos) + Math.Abs(this.MyImage.Height * sin));
            }

            MyImage imageTournée = new MyImage(targetHeight, targetWidth, this.MyImage.PixelFormat);

            int hwidth = targetWidth / 2;
            int hheight = targetHeight / 2;

            int adjustPixelsBorder = this.InterpolationMode == InterpolationMode.NearestNeighbour ? 0 : 1;

            for (int i = 0; i < imageTournée.Height; i++)
            {
                for (int j = 0; j < imageTournée.Width; j++)
                {
                    double rapportY = i - hheight; //On se place par rapport au centre de la nouvelle image
                    double rapportX = j - hwidth;

                    double x = cos * rapportX + sin * rapportY + this.MyImage.Width / 2.0;  //Position du pixel tourné, coord polaires + ajustement pour translater l'image.
                    double y = cos * rapportY - sin * rapportX + this.MyImage.Height / 2.0;

                    if (!bordsCouleurIm && (y < 0 || x < 0 || x >= this.MyImage.Width - adjustPixelsBorder || y >= this.MyImage.Height - adjustPixelsBorder)) //En dehors du cadre
                    {
                        imageTournée[i, j] = this.PixelRemplissage;
                    }
                    else
                    {
                        imageTournée[i, j] = Pixel.FromArgb(GetPixelInterpolé(x, y)); //On interpole en fct du Mode pré-défini
                    }

                }
            }

            ReplaceImage(imageTournée);
        }


        /// <summary>
        /// Adapte l'image en fonction d'un rapport d'aggrandissement ou de rétrécissement. Avec 1, la taille de l'image de base.
        /// </summary>
        /// <param name="rapport">Rapport de changement de la taille d'une image avec 1 la taille actuelle de l'image</param>
        public void Redimensionnement(double rapport)
        {
            bool b = this.KeepAspectRatio;
            this.KeepAspectRatio = false; //Eviter que des trous ne soient remplis par le pixel remplissage, on étire l'image

            this.Redimensionnement((int)Math.Round(this.MyImage.Height * rapport), (int)Math.Round(this.MyImage.Width * rapport));

            this.KeepAspectRatio = b;
        }

        /// <summary>
        /// Adapte l'image en fonction de nouveaux paramètres de hauteur et de largeur.
        /// </summary>
        /// <param name="targetHeight">Nouvelle taille de l'image en hauteur</param>
        /// <param name="targetWidth">Nouvelle taille de l'image en largeur</param>
        public void Redimensionnement(int targetHeight, int targetWidth)
        {
            int keepRatioAdjustedTargetHeight = targetHeight, keepRatioAdjustedTargetWidth = targetWidth;

            //On garde le rapport des dimensions de l'image d'origine. L'image aggrandie sera au milieu (soit en largeur soit en hauteur) 
            //d'une image avec les dimensions demandées par l'utilisateur. Les bords ajoutés sont remplis par PixelRemplissage
            if (this.KeepAspectRatio)
            {
                double ratioW = (double)targetWidth / this.MyImage.Width;
                double ratioH = (double)targetHeight / this.MyImage.Height;

                if (ratioH > ratioW)
                {
                    keepRatioAdjustedTargetHeight = (int)(this.MyImage.Height * ratioW);
                }
                else
                {
                    keepRatioAdjustedTargetWidth = (int)(this.MyImage.Width * ratioH);
                }
            }

            MyImage imageRedimensionnée;

            if (this.InterpolationMode >= InterpolationMode.Windows_Bilinear) //gdi+
            {
                imageRedimensionnée = Redimensionnement_Windows(this.MyImage, keepRatioAdjustedTargetHeight, keepRatioAdjustedTargetWidth, this.InterpolationMode);
            }
            else
            {
                imageRedimensionnée = new MyImage(keepRatioAdjustedTargetHeight, keepRatioAdjustedTargetWidth, this.MyImage.PixelFormat);

                InternalRedimensionnement(imageRedimensionnée);
            }


            if (keepRatioAdjustedTargetHeight != targetHeight || keepRatioAdjustedTargetWidth != targetWidth) //On crée une nouvelle image avec l'image dont on vient de changer la taille au centre
            {
                MyImage imageRedimensionnéeCentrée = new MyImage(targetHeight, targetWidth, this.PixelRemplissage, this.MyImage.PixelFormat);

                new MyGraphics(imageRedimensionnéeCentrée).DrawImageCenter(imageRedimensionnée, -1);

                ReplaceImage(imageRedimensionnéeCentrée);
            }
            else
            {
                ReplaceImage(imageRedimensionnée);
            }
        }


        /// <summary>
        /// Dessine l'image de ce MyGraphics dans l'image entrée en paramètre de manière à remplir tout l'espace disponible.
        /// </summary>
        /// <param name="imageÀRemplir"></param>
        private void InternalRedimensionnement(MyImage imageÀRemplir)
        {
            const int décalageDouble = 32;

            double rapportHeight = (double)imageÀRemplir.Height / this.MyImage.Height;
            double rapportWidth = (double)imageÀRemplir.Width / this.MyImage.Width;

            bool alphaExiste = imageÀRemplir.PixelFormat == Formats.PixelFormat.BMP_Argb32;

            int lengthPixel = alphaExiste ? 4 : 3;

            int maxW = imageÀRemplir.Width;
            int maxH = imageÀRemplir.Height;

            if (this.InterpolationMode == InterpolationMode.NearestNeighbour)
            {
                //Code inspiré de http://tech-algorithm.com/articles/nearest-neighbor-image-scaling/ et largement optimisé depuis

                long rapportWidthInt = ((long)this.MyImage.Width << décalageDouble) / imageÀRemplir.Width + 1;// '>> 32' pour simuler un float avec un long.
                long rapportHeightInt = ((long)this.MyImage.Height << décalageDouble) / imageÀRemplir.Height + 1; // '+1' pour arrondir, un double arrondissant à la 'virgule au dessus'.

                try
                {
                    unsafe //Un peu plus lent que gdi+
                    {
                        fixed (byte* bufferDest = imageÀRemplir.ToBGRArray())
                        fixed (byte* bufferSrc = this.MyImage.ToBGRArray())
                        {
                            //Redimensionnement opti : on cherche d'abord la ligne dans l'image source dans la première boucle,
                            //dans la 2ème on cherche dans cette ligne à quel index se situe le pixel à interpoler (à copier).

                            long ratioHeight = 0;

                            if (alphaExiste)
                            {
                                int* destIm = (int*)bufferDest; //On passe en pointeur int pour copier directement les pixels.

                                for (int indexH = 0; indexH < maxH; ++indexH)
                                {
                                    int* srcIm = (int*)bufferSrc + (ratioHeight >> décalageDouble) * this.MyImage.Width; //Hauteur

                                    long ratioWidth = 0;

                                    for (int indexImageW = 0; indexImageW < maxW; ++indexImageW)
                                    {
                                        *destIm = *(srcIm + (int)(ratioWidth >> décalageDouble)); //Hauteur + largeur = emplacement final

                                        ++destIm; //Prochain pixel (int*++ = byte*+=(sizeof(int)))

                                        ratioWidth += rapportWidthInt;
                                    }

                                    ratioHeight += rapportHeightInt;
                                }
                            }
                            else
                            {
                                int srcStride = this.MyImage.Stride;
                                int destStride = imageÀRemplir.Stride;

                                byte* destIm = bufferDest;
                                int destPad = imageÀRemplir.GetPadding();

                                for (int indexH = 0; indexH < maxH; ++indexH)
                                {
                                    byte* srcIm = bufferSrc + (ratioHeight >> décalageDouble) * srcStride; //Hauteur dans l'im source

                                    long ratioWidth = 0;

                                    for (int indexImageW = 0; indexImageW < maxW; ++indexImageW)
                                    {
                                        //Le byte de trop sera overwrite au pixel d'après (sinon ce sera un byte de padding, donc peu importe)
                                        *(int*)destIm = *(int*)(srcIm + ((int)(ratioWidth >> décalageDouble) * lengthPixel)); //Hauteur + largeur

                                        destIm += lengthPixel; //Prochain pixel (byte* += 3)

                                        ratioWidth += rapportWidthInt;
                                    }

                                    ratioHeight += rapportHeightInt;
                                    destIm += destPad; //on skip le padding de fin de ligne
                                }
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    //À priori ce problème a été réglé en passant d'un décalage 'float' (16 après la ',') à un décalage 'double' (32).
                    //32 bits étant trop petit pour simuler suffisamment de chiffre après la virgule, un long (64) fait l'affaire.

                    throw new Exception("Impossible de redimensionner l'image car le rapport entre la taille de l'image d'origine et " +
                        "la taille de la nouvelle image est trop petit. Veuillez redimensionner avec des paliers de redimensionnement.", e);
                }

            }
            //else if (this.InterpolationMode == InterpolationMode.Bilineaire)
            //{
            //opti bilinéaire à faire
            //}
            else
            {
                if (this.InterpolationMode == InterpolationMode.Bicubique || this.InterpolationMode == InterpolationMode.Bilineaire)//|| this.InterpolationMode == InterpolationMode.Bicubique_BordsAccentues)//Décalage
                {
                    rapportHeight = (double)imageÀRemplir.Height / (this.MyImage.Height - 1);
                    rapportWidth = (double)imageÀRemplir.Width / (this.MyImage.Width - 1);
                }

                int indexH = 0;

                unsafe
                {
                    fixed (byte* buffer = imageÀRemplir.ToBGRArray())
                    {
                        if (alphaExiste)
                        {
                            int* bufferInt = (int*)buffer;

                            while (indexH < maxH)
                            {
                                for (int indexW = 0; indexW < maxW; ++indexW)
                                {
                                    bufferInt[indexW] = GetPixelInterpolé(indexW / rapportWidth, indexH / rapportHeight);
                                }
                                indexH++;

                                bufferInt += imageÀRemplir.Width;
                            }
                        }
                        else
                        {
                            while (indexH < maxH)
                            {
                                int décalage = indexH * imageÀRemplir.Stride;

                                int indexImageW = 0;

                                maxW = imageÀRemplir.Stride;

                                for (int indexW = 0; indexW < maxW; ++indexImageW)
                                {
                                    int p = GetPixelInterpolé(indexImageW / rapportWidth, indexH / rapportHeight);

                                    buffer[décalage + indexW] = (byte)(p >> Pixel.ArgbBlueShift);
                                    indexW++;

                                    buffer[décalage + indexW] = (byte)(p >> Pixel.ArgbGreenShift);
                                    indexW++;

                                    buffer[décalage + indexW] = (byte)(p >> Pixel.ArgbRedShift);
                                    indexW++;
                                }
                                indexH++;
                            }
                        }
                    }
                }

            }
        }



        /// <summary>
        /// Crée un pixel argb en fonction d'une position dans le <see cref="Photoshop3000.MyImage"/> de cette instance et d'une méthode <see cref="Photoshop3000.InterpolationMode"/>
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private int GetPixelInterpolé(double x, double y)
        {
            int p = 0;

            switch (this.InterpolationMode)
            {
                case InterpolationMode.NearestNeighbour:
                    byte[] pixels = this.MyImage.ToBGRArray();

                    int lengthPixel = this.MyImage.PixelFormat.GetPixelLength();

                    int pos = (int)y * this.MyImage.Stride + (int)x * lengthPixel;

                    int rgb = pixels[pos] << Pixel.ArgbBlueShift;
                    rgb |= pixels[pos + 1] << Pixel.ArgbGreenShift;
                    rgb |= pixels[pos + 2] << Pixel.ArgbRedShift;
                    rgb |= (lengthPixel == 3 ? 255 : pixels[pos + 3]) << Pixel.ArgbAlphaShift;

                    p = rgb;
                    break;

                case InterpolationMode.Bilineaire:
                    p = GetInterpolationBilineaire(x, y); //On interpole les pixels en fct de leur distance à la position d'origine
                    break;

                case InterpolationMode.Bilineaire_BordsAccentuésStyle1:
                case InterpolationMode.Bilineaire_BordsAccentuésStyle2:
                    p = GetInterpolationBilineaireBords(x, y); //On interpole les pixels en fct de leur distance à la position d'origine
                    break;

                case InterpolationMode.Bicubique: //Méthode d'Hermite
                case InterpolationMode.Bicubique_BordsAccentués:
                    Pixel[,] voisins = VoisinsBicubique(x, y); //Voisins du pixel

                    double xFrac = x - (int)x;
                    double yFrac = y - (int)y;

                    //On récupère la valeur d'intensité de pixel pour chaque colonne et enfin on détermine la solution de l'equation générée avec ces 4 intensités en ligne

                    float colonne0 = GetInterpolationBicubique(voisins[0, 0].R, voisins[1, 0].R, voisins[2, 0].R, voisins[3, 0].R, (float)yFrac);
                    float colonne1 = GetInterpolationBicubique(voisins[0, 1].R, voisins[1, 1].R, voisins[2, 1].R, voisins[3, 1].R, (float)yFrac);
                    float colonne2 = GetInterpolationBicubique(voisins[0, 2].R, voisins[1, 2].R, voisins[2, 2].R, voisins[3, 2].R, (float)yFrac);
                    float colonne3 = GetInterpolationBicubique(voisins[0, 3].R, voisins[1, 3].R, voisins[2, 3].R, voisins[3, 3].R, (float)yFrac);
                    float valeurPixel = GetInterpolationBicubique(colonne0, colonne1, colonne2, colonne3, (float)xFrac);
                    int argb = ((byte)Math.Max(0, Math.Min(valeurPixel, 255))) << Pixel.ArgbRedShift;

                    colonne0 = GetInterpolationBicubique(voisins[0, 0].G, voisins[1, 0].G, voisins[2, 0].G, voisins[3, 0].G, (float)yFrac);
                    colonne1 = GetInterpolationBicubique(voisins[0, 1].G, voisins[1, 1].G, voisins[2, 1].G, voisins[3, 1].G, (float)yFrac);
                    colonne2 = GetInterpolationBicubique(voisins[0, 2].G, voisins[1, 2].G, voisins[2, 2].G, voisins[3, 2].G, (float)yFrac);
                    colonne3 = GetInterpolationBicubique(voisins[0, 3].G, voisins[1, 3].G, voisins[2, 3].G, voisins[3, 3].G, (float)yFrac);
                    valeurPixel = GetInterpolationBicubique(colonne0, colonne1, colonne2, colonne3, (float)xFrac);
                    argb |= ((byte)Math.Max(0, Math.Min(valeurPixel, 255))) << Pixel.ArgbGreenShift;

                    colonne0 = GetInterpolationBicubique(voisins[0, 0].B, voisins[1, 0].B, voisins[2, 0].B, voisins[3, 0].B, (float)yFrac);
                    colonne1 = GetInterpolationBicubique(voisins[0, 1].B, voisins[1, 1].B, voisins[2, 1].B, voisins[3, 1].B, (float)yFrac);
                    colonne2 = GetInterpolationBicubique(voisins[0, 2].B, voisins[1, 2].B, voisins[2, 2].B, voisins[3, 2].B, (float)yFrac);
                    colonne3 = GetInterpolationBicubique(voisins[0, 3].B, voisins[1, 3].B, voisins[2, 3].B, voisins[3, 3].B, (float)yFrac);
                    valeurPixel = GetInterpolationBicubique(colonne0, colonne1, colonne2, colonne3, (float)xFrac);
                    argb |= ((byte)Math.Max(0, Math.Min(valeurPixel, 255))) << Pixel.ArgbBlueShift;

                    if (this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Argb32)
                    {
                        colonne0 = GetInterpolationBicubique(voisins[0, 0].A, voisins[1, 0].A, voisins[2, 0].A, voisins[3, 0].A, (float)yFrac);
                        colonne1 = GetInterpolationBicubique(voisins[0, 1].A, voisins[1, 1].A, voisins[2, 1].A, voisins[3, 1].A, (float)yFrac);
                        colonne2 = GetInterpolationBicubique(voisins[0, 2].A, voisins[1, 2].A, voisins[2, 2].A, voisins[3, 2].A, (float)yFrac);
                        colonne3 = GetInterpolationBicubique(voisins[0, 3].A, voisins[1, 3].A, voisins[2, 3].A, voisins[3, 3].A, (float)yFrac);
                        valeurPixel = GetInterpolationBicubique(colonne0, colonne1, colonne2, colonne3, (float)xFrac);
                        argb |= ((byte)Math.Max(0, Math.Min(valeurPixel, 255))) << Pixel.ArgbAlphaShift;

                    }
                    else
                    {
                        argb |= 0xFF << Pixel.ArgbAlphaShift;
                    }

                    p = argb;
                    break;
            }

            return p;
        }


        /// <summary>
        /// Renvoie la solution de f(t) avec t compris entre 0 et 1. Les autres paramètres sont les positions en x ou y des pixels à interpoler de gauche à droite. Basé sur le polynôme d'Hermite
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private static float GetInterpolationBicubique(float A, float B, float C, float D, float t)
        {
            //On a f(t) = at^3 + bt² + ct + d, le but est de trouver a, b, c et d pour ensuite trouver la solution de l'equation f(t)
            //On sait que f(0) = d, f(1) = a + b + c + d, f'(0) = c et f'(1) = 3at² + 2bt + c
            //On a alors a = 2f(0) − 2f(1) + f′(0) + f′(1)  et  b = −3f(0) + 3f(1) − 2f′(0) − f′(1), on sait que f(0) = (x0, B) et f(1) = (x1, C) donc f(0) = B et f(1) = C
            //Il vient alors f'(0) = (C - A) / 2  et f'(1) = (D - B) / 2 donc :

            float a = -(A / 2.0f) + (3.0f * B / 2.0f) - (3.0f * C / 2.0f) + (D / 2.0f);
            float b = A - (5.0f * B / 2.0f) + (2.0f * C) - (D / 2.0f);
            float c = -(A / 2.0f) + (C / 2.0f);
            float d = B;

            return a * t * t * t + b * t * t + c * t + d; //Equation polynomiale du 3ème degré
        }

        /// <summary>
        /// Renvoie les 16 voisins les plus proches du point entré en paramètre
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private Pixel[,] VoisinsBicubique(double x, double y)
        {
            Pixel[,] voisins = new Pixel[4, 4];

            if (this.InterpolationMode == InterpolationMode.Bicubique)
            {
                int minY = (int)y - 1;
                int minX = (int)x - 1;

                for (int i = 0; i < voisins.GetLength(0); i++)
                {
                    for (int j = 0; j < voisins.GetLength(1); j++)
                    {
                        int posY = minY + i;
                        int posX = minX + j;

                        if (posY < 0)
                            posY = 0;

                        if (posY >= this.MyImage.Height)
                            posY = this.MyImage.Height - 1;

                        if (posX < 0)
                            posX = 0;

                        if (posX >= this.MyImage.Width)
                            posX = this.MyImage.Width - 1;

                        voisins[i, j] = this.MyImage[posY, posX];
                    }
                }
            }
            else //Style bords
            {
                double minY = Math.Floor(y - 0.0) - 0.5;
                double minX = Math.Floor(x - 0.0) - 0.5;

                for (int i = 0; i < voisins.GetLength(0); i++)
                {
                    for (int j = 0; j < voisins.GetLength(1); j++)
                    {
                        double posY = minY + i;
                        double posX = minX + j;

                        if (posY < 0)
                        {
                            if (posX < 0)
                            {
                                voisins[i, j] = this.MyImage[0, 0];
                            }
                            else if (posX >= this.MyImage.Width)
                            {
                                voisins[i, j] = this.MyImage[0, this.MyImage.Width - 1];
                            }
                            else
                            {
                                voisins[i, j] = this.MyImage[0, (int)posX];
                            }
                        }
                        else if (posY >= this.MyImage.Height)
                        {
                            if (posX < 0)
                            {
                                voisins[i, j] = this.MyImage[this.MyImage.Height - 1, 0];
                            }
                            else if (posX >= this.MyImage.Width)
                            {
                                voisins[i, j] = this.MyImage[this.MyImage.Height - 1, this.MyImage.Width - 1];
                            }
                            else
                            {
                                voisins[i, j] = this.MyImage[this.MyImage.Height - 1, (int)posX];
                            }
                        }
                        else
                        {
                            if (posX < 0)
                            {
                                voisins[i, j] = this.MyImage[(int)posY, 0];
                            }
                            else if (posX >= this.MyImage.Width)
                            {
                                voisins[i, j] = this.MyImage[(int)posY, this.MyImage.Width - 1];
                            }
                            else
                            {
                                voisins[i, j] = this.MyImage[(int)posY, (int)posX];
                            }
                        }

                        //if (posY < 0 || posY >= this.MyImage.Height || posX < 0 || posX >= this.MyImage.Width)
                        //{
                        //    voisins[i, j] = this.PixelRemplissage;
                        //}
                        //else
                        //{
                        //    voisins[i, j] = this.MyImage[(int)posY, (int)posX];
                        //}
                    }
                }
            }

            return voisins;
        }


        /// <summary>
        /// Renvoie le <see cref="Pixel"/> moyen déterminé en fonction de la distance des <see cref="Pixel"/> de la <see cref="List{T}"/> par rapport à un <see cref="Point"/> d'origine
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int GetInterpolationBilineaire(double x, double y)
        {
            int y1 = (int)y;
            int x1 = (int)x;

            int y2 = y1 + 1;
            int x2 = x1 + 1;

            int lengthPixel = this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Rgb24 ? 3 : 4;

            unsafe
            {
                fixed (byte* data = this.MyImage.ToBGRArray()) //risque d'overflow, attention !
                {
                    int pos1 = this.MyImage.Stride * y1 + x1 * lengthPixel;
                    int pos2 = pos1 + lengthPixel;
                    int pos3 = pos1 + this.MyImage.Stride;
                    int pos4 = pos3 + lengthPixel;


                    double rX2xX2X1 = x2 - x;
                    double rxX1X2X1 = x - x1;
                    double rY2yY2Y1 = y2 - y;
                    double ryY1Y2Y1 = y - y1;


                    double B1 = rX2xX2X1 * data[pos1]
                              + rxX1X2X1 * data[pos2];

                    double B2 = rX2xX2X1 * data[pos3]
                              + rxX1X2X1 * data[pos4];

                    int argb = (byte)((rY2yY2Y1 * B1) + (ryY1Y2Y1 * B2)) << Pixel.ArgbBlueShift;


                    double G1 = rX2xX2X1 * data[pos1 + 1]
                              + rxX1X2X1 * data[pos2 + 1];

                    double G2 = rX2xX2X1 * data[pos3 + 1]
                              + rxX1X2X1 * data[pos4 + 1];

                    argb |= (byte)((rY2yY2Y1 * G1) + (ryY1Y2Y1 * G2)) << Pixel.ArgbGreenShift;


                    double R1 = rX2xX2X1 * data[pos1 + 2]
                              + rxX1X2X1 * data[pos2 + 2];

                    double R2 = rX2xX2X1 * data[pos3 + 2]
                              + rxX1X2X1 * data[pos4 + 2];

                    argb |= (byte)((rY2yY2Y1 * R1) + (ryY1Y2Y1 * R2)) << Pixel.ArgbRedShift;


                    if (this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Argb32)
                    {
                        double A1 = rX2xX2X1 * data[pos1 + 3]
                                  + rxX1X2X1 * data[pos2 + 3];

                        double A2 = rX2xX2X1 * data[pos3 + 3]
                                  + rxX1X2X1 * data[pos4 + 3];

                        argb |= (byte)((rY2yY2Y1 * A1) + (ryY1Y2Y1 * A2)) << Pixel.ArgbAlphaShift;
                    }
                    else
                    {
                        argb |= 0xFF << Pixel.ArgbAlphaShift; //Inutile en soit
                    }

                    return argb;
                }
            }

        }

        /// <summary>
        /// Equivalent de l'interpolation bilinéaire de Windows, les bords sont plus accentués, moins opti
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int GetInterpolationBilineaireBords(double x, double y)
        {
            double y1 = Math.Floor(y - 0.5) + 0.5;
            double x1 = Math.Floor(x - 0.5) + 0.5;

            double y2 = y1 + 1;
            double x2 = x1 + 1;

            //Problème d'optimisation, on est obligé de tester si on est pas en dehors de l'image pour les pixels sur les bords
            Pixel p1, p2, p3, p4;

            if (y1 < 0)
            {
                if (x1 < 0)
                {
                    if (this.InterpolationMode == InterpolationMode.Bilineaire_BordsAccentuésStyle1)
                    {
                        p1 = this.PixelRemplissage;
                        p2 = this.PixelRemplissage;
                        p3 = this.PixelRemplissage;
                    }
                    else //Style 2
                    {
                        p1 = this.MyImage[(int)(y1 + 1), (int)(x1 + 1)];
                        p3 = this.MyImage[(int)y2, (int)(x1 + 1)];

                        p2 = this.MyImage[(int)(y1 + 1), (int)x2];
                    }

                    p4 = this.MyImage[(int)y2, (int)x2];
                }
                else if (x2 >= this.MyImage.Width)
                {
                    if (this.InterpolationMode == InterpolationMode.Bilineaire_BordsAccentuésStyle1)
                    {
                        p1 = this.PixelRemplissage;
                        p2 = this.PixelRemplissage;
                        p4 = this.PixelRemplissage;
                    }
                    else
                    {
                        p2 = this.MyImage[(int)(y1 + 1), (int)(x2 - 1)];
                        p4 = this.MyImage[(int)y2, (int)(x2 - 1)];

                        p1 = this.MyImage[(int)(y1 + 1), (int)x1];
                    }

                    p3 = this.MyImage[(int)y2, (int)x1];

                }
                else
                {
                    if (this.InterpolationMode == InterpolationMode.Bilineaire_BordsAccentuésStyle1)
                    {
                        p1 = this.PixelRemplissage;
                        p2 = this.PixelRemplissage;
                    }
                    else
                    {
                        p1 = this.MyImage[(int)(y1 + 1), (int)x1];
                        p2 = this.MyImage[(int)(y1 + 1), (int)x2];
                    }

                    p3 = this.MyImage[(int)y2, (int)x1];
                    p4 = this.MyImage[(int)y2, (int)x2];
                }
            }
            else if (y2 >= this.MyImage.Height)
            {
                if (x1 < 0)
                {
                    if (this.InterpolationMode == InterpolationMode.Bilineaire_BordsAccentuésStyle1)
                    {
                        p1 = this.PixelRemplissage;
                        p3 = this.PixelRemplissage;
                        p4 = this.PixelRemplissage;
                    }
                    else
                    {
                        p1 = this.MyImage[(int)y1, (int)(x1 + 1)];
                        p3 = this.MyImage[(int)(y2 - 1), (int)(x1 + 1)];

                        p4 = this.MyImage[(int)(y2 - 1), (int)x2];
                    }

                    p2 = this.MyImage[(int)y1, (int)x2];
                }
                else if (x2 >= this.MyImage.Width)
                {
                    if (this.InterpolationMode == InterpolationMode.Bilineaire_BordsAccentuésStyle1)
                    {
                        p2 = this.PixelRemplissage;
                        p3 = this.PixelRemplissage;
                        p4 = this.PixelRemplissage;
                    }
                    else
                    {
                        p2 = this.MyImage[(int)y1, (int)(x2 - 1)];
                        p4 = this.MyImage[(int)(y2 - 1), (int)(x2 - 1)];

                        p3 = this.MyImage[(int)(y2 - 1), (int)x1];
                    }

                    p1 = this.MyImage[(int)y1, (int)x1];
                }
                else
                {
                    if (this.InterpolationMode == InterpolationMode.Bilineaire_BordsAccentuésStyle1)
                    {
                        p3 = this.PixelRemplissage;
                        p4 = this.PixelRemplissage;
                    }
                    else
                    {
                        p3 = this.MyImage[(int)(y2 - 1), (int)x1];
                        p4 = this.MyImage[(int)(y2 - 1), (int)x2];
                    }

                    p1 = this.MyImage[(int)y1, (int)x1];
                    p2 = this.MyImage[(int)y1, (int)x2];
                }
            }
            else
            {
                if (x1 < 0)
                {
                    if (this.InterpolationMode == InterpolationMode.Bilineaire_BordsAccentuésStyle1)
                    {
                        p1 = this.PixelRemplissage;
                        p3 = this.PixelRemplissage;
                    }
                    else
                    {
                        p1 = this.MyImage[(int)y1, (int)(x1 + 1)];
                        p3 = this.MyImage[(int)y2, (int)(x1 + 1)];
                    }

                    p2 = this.MyImage[(int)y1, (int)x2];
                    p4 = this.MyImage[(int)y2, (int)x2];
                }
                else if (x2 >= this.MyImage.Width)
                {
                    if (this.InterpolationMode == InterpolationMode.Bilineaire_BordsAccentuésStyle1)
                    {
                        p2 = this.PixelRemplissage;
                        p4 = this.PixelRemplissage;
                    }
                    else
                    {
                        p2 = this.MyImage[(int)y1, (int)(x2 - 1)];
                        p4 = this.MyImage[(int)y2, (int)(x2 - 1)];
                    }

                    p1 = this.MyImage[(int)y1, (int)x1];
                    p3 = this.MyImage[(int)y2, (int)x1];
                }
                else
                {
                    p1 = this.MyImage[(int)y1, (int)x1];
                    p2 = this.MyImage[(int)y1, (int)x2];
                    p3 = this.MyImage[(int)y2, (int)x1];
                    p4 = this.MyImage[(int)y2, (int)x2];
                }
            }

            double rX2xX2X1 = x2 - x;
            double rxX1X2X1 = x - x1;
            double rY2yY2Y1 = y2 - y;
            double ryY1Y2Y1 = y - y1;


            double B1 = rX2xX2X1 * p1.B
                      + rxX1X2X1 * p2.B;

            double B2 = rX2xX2X1 * p3.B
                      + rxX1X2X1 * p4.B;

            int argb = (byte)((rY2yY2Y1 * B1) + (ryY1Y2Y1 * B2)) << Pixel.ArgbBlueShift;


            double G1 = rX2xX2X1 * p1.G
                      + rxX1X2X1 * p2.G;

            double G2 = rX2xX2X1 * p3.G
                      + rxX1X2X1 * p4.G;

            argb |= (byte)((rY2yY2Y1 * G1) + (ryY1Y2Y1 * G2)) << Pixel.ArgbGreenShift;


            double R1 = rX2xX2X1 * p1.R
                      + rxX1X2X1 * p2.R;

            double R2 = rX2xX2X1 * p3.R
                      + rxX1X2X1 * p4.R;

            argb |= (byte)((rY2yY2Y1 * R1) + (ryY1Y2Y1 * R2)) << Pixel.ArgbRedShift;


            if (this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Argb32)
            {
                double A1 = rX2xX2X1 * p1.A
                          + rxX1X2X1 * p2.A;

                double A2 = rX2xX2X1 * p3.A
                          + rxX1X2X1 * p4.A;

                argb |= (byte)((rY2yY2Y1 * A1) + (ryY1Y2Y1 * A2)) << Pixel.ArgbAlphaShift;
            }
            else
            {
                argb |= 0xFF << Pixel.ArgbAlphaShift;
            }


            return argb;
        }


        /// <summary>
        /// Applique une rotation à une image, algorithmie gérée par GDI+
        /// </summary>
        /// <param name="myImage"></param>
        /// <param name="angle"></param>
        /// <param name="adjustSize"></param>
        /// <param name="couperBords"></param>
        /// <param name="mode"></param>
        /// <param name="backPixel"></param>
        /// <returns></returns>
        private static MyImage Rotation_Windows(MyImage myImage, double angle, bool adjustSize, bool couperBords, InterpolationMode mode, Pixel backPixel)
        {
            // clone si l'angle de rotation est nul
            if ((angle %= 360) == 0)
                return myImage.Clone();

            // On part du principe qu'on coupe les bords sans changer la taille de l'image
            int oldWidth = myImage.Width;
            int oldHeight = myImage.Height;
            int targetWidth = oldWidth;
            int targetHeight = oldHeight;
            float scaleFactor = 1f;

            // On modifie la taille de l'image à créer, car on coupe pas les bords et on augmente la taille de l'image
            if (!adjustSize || !couperBords)
            {
                double angleRadians = angle * Math.PI / 180d;

                double cos = Math.Abs(Math.Cos(angleRadians));
                double sin = Math.Abs(Math.Sin(angleRadians));
                targetWidth = (int)Math.Round(oldWidth * cos + oldHeight * sin);
                targetHeight = (int)Math.Round(oldWidth * sin + oldHeight * cos);
            }

            // On garde la taille de l'image sans couper les bords, il faut en réduire la taille pour qu'elle rentre dans la taille de départ
            if (adjustSize && !couperBords)
            {
                scaleFactor = Math.Min((float)oldWidth / targetWidth, (float)oldHeight / targetHeight);
                targetWidth = oldWidth;
                targetHeight = oldHeight;
            }

            // Si on coupe les bords alors on garde la taille d'origine de l'image

            using (Image img = myImage.ToBitmap())
            using (Bitmap newBitmap = new Bitmap(targetWidth, targetHeight, img.PixelFormat))
            {
                newBitmap.SetResolution(img.HorizontalResolution, img.VerticalResolution);

                // Algorithmie gérée par gdi+ 
                using (Graphics g = Graphics.FromImage(newBitmap))
                {
                    g.InterpolationMode = (System.Drawing.Drawing2D.InterpolationMode)(mode - InterpolationMode.Windows_Bilinear + 3);
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality; //Toujours en haute qualité (pour matcher mon algorithme de rotation bien sur)

                    // On remplit le background par la couleur spécifiée
                    g.Clear(backPixel.ToColor());

                    // On ajoute la translation spécifiée
                    g.TranslateTransform(targetWidth / 2f, targetHeight / 2f);

                    // Mise à l'échelle si on garde la taille de l'image sans couper les bords, il faut en réduire la taille pour qu'elle 'fit'
                    if (scaleFactor != 1f)
                        g.ScaleTransform(scaleFactor, scaleFactor);

                    g.RotateTransform((float)angle);
                    g.TranslateTransform(-oldWidth / 2f, -oldHeight / 2f);

                    // Dessin du resultat
                    g.DrawImage(img, 0, 0);
                }

                return MyImage.FromBitmap(newBitmap);
            }

        }


        /// <summary>
        /// Applique une mise à l'échelle à une image, algorithmie gérée par GDI+
        /// </summary>
        /// <param name="myImage"></param>
        /// <param name="targetHeight"></param>
        /// <param name="targetWidth"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        private static MyImage Redimensionnement_Windows(MyImage myImage, int targetHeight, int targetWidth, InterpolationMode mode)
        {
            if (targetHeight == myImage.Height && targetWidth == myImage.Width)
                return myImage.Clone();

            using (Image img = myImage.ToBitmap())
            using (Bitmap newBitmap = new Bitmap(targetWidth, targetHeight, img.PixelFormat))
            {
                newBitmap.SetResolution(img.HorizontalResolution, img.VerticalResolution);

                // Algorithmie gérée par gdi+ 
                using (Graphics g = Graphics.FromImage(newBitmap))
                {
                    g.InterpolationMode = (System.Drawing.Drawing2D.InterpolationMode)(mode - InterpolationMode.Windows_Bilinear + 3);
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingMode = CompositingMode.SourceCopy;
                    g.CompositingQuality = CompositingQuality.HighQuality;

                    //On dessine l'image aux coordonnées spécifiées, windows change auto l'aspect de l'image donc on gère ça avant
                    g.DrawImage(img, 0, 0, targetWidth, targetHeight);
                }

                return MyImage.FromBitmap(newBitmap);
            }
        }

        #endregion


        //Méthodes de Rognage

        #region Rognage

        /// <summary>
        /// Effectue un rognage dans l'image entre les coordonnées spécifiées. N'aggrandie pas la nouvelle image
        /// </summary>
        public void Rognage()
        {
            ReplaceImage(GetRognage());
        }

        /// <summary>
        /// Renvoie un rognage de l'image entre les coordonnées spécifiées
        /// </summary>
        public MyImage GetRognage()
        {
            Rectangle rClip = this.Clip.GetIntersectionWithImage(new Size(this.MyImage.Width, this.MyImage.Height), 0);

            MyImage im = new MyImage(rClip.Height, rClip.Width, this.MyImage.PixelFormat);
            im.ChangeData(InternalRognage(rClip.X, rClip.Y, rClip.Right, rClip.Bottom));

            return im;
        }

        /// <summary>
        /// Rogne une image avec des nouvelles dimensions.
        /// </summary>
        /// <param name="startW"></param>
        /// <param name="startH"></param>
        /// <param name="endW"></param>
        /// <param name="endH"></param>
        private byte[] InternalRognage(int startW, int startH, int endW, int endH)
        {
            if (startW < 0 || startH < 0 || startW > endW || startH > endH || endW > this.MyImage.Width || endH > this.MyImage.Height)
            {
                throw new ArgumentOutOfRangeException("Encadrement de l'image non valide.");
            }

            int destWidth = endW - startW;
            int destHeight = endH - startH;

            int destPad = Formats.GetPaddingPixel(destWidth, this.MyImage.PixelFormat.GetPixelLength());

            int lengthPixel = this.MyImage.PixelFormat.GetPixelLength();

            int destStride = destWidth * lengthPixel + destPad;

            byte[] newData = new byte[destStride * destHeight];

            int dataStartIndex = this.MyImage.Stride * startH + startW * lengthPixel;

            uint uAmount = (uint)(destStride - destPad);

            unsafe
            {
                fixed (byte* fSrcBuffer = this.MyImage.ToBGRArray())
                fixed (byte* fDestBuffer = newData)
                {
                    CopyLines(fDestBuffer, fSrcBuffer + dataStartIndex, uAmount, destHeight, destStride, this.MyImage.Stride);
                }
            }

            return newData;
        }

        #endregion


        //Transformations des couleurs

        #region Transformations couleurs

        /// <summary>
        /// Inverse les couleurs des pixels d'un <see cref="Photoshop3000.MyImage"/>
        /// </summary>
        public void InversionCouleurs()
        {
            //optimisation done

            byte[] data = this.MyImage.ToBGRArray();


            bool alphaExiste = this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Argb32;

            int skipAlpha = alphaExiste ? 1 : 0;

            int indexH = 0;
            int maxW = this.MyImage.Stride - this.MyImage.GetPadding();

            while (indexH < this.MyImage.Height)
            {
                int décalage = indexH++ * this.MyImage.Stride;

                for (int indexW = 0; indexW < maxW;)
                {
                    data[décalage + indexW] = (byte)(255 - data[décalage + indexW++]);

                    data[décalage + indexW] = (byte)(255 - data[décalage + indexW++]);

                    data[décalage + indexW] = (byte)(255 - data[décalage + indexW++]);

                    indexW += skipAlpha; //on inverse pas la composante alpha, si elle est là
                }

            }

        }


        /// <summary>
        /// Transforme une image en nuance de gris
        /// </summary>
        public void TransformationGris()
        {
            TransformationNoirEtBlanc(0);
        }

        /// <summary>
        /// Transforme les pixels d'un <see cref="Photoshop3000.MyImage"/> en des nuances de gris, de noir et de blanc en fonction d'un facteur %. <para/>100% revient à changer la photo qu'en noir et qu'en blanc, 0 revient à mettre la photo en nuance de gris normalement
        /// </summary>
        public void TransformationNoirEtBlanc(int intensitéPourcentage)
        {
            //optimisation done

            int intensité = Math.Min(Math.Max(intensitéPourcentage, 0), 100) / 2 * 255 / 100;

            byte[] data = this.MyImage.ToBGRArray();

            bool alphaExiste = this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Argb32;

            int lengthPixel = alphaExiste ? 4 : 3;

            int indexH = 0;
            int maxW = this.MyImage.Stride - this.MyImage.GetPadding();

            while (indexH < this.MyImage.Height)
            {
                int décalage = indexH * this.MyImage.Stride;
                int indexW = 0;

                while (indexW < maxW)
                {
                    byte b = data[décalage + indexW];
                    byte g = data[décalage + indexW + 1];
                    byte r = data[décalage + indexW + 2];

                    int moyenne = (b + r + g) / 3;

                    if (moyenne > 255 / 2) //Moitié haute, on augmente l'intensité des composants du pixel
                    {
                        moyenne += intensité;
                    }
                    else //En dessous on les baisses
                    {
                        moyenne -= intensité;
                    }
                    byte moyenneB = (byte)Math.Min(Math.Max(moyenne, 0), 255);

                    data[décalage + indexW] = moyenneB;  //Bleu
                    data[décalage + indexW + 1] = moyenneB;  //Vert
                    data[décalage + indexW + 2] = moyenneB;  //Rouge

                    indexW += lengthPixel;
                }
                indexH++;
            }
        }


        /// <summary>
        /// Transforme une image en sépia
        /// </summary>
        public void TransformationSépia()
        {
            //optimisation done

            byte[] data = this.MyImage.ToBGRArray();

            bool alphaExiste = this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Argb32;

            int lengthPixel = alphaExiste ? 4 : 3;

            int indexH = 0;
            int maxW = this.MyImage.Stride - this.MyImage.GetPadding();

            while (indexH < this.MyImage.Height)
            {
                int décalage = indexH * this.MyImage.Stride;
                int indexW = 0;

                while (indexW < maxW)
                {
                    byte b = data[décalage + indexW];
                    byte g = data[décalage + indexW + 1];
                    byte r = data[décalage + indexW + 2];

                    data[décalage + indexW] = (byte)(0.272f * r + 0.534f * g + 0.131f * b);  //Bleu
                    data[décalage + indexW + 1] = (byte)Math.Min(0.349f * r + 0.686f * g + 0.168f * b, 255f);  //Vert
                    data[décalage + indexW + 2] = (byte)Math.Min(0.393f * r + 0.769f * g + 0.189f * b, 255f);  //Rouge

                    indexW += lengthPixel;
                }
                indexH++;
            }
        }



        /// <summary>
        /// Modifie la luminosité de l'image en fonction d'un facteur pourcentage : 50 = luminosité actuelle, 100 = tout blanc, 0 = tout noir
        /// </summary>
        /// <param name="brightnessPourcentage"></param>
        public void TransformationLuminositéPerçue(int brightnessPourcentage)
        {
            //optimisation done

            brightnessPourcentage = (2 * brightnessPourcentage - 100) * 255 / 100; //on passe de 0-100 à -100 - +100 puis -255 - +255

            byte[] data = this.MyImage.ToBGRArray();

            bool alphaExiste = this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Argb32;

            int lengthPixel = alphaExiste ? 4 : 3;

            int indexH = 0;
            int maxW = this.MyImage.Stride - this.MyImage.GetPadding();

            while (indexH < this.MyImage.Height)
            {
                int décalage = indexH * this.MyImage.Stride;
                int indexW = 0;

                while (indexW < maxW)
                {
                    data[décalage + indexW] = (byte)Math.Min(Math.Max(data[décalage + indexW] + brightnessPourcentage, 0), 255);  //Bleu
                    data[décalage + indexW + 1] = (byte)Math.Min(Math.Max(data[décalage + indexW + 1] + brightnessPourcentage, 0), 255);  //Vert
                    data[décalage + indexW + 2] = (byte)Math.Min(Math.Max(data[décalage + indexW + 2] + brightnessPourcentage, 0), 255);  //Rouge

                    indexW += lengthPixel;
                }
                indexH++;
            }
        }

        /// <summary>
        /// Atténue les couleurs d'une image
        /// </summary>
        /// <param name="couleurChgmtIntensité">Pourcentage d'atténuation (0% = noir et blanc, 100% = couleurs normales)</param>
        public void TransformationCouleurIntensité(int couleurChgmtIntensité)
        {
            MyImage imageGrise = this.MyImage.Clone();

            new MyGraphics(imageGrise).TransformationGris();

            Clip = new Rectangle(0, 0, this.MyImage.Width, this.MyImage.Height);

            this.DrawImage(imageGrise, couleurChgmtIntensité);
        }



        /// <summary>
        /// Transforme l'image en ne retenant que les composants RGB sélectionnés
        /// </summary>
        public void TransformationFiltreRGB(bool rouge, bool vert, bool bleu)
        {
            if (!bleu)
                this.ModifyComponentValue(0, 0);
            if (!vert)
                this.ModifyComponentValue(1, 0);
            if (!rouge)
                this.ModifyComponentValue(2, 0);
        }

        /// <summary>
        /// Applique un filtre de la couleur spécifiée à l'image.
        /// </summary>
        /// <param name="pixel"></param>
        public void TransformationFiltreCouleur(Pixel pixel)
        {
            //Note: exemple, entrer en paramètre le pixel rouge(255, 0, 0) revient à utiliser
            // TransformationFiltreRGB(rouge = true, vert = false, bleu = false) (méthode bcp plus rapide)

            //Inutile d'optimiser ici comme dans les autres fcts car on est obligé de passer par un Pixel

            int hue = pixel.Hue();
            float sat = pixel.Saturation();
            float lum = pixel.Brightness();

            for (int i = 0; i < this.MyImage.Height; i++)
            {
                for (int j = 0; j < this.MyImage.Width; j++)
                {
                    this.MyImage[i, j] = Pixel.FromHSL(hue, sat, (lum + this.MyImage[i, j].Brightness()) / 510f); // bright / 2*255 pour avoir un float
                }
            }

        }


        /// <summary>
        /// Ne retient que les composants sélectionnés à partir d'une certaine intensité (0-255) et grise toutes les autres 
        /// parties de l'image où l'intensité est en dessous. Mettre à 0 revient à ignorer un composant.
        /// </summary>
        public void TransformationGriserRGB(int intensitéR, int intensitéV, int intensitéB, bool higherR, bool higherV, bool higherB)
        {
            for (int i = 0; i < this.MyImage.Height; i++)
            {
                for (int j = 0; j < this.MyImage.Width; j++)
                {
                    Pixel p = this.MyImage[i, j];

                    if (!((higherR ? p.R >= intensitéR : p.R <= intensitéR) && (higherV ? p.G >= intensitéV : p.G <= intensitéV)
                        && (higherB ? p.B >= intensitéB : p.B <= intensitéB)))
                    {
                        this.MyImage[i, j] = p.TransformationGris(0);
                    }
                }
            }

        }


        #region Tri des couleurs

        /// <summary>
        /// Trie les couleurs de l'image de la plus utilisée à la moins utilisée. Prend en compte la transparence.
        /// </summary>
        /// <param name="coté">1 = haut bas, 2 = droite gauche, 3 = bas haut, 4 = gauche droite</param>
        public void TrierParCouleurCoté(int coté)
        {
            int index1, index2 = 0;

            bool droiteGauche = coté == 2 || coté == 4;
            bool plusPetit = coté == 2 || coté == 3;

            int incr1, max1, max2;

            if (coté == 1 || coté == 3)
            {
                index1 = 0;
                incr1 = 1;
                max1 = this.MyImage.Height - 1;
                max2 = this.MyImage.Width - 1;
            }
            else
            {
                index1 = 0;
                incr1 = 1;
                max1 = this.MyImage.Width - 1;
                max2 = this.MyImage.Height - 1;
            }

            var pixels = MyImageStats.GetPixelsByOccurence(this.MyImage);

            foreach (var pixel in plusPetit ? pixels.OrderBy(x => x.Value) : pixels.OrderByDescending(x => x.Value))
            {
                int indexPix = 0;

                for (; index1 <= max1; index1 += incr1, index2 = 0)
                {
                    for (; index2 <= max2; index2++, indexPix++)
                    {
                        if (indexPix >= pixel.Value) //Plus efficace ac un goto que 2 conditions dans les 2 boucles for
                            goto next;
                        if (droiteGauche)
                            this.MyImage[index2, index1] = Pixel.FromArgb(pixel.Key);
                        else
                            this.MyImage[index1, index2] = Pixel.FromArgb(pixel.Key);

                    }
                }

            next:;
            }
        }


        /// <summary>
        /// Trie les couleurs de l'image de la plus utilisée à la moins utilisée. Prend en compte la transparence.
        /// </summary>
        /// <param name="coin">1 = haut gauche, 2 = haut droite, 3 = bas gauche, 4 = bas droite</param>
        /// <param name="orderByAscending">Manière dont le dico est trié. 0 : aléatoirement, 1 : grand -> petit,
        /// 2 : petit -> grand</param>
        public void TrierParCouleurCoin(int coin, int orderByAscending)
        {
            int indexY, indexX; //Index dans l'image.

            int valueInverseY, valueInverseX; //Valeur pour laquelle incrX/Y devient -valueInverse, sinon on tourne à 90°

            int incrX, incrY; //Incrémentation pour X ou Y

            int limDynamiqueX, limDynamiqueY, limFixeX, limFixeY; //Limites

            if (coin == 1)
            {
                indexX = this.MyImage.Width - 1;
                indexY = this.MyImage.Height - 1;

                incrX = -1;
                incrY = 0;
            }
            else if (coin == 2)
            {
                indexX = 0;
                indexY = this.MyImage.Height - 1;

                incrX = 0;
                incrY = -1;
            }
            else if (coin == 3)
            {
                indexX = this.MyImage.Width - 1;
                indexY = 0;

                incrX = 0;
                incrY = 1;
            }
            else //if (coin == 4)
            {
                indexX = 0;
                indexY = 0;

                incrX = 1;
                incrY = 0;
            }

            limDynamiqueX = indexX;
            limDynamiqueY = indexY;

            limFixeX = this.MyImage.Width - 1 - indexX;
            limFixeY = this.MyImage.Height - 1 - indexY;

            valueInverseX = indexX == 0 ? 1 : -1;
            valueInverseY = indexY == 0 ? 1 : -1;


            var pixelsAndOccurences = MyImageStats.GetPixelsByOccurence(this.MyImage);

            var ordonné = orderByAscending == 1 ? pixelsAndOccurences.OrderByDescending(x => x.Value) :
                pixelsAndOccurences.OrderBy(x => orderByAscending == 2 ? x.Value : Rand.Next());

            //On utilise des goto pour s'éviter le plaisir d'avoir pleins de fonctions et pleins de boucles
            //qui compliqueraient franchement la compréhension et l'efficacité du code (déjà limite).
            foreach (var pixel in ordonné)
            {
                int indexPix = 0;

                if (incrX == 0) goto forHeight;
                else goto forWidth;


                forWidth:; //Boucle pour la largeur

                if (indexPix >= pixel.Value) //Plus efficace ac un goto que 2 conditions dans les 2 boucles for
                    goto nextPixel;

                for (; indexX <= Math.Max(limDynamiqueX, limFixeX) && indexX >= Math.Min(limDynamiqueX, limFixeX); indexX += incrX, indexPix++)
                {
                    if (indexPix >= pixel.Value) //Plus efficace ac un goto que 2 conditions dans les 2 boucles for
                        goto nextPixel;
                    this.MyImage[indexY, indexX] = Pixel.FromArgb(pixel.Key);
                }


                if (incrX == valueInverseX) //On inverse le sens = 180°
                {
                    incrX = -valueInverseX;
                    indexY += valueInverseY;
                    indexX += -valueInverseX;
                    limDynamiqueY += valueInverseY;
                    goto forWidth;
                }
                else //if (incrX == -valueInverseX) //On tourne à 90°
                {
                    incrX = 0;
                    incrY = valueInverseY;
                    indexX += valueInverseX;
                    indexY += valueInverseY;
                    limDynamiqueY += valueInverseY;
                    goto forHeight;
                }


            forHeight:; //Boucle pour la hauteur

                if (indexPix >= pixel.Value) //Plus efficace ac un goto que 2 conditions dans les 2 boucles for
                    goto nextPixel;

                for (; indexY <= Math.Max(limDynamiqueY, limFixeY) && indexY >= Math.Min(limDynamiqueY, limFixeY); indexY += incrY, indexPix++)
                {
                    if (indexPix >= pixel.Value) //Plus efficace ac un goto que 2 conditions dans les 2 boucles for
                        goto nextPixel;
                    this.MyImage[indexY, indexX] = Pixel.FromArgb(pixel.Key);
                }

                if (incrY == valueInverseY) //On inverse le sens = 180°
                {
                    incrY = -valueInverseY;
                    indexX += valueInverseX;
                    indexY += -valueInverseY;
                    limDynamiqueX += valueInverseX;
                    goto forHeight;
                }
                else //if (incrY == -valueInverseY) //On tourne à 90°
                {
                    incrY = 0;
                    incrX = valueInverseX;
                    indexX += valueInverseX;
                    indexY += valueInverseY;
                    limDynamiqueX += valueInverseX;
                    goto forWidth;
                }

            nextPixel:; //Prochain pixel
            }
        }


        #endregion


        /// <summary>
        /// Modifie toutes les valeurs du composant sélectionné avec la valeur indiquée.
        /// </summary>
        /// <param name="indexComponent"> b = 0 | g = 1 | r = 2 | a =? 3</param>
        /// <param name="value">0-255</param>
        public void ModifyComponentValue(int indexComponent, int value)
        {
            //optimisation done

            byte _value = (byte)value;

            bool alphaExiste = this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Argb32;

            int lengthPixel = alphaExiste ? 4 : 3;

            if (indexComponent == 0 || indexComponent == 1 || indexComponent == 2 || (indexComponent == 3 && alphaExiste)) //b, g, r, a
            {
                int scanLine = 0;
                int strideIm = this.MyImage.Stride;
                int maxW = this.MyImage.Stride - this.MyImage.GetPadding();
                int maxH = this.MyImage.Height;

                unsafe
                {
                    fixed (byte* buffer = this.MyImage.ToBGRArray())
                    {
                        byte* destBuffer = buffer + indexComponent;

                        while (scanLine++ < maxH)
                        {
                            for (int index = 0; index < maxW; index += lengthPixel)
                            {
                                *(destBuffer + index) = _value;
                            }

                            destBuffer += strideIm;
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Intervertit les couleurs (au format argb) en un nouveau format selon un tableau de byte avec le nouvel ordre pour les
        /// composants. <para/>
        /// Exemple : actuellement {b, g, r, a} = {1, 2, 3, 4}.  Si { 3, 2, 1, 4 } est entré en paramètre alors
        /// { b, g, r, a } => {r, g, b, a} (le rouge et le bleu sont intervertis, l'alpha et le vert restent inchangés).
        /// Exemple 2 : si { 2, 2, 2, 1 } , { b, g, r, a } devient { g, g, g, b }
        /// <para/>La longueur du tableau doit etre de 3 ou 4, la 4ème composante sera ignorée ou rajoutée si le format pivot
        /// est 24 ou 32 bpp.
        /// <para/>Le tableau doit exclusivement être composé de chiffres entre 1 et 3/4 (en fonction de la longueur de celui-ci).
        /// </summary>
        /// <param name="newOrderRgba"></param>
        public void SwapComponents(byte[] newOrderRgba)
        {
            if (newOrderRgba == null)
                return;

            bool b_32bpp = (int)this.MyImage.PixelFormat == 32;

            byte[] originalOrderRgba;

            if (b_32bpp)
            {
                if (newOrderRgba.Length == 3)
                {
                    newOrderRgba = new byte[4] { newOrderRgba[0], newOrderRgba[1], newOrderRgba[2], 4 };
                }
                else if (newOrderRgba.Length != 4)
                {
                    return;
                }

                originalOrderRgba = new byte[4] { (Pixel.ArgbRedShift / 8) + 1 , (Pixel.ArgbGreenShift / 8) + 1,
                (Pixel.ArgbBlueShift / 8) + 1, (Pixel.ArgbAlphaShift / 8) + 1 };
            }
            else
            {
                if (newOrderRgba.Length == 4)
                {
                    newOrderRgba = new byte[3] { newOrderRgba[0], newOrderRgba[1], newOrderRgba[2] };
                }
                else if (newOrderRgba.Length != 3)
                {
                    return;
                }

                originalOrderRgba = new byte[3] { (Pixel.ArgbRedShift / 8) + 1 , (Pixel.ArgbGreenShift / 8) + 1,
                (Pixel.ArgbBlueShift / 8) + 1 };
            }


            Formats.InvertOrderRgbaArray(this.MyImage.ToBGRArray(), originalOrderRgba, newOrderRgba, b_32bpp ? 0 : this.MyImage.Width);
        }

        /// <summary>
        /// Efface la surface de l'image et la remplit avec le <see cref="Pixel"/> spécifié
        /// </summary>
        /// <param name="color">Pixel</param>
        public void Remplissage(Pixel color)
        {
            ModifyComponentValue(0, color.B);
            ModifyComponentValue(1, color.G);
            ModifyComponentValue(2, color.R);
            ModifyComponentValue(3, color.A);
        }


        #endregion


        //Effet miroir

        #region Effet miroir

        /// <summary>
        /// Inverse les pixels de droite à gauche d'un <see cref="Photoshop3000.MyImage"/> par rapport au milieu
        /// </summary>
        public void EffetMiroir(bool largeur = true)
        {
            //En hauteur on peut jouer avec les pointeurs plus facilement qu'en largeur où on est obligé 
            //d'inverser manuellement les lignes de donnée et on doit parcourir toute l'image et pas juste la moitié
            if (largeur)
            {
                unsafe
                {
                    fixed (byte* data = this.MyImage.ToBGRArray())
                    {
                        int lengthPixel = this.MyImage.PixelFormat.GetPixelLength();
                        int padding = this.MyImage.GetPadding();

                        byte* lineData = data;

                        for (int i = 0; i < this.MyImage.Height; i++)
                        {
                            ReversePixelsLine(lineData, lengthPixel, this.MyImage.Stride - padding); //inversement

                            lineData += this.MyImage.Stride;
                        }
                    }
                }
            }
            else
            {
                Formats.ReverseLineArray(this.MyImage.ToBGRArray(), this.MyImage.Height);
            }
        }


        /// <summary>
        /// L'image de l'instance est ajoutée à côté d'elle même et/ou sous elle de manière symétrique.
        /// </summary>
        /// <param name="effetLargeur">L'image est démultipliée en largeur</param>
        /// <param name="effetHauteur">L'image est démultipliée en hauteur</param>
        public void EffetMiroirDoubleOuQuadruple(bool effetLargeur, bool effetHauteur)
        {
            MyImage destImage = new MyImage(this.MyImage.Height * (effetHauteur ? 2 : 1), this.MyImage.Width * (effetLargeur ? 2 : 1), this.MyImage.PixelFormat);

            unsafe
            {
                fixed (byte* destData = destImage.ToBGRArray())
                fixed (byte* srcData = this.MyImage.ToBGRArray())
                {
                    byte* lineSrcData = srcData;
                    byte* lineDestData = destData;

                    uint srcStride = (uint)(this.MyImage.Stride - this.MyImage.GetPadding()); //stride sans le padding

                    //Image en haut à gauche, à l'endroit
                    CopyLines(lineDestData, lineSrcData, srcStride, this.MyImage.Height, destImage.Stride, this.MyImage.Stride);

                    //A droite, on inverse les lignes de pixels
                    if (effetLargeur)
                    {
                        lineDestData = destData + srcStride;
                        lineSrcData = destData;

                        for (int i = 0; i < this.MyImage.Height; i++)
                        {
                            Formats.SafeMemCpy((IntPtr)lineDestData, (IntPtr)lineSrcData, srcStride); //Copie à droite 

                            ReversePixelsLine(lineDestData, this.MyImage.PixelFormat.GetPixelLength(), (int)srcStride); //Inversement

                            lineDestData += destImage.Stride;
                            lineSrcData += destImage.Stride;
                        }
                    }

                    //En bas, on colle les lignes de pixels à l'opposé
                    if (effetHauteur)
                    {
                        lineDestData = destData + destImage.Stride * (destImage.Height - 1);
                        lineSrcData = destData;

                        CopyLines(lineDestData, lineSrcData, srcStride, this.MyImage.Height, -destImage.Stride, destImage.Stride);
                    }

                    //En bas à droite, on colle les lignes de pixels inversée à l'opposé
                    if (effetHauteur && effetLargeur)
                    {
                        lineDestData = destData + destImage.Stride * (destImage.Height - 1) + srcStride;
                        lineSrcData = destData + srcStride;

                        CopyLines(lineDestData, lineSrcData, srcStride, this.MyImage.Height, -destImage.Stride, destImage.Stride);
                    }
                }
            }


            ReplaceImage(destImage);
        }


        /// <summary>
        /// La moitié de l'image est dédoublée soit en largeur soit en hauteur
        /// </summary>
        /// <param name="hauteur"></param>
        public void EffetMiroirMoitié(bool hauteur)
        {
            int cotéÀRogner = hauteur ? this.MyImage.Height : this.MyImage.Width;
            int cotéInverse = hauteur ? this.MyImage.Width : this.MyImage.Height;

            int moitié = cotéÀRogner / 2;

            for (int i = moitié; i < cotéÀRogner; i++)
            {
                for (int j = 0; j < cotéInverse; j++)
                {
                    if (hauteur)
                    {
                        this.MyImage[i, j] = this.MyImage[moitié - (i - moitié), j];
                    }
                    else
                    {
                        this.MyImage[j, i] = this.MyImage[j, moitié - (i - moitié)];
                    }
                }
            }
        }

        #endregion


        //Filtres

        #region Filtres

        /// <summary>
        /// Filtre de Sobel pour détecter les contours
        /// </summary>
        public void FiltreDetectionContours()
        {
            ReplaceImage(new Filtre(this.MyImage, new ConvolutionMatrix(ConvolutionMatrix.ImageFiltre.Bords_Détection)).MyImage);
        }


        /// <summary>
        /// Transforme une image pour détecter les contours avec la méthode de Sobel (gris -> flou -> filtre mat verticale + filtre mat horizontale -> somme)
        /// </summary>
        public void FiltreSobel()
        {
            InternalSobelCanny(false);
        }

        /// <summary>
        /// Transforme une image avec le filtre de detection de contour avec la méthode de Canny
        /// </summary>
        /// <param name="intensitéMax">Intensité de pixel (0-255) à partir de laquelle on est sur qu'un pixel fait parti d'une ligne de contour</param>
        /// <param name="intensitéMin">Intensité de pixel (0-255) en dessous de laquelle on est sur qu'un pixel ne fait pas parti d'une ligne de contour</param>
        public void FiltreCanny(int intensitéMax = 200, int intensitéMin = 50)
        {
            //1.  - Application du filtre de Sobel à l'image
            //
            //2.  - On rétrécit la taille des bords d'une ligne de contour : pour cela on regarde la direction des bords de l'image, 
            //  en fonction de ça on élimine les pixels les moins importants de sorte qu'il ne reste qu'1 ou 2 pixel pour un contour. 
            //
            // . Direction Teta = tan-1(Gy / Gx)  ;   Teta = 67.5-112.5 % 180 = haut/bas    |    
            //   Teta = 112.5-157.5 % 180 = diagonale basGauche/hautDroite, 22.5-67.5 % 180 = autres diago
            //      = 0-22.5 && 180-157.5 % 180 = gauche/droite
            //
            //3.  - On élimine le bruit crée par des lignes de contours indésirables en éliminant les pixels en dessous d'une intensité
            //   X et en gardant les pixels au dessus d'une intensité Y. Pour les pixels à l'intensité entre X et Y on regarde si ces
            //   pixels font parti d'une ligne de bord forte ou non, si oui on les garde sinon on les élimine.

            InternalSobelCanny(true, intensitéMax, intensitéMin);
        }

        private void InternalSobelCanny(bool canny, int intensitéMax = 200, int intensitéMin = 50)
        {
            this.TransformationGris();
            this.FiltreFlouGaussien();

            MyImage x = new Filtre(this.MyImage, new ConvolutionMatrix(ConvolutionMatrix.ImageFiltre.Sobel_Horizontal)).MyImage;
            MyImage y = new Filtre(this.MyImage, new ConvolutionMatrix(ConvolutionMatrix.ImageFiltre.Sobel_Vertical)).MyImage;

            //Sobel

            for (int i = 0; i < this.MyImage.Height; i++)
            {
                for (int j = 0; j < this.MyImage.Width; j++)
                {
                    int valueMoyenneP = Math.Min((int)Math.Sqrt(Math.Pow(y[i, j].Moyenne(), 2) + Math.Pow(x[i, j].Moyenne(), 2)), 255);
                    this.MyImage[i, j] = Pixel.FromArgb(valueMoyenneP, valueMoyenneP, valueMoyenneP);
                }
            }
            if (!canny)
                return;

            //Retrecissement lignes de contours

            for (int i = 1; i < this.MyImage.Height - 1; i++)
            {
                for (int j = 1; j < this.MyImage.Width - 1; j++)
                {
                    int mgntd = Math.Min((int)Math.Sqrt(Math.Pow(y[i, j].Moyenne(), 2) + Math.Pow(x[i, j].Moyenne(), 2)), 255);

                    double theta = Math.Atan(y[i, j].Moyenne() / (x[i, j].Moyenne() == 0 ? 1 : x[i, j].Moyenne())) % 180;

                    //if (mgntd != 0)
                    {
                        int increX = 0;
                        int increY = 0;

                        if (theta >= 67.5 && theta < 112.5) //haut bas   |
                        {
                            increY = 1;
                        }
                        else if (theta >= 112.5 && theta < 157.5)  //Diago /
                        {
                            increX = -1;
                            increY = 1;
                        }
                        else if (theta >= 22.5 && theta < 67.5)   // Diago \
                        {
                            increX = 1;
                            increY = 1;
                        }
                        else if ((theta >= 0 && theta < 22.5) || (theta >= 157.5 && theta < 180))  //Gauche droite _
                        {
                            increX = 1;
                        }

                        int mgntdBas = Math.Min((int)Math.Sqrt(y[i + increY, j + increX].Moyenne() * y[i + increY, j + increX].Moyenne() + x[i + increY, j + increX].Moyenne() * x[i + increY, j + increX].Moyenne()), 255);
                        int mgntdHaut = Math.Min((int)Math.Sqrt(y[i - increY, j - increX].Moyenne() * y[i - increY, j - increX].Moyenne() + x[i - increY, j - increX].Moyenne() * x[i - increY, j - increX].Moyenne()), 255);

                        if (mgntdHaut >= mgntd)
                        {
                            this.MyImage[i + increY, j + increX] = Pixel.Zero;
                        }
                        else if (mgntdBas >= mgntd)
                        {
                            this.MyImage[i - increY, j - increX] = Pixel.Zero;
                        }
                        else
                        {
                            this.MyImage[i - increY, j - increX] = this.MyImage[i + increY, j + increX] = Pixel.Zero;
                        }
                    }
                }
            }

            //Elimination du bruit

            for (int i = 0; i < this.MyImage.Height; i++)
            {
                for (int j = 0; j < this.MyImage.Width; j++)
                {
                    if (this.MyImage[i, j].Moyenne() < intensitéMin)
                        this.MyImage[i, j] = Pixel.Zero;
                    else if (this.MyImage[i, j].Moyenne() < intensitéMax)
                    {
                        int minX = i < 2 ? 0 : i - 2;
                        int maxX = i >= this.MyImage.Height - 2 ? this.MyImage.Height - 1 : i + 2;
                        int minY = j < 2 ? 0 : j - 2;
                        int maxY = j >= this.MyImage.Width - 2 ? this.MyImage.Width - 1 : j + 2;
                        bool voisinBord = false;
                        for (int k = minX; k < maxX && !voisinBord; k++)
                        {
                            for (int l = minY; l < maxY; l++)
                            {
                                if (this.MyImage[k, l].Moyenne() > intensitéMin && (k != i || l != j))
                                {
                                    voisinBord = true;
                                    break;
                                }
                            }
                        }
                        if (!voisinBord)
                            this.MyImage[i, j] = Pixel.Zero;
                    }
                }
            }
        }


        /// <summary>
        /// Filtre de renforcement des bords
        /// </summary>
        public void FiltreRenforcementContours()
        {
            ReplaceImage(new Filtre(this.MyImage, new ConvolutionMatrix(ConvolutionMatrix.ImageFiltre.Bords_Renforcement)).MyImage);
        }

        /// <summary>
        /// Filtre d'affaiblissement des bords
        /// </summary>
        public void FiltreAffaiblissementContours()
        {
            ReplaceImage(new Filtre(this.MyImage, new ConvolutionMatrix(ConvolutionMatrix.ImageFiltre.Bords_Affaiblissement)).MyImage);
        }



        /// <summary>
        /// Transforme une image en un dessin, les couleurs de l'image peuvent être inversées pour se rapprocher du résultat attendu 
        /// (paysage entre autres, à éviter sur les portraits). <para/>Ajouter du bruit peut également améliorer le résultat.
        /// </summary>
        /// <param name="inversion">Inversion des couleurs de l'image ou non</param>
        public void FiltreDessin(bool inversion)
        {
            this.AddNoise(25);
            this.TransformationGris();
            if (inversion)
                this.InversionCouleurs();

            MyImage im = new Filtre(this.MyImage, new ConvolutionMatrix(ConvolutionMatrix.ImageFiltre.FlouGaussien)).MyImage;
            im = new Filtre(im, new ConvolutionMatrix(ConvolutionMatrix.ImageFiltre.Dessin)).MyImage;

            ReplaceImage(im);
        }

        /// <summary>
        /// Transforme une image en son équivalent en style gravure
        /// </summary>
        public void FiltreGravure()
        {
            //Un peu long à process
            this.TransformationNoirEtBlanc(0);

            MyImage im = new Filtre(this.MyImage, new ConvolutionMatrix(ConvolutionMatrix.ImageFiltre.Contraste)).MyImage;
            im = new Filtre(im, new ConvolutionMatrix(ConvolutionMatrix.ImageFiltre.Repoussage_Fort)).MyImage;
            im = new Filtre(im, new ConvolutionMatrix(ConvolutionMatrix.ImageFiltre.Dessin)).MyImage;

            ReplaceImage(im);
        }



        /// <summary>
        /// Transforme une image avec le filtre de repoussage faible
        /// </summary>
        public void FiltreRepoussageFaible()
        {
            this.TransformationNoirEtBlanc(0);
            ReplaceImage(new Filtre(this.MyImage, new ConvolutionMatrix(ConvolutionMatrix.ImageFiltre.Repoussage_Léger)).MyImage);
        }

        /// <summary>
        /// Transforme une image avec le filtre de repoussage fort
        /// </summary>
        public void FiltreRepoussageFort()
        {
            this.TransformationNoirEtBlanc(0);
            ReplaceImage(new Filtre(this.MyImage, new ConvolutionMatrix(ConvolutionMatrix.ImageFiltre.Repoussage_Fort)).MyImage);
        }



        /// <summary>
        /// Aiguise une image
        /// </summary>
        public void FiltreAiguisage()
        {
            ReplaceImage(new Filtre(this.MyImage, new ConvolutionMatrix(ConvolutionMatrix.ImageFiltre.Aiguiser)).MyImage);
        }

        /// <summary>
        /// Augmente le contraste d'une image
        /// </summary>
        public void FiltreContraste()
        {
            ReplaceImage(new Filtre(this.MyImage, new ConvolutionMatrix(ConvolutionMatrix.ImageFiltre.Contraste)).MyImage);
        }



        /// <summary>
        /// Rend une image floue (flou de mouvement)
        /// </summary>
        /// <param name="flouDeMouvement">Filtre de mouvement ou filtre normal</param>
        public void FiltreFlou(bool flouDeMouvement)
        {
            //this.Resize(0.5);
            ReplaceImage(new Filtre(this.MyImage, new ConvolutionMatrix((ConvolutionMatrix.ImageFiltre)(flouDeMouvement ? 1 : 0))).MyImage);
            //this.Resize(2);
        }

        /// <summary>
        /// Rend une image floue (flou Gaussien)
        /// </summary>
        public void FiltreFlouGaussien()
        {
            //this.Resize(0.5);
            ReplaceImage(new Filtre(this.MyImage, new ConvolutionMatrix(ConvolutionMatrix.ImageFiltre.FlouGaussien)).MyImage);
            //this.Resize(2);
        }

        /// <summary>
        /// Filtre de lissage
        /// </summary>
        public void FiltreLissage()
        {
            ReplaceImage(new Filtre(this.MyImage, new ConvolutionMatrix(ConvolutionMatrix.ImageFiltre.Lissage)).MyImage);
        }


        /// <summary>
        /// Test
        /// </summary>
        public void FiltreTest()
        {
            ReplaceImage(new Filtre(this.MyImage, new ConvolutionMatrix(ConvolutionMatrix.ImageFiltre.Test)).MyImage);
        }


        #endregion


        //Pixélisation

        #region Pixellisation

        /// <summary>
        /// Rend une <see cref="Photoshop3000.MyImage"/> pixélisée à partir de la taille d'un nouveau gros Pixel défini par rapport à la largeur de l'image. Dans un gros pixel, tous les petits pixels ont la même couleur.<para/>
        /// 100% revient à pixéliser une image avec un nouveau pixel de la taille maximale en largeur.
        /// </summary>
        /// <param name="rapport"></param>
        public void Pixellisation(double rapport)
        {
            Pixellisation((int)(rapport * this.MyImage.Width / 100.0));
        }

        /// <summary>
        /// Rend une <see cref="Photoshop3000.MyImage"/> pixélisée à partir d'une taille d'un nouveau gros Pixel. Dans un gros pixel, tous les petits pixels ont la même couleur. <para/>
        /// Si <see cref="KeepAspectRatio"/> est set à <see langword="true"/> les gros pixels auront tous les même taille. <para/>
        /// Si <see langword="false"/> les gros pixels auront la taille indiquée, certains pourraient donc être plus petits sur les bords de l'image
        /// </summary>
        /// <param name="taillePixel">Taille d'un gros pixel avec 1 la taille d'un pixel d'origine</param>
        public void Pixellisation(int taillePixel)
        {
            int max = Math.Min(this.MyImage.Height, this.MyImage.Width);
            taillePixel = Math.Max(Math.Min(max, taillePixel), 0);

            int taillePixelWidth = taillePixel;
            int taillePixelHeight = taillePixel;

            if (this.KeepAspectRatio)  //On change la taille des nouveaux gros pixels pour ne pas avoir de pixels d'une taille trop inférieur sur les bords
            {
                double maxDifférence = 5.0 / taillePixel;  // 5 pixels max d'écart pour les bords

                double resteWidth = (double)this.MyImage.Width / taillePixelWidth % 1;
                double resteHeight = (double)this.MyImage.Height / taillePixelHeight % 1;

                bool inversion = false;
                bool incrémentationDecimal = (double)this.MyImage.Width / taillePixelWidth % 1 > 0.5; //On va au plus proche d'abord

                while (!(resteWidth < maxDifférence / 1.5 || resteWidth > 1 - maxDifférence))//On préfère avoir un pixel d'une taille presque égale aux autres qu'un tout petit pixel d'une taille très inférieur pour les bords
                {
                    if (!inversion && ((taillePixelWidth < this.MyImage.Width && incrémentationDecimal) || (taillePixelWidth > 1 && !incrémentationDecimal))) //1ere étape, on augmente la taille si on peut
                    {
                        if (incrémentationDecimal)
                            taillePixelWidth++;
                        else
                            taillePixelWidth--;
                    }
                    else if (!inversion) //2ème étape
                    {
                        taillePixelWidth = taillePixel;
                        inversion = true;
                    }
                    else if ((taillePixelWidth > 1 && incrémentationDecimal) || (taillePixelWidth < this.MyImage.Width && !incrémentationDecimal)) //3ème étape, dans l'autre sens
                    {
                        if (incrémentationDecimal)
                            taillePixelWidth--;
                        else
                            taillePixelWidth++;
                    }
                    else //Fin
                    {
                        taillePixelWidth = taillePixel; //Echec, on part sur la taille donnée par l'utilisateur
                        inversion = false;
                        break;
                    }
                    resteWidth = (double)this.MyImage.Width / taillePixelWidth % 1;
                }

                incrémentationDecimal = (double)this.MyImage.Height / taillePixelHeight % 1 > 0.5;

                while (resteHeight > maxDifférence / 1.5 && resteHeight < 1 - maxDifférence) //On préfère avoir un pixel d'une taille presque égale aux autres qu'un tout petit pixel d'une taille très inférieur pour les bords
                {
                    if (!inversion && ((taillePixelHeight < this.MyImage.Height && incrémentationDecimal) || (taillePixelHeight > 1 && !incrémentationDecimal))) //1ere étape, on augmente la taille si on peut
                    {
                        if (incrémentationDecimal)
                            taillePixelHeight++;
                        else
                            taillePixelHeight--;
                    }
                    else if (!inversion) //2ème étape
                    {
                        taillePixelHeight = taillePixel;
                        inversion = true;
                    }
                    else if ((taillePixelHeight > 1 && incrémentationDecimal) || (taillePixelHeight < this.MyImage.Height && !incrémentationDecimal)) //3ème étape, dans l'autre sens
                    {
                        if (incrémentationDecimal)
                            taillePixelHeight--;
                        else
                            taillePixelHeight++;
                    }
                    else //Fin
                    {
                        taillePixelHeight = taillePixel; //Echec, on part sur la taille donnée par l'utilisateur
                        break;
                    }
                    resteHeight = (double)this.MyImage.Height / taillePixelHeight % 1;
                }
            }

            int quantitéGrosPixHauteur = (int)Math.Ceiling((double)this.MyImage.Height / taillePixelHeight);
            int quantitéGrosPixLargeur = (int)Math.Ceiling((double)this.MyImage.Width / taillePixelWidth);

            //Pixellisation
            for (int i = 0; i < quantitéGrosPixHauteur; i++)
            {
                for (int j = 0; j < quantitéGrosPixLargeur; j++)
                {
                    int X = taillePixelWidth * j;
                    int Y = taillePixelHeight * i;

                    this.Clip = new Rectangle(X, Y, taillePixelWidth, taillePixelHeight);

                    MyImage im = GetRognage(); //On rogne l'image pour isoler les couleurs

                    //new MyGraphics(im).Remplissage(MyImageStatistiques.GetAverageColor(im)); //On remplit l'image par la couleur moyenne de cette image

                    //DrawImage(im, rect, -1); //On copie cette image au point determiné préalablement dans l'image

                    FillRectangle(this.Clip, MyImageStats.GetAverageColor(im));
                }
            }
        }

        #endregion


        //Décalage

        #region Décalage

        /// <summary>
        /// Décale tous les pixels d'une image en fonction de la fonction sinus ou cosinus, en hauteur ou en largeur
        /// </summary>
        /// <param name="intensitéPeriodes">Intensité des décalages en hauteur (les valeurs basses sont préférables)</param>
        /// <param name="nombrePeriodes">Nombre de décalage periodiques sur toute l'image</param>
        /// <param name="cos"><see langword="true"/> = cosinus, sinon on utilise le sinus (ça change juste le décalage horizontale d'1/2 periode finalement)</param>
        /// <param name="largeur"><see langword="true"/> = décalage en largeur, sinon en hauteur</param>
        public void DécalagePixelsSinCos(double nombrePeriodes = 10, double intensitéPeriodes = 0.2, bool cos = false, bool largeur = true)
        {
            MyImage imageDécalée = new MyImage(this.MyImage.Height, this.MyImage.Width, this.MyImage.PixelFormat);

            int cote1 = largeur ? this.MyImage.Width : this.MyImage.Height;
            int cote2 = largeur ? this.MyImage.Height : this.MyImage.Width;

            double rapport = Math.PI * 2 * nombrePeriodes / cote1;

            for (int j = 0; j < cote1; j++)
            {
                double angle = cos ? Math.Cos(j * rapport) : Math.Sin(j * rapport);
                int décalage = (int)(angle * (cote2 / 2) * intensitéPeriodes);

                for (int i = 0; i < cote2; i++)
                {
                    int realHeight = décalage + i;

                    while (realHeight >= cote2)
                    {
                        realHeight -= cote2;
                    }
                    while (realHeight < 0)
                    {
                        realHeight += cote2;
                    }
                    if (largeur)
                        imageDécalée[realHeight, j] = this.MyImage[i, j]; //Pourrait être plus rapide en 2 fonctions distinctes
                    else
                        imageDécalée[j, realHeight] = this.MyImage[j, i];

                }
            }

            ReplaceImage(imageDécalée);
        }

        /// <summary>
        /// Décale tous les pixels d'une image dans le sens de la largeur ou de la hauteur selon une intensité de décalage et une largeur des décalages 
        /// </summary>
        /// <param name="intensitéPourcentage"></param>
        /// <param name="largeurLigne"></param>
        /// <param name="largeur"></param>
        public void DécalagePixels(int intensitéPourcentage, int largeurLigne, bool largeur)
        {
            intensitéPourcentage = intensitéPourcentage > 100 ? 100 : intensitéPourcentage < 0 ? 0 : intensitéPourcentage;

            MyImage imageDécalée = new MyImage(this.MyImage.Height, this.MyImage.Width, this.MyImage.PixelFormat);

            if (largeur)
            {
                int max = this.MyImage.Width * intensitéPourcentage / 100;

                int nombrePixelLargeur = this.MyImage.Height * largeurLigne / 100;

                for (int i = 0; i < this.MyImage.Height;)
                {
                    int décalage = Rand.Next(-max, max + 1);

                    for (int k = 0; k < nombrePixelLargeur && i < this.MyImage.Height; k++, i++)
                    {
                        for (int j = 0; j < this.MyImage.Width; j++)
                        {
                            int posX = j + décalage;

                            while (posX < 0)
                                posX += this.MyImage.Width;

                            while (posX > this.MyImage.Width - 1)
                                posX -= this.MyImage.Width;

                            imageDécalée[i, j] = this.MyImage[i, posX];
                        }
                    }

                }
            }
            else
            {
                int max = this.MyImage.Height * intensitéPourcentage / 100;

                int nombrePixelLargeur = (this.MyImage.Width - 0) * largeurLigne / 100;

                for (int i = 0; i < this.MyImage.Width;)
                {
                    int décalage = Rand.Next(-max, max + 1);

                    for (int k = 0; k < nombrePixelLargeur && i < this.MyImage.Width; k++, i++)
                    {
                        for (int j = 0; j < this.MyImage.Height; j++)
                        {
                            int posY = j + décalage;

                            while (posY < 0)
                                posY += this.MyImage.Height;

                            while (posY > this.MyImage.Height - 1)
                                posY -= this.MyImage.Height;

                            imageDécalée[j, i] = this.MyImage[posY, i];
                        }
                    }
                }
            }

            ReplaceImage(imageDécalée);
        }

        /// <summary>
        /// Décale tous les pixels d'une image selon des cercles concentriques
        /// </summary>
        public void DécalageCercle()
        {

        }

        #endregion


        //Bruit numérique

        #region Bruit num

        /// <summary>
        /// Ajoute du bruit coloré à une <see cref="Photoshop3000.MyImage"/>
        /// </summary>
        /// <param name="quantité">Montant à ajouter ou à enlever à chaque pixel</param>
        public void AddNoise(int quantité)
        {
            quantité = Math.Abs(quantité);

            byte[] data = this.MyImage.ToBGRArray();

            bool alphaExiste = this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Argb32;

            int lengthPixel = alphaExiste ? 4 : 3;

            int indexH = 0;
            int maxW = this.MyImage.Stride - this.MyImage.GetPadding();

            while (indexH < this.MyImage.Height)
            {
                int décalage = indexH * this.MyImage.Stride;
                int indexW = 0;

                while (indexW < maxW)
                {
                    data[décalage + indexW] = (byte)Math.Min(Math.Max(data[décalage + indexW] + Rand.Next(-quantité, quantité + 1), 0), 255);  //Bleu
                    data[décalage + indexW + 1] = (byte)Math.Min(Math.Max(data[décalage + indexW + 1] + Rand.Next(-quantité, quantité + 1), 0), 255);  //Vert
                    data[décalage + indexW + 2] = (byte)Math.Min(Math.Max(data[décalage + indexW + 2] + Rand.Next(-quantité, quantité + 1), 0), 255);  //Rouge

                    indexW += lengthPixel;
                }
                indexH++;
            }
        }

        /// <summary>
        /// Ajoute du bruit de type "sel et poivre" à une <see cref="Photoshop3000.MyImage"/>
        /// </summary>
        /// <param name="pourcentage">pourcentage de pixels rendus blancs ou noirs</param>
        public void AddPepperNoise(int pourcentage)
        {
            InternalAddNoise(false, pourcentage);
        }

        /// <summary>
        /// Ajoute du bruit de Grenaille à une <see cref="Photoshop3000.MyImage"/>
        /// </summary>
        public void AddShotNoise(int pourcentage)
        {
            InternalAddNoise(true, pourcentage);
        }

        /// <summary>
        /// Note : On ne teste pas si le pixel a déjà été changé pour accélérer le processus
        /// </summary>
        /// <param name="blackOnly"></param>
        /// <param name="pourcentage"></param>
        private void InternalAddNoise(bool blackOnly, int pourcentage)
        {
            pourcentage = Math.Min(Math.Max(pourcentage, 0), 100);

            int totalPixelAChanger = this.MyImage.Height * this.MyImage.Width * pourcentage / 100;
            for (int i = 0; i < totalPixelAChanger; i++)
            {
                int Y = Rand.Next(0, this.MyImage.Height);
                int X = Rand.Next(0, this.MyImage.Width);

                this.MyImage[Y, X] = (Pixel)(blackOnly || Rand.Next(0, 2) == 1 ? Couleurs.Noir : Couleurs.Blanc);
            }
        }

        #endregion


        //Stéganographie

        #region Stégano

        /// <summary>
        /// Cache un <see cref="Photoshop3000.MyImage"/> dans le <see cref="Photoshop3000.MyImage"/> de cette instance à partir d'un nombre de bits à cacher dans les bits les moins importants de l'image visible.<para/>
        /// L'image à cacher l'est au centre de l'image visible, si elle est plus grande que l'image visible alors ses bords sont rognés pour rentrer dans l'image visible en largeur ou en hauteur<para/>
        /// </summary>
        /// <param name="imageToHide">Image à cacher</param>
        /// <param name="nbBitsToHide">Nombre de bits à cacher</param>
        public void CacherImage(MyImage imageToHide, int nbBitsToHide = 4)
        {
            ReplaceImage(Stéganographie.CacherImage(this.MyImage, imageToHide, nbBitsToHide));
        }

        /// <summary>
        /// Trouve une image cachée dans une image
        /// </summary>
        /// <param name="nbBitsHidden">Nombre de bits de décallage</param>
        /// <returns></returns>
        public void GetImageCachée(int nbBitsHidden = 4)
        {
            ReplaceImage(Stéganographie.GetImageCachée(this.MyImage, nbBitsHidden));
        }

        /// <summary>
        /// Cache du texte au format 8 bits (256 premiers chars du format UTF-8) dans les bits les moins importants d'une copie d'une image et renvoie le résultat. <para/>
        /// Les pixels sont choisis pseudo-aléatoirement à partir d'un mot de passe. Les charactères ne faisant pas parti du format 8 bits sont ignorés. <para/>
        /// Ne pas utiliser la même image pour cacher 2 textes car l'un pourrait empiéter sur l'autre et corrompre certaines parties du texte. <para/>
        /// La longueur du texte par rapport à la taille de l'image n'influe pas sur le temps de calcul
        /// </summary>
        /// <param name="txt">Le texte à cacher au format 8 bits. Si la longueur du texte sera inconnue lors du décodage, le terminer par \u0003 </param>
        /// <param name="mdp">Mot de passe pour retrouver le texte par la suite. Pas de format particulier à respecter</param>
        /// <param name="nbreBitsCachés">Nombre de bits sur lesquels cacher le texte sur une couleur (rgb) d'un pixel. 1, 2, 4 ou 8</param>
        /// <returns></returns>
        public void CacherTexte(string txt, string mdp, int nbreBitsCachés = 2)
        {
            ReplaceImage(Stéganographie.CacherTexte(this.MyImage, txt, mdp, nbreBitsCachés));
        }

        /// <summary>
        /// Récupère un texte caché dans un <see cref="Photoshop3000.MyImage"/> à partir d'un mot de passe et de la longueur du texte à trouver (pas obligé mais le texte doit alors finir par \u0003 pour signifier la fin).
        /// </summary>
        /// <param name="mdp">Mot de passe</param>
        /// <param name="tailleMessage">Taille du message à retrouver. Si inconnue, mettre à 0, la longueur de texte maximale cherchée sera alors de 10 000 char. Le texte renvoyé s'arrête alors au char 'u\0003' ou jusqu'à la taille max</param>
        /// <param name="nbreBitsCachés">Nombre de bits utilisés pour cacher le message. 1, 2, 4 ou 8</param>
        /// <returns></returns>
        public string GetTexteCaché(string mdp, int tailleMessage = 0, int nbreBitsCachés = 2)
        {
            return Stéganographie.GetTexteCaché(this.MyImage, mdp, tailleMessage, nbreBitsCachés);
        }

        #endregion


        //effet peinture

        #region Filtres peinture

        /// <summary>
        /// Méthode qui intensifie les couleurs principales d'une image, donne un peu un effet peinture
        /// </summary>
        public void EffetPeinture(bool aiguisage, bool contraste)
        {
            if (aiguisage)
                FiltreAiguisage();

            if (contraste)
                FiltreContraste();

            for (int i = 6; i > 3; i--)
                CacherImage(this.MyImage, i);
        }

        /// <summary>
        /// Applique un effet 'peinture à l'huile' à l'image
        /// </summary>
        /// <param name="rayon"></param>
        /// <param name="intensité"></param>
        public void EffetPeintureHuile(int rayon, float intensité)
        {
            // Implémenté en c# depuis le programme en c++ : https://www.codeproject.com/Articles/471994/OilPaintEffect
            // et optimisé pour considérer les bords efficacement

            if (rayon >= this.MyImage.Height || rayon >= this.MyImage.Width)
                return;

            byte[] data = this.MyImage.ToBGRArray();

            byte[] originalData = (byte[])data.Clone();

            int lengthPixel = this.MyImage.PixelFormat.GetPixelLength();

            int maxW = this.MyImage.Stride - this.MyImage.GetPadding();
            int maxH = this.MyImage.Height;

            int[] intensités;
            int[] totalR;
            int[] totalG;
            int[] totalB;

            for (int indexH = 0; indexH < maxH; ++indexH)
            {
                int décalage = indexH * this.MyImage.Stride;

                for (int indexW = 0; indexW < maxW; indexW += lengthPixel)
                {
                    intensités = new int[256];
                    totalR = new int[256];
                    totalG = new int[256];
                    totalB = new int[256];

                    int startY = (indexH - rayon) * this.MyImage.Stride;

                    int startX = indexW - rayon * lengthPixel;
                    int startXAbs = Math.Abs(startX);

                    //On cherche l'intensité rgb de tous les pixels dans un carré autour du pixel, le mieux serait un cercle
                    int bordsIgnorésY = indexH;
                    for (int nY_O = -rayon; nY_O <= rayon; ++nY_O)
                    {
                        int bordsIgnorésX = startX;
                        int bordsConsidérésIndex = Math.Abs(startY) + startXAbs;

                        for (int nX_O = -rayon; nX_O <= rayon; ++nX_O)
                        {
                            int B = originalData[bordsConsidérésIndex];
                            int G = originalData[bordsConsidérésIndex + 1];
                            int R = originalData[bordsConsidérésIndex + 2];

                            bordsIgnorésX += lengthPixel;

                            bordsConsidérésIndex += bordsIgnorésX < 0 || bordsIgnorésX >= maxW ? -lengthPixel : lengthPixel;

                            //On cherche le niveau d'intensité des couleurs rgb et on y applique le niveau d'intensité désiré.
                            int nCurIntensity = Math.Min((int)((B + G + R) / 3F * intensité / 255F), 255);

                            intensités[nCurIntensity]++;

                            totalR[nCurIntensity] += R;
                            totalG[nCurIntensity] += G;
                            totalB[nCurIntensity] += B;
                        }

                        startY += ++bordsIgnorésY > maxH ? -this.MyImage.Stride : this.MyImage.Stride; //bords bas, on cherche vers l'intérieur
                    }

                    int valMax = 0;
                    int indexValMax = 0;

                    for (int nI = 0; nI < intensités.Length; ++nI)
                    {
                        if (intensités[nI] > valMax)
                        {
                            valMax = intensités[nI];
                            indexValMax = nI;
                        }
                    }
                    if (valMax > 0)
                    {
                        data[décalage + indexW] = (byte)(totalB[indexValMax] / valMax);
                        data[décalage + indexW + 1] = (byte)(totalG[indexValMax] / valMax);
                        data[décalage + indexW + 2] = (byte)(totalR[indexValMax] / valMax);
                    }

                }
            }
        }

        /// <summary>
        /// Applique un effet 'peinture à l'huile' à l'image
        /// </summary>
        /// <param name="rayon"></param>
        /// <param name="intensité"></param>
        public void EffetPeintureHuileContraste(int rayon, float intensité)
        {
            EffetPeintureHuile(rayon, intensité);

            FiltreContraste();
        }


        #endregion


        //Copie d'une image dans une image

        #region Dessin image

        /// <summary>
        /// Copie un <see cref="Photoshop3000.MyImage"/> au milieu du <see cref="Photoshop3000.MyImage"/> de ce <see cref="MyGraphics"/>.
        /// </summary>
        /// <param name="imagePremierPlan">Image</param>
        /// <param name="opacité">Opacité de l'image à copier, -1 pour utiliser le canal alpha de l'image à copier</param>
        public void DrawImageCenter(MyImage imagePremierPlan, int opacité = -1)
        {
            int diffX = this.MyImage.Width - imagePremierPlan.Width;
            int diffY = this.MyImage.Height - imagePremierPlan.Height;

            DrawImage(imagePremierPlan, new Point(diffY / 2, diffX / 2), opacité);
        }

        /// <summary>
        /// Copie un <see cref="Photoshop3000.MyImage"/> sur le <see cref="Photoshop3000.MyImage"/> de ce <see cref="MyGraphics"/> à partir d'un <see cref="Point"/> et d'une opacité spécifiés.
        /// Le paramètre alpha de l'image est ignoré, mettre l'opacité à -1 pour l'utiliser.
        /// </summary>
        /// <param name="imagePremierPlan">Image à copier à partir du point spécifié</param>
        /// <param name="opacité">Opacité de l'image à copier, -1 pour utiliser le canal alpha de l'image à copier</param>
        /// <param name="topLeft">Point à partir duquel l'image est copiée</param>
        public void DrawImage(MyImage imagePremierPlan, Point topLeft, int opacité = -1)
        {
            this.Clip = new Rectangle((int)topLeft.X, (int)topLeft.Y, imagePremierPlan.Width, imagePremierPlan.Height);

            DrawImage(imagePremierPlan, opacité);
        }

        /// <summary>
        /// Copie un <see cref="Photoshop3000.MyImage"/> sur le <see cref="Photoshop3000.MyImage"/> de ce <see cref="MyGraphics"/> dans le <see cref="Rectangle"/> <see cref="Clip"/> indiqué avec une opacité précisée.
        /// </summary>
        /// <param name="imagePremierPlan">Image à copier à partir du point spécifié</param>
        /// <param name="opacité">Opacité de l'image à copier 0-100, -1 pour utiliser le canal alpha de l'image à copier</param>
        public void DrawImage(MyImage imagePremierPlan, int opacité)
        {
            if (opacité == 0)
            {
                return;
            }

            MyImage imagePremierPlanCopie;

            bool cloned = false;

            this.Clip = this.Clip.GetRectangleRightWay(); //Pg si le rect ne rentre pas dans l'image actuelle, doit juste etre dans le bon sens

            //On modifie la taille de l'image selon le rectangle, peu importe si ce rect dépasse ou non de l'image :
            //l'exception est gérée dans la méthode InternalDrawImage.
            if (this.Clip.Width != imagePremierPlan.Width || this.Clip.Height != imagePremierPlan.Height)
            {
                imagePremierPlanCopie = new MyImage(imagePremierPlan, this.Clip.Height, this.Clip.Width);
                cloned = true;
            }
            else
            {
                imagePremierPlanCopie = imagePremierPlan;
            }

            if (opacité < 0 || opacité == 100) //on utilise le canal alpha de l'image
            {
                InternalDrawImage(imagePremierPlanCopie, this.Clip.Y, this.Clip.X);
            }
            else //on modifie le canal alpha de l'image pour pouvoir copier l'image comme attendu
            {
                Formats.PixelFormat pfDest = opacité == 100 ? Formats.PixelFormat.BMP_Rgb24 : Formats.PixelFormat.BMP_Argb32;

                if (cloned)
                {
                    imagePremierPlanCopie.ConvertPixelFormat(pfDest);

                    new MyGraphics(imagePremierPlanCopie).ModifyComponentValue(3, Math.Min(opacité * 255 / 100, 255));

                    InternalDrawImage(imagePremierPlanCopie, this.Clip.Y, this.Clip.X);
                }
                else
                {
                    MyImage clone = imagePremierPlanCopie.Clone();

                    clone.ConvertPixelFormat(pfDest);
                    new MyGraphics(clone).ModifyComponentValue(3, Math.Min(opacité * 255 / 100, 255));

                    InternalDrawImage(clone, this.Clip.Y, this.Clip.X);
                }
            }
        }


        /// <summary>
        /// Ajoute les données de l'image sur l'image actuelle en prenant en compte la transparence (s'il y a) de l'image à copier. 
        /// Peut prendre en charge les dépassements des bords de l'image à dessiner (s'il y a) sur l'image actuelle.
        /// </summary>
        /// <param name="drawImage">Image à dessiner</param>
        /// <param name="origineY">Y</param>
        /// <param name="origineX">X</param>
        private unsafe void InternalDrawImage(MyImage drawImage, int origineY, int origineX)
        {
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

            int maxX = Math.Min(origineX + drawImage.Width - offSetX, this.MyImage.Width); //limites hautes non comprises
            int maxY = Math.Min(origineY + drawImage.Height - offSetY, this.MyImage.Height);

            int destLengthPixel = this.MyImage.PixelFormat.GetPixelLength();
            int drawLengthPixel = drawImage.PixelFormat.GetPixelLength();

            int lengthDrawDataSkipStart = offSetX * drawLengthPixel;

            int lengthDrawDataSkipEnd = origineX + drawImage.Width - offSetX - this.MyImage.Width;
            if (lengthDrawDataSkipEnd < 0) //Que le padding à skip car on ne dépasse pas des bords à droite de l'image
            {
                lengthDrawDataSkipEnd = drawImage.GetPadding();
            }
            else //On dépasse des bords à droite, on calcule combien de byte il faut skip
            {
                lengthDrawDataSkipEnd = lengthDrawDataSkipEnd * drawLengthPixel + drawImage.GetPadding();
            }


            int totalDrawDataSkip = lengthDrawDataSkipStart + lengthDrawDataSkipEnd; //Fera office de stride pour drawIm

            int drawDataIndex = lengthDrawDataSkipStart + offSetY * drawImage.Stride; //index du 1er pixel à copier sur l'im source
            int destDataIndex = this.MyImage.Stride * origineY + origineX * destLengthPixel;

            int totalWidthDraw = maxX - origineX;
            int totalHeightDraw = maxY - origineY;

            fixed (byte* bufferDest = this.MyImage.ToBGRArray())
            fixed (byte* bufferDraw = drawImage.ToBGRArray())
            {
                byte* destBuffer = bufferDest + destDataIndex;
                byte* drawBuffer = bufferDraw + drawDataIndex;

                //En soit seul le format de l'image à copier compte, on distingue
                //quand même 3 cas (au lieu de 2) pour des raisons de performances.

                if (drawImage.PixelFormat == Formats.PixelFormat.BMP_Rgb24)
                {
                    if (this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Rgb24)
                    {
                        //Même format, on peut copier directement chaque ligne de donnée de l'image à copier sur l'image de dest
                        //Un peu plus rapide que gdi+

                        CopyLines(destBuffer, drawBuffer, (uint)(totalWidthDraw * drawLengthPixel), 
                            totalHeightDraw, this.MyImage.Stride, drawImage.Stride);
                    }
                    else if (this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Argb32)
                    {
                        //Quand on copie sur du 32bpp avec une image 24bpp, on doit manuellement changer chaque pixel tout en
                        //gardant la composante alpha de l'image de dest (celle sur laquelle on copie). Ici on prend en mémoire
                        //la composante alpha d'un pixel puis on colle directement 4 octets de l'image à copier (4 sur 3 donc)
                        //Enfin on modifie le dernier composant non valide avec la composante alpha pré-enregistrée. 
                        //Un peu plus lent que gdi+

                        int* destBufferInt = (int*)destBuffer;

                        int incrDest = this.MyImage.Width - totalWidthDraw;

                        int* lastLine = destBufferInt + (totalHeightDraw - 1) * this.MyImage.Width + 1; //début derniere ligne + 1

                        while (destBufferInt < lastLine)
                        {
                            int* endLine = destBufferInt + totalWidthDraw; //Fin de ligne actuelle

                            while (destBufferInt < endLine)
                            {
                                byte alpha = *((byte*)destBufferInt + 3); //dernier octet

                                *destBufferInt = *(int*)drawBuffer; //copie de 4 octet dont le dernier appartient au prochain pixel (ou padding)

                                *((byte*)destBufferInt + 3) = alpha; //on remplace ce dernier faux octet par le vrai alpha

                                drawBuffer += 3;
                                destBufferInt += 1;
                            }

                            drawBuffer += totalDrawDataSkip;

                            destBufferInt += incrDest;
                        }
                    }
                }
                else if (drawImage.PixelFormat == Formats.PixelFormat.BMP_Argb32) // 2x plus lent
                {
                    //Dernière méthode : on copie une image 32bpp sur une image 24 ou 32bpp. Le format de l'image source n'importe 
                    //pas puisqu'il faut changer individuellement chaque composante rgb du pixel, la composante alpha restant 
                    //inchangée si jamais elle existait (dépend de sourceAlpha en fait). On récupère donc le canal alpha d'un pixel 
                    //de l'image à copier et on fait une moyenne des pixels entre l'image dest et l'image à dessiner.
                    //Difficile d'optimiser plus qu'actuellement. 2* plus lent que gdi+.

                    bool useDrawAlpha = this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Argb32 && !this.SourceAlpha;
                    int incrDestBufferNextPixel = this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Rgb24 || useDrawAlpha ? 1 : 2;

                    const int décalageFloat = 16; //simul un float avec un int, plus efficace
                    int inverse = (255 << décalageFloat) / 256; //On évite d'avoir un float

                    for (int indexY = 0; indexY < totalHeightDraw; ++indexY)
                    {
                        byte* destBufferLineIndex = destBuffer; //buffer valable juste pour la ligne de donnée en cours

                        for (int indexW = 0; indexW < totalWidthDraw; ++indexW)
                        {
                            int alpha = (*(drawBuffer + 3) << décalageFloat) / 256;   //On pourrait éviter de calculer ce résultat à chaque fois
                            //float alphaA = *(drawBuffer + 3) / 256f;
                            //int alpha = *(int*)&alphaA;
                            int inverseAlpha = inverse - alpha;   //en spécifiant une nvlle fonction pour un alpha en particulier

                            *destBufferLineIndex = (byte)((*destBufferLineIndex * inverseAlpha + *drawBuffer * alpha) >> décalageFloat);
                            *++destBufferLineIndex = (byte)((*destBufferLineIndex * inverseAlpha + drawBuffer[1] * alpha) >> décalageFloat);
                            *++destBufferLineIndex = (byte)((*destBufferLineIndex * inverseAlpha + drawBuffer[2] * alpha) >> décalageFloat);

                            if (useDrawAlpha) //Faire une autre boucle for pour éviter d'avoir à vérifier ce bool à chaque passage, est-ce que le compilateur peut l'opti auto ? => Non c'est forcément du runtime
                                *++destBufferLineIndex = *(drawBuffer + 3);

                            drawBuffer += drawLengthPixel;
                            destBufferLineIndex += incrDestBufferNextPixel;
                        }

                        drawBuffer += totalDrawDataSkip;
                        destBuffer += this.MyImage.Stride;
                    }
                }
            }
        }

        #endregion


        //Méthodes de dessin

        #region Dessin géométrie

        /// <summary>
        /// Dessine une ligne entre 2 points par la couleur <see cref="PixelRemplissage"/>
        /// </summary>
        public void DrawLineBresenham(Point départ, Point arrivée)
        {
            DrawLineBresenham(départ, arrivée, this.PixelRemplissage);

        }

        /// <summary>
        /// Dessine une ligne entre 2 points avec la couleur spécifiée. Basé sur l'algorithme de Bresenham.
        /// </summary>
        public void DrawLineBresenham(Point départ, Point arrivée, Pixel color)
        {
            bool dép = IsPointInImage(départ);
            bool arriv = IsPointInImage(arrivée);

            if (!dép && !arriv) //L'intersection avec l'image peut quand même exister mais ça prendrait trop de temps de re-checker.
                return;
            if (!dép)
            {
                départ = PointCollisionImage(départ, arrivée);
            }
            else if (!arriv)
            {
                arrivée = PointCollisionImage(arrivée, départ);
            }

            //On change les coordonnées en int
            int x0 = (int)Math.Round(départ.X);
            int y0 = (int)Math.Round(départ.Y);
            int x1 = (int)Math.Round(arrivée.X);
            int y1 = (int)Math.Round(arrivée.Y);


            if (Math.Abs(y1 - y0) < Math.Abs(x1 - x0))
            {
                if (x0 > x1)
                {
                    LigneBasBresenham(x1, y1, x0, y0, color);
                }
                else
                {
                    LigneBasBresenham(x0, y0, x1, y1, color);
                }
            }
            else
            {
                if (y0 > y1)
                {
                    LigneHautBresenham(x1, y1, x0, y0, color);
                }
                else
                {
                    LigneHautBresenham(x0, y0, x1, y1, color);
                }
            }
        }


        private void LigneBasBresenham(int x0, int y0, int x1, int y1, Pixel color)
        {
            int dx = x1 - x0;
            int dy = y1 - y0;
            int yi = 1;

            if (dy < 0)
            {
                yi = -1;
                dy = -dy;
            }

            int d = 2 * dy - dx;
            //int y = y0;

            //for (int x = x0; x <= x1; x++)
            //{
            //    //Pas opti mais aucune possibilité de crash. Le mieux serait de déterminer le point en bordure d'image 
            //    //avant de dessiner au lieu de tester si le point est dans l'image à chaque itération  -> fixé -> en fait non
            //    //if (TestPoint(new Point(y, x)))
            //    //{
            //    this.MyImage[y, x] = color;
            //    //}
            //    if (d > 0)
            //    {
            //        y += yi;
            //        d += -2 * dx;
            //    }
            //    d += 2 * dy;
            //}

            byte[] data = this.MyImage.ToBGRArray();

            int lengthPixel = this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Rgb24 ? 3 : 4;

            int indexData = y0 * this.MyImage.Stride + x0 * lengthPixel;

            byte b = color.B;
            byte g = color.G;
            byte r = color.R;
            byte a = color.A;

            for (int x = x0; x <= x1; x++)
            {
                data[indexData] = b;
                data[indexData + 1] = g;
                data[indexData + 2] = r;
                if (lengthPixel == 4)
                    data[indexData + 3] = a;

                if (d > 0)
                {
                    indexData += yi * this.MyImage.Stride;
                    d += -2 * dx;
                }
                d += 2 * dy;

                indexData += lengthPixel;
            }

        }
        private void LigneHautBresenham(int x0, int y0, int x1, int y1, Pixel color)
        {
            int dx = x1 - x0;
            int dy = y1 - y0;
            int xi = 1;

            if (dx < 0)
            {
                xi = -1;
                dx = -dx;
            }

            int d = 2 * dx - dy;
            int x = x0;

            //for (int y = y0; y <= y1; y++)
            //{
            //    //Pas opti mais aucune possibilité de crash. Le mieux serait de déterminer le point en bordure d'image 
            //    //avant de dessiner au lieu de tester si le point est dans l'image à chaque itération --> c'est fait
            //    //if (TestPoint(new Point(y, x)))
            //    //{
            //    this.MyImage[y, x] = color;
            //    //}

            //    if (d > 0)
            //    {
            //        x += xi;
            //        d += -2 * dy;
            //    }
            //    d += 2 * dx;
            //}

            byte[] data = this.MyImage.ToBGRArray();

            int lengthPixel = this.MyImage.PixelFormat == Formats.PixelFormat.BMP_Rgb24 ? 3 : 4;

            int indexData = y0 * this.MyImage.Stride + x0 * lengthPixel;

            byte b = color.B;
            byte g = color.G;
            byte r = color.R;

            if (lengthPixel == 3)
            {
                for (int y = y0; y <= y1; y++)
                {
                    data[indexData] = b;
                    data[indexData + 1] = g;
                    data[indexData + 2] = r;

                    if (d > 0)
                    {
                        indexData += xi * lengthPixel;
                        d += -2 * dy;
                    }
                    d += 2 * dx;

                    indexData += this.MyImage.Stride;

                }
            }
            else
            {
                byte a = color.A;
                for (int y = y0; y <= y1; y++)
                {
                    data[indexData] = b;
                    data[indexData + 1] = g;
                    data[indexData + 2] = r;
                    data[indexData + 3] = a;

                    if (d > 0)
                    {
                        indexData += xi * lengthPixel;
                        d += -2 * dy;
                    }
                    d += 2 * dx;

                    indexData += this.MyImage.Stride;

                }
            }
        }



        /// <summary>
        /// Dessine une ligne entre 2 points par la couleur <see cref="PixelRemplissage"/> avec la méthode de Xiaolin Wu (anticrénelage)
        /// </summary>
        public void DrawLineWu(Point départ, Point arrivée)
        {
            DrawLineWu(départ, arrivée, this.PixelRemplissage);
        }

        /// <summary>
        /// Dessine une ligne entre 2 points par la couleur spécifiée avec la méthode de Xiaolin Wu (anticrénelage)
        /// </summary>
        public void DrawLineWu(Point départ, Point arrivée, Pixel color)
        {
            bool dép = IsPointInImage(départ);
            bool arriv = IsPointInImage(arrivée);

            if (!dép && !arriv)
                return;
            if (!dép)
            {
                départ = PointCollisionImage(départ, arrivée);
            }
            if (!arriv)
            {
                arrivée = PointCollisionImage(arrivée, départ);
            }

            int x0 = (int)départ.X;
            int x1 = (int)arrivée.X;
            int y0 = (int)départ.Y;
            int y1 = (int)arrivée.Y;

            if (Math.Abs(y1 - y0) < Math.Abs(x1 - x0))
            {
                if (x0 > x1)
                {
                    LigneWu(x1, y1, x0, y0, color);
                }
                else
                {
                    LigneWu(x0, y0, x1, y1, color);
                }
            }
            else
            {
                if (y0 > y1)
                {
                    LigneWu(x1, y1, x0, y0, color);
                }
                else
                {
                    LigneWu(x0, y0, x1, y1, color);
                }
            }
        }


        private void LigneWu(int x0, int y0, int x1, int y1, Pixel color)
        {
            InternalDrawPointWu(y0, x0, 1, color);
            InternalDrawPointWu(y1, x1, 1, color);

            float dx = x1 - x0;
            float dy = y1 - y0;
            float pente = dy / dx;
            if (Math.Abs(pente) <= 1f)
            {
                float y = y0 + pente;
                for (int x = x0 + 1; x < x1; x++)
                {
                    InternalDrawPointWu((int)y, x, 1 - (y - (int)y), color);
                    if ((int)y + 1 < this.MyImage.Height)
                    {
                        InternalDrawPointWu((int)y + 1, x, y - (int)y, color);
                    }
                    y += pente;
                }
            }
            else
            {
                pente = 1f / pente; //On inverse la pente
                float x = x0 + pente;
                for (int y = y0 + 1; y < y1; y++)
                {
                    InternalDrawPointWu(y, (int)x, 1 - (x - (int)x), color);
                    if (x + 1 < this.MyImage.Width)
                    {
                        InternalDrawPointWu(y, (int)x + 1, x - (int)x, color);
                    }
                    x += pente;
                }
            }
        }

        private void InternalDrawPointWu(int y, int x, float intensité, Pixel color)
        {
            this.MyImage[y, x] = Pixel.AddTransparence(this.MyImage[y, x], color, intensité);
        }


        /// <summary>
        /// Ne fonctionne pas.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="color"></param>
        public void DrawBézier(Point p1, Point p2, Point p3, Point p4, Pixel color)
        {
            const int N_SEG = 20;

            Point[] pts = new Point[N_SEG + 1];

            for (int i = 0; i <= N_SEG; ++i)
            {
                double t = (double)i / N_SEG;

                double a = Math.Pow((1.0 - t), 3.0);
                double b = 3.0 * t * Math.Pow((1.0 - t), 2.0);
                double c = 3.0 * Math.Pow(t, 2.0) * (1.0 - t);
                double d = Math.Pow(t, 3.0);

                double x = a * p1.X + b * p2.X + c * p3.X + d * p4.X;
                double y = a * p1.Y + b * p2.Y + c * p3.Y + d * p4.Y;

                pts[i] = new Point(y, x);
            }

            int debut = (int)(p2.X - p1.X);
            int fin = (int)(p3.X - p2.X) + debut;

            for (int i = 0; i < N_SEG; ++i)
            {
                this.DrawLineBresenham(pts[i], pts[i + 1], color);
            }
        }



        /// <summary>
        /// Dessine un cercle avec le point central et le rayon spécifiés de la couleur spécifiée. Basé sur l'algorithme de Bresenham.
        /// </summary>
        public void DrawCircleBresenham(Point centre, int rayon, Pixel color)
        {
            int d = (5 - rayon * 4) / 4;
            int y = 0;
            int x = rayon;

            do
            {
                if (centre.Y + y >= 0 && centre.Y + y <= this.MyImage.Height - 1 && centre.X + x >= 0 && centre.X + x <= this.MyImage.Width - 1) this.MyImage[(int)Math.Round(centre.Y + y), (int)Math.Round(centre.X + x)] = color;
                if (centre.Y + y >= 0 && centre.Y + y <= this.MyImage.Height - 1 && centre.X - x >= 0 && centre.X - x <= this.MyImage.Width - 1) this.MyImage[(int)Math.Round(centre.Y + y), (int)Math.Round(centre.X - x)] = color;
                if (centre.Y - y >= 0 && centre.Y - y <= this.MyImage.Height - 1 && centre.X + x >= 0 && centre.X + x <= this.MyImage.Width - 1) this.MyImage[(int)Math.Round(centre.Y - y), (int)Math.Round(centre.X + x)] = color;
                if (centre.Y - y >= 0 && centre.Y - y <= this.MyImage.Height - 1 && centre.X - x >= 0 && centre.X - x <= this.MyImage.Width - 1) this.MyImage[(int)Math.Round(centre.Y - y), (int)Math.Round(centre.X - x)] = color;
                if (centre.Y + x >= 0 && centre.Y + x <= this.MyImage.Height - 1 && centre.X + y >= 0 && centre.X + y <= this.MyImage.Width - 1) this.MyImage[(int)Math.Round(centre.Y + x), (int)Math.Round(centre.X + y)] = color;
                if (centre.Y + x >= 0 && centre.Y + x <= this.MyImage.Height - 1 && centre.X - y >= 0 && centre.X - y <= this.MyImage.Width - 1) this.MyImage[(int)Math.Round(centre.Y + x), (int)Math.Round(centre.X - y)] = color;
                if (centre.Y - x >= 0 && centre.Y - x <= this.MyImage.Height - 1 && centre.X + y >= 0 && centre.X + y <= this.MyImage.Width - 1) this.MyImage[(int)Math.Round(centre.Y - x), (int)Math.Round(centre.X + y)] = color;
                if (centre.Y - x >= 0 && centre.Y - x <= this.MyImage.Height - 1 && centre.X - y >= 0 && centre.X - y <= this.MyImage.Width - 1) this.MyImage[(int)Math.Round(centre.Y - x), (int)Math.Round(centre.X - y)] = color;

                if (d < 0)
                {
                    d += 2 * y + 1;
                }
                else
                {
                    d += 2 * (y - x) + 1;
                    x--;
                }
                y++;

            } while (y <= x);
        }



        /// <summary>
        /// Remplit un cercle formé par son centre et son rayon par la couleur indiquée. Basé sur l'algorithme de Bresenham
        /// </summary>
        /// <param name="centre">Centre du cercle</param>
        /// <param name="rayon">Rayon du cercle</param>
        /// <param name="color">Couleur de remplissage</param>
        public void FillCircle(Point centre, int rayon, Pixel color)
        {
            double x = 0;
            double y = rayon;
            double balance = -rayon;

            while (x <= y)
            {
                double p0 = centre.X - x;
                double p1 = centre.X - y;

                double w0 = x + x;
                double w1 = y + y;

                DrawLineBresenham(new Point(centre.Y + y, p0), new Point(centre.Y + y, p0 + w0), color);
                DrawLineBresenham(new Point(centre.Y - y, p0), new Point(centre.Y - y, p0 + w0), color);

                DrawLineBresenham(new Point(centre.Y + x, p1), new Point(centre.Y + x, p1 + w1), color);
                DrawLineBresenham(new Point(centre.Y - x, p1), new Point(centre.Y - x, p1 + w1), color);

                if ((balance += x++ + x) >= 0)
                {
                    balance -= --y + y;
                }
            }

        }




        /// <summary>
        /// Dessine un carré de la taille indiquée avec la couleur spécifiée à l'endroit donné
        /// </summary>
        /// <param name="milieu">Coin en haut à gauche du carré à partir duquel le carré est dessiné</param>
        /// <param name="taille">Taille du carré</param>
        /// <param name="color">Couleur de remplissage</param>
        public void DrawCarréMid(Point milieu, int taille, Pixel color)
        {
            DrawRectangle(new Point(milieu.Y - taille / 2, milieu.X - taille / 2), new Point(milieu.Y + taille / 2, milieu.X + taille / 2), color);
        }

        /// <summary>
        /// Remplit un carré de la taille indiquée avec la couleur spécifiée à partir du point du milieu du carré
        /// </summary>
        /// <param name="milieu">Coin au milieu du carré à partir duquel le carré est remplit</param>
        /// <param name="taille">Taille du carré</param>
        /// <param name="color">Couleur de remplissage</param>
        public void FillCarréMid(Point milieu, int taille, Pixel color)
        {
            FillRectangle(new Point(milieu.Y - taille / 2, milieu.X - taille / 2), new Point(milieu.Y + taille / 2, milieu.X + taille / 2), color);
        }

        /// <summary>
        /// Remplit un carré de la taille indiquée avec la couleur spécifiée à partir du point du milieu du carré
        /// </summary>
        /// <param name="topLeft">Coin en haut à gauche du carré à partir duquel le carré est remplit</param>
        /// <param name="taille">Taille du carré</param>
        /// <param name="color">Couleur de remplissage</param>
        public void FillCarré(Point topLeft, int taille, Pixel color)
        {
            FillRectangle(topLeft, new Point(topLeft.Y + taille, topLeft.X + taille), color);
        }



        /// <summary>
        /// Dessine les contours d'un rectangle dont les coordonnées sont spécifiées par 2 coins d'en haut à gauche et d'en bas à droite, par la couleur spécifiée
        /// </summary>
        /// <param name="hautGauche">Coin en haut à gauche à partir duquel le rectangle est dessiné</param>
        /// <param name="basDroit">Coin en bas à droite. Limite du rectangle</param>
        /// <param name="color">Couleur de remplissage</param>
        public void DrawRectangle(Point hautGauche, Point basDroit, Pixel color)
        {
            Point hautDroit = new Point(hautGauche.Y, basDroit.X);
            Point basGauche = new Point(basDroit.Y, hautGauche.X);

            DrawLineBresenham(hautGauche, hautDroit, color);
            DrawLineBresenham(hautDroit, basDroit, color);
            DrawLineBresenham(basDroit, basGauche, color);
            DrawLineBresenham(basGauche, hautGauche, color);
        }

        /// <summary>
        /// Remplit un rectangle avec la couleur spécifiée à partir des coins à gauche en haut et à droite en bas
        /// </summary>
        /// <param name="hautGauche">Coin en haut à gauche à partir duquel le rectangle est remplit</param>
        /// <param name="basDroit">Coin en bas à droite. Limite du rectangle</param>
        /// <param name="color">Couleur de remplissage</param>
        public void FillRectangle(Point hautGauche, Point basDroit, Pixel color)
        {
            //FillForme_4(hautGauche, basDroit, new Point(hautGauche.Y, basDroit.X), new Point(basDroit.Y, hautGauche.X), color);
            //Algo plus opti :

            int startHeight = Math.Min(this.MyImage.Height, Math.Max(0, Math.Min((int)hautGauche.Y, (int)basDroit.Y)));
            int endHeight = Math.Min(this.MyImage.Height, Math.Max(0, Math.Max((int)hautGauche.Y, (int)basDroit.Y)));

            int startWidth = Math.Min(this.MyImage.Width, Math.Max(0, Math.Min((int)hautGauche.X, (int)basDroit.X)));
            int endWidth = Math.Min(this.MyImage.Width, Math.Max(0, Math.Max((int)hautGauche.X, (int)basDroit.X)));

            int lengthPixel = this.MyImage.PixelFormat.GetPixelLength();

            unsafe
            {
                fixed (byte* buffFixed = this.MyImage.ToBGRArray())
                {
                    if (lengthPixel == 4)
                    {
                        int endNewDataLine = endWidth - startWidth;

                        int* buff = (int*)buffFixed + this.MyImage.Width * startHeight + startWidth;

                        int argb = color.ToArgb();

                        while (startHeight < endHeight)
                        {
                            int indexW = 0;

                            while (indexW < endNewDataLine)
                            {
                                *(buff + indexW) = argb;

                                indexW += 1;
                            }

                            buff += this.MyImage.Width;

                            startHeight += 1;
                        }
                    }
                    else if (lengthPixel == 3)
                    {
                        byte b = color.B, g = color.G, r = color.R;

                        int endNewDataLine = (endWidth - startWidth) * lengthPixel;

                        int incr = this.MyImage.Stride - endNewDataLine;

                        byte* buff = buffFixed + this.MyImage.Stride * startHeight + startWidth * lengthPixel;

                        while (startHeight++ < endHeight)
                        {
                            int indexW = 0;

                            while (indexW < endNewDataLine)
                            {
                                *buff = b;
                                ++buff;
                                *buff = g;
                                ++buff;
                                *buff = r;
                                ++buff;

                                indexW += lengthPixel;
                            }

                            buff += incr;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Remplit un rectangle avec la couleur spécifiée à partir des coins à gauche en haut et à droite en bas
        /// </summary>
        public void FillRectangle(Rectangle rect, Pixel color)
        {
            FillRectangle(new Point(rect.Y, rect.X), new Point(rect.Y + rect.Height, rect.X + rect.Width), color);
        }


        /// <summary>
        /// Remplit le triangle formé par les <see cref="Point"/> indiqués par la couleur <see cref="PixelRemplissage"/>
        /// </summary>
        public void FillTriangle(Point haut, Point droite, Point gauche)
        {
            FillTriangle(haut, droite, gauche, this.PixelRemplissage);
        }

        /// <summary>
        /// Remplit par la couleur spécifiée le triangle formé par les <see cref="Point"/> indiqués
        /// </summary>
        public void FillTriangle(Point haut, Point droite, Point gauche, Pixel color)
        {
            //Trie par hauteur puis par largeur les points

            Point[] points = new Point[] { haut, droite, gauche }.OrderBy(x => x.Y).ThenBy(x => x.X).ToArray();


            //On appelle des méthodes en fonction des points

            if (points[2].Y == points[1].Y)  //Plat en bas
            {
                FillTrianglePlatBottom(points[0], points[2], points[1], color);
            }
            else if (points[0].Y == points[1].Y)  //Plat en haut
            {
                FillTrianglePlatTop(points[2], points[1], points[0], color);
            }
            else  //Autres formes, on divise le triangle en 2 pour travailler avec 2 triangles plats
            {
                Point intersection = new Point
                {
                    Y = points[1].Y,
                    X = points[0].X + ((points[1].Y - points[0].Y) / (points[2].Y - points[0].Y) * (points[2].X - points[0].X))
                };

                FillTrianglePlatBottom(points[0], points[1], intersection, color);
                FillTrianglePlatTop(points[2], intersection, points[1], color);

            }

        }


        private void FillTrianglePlatBottom(Point haut, Point droite, Point gauche, Pixel color)
        {
            //this.DrawLineWu(haut, droite, color);
            //this.DrawLineWu(droite, gauche, color);
            //this.DrawLineWu(haut, gauche, Pixel.FromColor(Couleurs.Noir));

            float penteHaut = (float)((gauche.X - haut.X) / (gauche.Y - haut.Y));
            float penteBas = (float)((droite.X - haut.X) / (droite.Y - haut.Y));

            float YHaut = (float)haut.X;
            float YBas = (float)haut.X;

            int minGauche = (int)Math.Ceiling(gauche.Y);

            for (int scanlineY = (int)Math.Ceiling(haut.Y); scanlineY <= minGauche; scanlineY++)
            {
                DrawLineBresenham(new Point(scanlineY, YHaut), new Point(scanlineY, YBas), color);
                YHaut += penteHaut;
                YBas += penteBas;
            }

            //this.DrawLineWu(haut, droite, color);
            //this.DrawLineWu(droite, gauche, color);
            //this.DrawLineWu(haut, gauche, color);

            //Point p1 = new Point(haut.Y, haut.X - 1);
            //Point p2 = new Point(gauche.Y, gauche.X - 1);
            //if (TestPoint(p1) && TestPoint(p2))
            //{
            //    //this.DrawLineWu(p1, p2, color);
            //}

        }

        private void FillTrianglePlatTop(Point bas, Point droite, Point gauche, Pixel color)
        {
            float penteGauche = (float)((bas.X - gauche.X) / (bas.Y - gauche.Y));
            float penteDroite = (float)((bas.X - droite.X) / (bas.Y - droite.Y));

            float xGauche = (float)bas.X;
            float xDroite = (float)bas.X;

            int minGauche = (int)Math.Ceiling(gauche.Y);

            for (int scanlineY = (int)Math.Ceiling(bas.Y); scanlineY >= minGauche; scanlineY--)
            {
                DrawLineBresenham(new Point(scanlineY, xGauche), new Point(scanlineY, xDroite), color);
                xGauche -= penteGauche;
                xDroite -= penteDroite;
            }

            //this.DrawLineWu(droite, gauche, color);
            //this.DrawLineWu(bas, gauche, color);

            //Point p1 = new Point(bas.Y, bas.X - 1);
            //Point p2 = new Point(droite.Y, droite.X - 1);
            //if (TestPoint(p1) && TestPoint(p2))
            //{
            //    //this.DrawLineWu(p1, p2, color);
            //}
        }



        /// <summary>
        /// Remplit par la couleur spécifiée le quadrilatère formé par les <see cref="Point"/> indiqués. L'ordre des points n'importe pas
        /// </summary>
        public void FillForme_4(Point p1, Point p2, Point p3, Point p4, Pixel color)
        {
            //On trie les points par distance au point le plus haut puis le plus à gauche

            var points = new Point[] { p1, p2, p3, p4 }.OrderBy(x => x.Y).ThenBy(x => x.X).ToArray();


            FillTriangle(points[0], points[1], points[2], color);
            FillTriangle(points[0], points[1], points[3], color);
            FillTriangle(points[1], points[2], points[3], color);
            FillTriangle(points[0], points[2], points[3], color);
        }

        /// <summary>
        /// Remplit par la couleur spécifiée le pentagone formé par les <see cref="Point"/> indiqués. L'ordre n'importe pas
        /// </summary>
        public void FillForme_5(Point p1, Point p2, Point p3, Point p4, Point p5, Pixel color)
        {
            //On trie les points par distance au point le plus haut puis le plus à gauche

            var points = new Point[] { p1, p2, p3, p4, p5 }.OrderBy(x => x.Y).ThenBy(x => x.X).ToArray();


            FillForme_4(points[0], points[1], points[2], points[3], color);
            FillTriangle(points[2], points[3], points[4], color);
        }

        /// <summary>
        /// Remplit par la couleur spécifiée l'hexagone formé par les <see cref="Point"/> indiqués. L'ordre n'importe pas
        /// </summary>
        public void FillForme_6(Point p1, Point p2, Point p3, Point p4, Point p5, Point p6, Pixel color)
        {
            //On trie les points par distance au point le plus haut puis le plus à gauche

            var points = new Point[] { p1, p2, p3, p4, p5, p6 }.OrderBy(x => x.Y).ThenBy(x => x.X).ToArray();


            FillForme_5(points[0], points[1], points[2], points[3], points[4], color);
            FillTriangle(points[3], points[4], points[5], color);
        }



        /// <summary>
        /// Remplit un hexagone parfait à partir du point central indiqué, de la taille indiquée avec la couleur spécifiée
        /// </summary>
        /// <param name="centre">Centre de l'hexagone</param>
        /// <param name="size">Taille de l'hexagone (taille d'un coté)</param>
        /// <param name="color">Couleur de remplissage</param>
        public void FillHexagone(Point centre, int size, Pixel color)
        {
            InternalHexagone(centre, size, color, true);
        }

        /// <summary>
        /// Dessine le contour d'un hexagone parfait à partir du point central indiqué, de la taille indiquée avec la couleur spécifiée
        /// </summary>
        /// <param name="centre">Centre de l'hexagone</param>
        /// <param name="size">Taille de l'hexagone (taille d'un coté)</param>
        /// <param name="color">Couleur de remplissage</param>
        public void DrawHexagone(Point centre, int size, Pixel color)
        {
            InternalHexagone(centre, size, color, false);
        }


        private void InternalHexagone(Point centre, int size, Pixel color, bool remplissage)
        {
            Point pLeft = new Point(centre.Y, centre.X - size);
            Point pRight = new Point(centre.Y, centre.X + size);

            double ri = Math.Sqrt(3) / 2 * size;

            Point pTopLeft = new Point(centre.Y - ri, centre.X - size / 2);
            Point pTopRight = new Point(pTopLeft.Y, pTopLeft.X + size);
            Point pBotLeft = new Point(centre.Y + ri, centre.X - size / 2);
            Point pBotRight = new Point(pBotLeft.Y, pBotLeft.X + size);

            if (remplissage)
                FillForme_6(pLeft, pRight, pTopLeft, pTopRight, pBotLeft, pBotRight, color);
            else
            {
                DrawLineBresenham(pLeft, pTopLeft, color);
                DrawLineBresenham(pTopLeft, pTopRight, color);
                DrawLineBresenham(pTopRight, pRight, color);
                DrawLineBresenham(pRight, pBotRight, color);
                DrawLineBresenham(pBotRight, pBotLeft, color);
                DrawLineBresenham(pBotLeft, pLeft, color);
            }
        }



        /// <summary>
        /// Dessine le contour d'un pentagone parfait à partir d'un point central, d'une taille et d'une couleur spécifiés
        /// </summary>
        /// <param name="centre">Centre du pentagone</param>
        /// <param name="size">Taille du pentagone (taille d'un coté)</param>
        /// <param name="color">Couleur de remplissage</param>
        public void DrawPentagone(Point centre, int size, Pixel color)
        {
            InternalPentagone(centre, size, color, false);
        }

        /// <summary>
        /// Remplit un pentagone parfait à partir d'un point central, d'une taille et d'une couleur spécifiés
        /// </summary>
        /// <param name="centre">Centre du pentagone</param>
        /// <param name="size">Taille du pentagone (taille d'un coté)</param>
        /// <param name="color">Couleur de remplissage</param>
        public void FillPentagone(Point centre, int size, Pixel color)
        {
            InternalPentagone(centre, size, color, true);
        }


        private void InternalPentagone(Point centre, int size, Pixel color, bool remplissage)
        {
            double circons = 1 / Math.Sqrt(3 - ((1 + Math.Sqrt(5)) / 2));
            double apoth = 1 / Math.Tan(Math.PI / 5) / 2;
            double angle = 105 * Math.PI / 180;

            double rCirc = size * circons;
            double rInsc = size * apoth;

            Point top = new Point(centre.Y - rCirc, centre.X);
            Point botLeft = new Point(centre.Y + rInsc, centre.X - size / 2);
            Point botRight = new Point(botLeft.Y, botLeft.X + size);

            Point left = new Point(botLeft.Y - size * Math.Sin(angle), botLeft.X + size * Math.Cos(angle));
            Point right = new Point(botRight.Y - size * Math.Sin(angle), botRight.X - size * Math.Cos(angle));

            if (remplissage)
                FillForme_5(top, botLeft, botRight, left, right, color);
            else
            {
                DrawLineBresenham(top, left, color);
                DrawLineBresenham(left, botLeft, color);
                DrawLineBresenham(botLeft, botRight, color);
                DrawLineBresenham(botRight, right, color);
                DrawLineBresenham(right, top, color);
            }
        }



        /// <summary>
        /// Teste si un <see cref="Point"/> est présent dans l'image
        /// </summary>
        private bool IsPointInImage(Point p1)
        {
            if (p1.X < 0)
                return false;
            else if (p1.X > this.MyImage.Width - 1)
                return false;

            if (p1.Y < 0)
                return false;
            else if (p1.Y > this.MyImage.Height - 1)
                return false;

            return true;
        }

        /// <summary>
        /// Renvoie le <see cref="Point"/> de l'intersection d'un segment entre 2 <see cref="Point"/> dont l'un est hors de l'image et les bords de cette image.
        /// L'intersection avec l'image du segment est nécéssaire sinon le résultat sera faux.
        /// </summary>
        /// <param name="pointToChange">Point à changer</param>
        /// <param name="départ">Point de départ à partir duquel tracer une droite vers le point à changer</param>
        /// <returns></returns>
        private Point PointCollisionImage(Point pointToChange, Point départ)
        {
            double pente = (pointToChange.Y - départ.Y) / (pointToChange.X - départ.X);
            //f(x) = ax + b, f(départ.X) = pente * départ.X + b <=> b = départ.Y - pente * départ.X
            double origineY = départ.Y - pente * départ.X;

            if (pointToChange.X == départ.X && pointToChange.Y == départ.Y)
            {
                return Point.Zero;
            }

            if ((pointToChange.X > this.MyImage.Width - 1 && pointToChange.Y > this.MyImage.Height - 1) ||
                (pointToChange.X < 0 && pointToChange.Y < 0) || (pointToChange.X > this.MyImage.Width - 1 && pointToChange.Y < 0)
                || (pointToChange.X < 0 && pointToChange.Y > this.MyImage.Height - 1)) //Dans les coins
            {
                //On regarde si le futur point changé est sur l'axe des abscisses de l'image ou des ordonnées 
                //f(maxWidth) = pente * maxWidth + origineY
                if (pointToChange.Y >= 0)
                {
                    double posY = pente * (this.MyImage.Width - 1) + origineY;
                    if (posY <= this.MyImage.Height - 1)
                    {
                        pointToChange.X = this.MyImage.Width - 1;
                        pointToChange.Y = origineY + pente * (this.MyImage.Width - 1);
                    }
                    else
                    {
                        pointToChange.Y = this.MyImage.Height - 1;
                        pointToChange.X = pente == 0 ? origineY : (this.MyImage.Height - 1 - origineY) / pente;
                    }
                }
                else
                {
                    if (origineY < 0)
                    {
                        pointToChange.Y = 0;
                        pointToChange.X = pente == 0 ? 0 : -origineY / pente;
                    }
                    else
                    {
                        pointToChange.X = 0;
                        pointToChange.Y = origineY;
                    }
                }
            }
            else if (pointToChange.Y > this.MyImage.Height - 1 || pointToChange.Y < 0) //Y
            {
                if (pointToChange.Y < 0)
                {
                    pointToChange.Y = 0;
                    pointToChange.X = pente == 0 ? 0 : -origineY / pente;
                }
                else
                {
                    pointToChange.Y = this.MyImage.Height - 1;
                    pointToChange.X = pente == 0 ? -origineY : (this.MyImage.Height - origineY) / pente;
                }
            }
            else //X
            {
                if (pointToChange.X < 0)
                {
                    pointToChange.X = 0;
                    pointToChange.Y = origineY;
                }
                else
                {
                    pointToChange.X = this.MyImage.Width - 1;
                    pointToChange.Y = origineY + pente * (this.MyImage.Width - 1);
                }
            }

            return pointToChange;
        }

        #endregion

    }
}
