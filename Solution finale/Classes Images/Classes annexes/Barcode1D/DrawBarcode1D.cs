using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = Photoshop3000.Point;

namespace Photoshop3000.Barcode
{
    /// <summary>
    /// Fournit des méthodes pour dessiner des code-barres GTIN (EAN-13, UPCA, UPCE, EAN-8)
    /// </summary>
    class DrawBarcode1D : Barcode1D
    {
        //Champs et propriétés

        private int largeur = 1000, hauteur = 500;

        private int barLength = 0;


        /// <summary>
        /// Taille de l'image
        /// </summary>
        public Size Size
        {
            get => new Size(largeur, hauteur);
            set
            {
                if (value.Width < this.Data.Barcode.BarAmount + 50)
                {
                    this.largeur = this.Data.Barcode.BarAmount + 50;
                }
                else
                {
                    this.largeur = value.Width;
                }
                if (value.Height < 50)
                {
                    this.hauteur = 50;
                }
                else
                {
                    this.hauteur = value.Height;
                }
            }
        }

        /// <summary>
        /// Largeur d'une barre en pixel, mettre à 0 pour que les barres aient la largeur maximale pour remplir l'image.
        /// </summary>
        public int BarWidth
        {
            get => this.barLength;
            set => this.barLength = value > 0 ? value : 0;
        }

        /// <summary>
        /// Couleur d'arrière plan (blanc par défaut, recommandé)
        /// </summary>
        public Pixel BackgroundClr { get; set; } = Pixel.FromColor(Couleurs.Blanc);

        /// <summary>
        /// Couleur des barres (noir par défaut, recommandé)
        /// </summary>
        public Pixel ForegroundClr { get; set; } = Pixel.FromColor(Couleurs.Noir);

        /// <summary>
        /// Si le code-barres à dessiner est un EAN-13 ou EAN-8, dessine ou non une petite flèche à la fin et/ou au début de celui-ci.
        /// </summary>
        public bool DrawFlecheEAN { get; set; } = true;

        /// <summary>
        /// Spécifie si les charactères correspondants au code-barres sont écris en dessous de celui-ci.
        /// Peut prendre un certain temps à dessiner en fonction de la taille de l'image
        /// </summary>
        public bool DrawText { get; set; } = true;


        //Constructeur

        /// <summary>
        /// Initialise une nouvelle instance à partir d'un <see cref="Barcode1D.BarcodeData"/> à dessiner.
        /// </summary>
        /// <param name="data"></param>
        public DrawBarcode1D(BarcodeData data)
            : base(null, data)
        {
        }


        //Méthodes

        /// <summary>
        /// Dessine un code-barres à partir d'un <see cref="Barcode1D.BarcodeData"/> valide.
        /// </summary>
        /// <returns></returns>
        public void Draw()
        {
            if (!this.Data.EstValide)
            {
                this.Image = null;
                return;
            }

            MyImage barcode = new MyImage(this.hauteur, this.largeur, this.BackgroundClr);
            MyGraphics g = new MyGraphics(barcode);


            int lengthBar = GetLengthBar(this.barLength);
            int totalLength = this.Data.Barcode.BarAmount * lengthBar;


            int debutW = (barcode.Width - totalLength) / 2;
            int finW = debutW + totalLength;

            int debutH = (int)((float)barcode.Height * (100 - hauteurBarPourc) / 2 / 100);

            int finH = (int)((float)barcode.Height * hauteurBarPourc / 100);
            int finHG = finH + hauteurBarGuardPourc * totalLength / 100;

            int lguardPosW = 0, cguardPosW = 0, rguardPosW = 0; //Pos des guard dans l'image pour le dessin des chiffres

            byte[] toDraw = GetEncodedBits();

            int startLeft = this.Data.Barcode.LGuard_LENGTH + this.Data.Barcode.LGuard_POS;
            int endRight = this.Data.Barcode.RGuard_POS - 1;

            for (int i = 0; i < toDraw.Length; i++)
            {
                int hauteurMax = finH;

                //les 2 conditions servent à déterminer l'emplacement des barres de controles.
                if (i < startLeft || i > endRight || (i >= this.Data.Barcode.CGuard_POS && i < this.Data.Barcode.CGuard_POS + this.Data.Barcode.CGuard_LENGTH))
                {
                    if (i == this.Data.Barcode.LGuard_POS)
                        lguardPosW = debutW + (i * lengthBar) + 1;
                    if (i == this.Data.Barcode.CGuard_POS)
                        cguardPosW = debutW + (i * lengthBar) + 1;
                    if (i == this.Data.Barcode.RGuard_POS)
                        rguardPosW = debutW + (i * lengthBar) + 1;

                    hauteurMax = finHG;
                }
                else if (this.Data.Barcode == "upca")
                {
                    if ((i >= startLeft && i < startLeft + this.Data.Barcode.BarPerDigit)
                    || (i > endRight - this.Data.Barcode.BarPerDigit && i <= endRight))
                    {
                        hauteurMax = finHG;
                    }
                }

                Point debut = new Point(debutH, debutW + (i * lengthBar));

                Point fin = new Point(hauteurMax, debutW + ((i + 1) * lengthBar));

                g.FillRectangle(debut, fin, toDraw[i] == 1 ? this.ForegroundClr : this.BackgroundClr);
            }


            if (this.DrawText) //On dessine les nombres et symboles en dessous du code-barres.
            {
                string stringToEncode = this.Data.GetScanCodeToEncode;

                int firstDigit = Convert.ToInt32(this.Data.FirstDigit);
                int lastDigit = Convert.ToInt32(this.Data.ScanCode[this.Data.ScanCode.Length - 1].ToString());

                Bitmap bmp = barcode.ToBitmap();
                Graphics graph = Graphics.FromImage(bmp); //Dessiner les nombres
                graph.TextRenderingHint = TextRenderingHint.AntiAlias;

                Brush brush = new SolidBrush(this.ForegroundClr.ToColor());
                Font f = new Font(new FontFamily(GenericFontFamilies.SansSerif), 75, FontStyle.Regular, GraphicsUnit.Pixel);

                if (this.Data.Barcode == BarcodeTypes.EAN13)
                {
                    f = DrawStringInRange(graph, f, brush, stringToEncode.Remove(6), new PointF(lguardPosW + (this.Data.Barcode.LGuard_LENGTH * lengthBar) + 1, finH), new PointF(cguardPosW, finH), (int)((finHG - finH) * rapportHauteurMax));

                    f = DrawStringInRange(graph, f, brush, stringToEncode.Remove(0, 6), new PointF(cguardPosW + (this.Data.Barcode.CGuard_LENGTH * lengthBar) + 1, finH), new PointF(rguardPosW, finH), (int)((finHG - finH) * rapportHauteurMax));

                    graph.DrawString(firstDigit.ToString(), f, brush, new PointF(debutW - graph.MeasureString(firstDigit.ToString(), f).Width, finH));

                    if (this.DrawFlecheEAN)
                    {
                        graph.DrawString('\uFF1E'.ToString(), f, brush, new PointF(finW, finH));
                    }
                }
                else if (this.Data.Barcode == BarcodeTypes.UPCA)
                {
                    string left = stringToEncode.Remove(6);
                    f = DrawStringInRange(graph, f, brush, left.Remove(0, 1), new PointF(lguardPosW + (this.Data.Barcode.LGuard_LENGTH * lengthBar) + 1 + (7 * lengthBar), finH), new PointF(cguardPosW, finH), (int)((finHG - finH) * rapportHauteurMax));

                    string right = stringToEncode.Remove(0, 6);
                    f = DrawStringInRange(graph, f, brush, right.Remove(right.Length - 1), new PointF(cguardPosW + (this.Data.Barcode.CGuard_LENGTH * lengthBar) + 1, finH), new PointF(rguardPosW - (7 * lengthBar), finH), (int)((finHG - finH) * rapportHauteurMax));

                    graph.DrawString(stringToEncode.First().ToString(), f, brush, new PointF(debutW - graph.MeasureString(stringToEncode.First().ToString(), f).Width, finH));

                    graph.DrawString(stringToEncode.Last().ToString(), f, brush, new PointF(debutW + totalLength, finH));

                }
                else if (this.Data.Barcode == BarcodeTypes.UPCE)
                {
                    f = DrawStringInRange(graph, f, brush, stringToEncode, new PointF(lguardPosW + (this.Data.Barcode.LGuard_LENGTH * lengthBar) + 1, finH), new PointF(cguardPosW, finH), (int)((finHG - finH) * rapportHauteurMax));

                    graph.DrawString(firstDigit.ToString(), f, brush, new PointF(debutW - graph.MeasureString(firstDigit.ToString(), f).Width, finH));

                    graph.DrawString(lastDigit.ToString(), f, brush, new PointF(debutW + totalLength, finH));
                }
                else if (this.Data.Barcode == BarcodeTypes.EAN8)
                {
                    f = DrawStringInRange(graph, f, brush, stringToEncode.Remove(4), new PointF(lguardPosW + (this.Data.Barcode.LGuard_LENGTH * lengthBar) + 1, finH), new PointF(cguardPosW, finH), (int)((finHG - finH) * rapportHauteurMax));

                    DrawStringInRange(graph, f, brush, stringToEncode.Remove(0, 4), new PointF(cguardPosW + (this.Data.Barcode.CGuard_LENGTH * lengthBar) + 1, finH), new PointF(rguardPosW, finH), (int)((finHG - finH) * rapportHauteurMax));

                    if (this.DrawFlecheEAN)
                    {
                        graph.DrawString(">", f, brush, new PointF(finW, finH));
                        graph.DrawString("<", f, brush, new PointF(debutW - graph.MeasureString("<", f).Width, finH));
                    }
                }

                this.Image = MyImage.FromBitmap(bmp);

                graph.Dispose(); //on libère tous les objets graphiques.
                bmp.Dispose();
                f.Dispose();
                brush.Dispose();
            }
            else
            {
                this.Image = barcode;
            }
        }

        /// <summary>
        /// Renvoie la valeur de largeur d'une barre en fonction de la taille de l'image
        /// </summary>
        /// <param name="barlength"></param>
        /// <returns></returns>
        private int GetLengthBar(int barlength)
        {
            if (barlength <= 0)
            {
                return (int)((float)tailleTotalePourc / 100 * this.largeur / this.Data.Barcode.BarAmount);
            }
            else
            {
                return barlength * this.Data.Barcode.BarAmount >= (float)tailleTotalePourc / 100 * this.largeur ? GetLengthBar(0) : barlength;
            }
        }

        /// <summary>
        /// Dessine les nombres en dessous des barres
        /// </summary>
        /// <param name="g"></param>
        /// <param name="f"></param>
        /// <param name="br"></param>
        /// <param name="str"></param>
        /// <param name="debut"></param>
        /// <param name="fin"></param>
        /// <param name="hauteurMax"></param>
        /// <returns></returns>
        private Font DrawStringInRange(Graphics g, Font f, Brush br, string str, PointF debut, PointF fin, int hauteurMax)
        {
            SizeF size;
            float sizeChr = 1;
            do
            {
                sizeChr++;
                size = g.MeasureString(str, new Font(f.FontFamily, sizeChr, f.Style, f.Unit));
            }
            while (size.Height < hauteurMax - 10 && size.Width < (fin.X - debut.X - 10));

            f = new Font(f.FontFamily, sizeChr - 1, f.Style, f.Unit);

            int debutMid = (int)((fin.X + debut.X - size.Width) / 2);

            g.DrawString(str, f, br, new PointF(debutMid, debut.Y));

            return f;
        }


        //Constantes

        private const int tailleTotalePourc = 80;

        private const int hauteurBarPourc = 80;

        /// <summary>
        /// taille = pourcentage de la largeur de l'image
        /// </summary>
        private const int hauteurBarGuardPourc = 5;

        private const float rapportHauteurMax = 4.0f;
    }
}
