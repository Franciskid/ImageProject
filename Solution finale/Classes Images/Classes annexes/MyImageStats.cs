using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Photoshop3000.Annexes
{

    /// <summary>
    /// Différents types d'histogramme possible 
    /// </summary>
    public enum HistogrammeMode
    {
        /// <summary>
        /// Histogramme d'échelle de gris
        /// </summary>
        Echelle_Gris,

        /// <summary>
        /// Histogramme des 3 couleurs R-G-B
        /// </summary>
        Echelle_Couleurs,

        /// <summary>
        /// TSV, pas implémenté
        /// </summary>
        HSV
    }


    /// <summary>
    /// Fournit des méthodes pour analyser une <see cref="MyImage"/> et afficher des statistiques sous forme d'image ou de texte.
    /// </summary>
    public class MyImageStats
    {
        //Champs

        private MyGraphics graphHisto;
        private MyImage histo;
        private MyImage image;

        /// <summary>
        /// Nombre de pixels pour chaque intensité 0-255
        /// </summary>
        private int[][] pixelsParIntensité;

        /// <summary>
        /// Taille de l'histo demandée par l'utilisateur
        /// </summary>
        private readonly int realWidth, realHeight;

        /// <summary>
        /// Couleurs aux extrémités de l'histogramme
        /// </summary>
        private Pixel[,] couleursRGB;

        private readonly byte minValue, maxValue;


        public Pixel RemplissageCouleur { get; set; } = Pixel.FromColor(Couleurs.Blanc);

        public bool Remplissage { get; set; } = false;

        public HistogrammeMode HistogrammeMode { get; set; } = HistogrammeMode.Echelle_Couleurs;


        //Constructeurs

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="MyImageStats"/> avec une <see cref="MyImage"/>
        /// </summary>
        /// <param name="image">Image</param>
        public MyImageStats(MyImage image)
            : this(image, 500, 1000)
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="MyImageStats"/> avec une <see cref="MyImage"/> et les dimensions de l'histogramme à réaliser
        /// </summary>
        /// <param name="image">Image</param>
        /// <param name="height">Hauteur de l'histogramme en nombre pixel</param>
        /// <param name="width">Largeur de l'histogramme en nombre pixel</param>
        /// <param name="minVal">Les pixels dont la valeur est en dessous sont ignorés</param>
        /// <param name="maxVal">Les pixels dont la valeur est au dessus sont ignorés</param>
        public MyImageStats(MyImage image, int height, int width, byte minVal = byte.MinValue, byte maxVal = byte.MaxValue)
        {
            this.realWidth = width;
            this.realHeight = height;

            //On est obligé de choisir une taille d'image correspondant au nombre de pixel avec lesquels créer l'histo sinon
            //on laisse des trous de manière irrégulière dans l'image. On change donc la taille mais on redimensionne l'image
            //avec la taille spécifiée par l'utilisateur à la fin

            byte realMin = minVal, realMax = maxVal;

            if (realMin < 0 || realMin > maxVal)
                realMin = 0;
            if (realMax > 255 || realMax < minVal)
                realMax = 255;

            this.minValue = realMin;
            this.maxValue = realMax;


            int valMax = this.maxValue + 1 - this.minValue;

            if (width < valMax)
            {
                width = valMax;
            }
            else if (width % valMax != 0)
            {
                double diff = width / (double)valMax;

                int incre = diff % 1 < 0.5 ? -1 : 1;
                while (width % valMax != 0)
                {
                    width += incre;
                }
            }
            width += 10; //bords
            height = height < 50 ? 50 : height;

            this.histo = new MyImage(height, width);
            this.graphHisto = new MyGraphics(histo);

            this.image = image;
        }


        //Méthodes statiques

        /// <summary>
        /// Renvoie un <see cref="Pixel"/> avec la couleur moyenne de l'image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Pixel GetAverageColor(MyImage image)
        {
            if (image == null || !image.Validité)
                return Pixel.Zero;

            byte[] data = image.ToBGRArray();

            bool alphaExiste = image.PixelFormat == Formats.PixelFormat.BMP_Argb32;

            int indexH = 0;
            int maxW = image.Stride - image.GetPadding();


            long a = 0, r = 0, g = 0, b = 0;

            while (indexH < image.Height)
            {
                int décalage = indexH * image.Stride;

                for (int indexW = 0; indexW < maxW; ++indexW)
                {
                    b += data[décalage + indexW];

                    g += data[décalage + ++indexW];

                    r += data[décalage + ++indexW];

                    if (alphaExiste)
                    {
                        a += data[décalage + ++indexW];
                    }
                }

                ++indexH;
            }

            int total = image.Height * image.Width;

            return Pixel.FromArgb(alphaExiste ? (int)a / total : 255, (int)r / total, (int)g / total, (int)b / total);
        }


        /// <summary>
        /// Renvoie le Pixel qui apparait le plus fréquemment dans une image.
        /// Met plus d'une sec à partir de 20M pixels (4k * 5k).
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Pixel GetMostOccurentPixel(MyImage image)
        {
            return Pixel.FromArgb(GetPixelsByOccurence(image).Aggregate((x, y) => x.Value > y.Value ? x : y).Key);
        }

        /// <summary>
        /// Renvoie une liste avec le nombre d'occurences de chaque pixel unique de l'image associé à sa valeur.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Dictionary<int, int> GetPixelsByOccurence(MyImage image)
        {
            Dictionary<int, int> pixelsAndOccurences = new Dictionary<int, int>();

            for (int i = 0; i < image.Width; i++)
                for (int j = 0; j < image.Height; j++)
                {
                    int pixel = image[j, i].ToArgb();

                    if (pixelsAndOccurences.Keys.Contains(pixel))
                    {
                        pixelsAndOccurences[pixel]++;
                    }
                    else
                    {
                        pixelsAndOccurences.Add(pixel, 1);
                    }
                }

            return pixelsAndOccurences;
        }


        /// <summary>
        /// Renvoie la luminosité moyenne perçue par rapport à 255
        /// </summary>
        /// <param name="image">Image</param>
        /// <returns></returns>
        public static int GetAverageBrightness(MyImage image)
        {
            double lumi = 0;

            for (int i = 0; i < image.Height; ++i)
            {
                for (int j = 0; j < image.Width; ++j)
                {
                    lumi += image[i, j].Brightness();
                }
            }
            lumi /= image.Height * image.Width;

            return (int)lumi;
        }


        /// <summary>
        /// Renvoie la teinte moyenne en degré % 360. 
        /// </summary>
        /// <param name="image">Image</param>
        /// <returns></returns>
        public static int GetAverageHue(MyImage image)
        {
            double hue = 0;

            for (int i = 0; i < image.Height; ++i)
            {
                for (int j = 0; j < image.Width; ++j)
                {
                    hue += image[i, j].Hue();
                }
            }
            hue /= image.Height * image.Width;

            return (int)hue;
        }

        /// <summary>
        /// Renvoie la luminosité moyenne par rapport à 1
        /// </summary>
        /// <param name="image">Image</param>
        /// <returns></returns>
        public static float GetAverageLightness(MyImage image)
        {
            float lumi = 0;

            for (int i = 0; i < image.Height; ++i)
            {
                for (int j = 0; j < image.Width; ++j)
                {
                    lumi += image[i, j].Lightness();
                }
            }
            lumi /= image.Height * image.Width;

            return lumi;
        }

        /// <summary>
        /// Renvoie la saturation moyenne par rapport à 1
        /// </summary>
        /// <param name="image">Image</param>
        /// <returns></returns>
        public static float GetAverageSaturation(MyImage image)
        {
            float lumi = 0;

            for (int i = 0; i < image.Height; ++i)
            {
                for (int j = 0; j < image.Width; ++j)
                {
                    lumi += image[i, j].Saturation();
                }
            }
            lumi /= image.Height * image.Width;

            return lumi;
        }


        /// <summary>
        /// Renvoie le nombre de pixel par couleur (0-255) en fct d'une couleur (0 = bleu, 1 = vert, 2 = rouge, 3 = alpha s'il y a)
        /// </summary>
        /// <param name="image"></param>
        /// <param name="couleur">0, 1, 2 ou 3</param>
        /// <returns></returns>
        private int[] IntensiteParCouleur(MyImage image, int couleur)
        {
            byte[] data = image.ToBGRArray();

            int startH = 0;

            int dataLineLength = image.Stride - image.GetPadding();

            int lengthPixel = image.PixelFormat.GetPixelLength();


            int[] données = new int[this.maxValue + 1 - this.minValue];

            unsafe
            {
                fixed (int* dataIntensité = données)
                fixed (byte* dataPixelF = data)
                {
                    byte* dataPixel = dataPixelF;
                    while (startH++ < image.Height)
                    {
                        int startLine = couleur;

                        while (startLine < dataLineLength)
                        {
                            byte intensite = *(dataPixel + startLine);

                            if (intensite >= this.minValue && intensite <= this.maxValue)
                            {
                                *(dataIntensité + intensite - this.minValue) += 1;
                            }

                            startLine += lengthPixel;
                        }

                        dataPixel += image.Stride;
                    }
                }

            }

            return données;
        }


        private static int Max(int[] tab)
        {
            return tab.Max(); //le plus performant
        }


        //Histogramme

        /// <summary>
        /// Crée un histogramme de l'image du nombre de pixels en fonction de l'intensité (0-255) selon un <see cref="HistogrammeMode"/> et des paramètres pré-définis
        /// </summary>
        public MyImage CreateHistogramme()
        {
            MyImage image;

            if (this.HistogrammeMode == HistogrammeMode.Echelle_Gris)
            {
                this.couleursRGB = new Pixel[3, 2];
                this.couleursRGB[0, 0] = Pixel.FromArgb(190, 190, 190);
                this.couleursRGB[0, 1] = Pixel.FromArgb(50, 50, 50);
                this.couleursRGB[1, 0] = Pixel.FromArgb(190, 190, 190);
                this.couleursRGB[1, 1] = Pixel.FromArgb(50, 50, 50);
                this.couleursRGB[2, 0] = Pixel.FromArgb(190, 190, 190);
                this.couleursRGB[2, 1] = Pixel.FromArgb(50, 50, 50);

                image = this.image.Clone();
                new MyGraphics(image).TransformationGris();
            }
            else
            {
                this.couleursRGB = new Pixel[3, 2];
                this.couleursRGB[0, 0] = Pixel.FromArgb(255, 80, 80);
                this.couleursRGB[0, 1] = Pixel.FromArgb(50, 20, 20);
                this.couleursRGB[1, 0] = Pixel.FromArgb(100, 255, 100);
                this.couleursRGB[1, 1] = Pixel.FromArgb(20, 50, 20);
                this.couleursRGB[2, 0] = Pixel.FromArgb(80, 80, 255);
                this.couleursRGB[2, 1] = Pixel.FromArgb(20, 20, 50);

                image = this.image;
            }

            this.pixelsParIntensité = new int[3][];
            for (int i = 0; i < 3; i++)
            {
                this.pixelsParIntensité[i] = IntensiteParCouleur(image, i);
            }

            this.graphHisto.Remplissage(this.RemplissageCouleur);

            int hauteur = this.histo.Height;
            const int décallageW = 4;
            const int décallageH = -2;

            int max = Math.Max(Max(pixelsParIntensité[0]), Math.Max(Max(pixelsParIntensité[1]), Max(pixelsParIntensité[2])));

            if (this.Remplissage) //Remplit par la couleur spécifiée le dessous de la courbe d'intensité
            {
                for (int j = 0; j < 3; j++)
                {
                    int incrementation = (int)Math.Floor((double)this.histo.Width / this.pixelsParIntensité[j].Length);
                    for (int i = 0; i < pixelsParIntensité[j].Length - 1; i++)
                    {
                        Point hautGauche = new Point((double)hauteur + décallageH - (double)pixelsParIntensité[j][i] * (hauteur + 2 * décallageH) / max, incrementation * i + décallageW);
                        Point hautDroite = new Point((double)hauteur + décallageH - (double)pixelsParIntensité[j][i + 1] * (hauteur + 2 * décallageH) / max, incrementation * (i + 1) + décallageW);
                        //Point basGauche = new Point(this.histo.Height - 1 + décallageH, hautGauche.X);
                        Point basDroite = new Point(this.histo.Height - 1 + décallageH, hautDroite.X);

                        double rapportOrigine = (hautGauche.X - décallageW) / this.histo.Width; //Distance par rapport à l'axe des ordonnées à l'origine du repère + décallageW

                        byte couleurR = (byte)Math.Min(Math.Max(0, this.couleursRGB[j, 0].R * rapportOrigine + this.couleursRGB[j, 1].R * (1 - rapportOrigine)) + 10, 255);

                        byte couleurG = (byte)Math.Min(Math.Max(0, this.couleursRGB[j, 0].G * rapportOrigine + this.couleursRGB[j, 1].G * (1 - rapportOrigine)) + 10, 255);

                        byte couleurB = (byte)Math.Min(Math.Max(0, this.couleursRGB[j, 0].B * rapportOrigine + this.couleursRGB[j, 1].B * (1 - rapportOrigine)) + 10, 255);

                        Pixel color = Pixel.FromArgb(couleurR, couleurG, couleurB);

                        this.graphHisto.FillRectangle(hautGauche, basDroite, color);

                        if (i == this.pixelsParIntensité[0].Length - 2) //Dernier
                        {
                            hautGauche = hautDroite;
                            basDroite = new Point(this.histo.Height - 1 + décallageH, hautGauche.X + incrementation);

                            this.graphHisto.FillRectangle(hautGauche, basDroite, color);
                        }

                    }
                }
            }

            for (int j = 2; j >= 0; j--)
            {
                int incrementation = this.histo.Width / this.pixelsParIntensité[j].Length;

                Couleurs color = this.HistogrammeMode == HistogrammeMode.Echelle_Gris ? Couleurs.Gris_Foncé :
                           j == 0 ? Couleurs.Rouge_Foncé : j == 1 ? Couleurs.Vert_Foncé : Couleurs.Bleu_Marine;

                for (int i = 0; i < pixelsParIntensité[j].Length - 1; i++)
                {
                    Point hautGauche = new Point((double)hauteur - 1 + décallageH - (double)pixelsParIntensité[j][i] * (hauteur + 2 * décallageH) / max, incrementation * (i + 0.5) + décallageW);
                    Point hautDroite = new Point((double)hauteur - 1 + décallageH - (double)pixelsParIntensité[j][i + 1] * (hauteur + 2 * décallageH) / max, incrementation * (i + 1.5) + décallageW);

                    for (int k = 0; k < 3; k++) //Bug, parfois des lignes traversent toute l'image
                    {
                        this.graphHisto.DrawLineWu(hautGauche, hautDroite, Pixel.FromColor(color));

                        hautGauche = new Point(hautGauche.Y + 1, hautGauche.X + 0.25);
                        hautDroite = new Point(hautDroite.Y + 1, hautDroite.X + 0.25);
                    }

                }
            }

            if (this.realWidth != this.histo.Width) //On redimensionne l'image vers les dimensions que l'utilisateur a choisi
            {
                this.graphHisto.KeepAspectRatio = false;
                this.graphHisto.InterpolationMode = this.realWidth < this.histo.Width ? InterpolationMode.Bilineaire : InterpolationMode.NearestNeighbour;
                this.graphHisto.Redimensionnement(this.realHeight, this.realWidth);
            }


            //Cadre

            Point hautGaucheLigneNoire = new Point(4, 4);
            Point basDroiteLigneNoire = new Point(this.histo.Height - 1, this.histo.Width - 5);

            this.graphHisto.DrawRectangle(hautGaucheLigneNoire, basDroiteLigneNoire, this.RemplissageCouleur.InversionCouleur());
            this.graphHisto.DrawRectangle(new Point(hautGaucheLigneNoire.Y - 1, hautGaucheLigneNoire.X + 1),
                new Point(basDroiteLigneNoire.Y - 1, basDroiteLigneNoire.X - 1), this.RemplissageCouleur.InversionCouleur());
            this.graphHisto.DrawRectangle(new Point(hautGaucheLigneNoire.Y - 2, hautGaucheLigneNoire.X + 2),
                new Point(basDroiteLigneNoire.Y - 2, basDroiteLigneNoire.X - 2), this.RemplissageCouleur.InversionCouleur());


            return this.histo;
        }


        /// <summary>
        /// Crée un histogramme de l'image du nombre de pixels en fonction de l'intensité (0-255) selon un <see cref="HistogrammeMode"/> et des paramètres pré-définis. 
        /// Les couleurs sont ajoutées sur l'image avec une certaine transparence pour qu'elles soient toutes visibles
        /// </summary>
        public MyImage CreateHistogrammeTransparence()
        {
            this.pixelsParIntensité = new int[3][];
            for (int i = 0; i < 3; i++)
            {
                this.pixelsParIntensité[i] = IntensiteParCouleur(image, i);
            }

            this.couleursRGB = new Pixel[3, 2];
            this.couleursRGB[0, 0] = Pixel.FromArgb(255, 80, 80);
            this.couleursRGB[0, 1] = Pixel.FromArgb(100, 20, 20);
            this.couleursRGB[1, 0] = Pixel.FromArgb(100, 255, 100);
            this.couleursRGB[1, 1] = Pixel.FromArgb(40, 100, 40);
            this.couleursRGB[2, 0] = Pixel.FromArgb(80, 80, 255);
            this.couleursRGB[2, 1] = Pixel.FromArgb(40, 50, 100);

            this.graphHisto.Remplissage(this.RemplissageCouleur);

            int hauteur = this.histo.Height;
            const int décallageW = 4;
            const int décallageH = -2;

            if (this.HistogrammeMode == HistogrammeMode.Echelle_Couleurs)
            {
                int max = Math.Max(Max(pixelsParIntensité[0]), Math.Max(Max(pixelsParIntensité[1]), Max(pixelsParIntensité[2])));

                if (this.Remplissage) //Remplit par la couleur spécifiée le dessous de la courbe d'intensité
                {
                    for (int j = 0; j < 3; j++)
                    {
                        MyImage image = new MyImage(this.histo.Height, this.histo.Width, Formats.PixelFormat.BMP_Argb32);
                        MyGraphics gIm = new MyGraphics(image);

                        int incrementation = (int)Math.Floor((double)this.histo.Width / this.pixelsParIntensité[j].Length);
                        int alpha = j == 0 ? 255 : j == 1 ? 190 : 170;
                        for (int i = 0; i < pixelsParIntensité[j].Length - 1; i++)
                        {
                            Point hautGauche = new Point((double)hauteur + décallageH - (double)pixelsParIntensité[j][i] * (hauteur + 2 * décallageH) / max, incrementation * i + décallageW);
                            Point hautDroite = new Point((double)hauteur + décallageH - (double)pixelsParIntensité[j][i + 1] * (hauteur + 2 * décallageH) / max, incrementation * (i + 1) + décallageW);
                            Point basDroite = new Point(this.histo.Height - 1 + décallageH, hautDroite.X);

                            double rapportOrigine = (hautGauche.X - décallageW) / this.histo.Width; //Distance par rapport à l'axe des ordonnées à l'origine du repère + décallageW

                            byte couleurR = (byte)Math.Min(Math.Max(0, this.couleursRGB[j, 0].R * rapportOrigine + this.couleursRGB[j, 1].R * (1 - rapportOrigine)) + 10, 255);

                            byte couleurG = (byte)Math.Min(Math.Max(0, this.couleursRGB[j, 0].G * rapportOrigine + this.couleursRGB[j, 1].G * (1 - rapportOrigine)) + 10, 255);

                            byte couleurB = (byte)Math.Min(Math.Max(0, this.couleursRGB[j, 0].B * rapportOrigine + this.couleursRGB[j, 1].B * (1 - rapportOrigine)) + 10, 255);

                            Pixel color = Pixel.FromArgb(alpha, couleurR, couleurG, couleurB);

                            gIm.FillRectangle(hautGauche, basDroite, color);

                            if (i == this.pixelsParIntensité[0].Length - 2) //Dernier
                            {
                                hautGauche = hautDroite;
                                basDroite = new Point(this.histo.Height - 1 + décallageH, hautGauche.X + incrementation);

                                gIm.FillRectangle(hautGauche, basDroite, color);
                            }

                        }

                        this.graphHisto.DrawImage(image, -1);

                    }
                }

                if (!this.Remplissage)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int incrementation = this.histo.Width / this.pixelsParIntensité[j].Length;
                        Couleurs color = j == 0 ? Couleurs.Rouge_Foncé : j == 1 ? Couleurs.Vert_Foncé : Couleurs.Bleu_Marine;

                        for (int i = 0; i < pixelsParIntensité[j].Length - 1; i++)
                        {
                            Point hautGauche = new Point((double)hauteur - 1 + décallageH - (double)pixelsParIntensité[j][i] * (hauteur + 2 * décallageH) / max, incrementation * (i + 0.5) + décallageW);
                            Point hautDroite = new Point((double)hauteur - 1 + décallageH - (double)pixelsParIntensité[j][i + 1] * (hauteur + 2 * décallageH) / max, incrementation * (i + 1.5) + décallageW);

                            for (int k = 0; k < 3; k++)
                            {
                                this.graphHisto.DrawLineWu(hautGauche, hautDroite, Pixel.FromColor(color));//Algo LineWu pas parfait

                                hautGauche = new Point(hautGauche.Y + 1, hautGauche.X + 0.25);
                                hautDroite = new Point(hautDroite.Y + 1, hautDroite.X + 0.25);
                            }

                        }
                    }
                }

            }
            else if (this.HistogrammeMode == HistogrammeMode.Echelle_Gris)
            {
                int incrementation = (int)Math.Floor((double)this.histo.Width / this.pixelsParIntensité[0].Length);
                int max = (Max(pixelsParIntensité[0]) + Max(pixelsParIntensité[1]) + Max(pixelsParIntensité[2])) / 3;
                for (int i = 1; i < this.pixelsParIntensité[0].Length - 1; i++)
                {
                    int moyenne = (this.pixelsParIntensité[0][i] + this.pixelsParIntensité[1][i] + this.pixelsParIntensité[2][i]) / 3;
                    Point hautGauche = new Point((double)hauteur + décallageH - (moyenne * (hauteur + 2.0 * décallageH) / max), incrementation * (i + 0.5) + décallageW);
                    moyenne = (this.pixelsParIntensité[0][i + 1] + this.pixelsParIntensité[1][i + 1] + this.pixelsParIntensité[2][i + 1]) / 3;
                    Point hautDroite = new Point((double)hauteur + décallageH - (moyenne * (hauteur + 2.0 * décallageH) / max), incrementation * (i + 1.5) + décallageW);

                    double rapportOrigine = (hautGauche.X - décallageW) / this.histo.Width; //Distance par rapport à l'axe des ordonnées à l'origine du repère + décallageW

                    byte couleur = (byte)Math.Min(Math.Max(0, Pixel.FromColor(Couleurs.Gris_Clair).Moyenne() * rapportOrigine + Pixel.FromColor(Couleurs.Noir_Clair).Moyenne() * (1 - rapportOrigine)) + 10, 255);

                    if (this.Remplissage)
                    {
                        hautGauche.Y += 1;
                        hautDroite.Y += 1;
                        Point basDroite = new Point((double)this.histo.Height - 1 + décallageH, hautDroite.X);

                        this.graphHisto.FillRectangle(hautGauche, basDroite, Pixel.FromArgb(couleur, couleur, couleur));
                    }

                    for (int k = 0; k < 3; k++) //Bug, parfois y'a des lignes qui traversent toute l'image
                    {
                        this.graphHisto.DrawLineBresenham(hautGauche, hautDroite, Pixel.FromColor(Couleurs.Noir));

                        hautGauche = new Point(hautGauche.Y + 1, hautGauche.X + 0.25);
                        hautDroite = new Point(hautDroite.Y + 1, hautDroite.X + 0.25);
                    }

                    if (this.Remplissage && i == this.pixelsParIntensité[0].Length - 2)
                    {
                        hautGauche = hautDroite;
                        Point basDroite = new Point((double)this.histo.Height - 1 + décallageH, hautGauche.X + incrementation);

                        this.graphHisto.FillRectangle(hautGauche, basDroite, Pixel.FromArgb(couleur, couleur, couleur));
                    }
                }

            }

            if (this.realWidth != this.histo.Width) //On redimensionne l'image vers les dimensions que l'utilisateur a choisi
            {
                this.graphHisto.KeepAspectRatio = false;
                this.graphHisto.InterpolationMode = this.realWidth < this.histo.Width ? InterpolationMode.NearestNeighbour : InterpolationMode.Bicubique;
                this.graphHisto.Redimensionnement(this.realHeight, this.realWidth);
            }


            //Cadre

            Point hautGaucheLigneNoire = new Point(2, 2);
            Point basDroiteLigneNoire = new Point(this.histo.Height - 1, this.histo.Width - 3);

            this.graphHisto.DrawRectangle(hautGaucheLigneNoire, basDroiteLigneNoire, this.RemplissageCouleur.InversionCouleur());
            this.graphHisto.DrawRectangle(new Point(hautGaucheLigneNoire.Y - 1, hautGaucheLigneNoire.X + 1),
                new Point(basDroiteLigneNoire.Y - 1, basDroiteLigneNoire.X - 1), this.RemplissageCouleur.InversionCouleur());
            this.graphHisto.DrawRectangle(new Point(hautGaucheLigneNoire.Y - 2, hautGaucheLigneNoire.X + 2),
                new Point(basDroiteLigneNoire.Y - 2, basDroiteLigneNoire.X - 2), this.RemplissageCouleur.InversionCouleur());


            return this.histo;
        }


        /// <summary>
        /// Crée un histogramme de l'image du nombre de pixels en fonction de l'intensité (0-255) selon un <see cref="HistogrammeMode"/> et des paramètres pré-définis
        /// Les couleurs sont ajoutées sur l'image avec une certaine transparence pour qu'elles soient toutes visibles et les contours sont lissés.
        /// </summary>
        public MyImage CreateHistogrammeTransparenceLissage()
        {
            this.pixelsParIntensité = new int[3][];
            for (int i = 0; i < 3; i++)
            {
                this.pixelsParIntensité[i] = IntensiteParCouleur(image, i);
            }

            this.couleursRGB = new Pixel[3, 2];
            this.couleursRGB[0, 0] = Pixel.FromArgb(255, 80, 80);
            this.couleursRGB[0, 1] = Pixel.FromArgb(100, 20, 20);
            this.couleursRGB[1, 0] = Pixel.FromArgb(100, 255, 100);
            this.couleursRGB[1, 1] = Pixel.FromArgb(40, 100, 40);
            this.couleursRGB[2, 0] = Pixel.FromArgb(80, 80, 255);
            this.couleursRGB[2, 1] = Pixel.FromArgb(40, 50, 100);

            this.graphHisto.Remplissage(this.RemplissageCouleur);

            int hauteur = this.histo.Height;
            const int décallageW = 4;
            const int décallageH = -2;

            if (this.HistogrammeMode == HistogrammeMode.Echelle_Couleurs)
            {
                int max = Math.Max(Max(pixelsParIntensité[0]), Math.Max(Max(pixelsParIntensité[1]), Max(pixelsParIntensité[2])));

                if (this.Remplissage) //Remplit par la couleur spécifiée le dessous de la courbe d'intensité
                {
                    for (int j = 0; j < 3; j++)
                    {
                        MyImage image = new MyImage(this.histo.Height, this.histo.Width, Formats.PixelFormat.BMP_Argb32);
                        MyGraphics gIm = new MyGraphics(image);

                        int incrementation = (int)Math.Floor((double)this.histo.Width / this.pixelsParIntensité[j].Length);
                        int alpha = j == 0 ? 255 : j == 1 ? 190 : 170;
                        for (int i = 0; i < pixelsParIntensité[j].Length - 1; i++)
                        {
                            Point hautGauche = new Point((double)hauteur - 1 + décallageH - (double)pixelsParIntensité[j][i] * (hauteur + 2 * décallageH) / max, incrementation * (i + 0.5) + décallageW);
                            Point hautDroite = new Point((double)hauteur - 1 + décallageH - (double)pixelsParIntensité[j][i + 1] * (hauteur + 2 * décallageH) / max, incrementation * (i + 1.5) + décallageW);

                            Point basDroite = new Point(this.histo.Height - 1 + décallageH, hautDroite.X);
                            Point basGauche = new Point(basDroite.Y, hautGauche.X);

                            double rapportOrigine = (hautGauche.X - décallageW) / this.histo.Width; //Distance par rapport à l'axe des ordonnées à l'origine du repère + décallageW

                            byte couleurR = (byte)Math.Min(Math.Max(0, this.couleursRGB[j, 0].R * rapportOrigine + this.couleursRGB[j, 1].R * (1 - rapportOrigine)) + 10, 255);

                            byte couleurG = (byte)Math.Min(Math.Max(0, this.couleursRGB[j, 0].G * rapportOrigine + this.couleursRGB[j, 1].G * (1 - rapportOrigine)) + 10, 255);

                            byte couleurB = (byte)Math.Min(Math.Max(0, this.couleursRGB[j, 0].B * rapportOrigine + this.couleursRGB[j, 1].B * (1 - rapportOrigine)) + 10, 255);

                            Pixel color = Pixel.FromArgb(alpha, couleurR, couleurG, couleurB);

                            gIm.FillForme_4(hautGauche, hautDroite, basDroite, basGauche, color);

                            if (i == this.pixelsParIntensité[0].Length - 2) //Dernier
                            {
                                hautGauche = hautDroite;
                                basDroite = new Point(this.histo.Height - 1 + décallageH, hautGauche.X + incrementation);

                                gIm.FillRectangle(hautGauche, basDroite, color);
                            }

                        }

                        this.graphHisto.DrawImage(image, -1);

                    }
                }

                if (!this.Remplissage)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int incrementation = this.histo.Width / this.pixelsParIntensité[j].Length;
                        Couleurs color = j == 0 ? Couleurs.Rouge_Foncé : j == 1 ? Couleurs.Vert_Foncé : Couleurs.Bleu_Marine;

                        for (int i = 1; i < pixelsParIntensité[j].Length - 2; i++)
                        {
                            Point p1 = new Point((double)hauteur - 1 + décallageH - (double)pixelsParIntensité[j][i - 1] * (hauteur + 2 * décallageH) / max, incrementation * (i - 0.5) + décallageW);
                            Point p2 = new Point((double)hauteur - 1 + décallageH - (double)pixelsParIntensité[j][i] * (hauteur + 2 * décallageH) / max, incrementation * (i + 0.5) + décallageW);
                            Point p3 = new Point((double)hauteur - 1 + décallageH - (double)pixelsParIntensité[j][i + 1] * (hauteur + 2 * décallageH) / max, incrementation * (i + 1.5) + décallageW);
                            Point p4 = new Point((double)hauteur - 1 + décallageH - (double)pixelsParIntensité[j][i + 2] * (hauteur + 2 * décallageH) / max, incrementation * (i + 2.5) + décallageW);

                            this.graphHisto.DrawBézier(p1, p2, p3, p4, (Pixel)color);

                        }
                    }
                }

            }
            else if (this.HistogrammeMode == HistogrammeMode.Echelle_Gris)
            {
                int incrementation = (int)Math.Floor((double)this.histo.Width / this.pixelsParIntensité[0].Length);
                int max = (Max(pixelsParIntensité[0]) + Max(pixelsParIntensité[1]) + Max(pixelsParIntensité[2])) / 3;
                for (int i = 1; i < this.pixelsParIntensité[0].Length - 1; i++)
                {
                    int moyenne = (this.pixelsParIntensité[0][i] + this.pixelsParIntensité[1][i] + this.pixelsParIntensité[2][i]) / 3;
                    Point hautGauche = new Point((double)hauteur + décallageH - (moyenne * (hauteur + 2.0 * décallageH) / max), incrementation * (i + 0.5) + décallageW);
                    moyenne = (this.pixelsParIntensité[0][i + 1] + this.pixelsParIntensité[1][i + 1] + this.pixelsParIntensité[2][i + 1]) / 3;
                    Point hautDroite = new Point((double)hauteur + décallageH - (moyenne * (hauteur + 2.0 * décallageH) / max), incrementation * (i + 1.5) + décallageW);

                    double rapportOrigine = (hautGauche.X - décallageW) / this.histo.Width; //Distance par rapport à l'axe des ordonnées à l'origine du repère + décallageW

                    byte couleur = (byte)Math.Min(Math.Max(0, Pixel.FromColor(Couleurs.Gris_Clair).Moyenne() * rapportOrigine + Pixel.FromColor(Couleurs.Noir_Clair).Moyenne() * (1 - rapportOrigine)) + 10, 255);

                    if (this.Remplissage)
                    {
                        hautGauche.Y += 1;
                        hautDroite.Y += 1;
                        Point basDroite = new Point((double)this.histo.Height - 1 + décallageH, hautDroite.X);

                        this.graphHisto.FillRectangle(hautGauche, basDroite, Pixel.FromArgb(couleur, couleur, couleur));
                    }

                    for (int k = 0; k < 3; k++) //Bug, parfois y'a des lignes qui traversent toute l'image
                    {
                        this.graphHisto.DrawLineBresenham(hautGauche, hautDroite, Pixel.FromColor(Couleurs.Noir));

                        hautGauche = new Point(hautGauche.Y + 1, hautGauche.X + 0.25);
                        hautDroite = new Point(hautDroite.Y + 1, hautDroite.X + 0.25);
                    }

                    if (this.Remplissage && i == this.pixelsParIntensité[0].Length - 2)
                    {
                        hautGauche = hautDroite;
                        Point basDroite = new Point((double)this.histo.Height - 1 + décallageH, hautGauche.X + incrementation);

                        this.graphHisto.FillRectangle(hautGauche, basDroite, Pixel.FromArgb(couleur, couleur, couleur));
                    }
                }

            }

            if (this.realWidth != this.histo.Width) //On redimensionne l'image vers les dimensions que l'utilisateur a choisi
            {
                this.graphHisto.KeepAspectRatio = false;
                this.graphHisto.InterpolationMode = this.realWidth < this.histo.Width ? InterpolationMode.NearestNeighbour : InterpolationMode.Bicubique;
                this.graphHisto.Redimensionnement(this.realHeight, this.realWidth);
            }


            //Cadre

            Point hautGaucheLigneNoire = new Point(1, 1);
            Point basDroiteLigneNoire = new Point(this.histo.Height - 1, this.histo.Width - 2);

            this.graphHisto.DrawRectangle(hautGaucheLigneNoire, basDroiteLigneNoire, this.RemplissageCouleur.InversionCouleur());
            this.graphHisto.DrawRectangle(new Point(hautGaucheLigneNoire.Y + 1, hautGaucheLigneNoire.X + 1),
                new Point(basDroiteLigneNoire.Y - 1, basDroiteLigneNoire.X - 1), this.RemplissageCouleur.InversionCouleur().TransformationSépia());
            this.graphHisto.DrawRectangle(new Point(hautGaucheLigneNoire.Y + 2, hautGaucheLigneNoire.X + 2),
                new Point(basDroiteLigneNoire.Y - 2, basDroiteLigneNoire.X - 2), this.RemplissageCouleur.InversionCouleur());


            return this.histo;
        }
    }
}
