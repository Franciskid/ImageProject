using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using comp = Photoshop3000.NombreComplex;

namespace Photoshop3000.Annexes
{
    /// <summary>
    /// Permet la création d'images de types fractales
    /// </summary>
    internal class Fractale
    {
        //Champs et propriétés

        private readonly int height, width;

        /// <summary>
        /// Image de la fractale
        /// </summary>
        private readonly MyImage image;

        /// <summary>
        /// Graphique de l'image de la fractale, sur lequel réaliser des opérations sur l'image
        /// </summary>
        private readonly MyGraphics graph;

        /// <summary>
        /// Image servant de référence pour la création d'une image type kaléidoscope
        /// </summary>
        private readonly MyImage imageKaléidocope;

        /// <summary>
        /// Liste contenant les couleurs utilisées pour les fractales de type Mandelbrot
        /// </summary>
        private List<Pixel> colors;

        private Couleurs? couleurDominante;

        private int maxTryColor = 10;

        private bool couleursHSL = false;

        private double contrastValue = 1;

        private double tailleFractJulia = 1.75;

        private int maxIterations = 400;

        private int tailleMosaique = 10;

        private readonly Random rand = new Random(Guid.NewGuid().GetHashCode());

        private Fractales FracType;


        /// <summary>
        /// Valeur du contraste de l'image
        /// </summary>
        public double Contraste { get => contrastValue; set => this.contrastValue = value; }

        /// <summary>
        /// Valeur du contraste de l'image
        /// </summary>
        public double TailleFractJulia { get => tailleFractJulia; set => this.tailleFractJulia = value; }

        /// <summary>
        /// Position de la fractale
        /// </summary>
        public RectangleD PositionJulia { get; set; }

        /// <summary>
        /// Nombre d'itération max lors du calcul d'une fractale. (Pour une fractale de type Mandelbrot, à mettre entre 100-1000)
        /// </summary>
        public int MaxItération { get => maxIterations; set => this.maxIterations = value; }

        /// <summary>
        /// Taille d'une mosaique de kaléidoscope
        /// </summary>
        public int TailleMosaique { get => tailleMosaique; set => this.tailleMosaique = value; }

        /// <summary>
        /// Couleur utilisée pour représenter les fractales : <see langword="false"/> = couleurs RGB   <see langword="true"/> = couleurs HSL (=TSV, teinte, saturation, luminosité)
        /// Pas implémenté
        /// </summary>
        public bool CouleursHSL { get => couleursHSL; set => this.couleursHSL = value; }

        /// <summary>
        /// Nombre de fois qu'on ré-essaie de trouver la bonne couleur pour le kaléidocsope à partir d'une <see cref="Photoshop3000.MyImage"/>
        /// </summary>
        public int MaxTryColor { get => maxTryColor; set => this.maxTryColor = value; }

        /// <summary>
        /// Type de forme fractale à générer
        /// </summary>
        public Fractales Type { get => FracType; private set => this.FracType = value; }

        /// <summary>
        /// Couleur dominante lors de la génération de kaléïdoscope
        /// </summary>
        public Couleurs? CouleurDominante { set => this.couleurDominante = value; private get => this.couleurDominante; }

        /// <summary>
        /// Couleur d'arrière plan, utilisée pour les formes récursives
        /// </summary>
        public Pixel BackgroundColor { get; set; } = Pixel.FromColor(Couleurs.Blanc);

        /// <summary>
        /// Récupère l'instance <see cref="Photoshop3000.MyImage"/> utilisée lors de la création de la fractale
        /// </summary>
        public MyImage MyImage { get => image; }

        /// <summary>
        /// Equation de l'utilisateur. <see cref="Fractales.UserEquationZSqrd"/> doit être sélectionné
        /// </summary>
        public comp UserEquation { get; set; } = 0;


        //Constructeurs

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="Fractale"/> avec les paramètres par défaut.
        /// </summary>
        public Fractale()
            : this(500, 500, Fractales.Mandelbrot)
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="Fractale"/> avec la taille de l'image et le type de <see cref="Fractales"/> à créer.
        /// </summary>
        /// <param name="height">Hauteur image</param>
        /// <param name="width">Largeur image (en général doit être de la même taille que la hauteur)</param>
        /// <param name="t">Type de fractale</param>
        public Fractale(int height, int width, Fractales t)
        {
            this.height = height;
            this.width = width;
            this.FracType = t;
            this.MaxItération = (int)t <= (int)Fractales.UserEquationZSqrd ? 400 : 6;

            this.image = new MyImage(this.height, this.width);

            this.graph = new MyGraphics(this.image);

            this.colors = InitializeColorList();

        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="Fractale"/> avec la <see cref="Photoshop3000.MyImage"/> spécifiée
        /// </summary>
        /// <param name="image">Image</param>
        public Fractale(MyImage image)
            : this(image.Height, image.Width, Fractales.Mosaique_From_Image)
        {
            this.imageKaléidocope = image;
        }


        //Méthodes publiques

        /// <summary>
        /// Crée une fractale à partir des paramètres pré-définis.
        /// </summary>
        public void Draw()
        {
            this.colors.Insert(0, this.BackgroundColor);

            float taille = (float)tailleFractJulia;
            this.PositionJulia = this.PositionJulia.Empty ? new RectangleD(-taille, -taille, taille * 2, taille * 2) : this.PositionJulia;

            NormeMaxSquare = this.PositionJulia.Width * this.PositionJulia.Height;

            switch (this.FracType)
            {
                case Fractales.Mandelbrot:
                case Fractales.Lapin:
                case Fractales.Ilots:
                case Fractales.Escargot_quadruple_revolution:
                case Fractales.Escargot_double_revolution:
                case Fractales.Dendrite_Normal:
                case Fractales.Dentriste_Division:
                case Fractales.TrucStylé1:
                case Fractales.TrucStylé2:
                case Fractales.TrucStylé3:
                case Fractales.TrucStylé4:
                case Fractales.TrucStylé5:
                case Fractales.TrucStylé6:
                    FractaleFromCompBeta(FractalesToComp(this.FracType));
                    break;

                case Fractales.UserEquationZSqrd:
                case Fractales.UserEquationZSin:
                case Fractales.UserEquationNewton:
                    FractaleFromCompBeta(this.UserEquation);
                    break;

                //Autres formes fractales
                case Fractales.Sierpinski_Triangle:
                case Fractales.Sierpinski_Tapis:
                case Fractales.Cercle_1:
                case Fractales.Cercle_2:
                case Fractales.Kaléidoscope:
                case Fractales.Mosaique_Kaléïdoscope:
                case Fractales.Mosaique_From_Image:
                case Fractales.Koch_Flocon:
                case Fractales.Koch_Flocon_Profondeur:
                case Fractales.Arbre_4_Cotés:
                case Fractales.Arbre_Descendant:
                    FractaleFromForme();
                    break;
            }
        }


        #region Création Fractale


        ///// <summary>
        ///// Forme fractale générée à partir d'une équation dans C
        ///// </summary>
        ///// <param name="equation"></param>
        //private void FractaleFromComp(comp equation)
        //{
        //    double scale = tailleFractJulia * tailleFractJulia / Math.Min(this.image.Height, this.image.Width);

        //    bool equationCorrecte = equation != 0;

        //    for (int i = 0; i < this.image.Height; i++)
        //    {
        //        double im = (this.image.Height / 2 - i) * scale;

        //        for (int j = 0; j < this.image.Width; j++)
        //        {
        //            double re = (j - this.image.Width / 2) * scale;

        //            double color = FractaleColor(new comp(re, im), equationCorrecte ? equation : new comp(re, im));

        //            this.image[i, j] = GetPixel(color);

        //        }
        //    }
        //}

        /// <summary>
        /// Forme fractale générée à partir d'une équation dans C
        /// </summary>
        /// <param name="equation"></param>
        private void FractaleFromCompBeta(comp equation)
        {
            bool equationCorrecte = equation != 0;

            double incrRe = this.PositionJulia.Width / (this.image.Width);
            double incrIm = this.PositionJulia.Height / (this.image.Height);

            double im = this.PositionJulia.Y;

            for (int i = 0; i < this.image.Height; i++)
            {
                double re = this.PositionJulia.X;

                for (int j = 0; j < this.image.Width; j++)
                {
                    double color = FractaleColor(new comp(re, im), equationCorrecte ? equation : new comp(re, im));

                    this.image[i, j] = GetPixel(color);

                    re += incrRe;
                }

                im += incrIm;
            }
        }


        /// <summary>
        /// Renvoie un nombre issu du calcul de fractale pour la position donnée avec l'équation indiquée
        /// </summary>
        /// <param name="position"></param>
        /// <param name="equation"></param>
        /// <returns></returns>
        private double FractaleColor(comp position, comp equation)
        {
            int iteration = 0;
            comp z = 0;

            if (this.Type == Fractales.UserEquationZSin)
            {
                z = GetComplexNumberZCos(position, equation, ref iteration);
            }
            else if (this.Type == Fractales.UserEquationNewton)
            {
                z = GetComplexNumberNewton(position, equation, ref iteration);
            }
            else
            {
                z = GetComplexNumberZSquare(position, equation, ref iteration);
            }

            if (iteration < maxIterations)
            {
                for (int i = 0; i < 3; i++)  //Correction pour plus de fluidité
                {
                    z = z * z + equation;
                    iteration++;
                }

                double mu = iteration + 1 - Math.Log(Math.Log(z.GetModule())) / Math.Log(NormeMaxSquare);

                mu = mu / maxIterations * colors.Count;

                return mu;
            }

            return 0;
        }

        double NormeMaxSquare = 0;
        private comp GetComplexNumberZSquare(comp z, comp c, ref int itération)
        {
            double modSquared = z.GetModuleCarré();
            if (modSquared >= NormeMaxSquare || itération >= maxIterations)
                return z;

            z = z * z + c;

            itération++;

            return GetComplexNumberZSquare(z, c, ref itération);
        }

        private comp GetComplexNumberZCos(comp z, comp c, ref int itération)
        {
            if (z.GetModule() >= NormeMaxSquare || itération >= this.maxIterations)
                return z;

            z = comp.Sin(z) * c;

            itération++;

            return GetComplexNumberZCos(z, c, ref itération);
        }

        private const double pow = 4;
        private comp GetComplexNumberNewton(comp z, comp c, ref int itération)
        {
            if (c.GetModule() < 0.00000001 || itération >= 31)
                return c;
            //zn+1 = zn - x * (f(z)/f'(z)), f(z) = z^pow, x = 1->0.5
            z = comp.Pow(c, pow) - 2;
            c -= 1 * z / (pow * comp.Pow(c, pow));
            itération++;

            itération++;

            return GetComplexNumberZCos(z, c, ref itération);
        }


        /// <summary>
        /// Renvoie une couleur à partir d'un nombre
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private Pixel GetPixel(double value)
        {
            if ((value = Math.Abs(value)) == 0)
                return Pixel.Zero; //Noir, appartient à la fractale

            int clr = ((int)value) % colors.Count;
            double c2 = value - clr;
            double c1 = 1 - c2;

            clr %= colors.Count;
            int nextClr = Math.Abs((clr + 1) % colors.Count); //Couleur d'après

            byte r = (byte)(colors[clr].R * c1 + colors[nextClr].R * c2);
            byte g = (byte)(colors[clr].G * c1 + colors[nextClr].G * c2);
            byte b = (byte)(colors[clr].B * c1 + colors[nextClr].B * c2);

            r = (byte)(r * this.contrastValue);
            g = (byte)(g * this.contrastValue);
            b = (byte)(b * this.contrastValue);

            return Pixel.FromArgb(r, g, b);

            //return Pixel.FromRGB(0, 0, (byte)Math.Min(value * 1000.0 * this.contrastValue / this.maxIterations, 255));  //Nuance de couleurs rgb
        }



        /// <summary>
        /// Autres formes fractales générées de manière récursive
        /// </summary>
        private void FractaleFromForme()
        {
            //Triangles et flocon

            if (this.FracType == Fractales.Sierpinski_Triangle || this.FracType == Fractales.Koch_Flocon || this.FracType == Fractales.Koch_Flocon_Profondeur)
            {
                double décalage = 1.5;
                if (this.FracType == Fractales.Koch_Flocon || this.FracType == Fractales.Koch_Flocon_Profondeur)
                    décalage = 20;
                double hauteurTriangle = this.image.Height * (100 - décalage * 2) / 100.0;
                double largeurTriangle = this.image.Width * (100 - décalage * 2) / 100.0;

                Point positionBottomLeft = new Point(this.height - this.height * décalage / 100, (this.width - largeurTriangle) / 2);
                Point positionBottomRight = new Point(this.height - this.height * décalage / 100, positionBottomLeft.X + largeurTriangle);
                Point positionTop = new Point((positionBottomLeft.Y - hauteurTriangle) / 2, (positionBottomLeft.X + positionBottomRight.X) / 2);

                this.graph.Remplissage(this.BackgroundColor);

                if (this.FracType == Fractales.Koch_Flocon || this.FracType == Fractales.Koch_Flocon_Profondeur)
                {
                    this.graph.PixelRemplissage = Pixel.FromColor(Couleurs.Bleu_Canard);
                    KochFlocon(positionTop, positionBottomRight, this.maxIterations);
                    this.graph.PixelRemplissage = Pixel.FromColor(Couleurs.Rose);
                    KochFlocon(positionBottomRight, positionBottomLeft, this.maxIterations);
                    this.graph.PixelRemplissage = Pixel.FromColor(Couleurs.Vert2);
                    KochFlocon(positionBottomLeft, positionTop, this.maxIterations);

                    if (this.FracType == Fractales.Koch_Flocon_Profondeur)
                    {
                        Point bot = new Point(positionBottomLeft.Y, positionTop.X);
                        Point topLeft = new Point((positionBottomLeft.Y + positionTop.Y) / 2, (positionBottomLeft.X + positionTop.X) / 2);
                        Point topRight = new Point((positionBottomRight.Y + positionTop.Y) / 2, (positionBottomRight.X + positionTop.X) / 2);

                        for (int i = 0; i < 6; i++)
                        {
                            if (i % 2 == 0)
                            {
                                this.graph.PixelRemplissage = Pixel.FromColor(Couleurs.Bleu_Marine);
                                KochFlocon(bot, topLeft, this.maxIterations);
                                this.graph.PixelRemplissage = Pixel.FromColor(Couleurs.Bleu_Marine);
                                KochFlocon(topRight, bot, this.maxIterations);
                                this.graph.PixelRemplissage = Pixel.FromColor(Couleurs.Bleu_Marine);
                                KochFlocon(topLeft, topRight, this.maxIterations);

                                Point botCopie = bot;
                                bot = new Point(topLeft.Y, bot.X);
                                topLeft = new Point((topLeft.Y + botCopie.Y) / 2, (topLeft.X + botCopie.X) / 2);
                                topRight = new Point((topRight.Y + botCopie.Y) / 2, (topRight.X + botCopie.X) / 2);

                            }
                            else
                            {
                                this.graph.PixelRemplissage = Pixel.FromColor(Couleurs.Vert_Foncé);
                                KochFlocon(topLeft, bot, this.maxIterations);
                                this.graph.PixelRemplissage = Pixel.FromColor(Couleurs.Vert_Foncé);
                                KochFlocon(bot, topRight, this.maxIterations);
                                this.graph.PixelRemplissage = Pixel.FromColor(Couleurs.Vert_Foncé);
                                KochFlocon(topRight, topLeft, this.maxIterations);

                                Point botCopie = bot;
                                bot = new Point(topLeft.Y, bot.X);
                                topLeft = new Point((topLeft.Y + botCopie.Y) / 2, (topLeft.X + botCopie.X) / 2);
                                topRight = new Point((topRight.Y + botCopie.Y) / 2, (topRight.X + botCopie.X) / 2);

                            }
                        }
                    }
                }
                else
                    TriangleRécursivité(this.maxIterations, positionTop, positionBottomLeft, positionBottomRight);
            }


            //Tapis

            if (this.FracType == Fractales.Sierpinski_Tapis)
            {
                double largeurCarré = this.image.Height * 97.0 / 100;
                double longueurCarré = this.image.Width * 97.0 / 100;

                Point topLeftSquare = new Point(this.image.Height * 1.5 / 100.0, this.image.Width * 1.5 / 100.0);
                this.graph.Remplissage(this.BackgroundColor);
                CarréRécursivité(this.maxIterations, topLeftSquare, largeurCarré, longueurCarré);
            }


            //Cercles

            if (this.FracType == Fractales.Cercle_1)
            {
                this.graph.Remplissage(this.BackgroundColor);

                CercleRec(Math.Min(this.image.Width, this.image.Height) / 2, 0);

            }

            if (this.FracType == Fractales.Cercle_2)
            {
                this.graph.Remplissage(this.BackgroundColor);

                CercleForme(new Point(this.image.Height / 2, this.image.Width / 2), Math.Min(this.image.Width, this.image.Height) / 2);
            }


            //Kaléi et mosaiques

            if (this.FracType == Fractales.Kaléidoscope)
            {
                Kaléidoscope();
            }

            if (this.FracType == Fractales.Mosaique_Kaléïdoscope)
            {
                this.graph.Remplissage(this.BackgroundColor);
                Mosaique();
            }

            if (this.FracType == Fractales.Mosaique_From_Image)
            {
                if (this.imageKaléidocope == null || !this.imageKaléidocope.Validité)
                    return;
                Mosaique();
            }


            //Arbres

            if (this.FracType == Fractales.Arbre_Descendant || this.FracType == Fractales.Arbre_4_Cotés)
            {
                while (Math.Min(this.image.Height, this.image.Width) / this.maxIterations <= 50)
                    this.maxIterations--;

                this.graph.Remplissage(this.BackgroundColor);
                ArbreRécursivité(new Point(0, (this.image.Width - 1) / 2), 90, this.maxIterations, Pixel.FromColor(Couleurs.Noir));

                if (this.FracType == Fractales.Arbre_4_Cotés)
                {
                    ArbreRécursivité(new Point(this.image.Height - 1, (this.image.Width - 1) / 2), -90, this.maxIterations, Pixel.FromColor(Couleurs.Vert_Foncé));
                    ArbreRécursivité(new Point((this.image.Height - 1) / 2, 0), 0, this.maxIterations, Pixel.FromColor(Couleurs.Bleu_Marine));
                    ArbreRécursivité(new Point((this.image.Height - 1) / 2, this.image.Width - 1), 180, this.maxIterations, Pixel.FromColor(Couleurs.Magenta));


                    ArbreRécursivité(new Point(this.image.Height - 1, this.image.Width - 1), -135, this.maxIterations, Pixel.FromColor(Couleurs.Bleu_Ciel1));
                    ArbreRécursivité(new Point(0, 0), 45, this.maxIterations, Pixel.FromColor(Couleurs.Vert));
                    ArbreRécursivité(new Point(this.image.Height - 1, 0), -45, this.maxIterations, Pixel.FromColor(Couleurs.Rouge));
                    ArbreRécursivité(new Point(0, this.image.Width - 1), 135, this.maxIterations, Pixel.FromColor(Couleurs.Jaune_Orangé));
                }
            }
        }



        /// <summary>
        /// Dessine des triangles de manière récursive selon la forme de Sierpinski
        /// </summary>
        /// <param name="niveau"></param>
        /// <param name="positionTop"></param>
        /// <param name="positionBottomLeft"></param>
        /// <param name="positionBottomRight"></param>
        /// <param name="color"></param>
        private void TriangleRécursivité(int niveau, Point positionTop, Point positionBottomLeft, Point positionBottomRight, Pixel? color = null)
        {
            if (niveau <= 0)
            {
                graph.FillTriangle(positionTop, positionBottomRight, positionBottomLeft, (Pixel)color);
                return;
            }

            Point newTopLeft = new Point((positionTop.Y + positionBottomLeft.Y) / 2, (positionTop.X + positionBottomLeft.X) / 2);
            Point newTopRight = new Point((positionTop.Y + positionBottomRight.Y) / 2, (positionTop.X + positionBottomRight.X) / 2);
            Point newBottom = new Point((positionBottomLeft.Y + positionBottomRight.Y) / 2, (positionBottomLeft.X + positionBottomRight.X) / 2);

            TriangleRécursivité(niveau - 1, positionTop, newTopLeft, newTopRight, Pixel.FromColor(Couleurs.Rose_Foncé));
            TriangleRécursivité(niveau - 1, newTopLeft, positionBottomLeft, newBottom, Pixel.FromColor(Couleurs.Vert_Foncé));
            TriangleRécursivité(niveau - 1, newTopRight, newBottom, positionBottomRight, Pixel.FromColor(Couleurs.Bleu_Marine));
        }

        /// <summary>
        /// Dessine des carrés de manière récursive selon la forme de Sierpinski
        /// </summary>
        /// <param name="niveau"></param>
        /// <param name="positionTopLeft"></param>
        /// <param name="largeur"></param>
        /// <param name="longueur"></param>
        private void CarréRécursivité(int niveau, Point positionTopLeft, double largeur, double longueur)
        {
            if (niveau <= 0)
            {
                graph.FillRectangle(positionTopLeft, new Point(positionTopLeft.Y + largeur, positionTopLeft.X + longueur), Pixel.FromColor(Couleurs.Noir_Clair));
                return;
            }

            double newLargeur = largeur / 3.0;
            double newLongueur = longueur / 3.0;

            graph.FillRectangle(new Point(positionTopLeft.Y + newLargeur, positionTopLeft.X + newLongueur),
                new Point(positionTopLeft.Y + (2 * newLargeur), positionTopLeft.X + (2 * newLongueur)), this.BackgroundColor);

            CarréRécursivité(niveau - 1, positionTopLeft, newLargeur, newLongueur);

            CarréRécursivité(niveau - 1, new Point(positionTopLeft.Y, positionTopLeft.X + newLongueur), newLargeur, newLongueur);

            CarréRécursivité(niveau - 1, new Point(positionTopLeft.Y, positionTopLeft.X + (2 * newLongueur)), newLargeur, newLongueur);

            CarréRécursivité(niveau - 1, new Point(positionTopLeft.Y + newLargeur, positionTopLeft.X), newLargeur, newLongueur);

            CarréRécursivité(niveau - 1, new Point(positionTopLeft.Y + newLargeur, positionTopLeft.X + (2 * newLongueur)), newLargeur, newLongueur);

            CarréRécursivité(niveau - 1, new Point(positionTopLeft.Y + (2 * newLargeur), positionTopLeft.X), newLargeur, newLongueur);

            CarréRécursivité(niveau - 1, new Point(positionTopLeft.Y + (2 * newLargeur), positionTopLeft.X + newLongueur), newLargeur, newLongueur);

            CarréRécursivité(niveau - 1, new Point(positionTopLeft.Y + (2 * newLargeur), positionTopLeft.X + (2 * newLongueur)), newLargeur, newLongueur);

        }

        /// <summary>
        /// Dessine des triangles de manière récursive selon la forme du flocon de Kosh
        /// </summary>
        /// <param name="niveau"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        private void KochFlocon(Point p1, Point p2, int niveau)
        {
            //3 points à trouver pour 4 lignes 
            if (niveau <= 0)
            {
                this.graph.DrawLineBresenham(p1, p2);
                return;
            }

            Point p3 = new Point(p1.Y + ((p2.Y - p1.Y) / 3), p1.X + ((p2.X - p1.X) / 3));
            Point p5 = new Point(p1.Y + (2 * (p2.Y - p1.Y) / 3), p1.X + (2 * (p2.X - p1.X) / 3));

            double cos = Math.Cos(Math.PI * 60.0 / 180.0);
            double sin = Math.Sin(Math.PI * 60.0 / 180.0);
            Point p4 = new Point(((p3.Y + p5.Y) * cos) + ((p3.X - p5.X) * sin), ((p3.X + p5.X) * cos) - ((p3.Y - p5.Y) * sin));

            KochFlocon(p1, p3, niveau - 1);
            KochFlocon(p3, p4, niveau - 1);
            KochFlocon(p4, p5, niveau - 1);
            KochFlocon(p5, p2, niveau - 1);
        }

        /// <summary>
        /// Dessine un arbre de manière récursive
        /// </summary>
        /// <param name="p"></param>
        /// <param name="angle"></param>
        /// <param name="profondeur"></param>
        /// <param name="couleur"></param>
        private void ArbreRécursivité(Point p, int angle, int profondeur, Pixel couleur)
        {
            if (profondeur <= 0)
                return;

            Point newP = new Point(p.Y + Math.Sin(angle * Math.PI / 180) * profondeur * 10,
                p.X + Math.Cos(angle * Math.PI / 180) * profondeur * 10);

            ArbreRécursivité(newP, angle + 30, profondeur - 1, couleur);
            ArbreRécursivité(newP, angle - 30, profondeur - 1, couleur);

            this.graph.DrawLineBresenham(p, newP, couleur);
        }


        /// <summary>
        /// Dessine des cercles de manière récursive de plus en plus petit avec le même centre à chaque génération
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="nuanceCoul"></param>
        private void CercleRec(int radius, double nuanceCoul)
        {
            this.graph.DrawCircleBresenham(new Point(this.graph.MyImage.Height / 2, this.graph.MyImage.Width / 2), radius, Pixel.FromArgb(Math.Min((int)nuanceCoul, 255), Math.Min((int)nuanceCoul + 60, 255), Math.Max((int)(254 + 119 - nuanceCoul), 0)));
            if (radius <= 20)
            {
                this.graph.FillCircle(new Point(this.graph.MyImage.Height / 2, this.graph.MyImage.Width / 2), radius, Pixel.FromArgb(255, 255, 0));
                return;
            }
            CercleRec((int)((double)(radius * 98) / 100), nuanceCoul + 2);
        }

        /// <summary>
        /// Dessine des cercles de manière récursive
        /// </summary>
        /// <param name="p"></param>
        /// <param name="rayon"></param>
        private void CercleForme(Point p, float rayon)
        {
            if (rayon > 8)
            {
                CercleForme(new Point(p.Y, p.X + rayon / 2), rayon / 2);
                CercleForme(new Point(p.Y, p.X - rayon / 2), rayon / 2);
                CercleForme(new Point(p.Y + rayon / 2, p.X), rayon / 2);
                CercleForme(new Point(p.Y - rayon / 2, p.X), rayon / 2);
            }
            Pixel color;
            if (rayon > 40 && rayon <= 100)
                color = Pixel.FromArgb(rand.Next(0, 255 - (int)rayon), rand.Next(0, 127), rand.Next(0, (int)rayon));
            else if (rayon > 100 && rayon < 255)
                color = Pixel.FromArgb(rand.Next(0, (int)rayon), rand.Next((int)rayon, 255), rand.Next((int)rayon / 8, 255));
            else
                color = Pixel.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));


            this.graph.DrawCircleBresenham(p, (int)rayon, color);
        }


        /// <summary>
        /// Générateur de kaléidoscope aléatoire (formes : triangle et rond)
        /// </summary>
        private void Kaléidoscope()
        {
            int amountClr = Enum.GetValues(typeof(Couleurs)).Cast<int>().Max() + 1;

            //Loi binomiale B(maxtrycolor - 1, 1 / amountClr)

            double probaBinoNP = 1 - Formats.LoiBinomiale(this.maxTryColor - 1, 0, (double)1 / amountClr); //Au moins un bon essai, P(X >= 1) = 1 - P(X=0)

            int probaScale1000 = (int)Math.Round(probaBinoNP * 1000);

            int color;

            //Remplissage arrière plan
            if (this.couleurDominante != null)
            {
                if (rand.Next(0, 1000) < probaScale1000)
                    color = (int)this.couleurDominante;
                else
                    color = rand.Next(0, amountClr);

                this.graph.Remplissage((Pixel)(Couleurs)color);
            }
            else
            {
                this.graph.Remplissage((Pixel)(Couleurs)this.rand.Next(0, amountClr));
            }

            //Remplissage formes
            for (int i = 0; i < this.maxIterations; i++)
            {
                Point p1 = new Point(rand.Next(0, this.image.Height), rand.Next(0, this.image.Width));

                if (this.couleurDominante == null)
                {
                    color = rand.Next(0, amountClr);
                }
                else
                {
                    if (rand.Next(0, 1000) < probaScale1000)
                        color = (int)this.couleurDominante;
                    else
                        color = rand.Next(0, amountClr);
                }

                Pixel p0 = Pixel.FromColor((Couleurs)color);

                if (rand.Next(0, 10) >= 5) //Triangle
                {
                    Point p2 = new Point(rand.Next(0, this.image.Height / 4), rand.Next(0, this.image.Width / 4));
                    Point p3 = new Point(rand.Next(0, this.image.Height / 4), rand.Next(0, this.image.Width / 4));

                    //On fait viser le triangle vers le centre de l'image
                    if (p1.X > p1.Y)
                    {
                        p1.SwapXY();
                    }
                    if (p2.X > p2.Y)
                    {
                        p2.SwapXY();
                    }
                    if (p3.X > p3.Y)
                    {
                        p3.SwapXY();
                    }


                    this.graph.FillTriangle(p1, p2, p3, p0);

                    Point newP1 = p1;
                    Point newP2 = p2;
                    Point newP3 = p3;

                    newP1.X = this.image.Width - p1.X;
                    newP2.X = this.image.Width - p2.X;
                    newP3.X = this.image.Width - p3.X;

                    this.graph.FillTriangle(newP1, newP2, newP3, p0);

                    newP1 = p1;
                    newP2 = p2;
                    newP3 = p3;

                    newP1.Y = this.image.Height - p1.Y;
                    newP2.Y = this.image.Height - p2.Y;
                    newP3.Y = this.image.Height - p3.Y;

                    this.graph.FillTriangle(newP1, newP2, newP3, p0);

                    newP1 = p1;
                    newP2 = p2;
                    newP3 = p3;

                    newP1.X = this.image.Width - p1.X;
                    newP2.X = this.image.Width - p2.X;
                    newP3.X = this.image.Width - p3.X;
                    newP1.Y = this.image.Height - p1.Y;
                    newP2.Y = this.image.Height - p2.Y;
                    newP3.Y = this.image.Height - p3.Y;

                    this.graph.FillTriangle(newP1, newP2, newP3, p0);

                    newP1 = p1;
                    newP2 = p2;
                    newP3 = p3;

                    newP1.X = p1.Y;
                    newP1.Y = p1.X;
                    newP2.X = p2.Y;
                    newP2.Y = p2.X;
                    newP3.X = p3.Y;
                    newP3.Y = p3.X;

                    this.graph.FillTriangle(newP1, newP2, newP3, p0);

                    newP1 = p1;
                    newP2 = p2;
                    newP3 = p3;

                    newP1.X = this.image.Height - p1.Y;
                    newP1.Y = p1.X;
                    newP2.X = this.image.Height - p2.Y;
                    newP2.Y = p2.X;
                    newP3.X = this.image.Height - p3.Y;
                    newP3.Y = p3.X;

                    this.graph.FillTriangle(newP1, newP2, newP3, p0);

                    newP1 = p1;
                    newP2 = p2;
                    newP3 = p3;

                    newP1.X = p1.Y;
                    newP1.Y = this.image.Width - p1.X;
                    newP2.X = p2.Y;
                    newP2.Y = this.image.Width - p2.X;
                    newP3.X = p3.Y;
                    newP3.Y = this.image.Width - p3.X;

                    this.graph.FillTriangle(newP1, newP2, newP3, p0);

                    newP1 = p1;
                    newP2 = p2;
                    newP3 = p3;

                    newP1.X = this.image.Height - p1.Y;
                    newP1.Y = this.image.Width - p1.X;
                    newP2.X = this.image.Height - p2.Y;
                    newP2.Y = this.image.Width - p2.X;
                    newP3.X = this.image.Height - p3.Y;
                    newP3.Y = this.image.Width - p3.X;

                    this.graph.FillTriangle(newP1, newP2, newP3, p0);
                }
                else //Cercle
                {
                    Point newP1 = p1;
                    int rayon;
                    int min = Math.Min(this.image.Height, this.image.Width);

                    if (min < 110)
                    {
                        rayon = rand.Next(0, min / 5);
                    }
                    else
                    {
                        rayon = rand.Next(10, Math.Min(this.image.Height, this.image.Width) / 10);
                    }

                    this.graph.FillCircle(newP1, rayon, p0);

                    newP1.X = image.Width - p1.X;

                    this.graph.FillCircle(newP1, rayon, p0);

                    newP1 = p1;

                    newP1.Y = image.Height - p1.Y;

                    this.graph.FillCircle(newP1, rayon, p0);
                    newP1 = p1;

                    newP1.X = image.Width - p1.X;
                    newP1.Y = image.Height - p1.Y;

                    this.graph.FillCircle(newP1, rayon, p0);
                    newP1 = p1;

                    newP1.X = p1.Y;
                    newP1.Y = p1.X;

                    this.graph.FillCircle(newP1, rayon, p0);
                    newP1 = p1;

                    newP1.X = this.image.Height - p1.Y;
                    newP1.Y = p1.X;

                    this.graph.FillCircle(newP1, rayon, p0);
                    newP1 = p1;

                    newP1.X = p1.Y;
                    newP1.Y = this.image.Width - p1.X;

                    this.graph.FillCircle(newP1, rayon, p0);

                    newP1 = p1;

                    newP1.X = this.image.Height - p1.Y;
                    newP1.Y = this.image.Width - p1.X;

                    this.graph.FillCircle(newP1, rayon, p0);
                }
            }
        }

        /// <summary>
        /// Mosaïque de kaléïdoscope. Le nombre de kaléidoscope est déterminé par <see cref="TailleMosaique"/>
        /// </summary>
        private void Mosaique()
        {
            int min = Math.Min(this.height, this.width);
            if (this.tailleMosaique > min || this.tailleMosaique <= 0)
            {
                tailleMosaique = min;
            }

            int taille = min / this.tailleMosaique;
            int height = (int)Math.Ceiling((double)this.height / taille);
            int width = (int)Math.Ceiling((double)this.width / taille);

            Fractale frac = new Fractale(taille, taille, Fractales.Kaléidoscope) //On crée les kaléidoscopes avec ce Fractale
            {
                MaxItération = this.maxIterations,
                MaxTryColor = this.maxTryColor,
                CouleurDominante = this.CouleurDominante  //Pas de couleur dominante si imageKaléi est null, on aurait pu utiliser un bool pour plus de clarté
            };

            MyGraphics gKaléi = new MyGraphics(this.imageKaléidocope);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Point hautGauche = new Point(taille * i, taille * j);

                    if (this.imageKaléidocope != null) //A partir d'une image, on prend en paramètre la couleur principale du rognage
                    {
                        gKaléi.Clip = new Rectangle((int)hautGauche.X, (int)hautGauche.Y, taille, taille);
                        MyImage im = gKaléi.GetRognage();

                        frac.CouleurDominante = Pixel.GetCouleur(MyImageStats.GetAverageColor(im));
                    }

                    frac.Draw();
                    this.graph.DrawImage(frac.MyImage, hautGauche);
                }
            }
        }


        #endregion



        /// <summary>
        /// Prend en paramètre des couleurs pour afficher des nuances
        /// </summary>
        /// <returns></returns>
        private List<Pixel> InitializeColorList()
        {
            return new List<Pixel>
            {
                Pixel.FromColor(Couleurs.Bleu_Marine),
                Pixel.FromColor(Couleurs.Indigo),
                Pixel.FromColor(Couleurs.Jaune_Orangé),
                Pixel.FromColor(Couleurs.Vert_Foncé),
                Pixel.FromColor(Couleurs.Bleu_Ciel3),
                Pixel.FromColor(Couleurs.Bleu_Marine),
                Pixel.FromColor(Couleurs.Rouge_Foncé),
            };
        }

        /// <summary>
        /// Renvoie le <see cref="comp"/> associé à ce <see cref="Fractales"/>
        /// </summary>
        /// <param name="frac"></param>
        /// <returns></returns>
        public static comp FractalesToComp(Fractales frac)
        {
            comp c = 0;

            switch (frac)
            {
                case Fractales.Mandelbrot:
                    return c;

                case Fractales.Lapin:
                    c = new comp(-0.123, 0.745);
                    return c;

                case Fractales.Ilots:
                    c = new comp(0.1, 0.65);
                    return c;

                case Fractales.Escargot_quadruple_revolution:
                    c = new comp(-0.79, 0.15); //0.3 +0.5i
                    return c;

                case Fractales.Escargot_double_revolution:
                    c = new comp(0.28, 0.008);
                    return c;

                case Fractales.Dendrite_Normal:
                    c = new comp(0, -1);
                    return c;

                case Fractales.Dentriste_Division:
                    c = new comp(-0.1, -1);
                    return c;

                case Fractales.Ilots_Serpent:
                    c = new comp(-0.231, 0.785);
                    return c;

                case Fractales.TrucStylé1: //inverse de trucstylé6
                    c = new comp(-0.39, -0.59);
                    return c;

                case Fractales.TrucStylé2:
                    c = new comp(-0.1, -0.65);
                    return c;

                case Fractales.TrucStylé3:
                    c = new comp(0.3, -0.01);
                    return c;

                case Fractales.TrucStylé4:
                    c = new comp(-0.512511498387847167, 0.521295573094847167);
                    return c;

                case Fractales.TrucStylé5:
                    c = new comp(-0.7269, 0.1889);
                    return c;

                case Fractales.TrucStylé6:
                    c = new comp(-0.4, 0.6);
                    return c;
            }

            return c;
        }


        /// <summary>
        /// Différents types de <see cref="Fractale"/> supportées
        /// </summary>
        public enum Fractales
        {
            /// <summary>
            /// Forme de Mandelbrot selon l'équation z = z² + c 
            /// </summary>
            Mandelbrot,

            /// <summary>
            /// Forme fractale selon l'équation z = z² -0.123 + 0.745i
            /// </summary>
            Lapin,

            /// <summary>
            /// Forme fractale selon l'équation z = z² -i
            /// </summary>
            Dendrite_Normal,

            /// <summary>
            /// Forme fractale selon l'équation z = z² -0.1 -i
            /// </summary>
            Dentriste_Division,

            /// <summary>
            /// Forme fractale selon l'équation z = z² + 0.1 + 0.65i
            /// </summary>
            Ilots,

            /// <summary>
            /// Forme fractale selon l'équation z = z² - 0.231 + 0.785i (mettre le contraste > 1)
            /// </summary>
            Ilots_Serpent,

            /// <summary>
            /// Forme fractale selon l'équation z = z² + 0.3 + 0.5i
            /// </summary>
            Escargot_quadruple_revolution,

            /// <summary>
            /// Forme fractale selon l'équation z = z² - 0.777 + 0.111i
            /// </summary>
            Escargot_double_revolution,

            /// <summary>
            /// Forme fractale selon l'équation z = z² -0.39 - 0.59i
            /// </summary>
            TrucStylé1,

            /// <summary>
            /// Forme fractale selon l'équation z = z² - 0.1 - 0.65i
            /// </summary>
            TrucStylé2,

            /// <summary>
            /// Forme fractale selon l'équation z = z² - 3/4
            /// </summary>
            TrucStylé3,

            /// <summary>
            /// Forme fractale selon l'équation z = z² - -0.512511498387 +  0.5212955730948i
            /// </summary>
            TrucStylé4,

            /// <summary>
            /// Forme fractale selon l'équation z = z² - -0.512511498387 +  0.5212955730948i
            /// </summary>
            TrucStylé5,

            /// <summary>
            /// Forme fractale selon l'équation z = z² - -0.512511498387 +  0.5212955730948i
            /// </summary>
            TrucStylé6,


            /// <summary>
            /// Equation de l'utilisateur de type z = z² + {nombre complexe de l'utilisateur}
            /// </summary>
            UserEquationNewton,

            /// <summary>
            /// Equation de l'utilisateur de type z = sin(z) * {nombre complexe de l'utilisateur}
            /// </summary>
            UserEquationZSin,

            /// <summary>
            /// Equation de l'utilisateur de type z = z² + {nombre complexe de l'utilisateur}
            /// </summary>
            UserEquationZSqrd,



            /// <summary>
            /// Forme fractale générée de manière récursive
            /// </summary>
            Sierpinski_Triangle,

            /// <summary>
            /// Forme fractale générée de manière récursive
            /// </summary>
            Sierpinski_Tapis,

            /// <summary>
            /// Forme fractale générée de manière récursive
            /// </summary>
            Koch_Flocon,

            /// <summary>
            /// Forme fractale générée de manière récursive
            /// </summary>
            Koch_Flocon_Profondeur,

            /// <summary>
            /// Forme fractale générée de manière récursive
            /// </summary>
            Arbre_Descendant,

            /// <summary>
            /// Forme fractale générée de manière récursive
            /// </summary>
            Arbre_4_Cotés,

            /// <summary>
            /// Forme fractale générée de manière récursive
            /// </summary>
            Cercle_1,

            /// <summary>
            /// Forme fractale générée de manière récursive
            /// </summary>
            Cercle_2,

            /// <summary>
            /// Forme générée aléatoirement
            /// </summary>
            Kaléidoscope,

            /// <summary>
            /// Mosaïque de formes générées aléatoirement
            /// </summary>
            Mosaique_Kaléïdoscope,

            /// <summary>
            /// Mosaique de formes générées aléatoirement à partir d'une <see cref="Photoshop3000.MyImage"/>. Utiliser le constructeur avec un <see cref="Photoshop3000.MyImage"/> en paramètre
            /// </summary>
            Mosaique_From_Image,
        }
    }
}
