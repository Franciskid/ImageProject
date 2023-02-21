using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Drawing.Imaging;
using Photoshop3000.Annexes;
using Photoshop3000.GUI.GUI_Annexes;
using System.Diagnostics;
using System.Globalization;

namespace Photoshop3000
{
    /// <summary>
    /// Form principal de l'application
    /// </summary>
    public partial class MainWindow : Form
    {
        private List<MyImage> imagesParOnglet = new List<MyImage>();
        private List<Panel> panelsParOnglet = new List<Panel>();
        private List<MyPictureBox> pictureBoxParOnglet = new List<MyPictureBox>();

        private Bitmap closeImage;


        /// <summary>
        /// Indique si au moins une image est affichée ou chargée.
        /// </summary>
        private bool imageChargée = false;

        /// <summary>
        /// Indique si les images sont affichées avec leur taille réelle.
        /// </summary>
        private bool tailleRéelleAffichée = false;

        /// <summary>
        /// Indique si le mode nuit de l'application est activé .
        /// </summary>
        private bool modeNuitActivé = false;

        /// <summary>
        /// Représente l'onglet dans lequel l'utilisateur se situe
        /// </summary>
        private int currentIndexOnglet = 0;


        private int numéroModif = 1;


        /// <summary>
        /// Fenetre principale.
        /// </summary>
        public MainWindow()
        {
            //MyImage im = new MyImage(2000, 2000, Formats.PixelFormat.BMP_Argb32);
            //MyGraphics g = new MyGraphics(im);
            //g.Remplissage(Pixel.FromColor(Couleurs.Bleu_Canard));

            //var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            //nfi.NumberGroupSeparator = " ";

            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            //g.FiltreContraste();

            //sw.Stop();

            //MessageBox.Show("Temps en ticks : " + sw.ElapsedTicks.ToString("#,0", nfi), "Temps", MessageBoxButtons.OK);

            //Environment.Exit(0);
            //MyImage im = new MyImage(100, 100, Formats.PixelFormat.BMP_Argb32);
            ////im[2, 0] = Pixel.FromColor(Couleurs.Gris_Foncé);
            ////im[2, 1] = Pixel.FromColor(Couleurs.Gris_Foncé);
            ////im[2, 2] = Pixel.FromColor(Couleurs.Gris_Foncé);
            ////im[1, 2] = Pixel.FromColor(Couleurs.Gris_Foncé);
            ////im[0, 1] = Pixel.FromColor(Couleurs.Gris_Foncé);
            //MyGraphics g = new MyGraphics(im)
            //{
            //    InterpolationMode = InterpolationMode.Windows_NearestNeighbour,
            //    PixelRemplissage = Pixel.FromArgb(0),
            //};
            //g.FillCarré(new Point(3 + 28 + 5 + 28 + 5, 3), 28, Pixel.FromColor(Couleurs.Gris_Foncé));
            //g.FillCarré(new Point(3 + 28 + 5 + 28 + 5, 3 + 28 + 5), 28, Pixel.FromColor(Couleurs.Gris_Foncé));
            //g.FillCarré(new Point(3 + 28 + 5 + 28 + 5, 3 + 28 + 5 + 28 + 5), 28, Pixel.FromColor(Couleurs.Gris_Foncé));
            //g.FillCarré(new Point(3 + 28 + 5, 3 + 28 + 5 + 28 + 5), 28, Pixel.FromColor(Couleurs.Gris_Foncé));
            //g.FillCarré(new Point(3, 3 + 28 + 5), 28, Pixel.FromColor(Couleurs.Gris_Foncé));
            ////g.Redimensionnement(200, 200);


            //im.Save(@"C:\C#\WPF\GameOfLife\Ressources\logoTrans.png");


            InitializeComponent();
            this.menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CouleurMenuDéroulant(this.modeNuitActivé));
            this.Tabs.Controls.Add(new TabPage());
            this.Tabs.TabPages[0].AutoScroll = true;
            this.Tabs.TabPages[0].BackColor = System.Drawing.Color.Gainsboro;
            this.Tabs.TabPages[0].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Tabs.TabPages[0].Location = new System.Drawing.Point(4, 25);
            this.Tabs.TabPages[0].Name = "Originales";
            this.Tabs.TabPages[0].Padding = new Padding(3);
            this.Tabs.TabPages[0].Size = new Size(720, 497);
            this.Tabs.TabPages[0].TabIndex = 0;
            this.Tabs.TabPages[0].Text = "  Bienvenue  ";
            this.Tabs.TabPages[0].BackgroundImageLayout = ImageLayout.Center;
            this.Tabs.TabPages[0].BackgroundImage = ImageHelper(this.modeNuitActivé);

            this.openFileDialog1.InitialDirectory = Program.ADRESSE_SAUVEGARDE;
            this.openFileDialog1.Filter = "Fichiers BMP|*.bmp|Fichiers PNG|*.png|Fichiers JPEG|*.jpg|Fichiers CSV" +
                "|*.csv|Fichiers BMP et CSV|*.bmp;*.csv|Tous les fichiers|*.bmp;*.csv;*.jpg;*.png";
            this.openFileDialog1.FilterIndex = 5;
            this.openFileDialog1.RestoreDirectory = false;

            this.saveFileDialog1.InitialDirectory = Program.ADRESSE_SAUVEGARDE;
            this.saveFileDialog1.Filter = "Fichiers BMP|*.bmp|Fichiers PNG|*.png|Fichiers JPEG|*.jpg|Fichiers CSV" +
                "|*.csv|Fichiers BMP et CSV|*.bmp;*.csv|Tous les fichiers|*.bmp;*.csv;*.jpg;*.png";
            this.saveFileDialog1.FilterIndex = 5;
            this.saveFileDialog1.RestoreDirectory = true;

            closeImage = new MyImage(new MyImage(@"../Resources/trash3.png"), 20, 20).ToBitmap();
        }


        //Param de l'application

        #region Open et Save file

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.FileName = "";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK && File.Exists(this.openFileDialog1.FileName))
            {
                //if (ClearEverything("Ouvrir une nouvelle image supprimera tous les onglets ouverts ! Continuer ?") == DialogResult.Yes)
                Cursor.Current = Cursors.WaitCursor;
                MyImage im = new MyImage(this.openFileDialog1.FileName);

                if (!im.Validité || im.Height < 2 || im.Width < 2)
                {
                    MessageBox.Show("L'image n'est pas valide ! Elle est peut-être trop petite (doit etre supérieur à 2 pixels en largeur et hauteur).",
                        "Erreur : Impossible de charger l'image !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    AddTabPage(im);

                    this.Tabs.TabPages[0].Text = $"  {Path.GetFileNameWithoutExtension(this.openFileDialog1.FileName).ToUpper()}  ";
                }

                Cursor.Current = Cursors.Default;
            }
        }


        private void sauvegarderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.imageChargée)
                SaveDialog(this.imagesParOnglet[this.currentIndexOnglet]);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (this.imageChargée)
                SaveDialog(this.imagesParOnglet[this.currentIndexOnglet]);
        }

        private void SaveDialog(MyImage imageToSave)
        {
            this.saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(this.openFileDialog1.FileName) + " Modif " + (this.numéroModif - 1).ToString();

            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK && Directory.Exists(Path.GetDirectoryName(this.saveFileDialog1.FileName)))
            {
                Cursor.Current = Cursors.WaitCursor;
                imageToSave.Save(this.saveFileDialog1.FileName);
                Cursor.Current = Cursors.Default;
            }

        }

        #endregion


        #region Paramètres internes


        //Ajout et suppression d'onglet

        /// <summary>
        /// Ajoute un onglet avec l'image spécifiée
        /// </summary>
        /// <param name="imageToAdd">Image à ajouter</param>
        private void AddTabPage(MyImage imageToAdd)
        {
            Cursor.Current = Cursors.WaitCursor;

            int onglet = this.imageChargée ? this.Tabs.TabCount : 0;
            this.imagesParOnglet.Add(imageToAdd);

            if (this.imageChargée) //Il y a tjrs un tab ouvert meme quand il n'y a pas d'image chargée, donc on ne crée pas de tab pour la 1ere image
            {
                TabPage page = new TabPage($"{this.Tabs.TabPages[0].Text.ToUpper()}|  MODIFICATION " + numéroModif++ + "      ")
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(3, 3, 3, 3),
                    Padding = new Padding(2, 2, 2, 2),
                    BackColor = Color.Gainsboro,
                    AutoScroll = true,
                };

                this.Tabs.TabPages.Add(page);
                this.Tabs.SelectedTab = this.Tabs.TabPages[onglet];
            }
            else
            {
                this.imageChargée = true;
                this.sauvegarderToolStripMenuItem.Enabled = true;
            }

            this.pictureBoxParOnglet.Add(InitializePictureBox(this.imagesParOnglet[onglet], onglet));

            this.panelsParOnglet.Add(InitializePanel(this.pictureBoxParOnglet[onglet]));

            this.Tabs.TabPages[onglet].Controls.Add(this.panelsParOnglet[onglet]);

            this.currentIndexOnglet = onglet;


            Cursor.Current = Cursors.Default;
        }

        private MyPictureBox InitializePictureBox(MyImage image, int onglet)
        {
            MyPictureBox pb = new MyPictureBox(image)
            {
                BackColor = !this.modeNuitActivé ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black,
                Dock = System.Windows.Forms.DockStyle.Fill,
                Location = new System.Drawing.Point(0, 0),
                Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)),
                Name = this.Tabs.TabPages.Count.ToString(),
                Size = this.Tabs.SelectedTab.Size,
                SizeMode = this.tailleRéelleAffichée ? PictureBoxSizeMode.AutoSize : PictureBoxSizeMode.Zoom,
                TabIndex = onglet,
                TabStop = false
            };

            pb.MouseDown += this.PictureBox_MouseDown;
            pb.MouseMove += this.PictureBox_MouseMove;
            pb.MouseUp += this.PictureBox_MouseUp;

            pb.DoubleClick += this.PictureBox_DoubleClick;

            return pb;
        }


        private Panel InitializePanel(Control addControl)
        {
            Panel p = new Panel
            {
                Size = this.Tabs.SelectedTab.Size,
                Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)),
                AutoScroll = true,
                BackColor = !this.modeNuitActivé ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black,
            };
            p.Controls.Add(addControl);

            return p;
        }


        private void clear_Click(object sender, EventArgs e)
        {
            ClearEverything("Vous avez plusieurs onglets ouverts, voulez-vous vraiment tous les supprimer ?");
        }

        /// <summary>
        /// Supprime tous les onglets en cours et renvoie un <see cref="DialogResult"/> indiquant si l'opération a bien été effectuée ou non
        /// </summary>
        /// <param name="MessageErreur"></param>
        /// <returns></returns>
        private DialogResult ClearEverything(string MessageErreur)
        {
            DialogResult result = DialogResult.Yes;
            if (this.imageChargée)
            {
                if (this.Tabs.TabCount > 1)
                    result = MessageBox.Show(MessageErreur, "Attention !",
                           MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                if (this.Tabs.TabCount <= 1 || result == DialogResult.Yes)
                {
                    this.imagesParOnglet = new List<MyImage>();
                    this.pictureBoxParOnglet = new List<MyPictureBox>();
                    this.panelsParOnglet = new List<Panel>();
                    SupprimerOnglets();

                    GC.Collect();
                }
            }

            return result;
        }

        private void SupprimerOnglets()
        {
            for (int i = Tabs.TabCount - 1; i > 0; i--)
            {
                this.Tabs.TabPages.RemoveAt(i);
            }

            this.Tabs.TabPages[0].Controls.Clear();
            this.Tabs.TabPages[0].BackgroundImage = ImageHelper(this.modeNuitActivé);
            this.Tabs.TabPages[0].Text = "  Bienvenue  ";

            this.currentIndexOnglet = 0;
            this.numéroModif = 1;

            this.sauvegarderToolStripMenuItem.Enabled = false;
            this.imageChargée = false;
        }


        //Settings à droite, avec mode nuit et affichage de l'image en taille réelle
        private void Settings_Click(object sender, EventArgs e)
        {
            this.CheckLBSettings.Visible = !this.CheckLBSettings.Visible;
            this.SaveButton.Location = new System.Drawing.Point(this.SaveButton.Location.X, this.SaveButton.Location.Y + (this.CheckLBSettings.Height * (this.CheckLBSettings.Visible ? 1 : -1)));

            this.SaveButton.Height += this.CheckLBSettings.Height * (this.CheckLBSettings.Visible ? -1 : 1);
        }

        private bool changeSizeMode = false;
        private bool changeNightMode = false;
        private void CheckLBSettings_CheckChanged(object sender, ItemCheckEventArgs e)
        {
            if (!this.changeNightMode && !this.changeSizeMode)
                this.changeNightMode = this.CheckLBSettings.GetSelected(0);

            if (!this.CheckLBSettings.GetItemChecked(0) && !this.modeNuitActivé && this.changeNightMode)
            {
                this.menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CouleurMenuDéroulant(true));
                this.Tabs.TabPages[0].BackColor = Color.Black;

                foreach (var pb in pictureBoxParOnglet)
                    pb.BackColor = Color.FromArgb(31, 31, 31);

                this.menuStrip1.BackColor = Color.FromArgb(31, 31, 31);
                this.BackColor = Color.FromArgb(31, 31, 31);
                this.Settings.BackColor = Color.FromArgb(61, 61, 61);
                this.CheckLBSettings.BackColor = Color.FromArgb(61, 61, 61);
                this.menuStrip1.ForeColor = Color.White;

                if (!this.imageChargée)
                {
                    this.Tabs.TabPages[0].BackgroundImage = ImageHelper(true);
                }
            }
            else if (this.CheckLBSettings.GetItemChecked(0) && this.modeNuitActivé && this.changeNightMode)
            {
                this.menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CouleurMenuDéroulant(false));
                this.Tabs.TabPages[0].BackColor = Color.White;

                foreach (var pb in pictureBoxParOnglet)
                    pb.BackColor = Color.Gainsboro;

                this.menuStrip1.BackColor = Color.WhiteSmoke;
                this.BackColor = Color.WhiteSmoke;
                this.Settings.BackColor = Color.DimGray;
                this.CheckLBSettings.BackColor = Color.DimGray;
                this.menuStrip1.ForeColor = Color.Black;

                if (!this.imageChargée)
                {
                    this.Tabs.TabPages[0].BackgroundImage = ImageHelper(false);
                }
            }
            if (this.changeNightMode)
                this.modeNuitActivé = !this.CheckLBSettings.GetItemChecked(0);

            this.changeNightMode = false;

            if (!this.changeSizeMode)
                this.changeSizeMode = this.CheckLBSettings.GetSelected(1);

            if (!this.CheckLBSettings.GetItemChecked(1) && !this.tailleRéelleAffichée && this.changeSizeMode)
            {
                foreach (var pb in pictureBoxParOnglet) //Pas parfait mais ça marche
                {
                    double ratioW = (double)pb.Width / pb.Image.Width;
                    double ratioH = (double)pb.Height / pb.Image.Height;

                    double rapportChgmtTaille = ratioH > ratioW ? ratioW : ratioH;

                    int hauteurEnTrop = (int)Math.Ceiling(pb.Height - pb.Image.Height * rapportChgmtTaille) / 2;
                    int largeurEnTrop = (int)Math.Ceiling(pb.Width - pb.Image.Width * rapportChgmtTaille) / 2;

                    System.Drawing.Point realPosOnImage = new System.Drawing.Point
                        (this.mouseLocation.X * pb.Image.Width / pb.Width, this.mouseLocation.Y * pb.Image.Height / pb.Height);

                    float rapportMH = (float)this.mouseLocation.Y / pb.Height;
                    float rapportMW = (float)this.mouseLocation.X / pb.Width;

                    System.Drawing.Point originePB = new System.Drawing.Point((int)(-realPosOnImage.X + (pb.Width * rapportMW) + (double)largeurEnTrop),
                        (int)(-(realPosOnImage.Y - (pb.Height * rapportMH)) + (double)hauteurEnTrop));

                    pb.SizeMode = PictureBoxSizeMode.AutoSize;

                    if (originePB.Y > 0)
                        originePB.Y = 0;
                    if (originePB.X > 0)
                        originePB.X = 0;

                    pb.Location = originePB;
                }
                this.Tabs.Refresh();
            }
            else if (this.CheckLBSettings.GetItemChecked(1) && this.tailleRéelleAffichée && this.changeSizeMode)
            {
                for (int i = 0; i < this.pictureBoxParOnglet.Count; i++)
                {
                    this.pictureBoxParOnglet[i].Size = this.panelsParOnglet[i].Size;
                    this.pictureBoxParOnglet[i].SizeMode = PictureBoxSizeMode.Zoom;

                    this.pictureBoxParOnglet[i].Left = 0;
                    this.pictureBoxParOnglet[i].Top = 0;
                }
                this.Tabs.Refresh();
            }
            if (this.changeSizeMode)
                this.tailleRéelleAffichée = !this.CheckLBSettings.GetItemChecked(1);

            this.changeSizeMode = false;
        }


        //Poubelle rouge à la fin d'un onglet
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush couleurTexte = Brushes.Black;

            if (!this.modeNuitActivé)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Gainsboro), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(31, 31, 31)), e.Bounds);
                couleurTexte = new SolidBrush(Color.Gainsboro);
            }

            var tabPage = this.Tabs.TabPages[e.Index];
            var tabRect = this.Tabs.GetTabRect(e.Index);
            tabRect.Inflate(-2, -2);

            if (e.Index != 0) //On ne met pas de croix sur la première page
            {
                e.Graphics.DrawImage(closeImage, (tabRect.Right - 20), tabRect.Top + (tabRect.Height - 20) / 2);

                TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font, tabRect, ((SolidBrush)couleurTexte).Color, TextFormatFlags.Left);
            }
            else
            {
                Rectangle bordures = e.Bounds;
                bordures.Inflate(-2, -2);
                e.Graphics.DrawString(this.Tabs.TabPages[e.Index].Text, this.Font, couleurTexte, bordures);
            }

        }


        //Chgmt d'onglet ou supression de l'onglet si on est sur une croix rouge
        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            for (var i = 1; i < this.Tabs.TabPages.Count; i++)
            {
                Rectangle tabRect = this.Tabs.GetTabRect(i);
                tabRect.Inflate(-2, -2);

                Rectangle imageRect = new Rectangle(tabRect.Right - 20, tabRect.Top + (tabRect.Height - 20) / 2, 20, 20);

                if (imageRect.Contains(e.Location))
                {
                    this.Tabs.TabPages.RemoveAt(i);
                    this.pictureBoxParOnglet.RemoveAt(i);
                    this.panelsParOnglet.RemoveAt(i);
                    this.imagesParOnglet.RemoveAt(i);

                    GC.Collect();

                    this.Tabs.SelectedTab = this.Tabs.TabPages[this.currentIndexOnglet > this.Tabs.TabCount - 1 ? this.currentIndexOnglet - 1 : this.currentIndexOnglet];
                    if (this.Tabs.TabCount == 1)
                        this.numéroModif = 1;
                    break;
                }
            }
            this.currentIndexOnglet = this.Tabs.SelectedIndex;
        }



        //Déplacement dans une image affichée avec la taille réelle et qui dépasse des bords de la picturebox
        private bool ImageMoving = false;

        private System.Drawing.Point imageLocation = new System.Drawing.Point();
        private System.Drawing.Point mouseLocation = new System.Drawing.Point();

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.tailleRéelleAffichée)
            {
                imageLocation = e.Location;
                ImageMoving = true;
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            this.mouseLocation = e.Location;

            if (this.ImageMoving && sender is Control c)
            {
                int top = e.Y - imageLocation.Y;
                int left = e.X - imageLocation.X;

                if (this.pictureBoxParOnglet[this.currentIndexOnglet].Image.Width + c.Left + left > this.panelsParOnglet[this.currentIndexOnglet].Width && c.Left + left < 0)
                {
                    c.Left += left;
                }
                if (this.pictureBoxParOnglet[this.currentIndexOnglet].Image.Height + c.Top + top > this.panelsParOnglet[this.currentIndexOnglet].Height && c.Top + top < 0)
                {
                    c.Top += top;
                }
                this.Tabs.TabPages[this.currentIndexOnglet].Refresh();
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            ImageMoving = false;
        }

        private void PictureBox_DoubleClick(object sender, EventArgs e)
        {
            this.changeSizeMode = true;
            this.CheckLBSettings.SetItemCheckState(1, this.tailleRéelleAffichée ? CheckState.Unchecked : CheckState.Checked);
        }


        //Quand changement de taille et que RealSize activé
        private Size sizeForm;

        private void Photoshop3000_sizebegin(object sender, EventArgs e)
        {
            this.sizeForm = base.Size;
        }

        private void Photoshop3000_SizeChange(object sender, EventArgs e)
        {
            if (this.imageChargée && this.tailleRéelleAffichée)
            {
                int diffX = this.Size.Width - this.sizeForm.Width;
                int diffY = this.Size.Height - this.sizeForm.Height;

                foreach (var pb in pictureBoxParOnglet)
                {
                    int origineX = pb.Left + diffX;
                    int origineY = pb.Top + diffY;

                    if (diffX > 0 && pb.Left + pb.Image.Width <= this.panelsParOnglet[this.currentIndexOnglet].Width)
                    {
                        if (origineX > 0)
                            pb.Left = 0;
                        else
                            pb.Left += diffX;
                    }

                    if (diffY > 0 && pb.Top + pb.Image.Height <= this.panelsParOnglet[this.currentIndexOnglet].Height)
                    {
                        if (origineY > 0)
                            pb.Top = 0;
                        else
                            pb.Top += diffY;
                    }

                }
            }
        }


        //Exit du programme
        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.imageChargée || MessageBox.Show("Êtes-vous sûr de vouloir quitter l'application ?", "Quitter",
                       MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Dispose(true);
                Environment.Exit(0);
            }
        }

        //Image d'aide

        /// <summary>
        /// Ajoute du texte pour indiquer à l'utilisateur que faire
        /// </summary>
        /// <returns></returns>
        private Bitmap ImageHelper(bool modeNuit)
        {
            int height = this.pictureBoxParOnglet.Count != 0 ? this.pictureBoxParOnglet[0].Height : this.Tabs.TabPages[0].Height - this.Tabs.TabPages[0].Padding.Vertical;
            int width = this.pictureBoxParOnglet.Count != 0 ? this.pictureBoxParOnglet[0].Width : this.Tabs.TabPages[0].Width - this.Tabs.TabPages[0].Padding.Horizontal;

            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawString(" Veuillez charger\n\rune image depuis\r\n le menu 'fichier'",
                new Font("Times New Roman", 40, FontStyle.Bold), modeNuit ? Brushes.White : Brushes.Black,
                new System.Drawing.Point(width / 2 - 210, height / 2 - 120));
            g.DrawString("ou créez-en une depuis les menus\r\n 'Fractales' et 'Création > Dessins'",
                new Font("Times New Roman", 17, FontStyle.Regular), modeNuit ? Brushes.White : Brushes.Black,
                new System.Drawing.Point(width / 2 - 170, height / 2 + 70));

            return bmp;
        }

        #endregion


        //Evènements liés au menu déroulant

        private void ApplicationMéthodes(int méthode)
        {
            if (this.imageChargée)
            {
                Cursor.Current = Cursors.WaitCursor;
                MyGraphics g = new MyGraphics(this.imagesParOnglet[this.currentIndexOnglet].Clone());

                switch (méthode)
                {
                    #region Filtres de matrice de convolution

                    case 1:
                        g.FiltreDetectionContours();
                        break;

                    case 2:
                        g.FiltreSobel();
                        break;
                    case 3:
                        g.FiltreCanny();
                        break;

                    case 4:
                        g.FiltreRenforcementContours();
                        break;
                    case 5:
                        g.FiltreAffaiblissementContours();
                        break;
                    case 6:
                        g.FiltreDessin(false);
                        break;
                    case 7:
                        g.FiltreGravure();
                        break;
                    case 8:
                        g.FiltreRepoussageFaible();
                        break;
                    case 9:
                        g.FiltreRepoussageFort();
                        break;
                    case 10:
                        g.FiltreAiguisage();
                        break;
                    case 11:
                        g.FiltreContraste();
                        break;
                    case 12:
                        g.FiltreFlou(false);
                        break;
                    case 13:
                        g.FiltreFlou(true);
                        break;
                    case 14:
                        g.FiltreFlouGaussien();
                        break;
                    case 15:
                        g.FiltreLissage();
                        break;
                    case 16:
                        g.FiltreTest();
                        break;

                    #endregion

                    #region bruit numérique

                    case 17:
                        g.AddNoise(20);
                        break;
                    case 18:
                        g.AddPepperNoise(10);
                        break;
                    case 19:
                        g.AddShotNoise(10);
                        break;

                    #endregion

                    #region effet miroir

                    case 20:
                        g.EffetMiroir(true);
                        break;

                    case 27:
                        g.EffetMiroir(false);
                        break;

                    case 21:
                        g.EffetMiroirDoubleOuQuadruple(true, false);
                        break;

                    case 22:
                        g.EffetMiroirDoubleOuQuadruple(false, true);
                        break;

                    case 23:
                        g.EffetMiroirDoubleOuQuadruple(true, true);
                        break;

                    case 28:
                        g.EffetMiroirMoitié(true);
                        break;

                    case 29:
                        g.EffetMiroirMoitié(false);
                        break;

                    #endregion

                    #region effet peinture

                    case 24:
                        g.EffetPeinture(false, false);
                        break;
                    case 25:
                        g.EffetPeinture(true, false);
                        break;
                    case 26:
                        g.EffetPeinture(false, true);
                        break;
                    case 30:
                        g.EffetPeintureHuile(6, 25);
                        break;
                    case 31:
                        g.EffetPeintureHuileContraste(6, 25);
                        break;

                    #endregion

                    #region Autres

                    case 32:
                        if (this.colorDialog1.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }

                        g.TransformationFiltreCouleur(Pixel.FromColor(this.colorDialog1.Color));
                        break;

                    case 33:
                        g.TrierParCouleurCoin(4, 1);
                        break;
                    case 34:
                        g.TrierParCouleurCoté(4);
                        break;

                        #endregion

                }
                AddTabPage(g.MyImage);
            }
            else
            {
                MessageBox.Show("Ouvrez d'abord une image avant de la modifier !", "Erreur : image non chargée",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region Filtre


        private void détectionDesContoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(1);
        }

        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(2);
        }

        private void cannyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(3);
        }

        private void renforcementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(4);
        }

        private void affaiblissementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(5);
        }

        private void dessinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(6);
        }

        private void gravureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(7);
        }

        private void faibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(8);
        }

        private void fortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(9);
        }

        private void aiguisageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(10);
        }

        private void contrasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(11);
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(12);
        }

        private void mouvementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(13);
        }

        private void gaussienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(14);
        }

        private void lissageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(15);
        }

        private void couleurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(32);
        }

        private void Test15x15ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(16);
        }



        private void couleursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.imageChargée)
            {
                using (CouleurFiltre colorFilter = new CouleurFiltre())
                {
                    if (colorFilter.ShowDialog() == DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        MyGraphics g = new MyGraphics(this.imagesParOnglet[this.currentIndexOnglet].Clone());

                        if (colorFilter.NoirEtBlanc)
                        {
                            g.TransformationNoirEtBlanc(colorFilter.NoirEtBlancValue);
                        }
                        else if (colorFilter.Rouge || colorFilter.Vert || colorFilter.Bleu)
                        {
                            g.TransformationFiltreRGB(colorFilter.Rouge, colorFilter.Vert, colorFilter.Bleu);
                        }

                        if (colorFilter.RougeIntensité || colorFilter.VertIntensité || colorFilter.BleuIntensité)
                        {
                            g.TransformationGriserRGB(colorFilter.RougeIntensité ? colorFilter.RougeIntensitéValue : 0,
                                colorFilter.VertIntensité ? colorFilter.VertIntensitéValue : 0,
                                colorFilter.BleuIntensité ? colorFilter.BleuIntensitéValue : 0,
                                colorFilter.RougeIntensité ? colorFilter.RougeIntensitéHigher : true,
                                colorFilter.VertIntensité ? colorFilter.VertIntensitéHigher : true,
                                colorFilter.BleuIntensité ? colorFilter.BleuIntensitéHigher : true);
                        }

                        if (colorFilter.Inversion)
                            g.InversionCouleurs();

                        if (colorFilter.Sépia)
                            g.TransformationSépia();

                        if (colorFilter.TransfoLum)
                            g.TransformationLuminositéPerçue(colorFilter.TransfoLumValue);

                        if (colorFilter.TransfoCouleur)
                            g.TransformationCouleurIntensité(100 - colorFilter.TransfoCouleurValue);

                        AddTabPage(g.MyImage);
                    }

                    colorFilter.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Ouvrez d'abord une image avant de la modifier !", "Erreur : image non chargée",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region Fractale

        private void fractalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FractaleForm frac = new FractaleForm(this.imageChargée ? this.imagesParOnglet[this.currentIndexOnglet].Clone() : null))
            {
                if (frac.ShowDialog() == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    Fractale fractale;
                    if (frac.type == Fractale.Fractales.Mosaique_From_Image)
                    {
                        fractale = new Fractale(frac.imageMosaique)
                        {
                            MaxItération = frac.maxItération,
                            MaxTryColor = frac.maxTryColor,
                            TailleMosaique = frac.TailleMosaique,
                        };
                    }
                    else
                    {
                        fractale = new Fractale(frac.height, frac.width, frac.type)
                        {
                            UserEquation = frac.userComp,
                            Contraste = frac.Contraste,
                            MaxItération = frac.maxItération,
                            MaxTryColor = frac.maxTryColor,
                            TailleMosaique = frac.TailleMosaique,
                            TailleFractJulia = frac.SizeJulia,
                            BackgroundColor = frac.BackgroundColor,
                            CouleurDominante = frac.BackgroundColor != Pixel.FromColor(Couleurs.Blanc) ?
                                 Pixel.GetCouleur(frac.BackgroundColor) : (Couleurs?)null,
                        };
                    }
                    fractale.Draw();

                    if (!this.imageChargée)
                    {
                        this.Tabs.TabPages[0].Text = "  Fractale  ";
                    }

                    this.AddTabPage(fractale.MyImage);
                }

                frac.Dispose();
            }
        }

        #endregion


        #region Qr et Code-barres

        private void barcodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (BarcodeForm form = new BarcodeForm(this.imageChargée ? this.imagesParOnglet[this.currentIndexOnglet].Clone() : null))
            {
                if (form.ShowDialog() == DialogResult.Yes)
                {
                    AddTabPage(form.MyImage);
                }
            }
        }

        #endregion


        #region Création

        private void dessinsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            using (Dessin drawing = new Dessin(this.imageChargée ? this.imagesParOnglet[this.currentIndexOnglet].Clone() : null))
            {
                if (drawing.ShowDialog() == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    if (drawing.imageOnLoad.Validité && drawing.imageOnLoad.Width > 2 && drawing.imageOnLoad.Height > 2)
                    {
                        this.AddTabPage(drawing.imageOnLoad);
                    }
                    else
                    {
                        MessageBox.Show("Malheureusement l'image n'est pas valide, veuillez réessayer (largeur et hauteur > 2) !", "Erreur : image non valide",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void copieImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.imageChargée)
            {
                using (CopieImageForm copie = new CopieImageForm(this.imagesParOnglet[this.currentIndexOnglet]))
                {
                    if (copie.ShowDialog() == DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        MyGraphics g = new MyGraphics(this.imagesParOnglet[this.currentIndexOnglet].Clone())
                        {
                            Clip = new Rectangle((int)copie.GetPointOrigine.X, (int)copie.GetPointOrigine.Y,
                            (int)(-copie.GetPointOrigine.X + copie.GetPointDestination.X),
                            (int)(-copie.GetPointOrigine.Y + copie.GetPointDestination.Y))
                        };

                        g.DrawImage(copie.GetImage, copie.Opacité);

                        this.AddTabPage(g.MyImage);
                    }

                    copie.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Ouvrez d'abord une image avant de la modifier !", "Erreur : image non chargée",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void pixélisationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.imageChargée)
            {
                using (Pixellisation pixel = new Pixellisation(this.imagesParOnglet[this.currentIndexOnglet].Width))
                {
                    if (pixel.ShowDialog() == DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        MyGraphics g = new MyGraphics(this.imagesParOnglet[this.currentIndexOnglet].Clone())
                        {
                            KeepAspectRatio = pixel.adaptateur
                        };
                        g.Pixellisation(pixel.taillePixel);

                        this.AddTabPage(g.MyImage);
                    }
                }
            }
            else
            {
                MessageBox.Show("Ouvrez d'abord une image avant de la modifier !", "Erreur : image non chargée",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DécalageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.imageChargée)
            {
                using (Décalage décal = new Décalage())
                {
                    if (décal.ShowDialog() == DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        MyGraphics g = new MyGraphics(this.imagesParOnglet[this.currentIndexOnglet].Clone());
                        if (décal.cercleTest)
                        {
                            g.DécalageCercle();
                        }
                        else
                        {
                            if (!décal.sin && !décal.cos)
                            {
                                g.DécalagePixels(décal.intensitéDécal, décal.largeurDécal, décal.largeur);
                            }
                            else
                            {
                                g.DécalagePixelsSinCos(décal.NombreDécal, décal.HauteurDécal, décal.cos, décal.largeur);
                            }
                        }

                        this.AddTabPage(g.MyImage);
                    }

                    décal.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Ouvrez d'abord une image avant de la modifier !", "Erreur : image non chargée",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        //Bruit numérique
        private void coloréToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(17);
        }

        private void selEtPoivreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(18);
        }

        private void grenailleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(19);
        }



        //Miroir
        private void simpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(27);
        }

        private void SimpleLargeurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(20);
        }

        private void doubleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(21);
        }

        private void doubleHauteurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(22);
        }

        private void quadrupleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(23);
        }

        private void MoitiéHauteurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(28);
        }

        private void MoitiéLargeurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(29);
        }

        //Peinture
        private void MéthodeNormaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(24);
        }

        private void MéthodeAiguisageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(25);
        }

        private void MéthodeContrasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(26);
        }

        private void EffetPeintureSurHuileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(30);
        }

        private void EffetPeintureÀLhuileContrasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(31);
        }



        private void parCôtéToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(34);
        }

        private void parCoinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodes(33);
        }

        #endregion


        #region stéganographie

        private void stéganographieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.imageChargée)
            {
                using (StéganographieForm ste = new StéganographieForm())
                {
                    if (ste.ShowDialog() == DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        MyGraphics g = new MyGraphics(this.imagesParOnglet[this.currentIndexOnglet].Clone());

                        if (ste.HideImage)
                        {
                            if (ste.AdapatSize)
                            {
                                double ratioW = (double)this.imagesParOnglet[this.currentIndexOnglet].Width / ste.imageToHide.Width;
                                double ratioH = (double)this.imagesParOnglet[this.currentIndexOnglet].Height / ste.imageToHide.Height;

                                new MyGraphics(ste.imageToHide).Redimensionnement(ratioH > ratioW ? ratioW : ratioH);
                            }
                            g.CacherImage(ste.imageToHide, ste.BitsToHide);
                        }
                        else if (ste.GetImage)
                        {
                            g.GetImageCachée(ste.BitsToHide);
                        }
                        else if (ste.HideTexte)
                        {
                            g.CacherTexte(ste.texteToHide, ste.mdp, ste.BitsToHide);

                            if (this.imagesParOnglet[this.currentIndexOnglet] == g.MyImage) //La fct renvoie la meme image si txt trop long
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("Le texte est trop long pour le cacher dans l'image !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        else if (ste.GetTexte)
                        {
                            string s = g.GetTexteCaché(ste.mdp, ste.longueurTexte, ste.BitsToHide);

                            Cursor.Current = Cursors.Default;
                            MessageBox.Show(s, "Texte trouvé", MessageBoxButtons.OK, MessageBoxIcon.None);
                            return;
                        }

                        this.AddTabPage(g.MyImage);
                    }
                }
            }
            else
            {
                MessageBox.Show("Ouvrez d'abord une image !", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion


        #region Histo

        private void infosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageChargée)
            {
                using (Histogramme histoForm = new Histogramme())
                {
                    if (histoForm.ShowDialog() == DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        MyImageStats histo = new MyImageStats(this.imagesParOnglet[this.currentIndexOnglet], histoForm.hauteur, histoForm.largeur, (byte)histoForm.Min, (byte)histoForm.Max)
                        {
                            RemplissageCouleur = histoForm.p_remplissage,
                            HistogrammeMode = histoForm.methode == 0 ? HistogrammeMode.Echelle_Gris : HistogrammeMode.Echelle_Couleurs,
                            Remplissage = histoForm.remplissage
                        };

                        this.AddTabPage(histoForm.methode == 2 ? histo.CreateHistogrammeTransparence() :
                            histoForm.methode == 3 ? histo.CreateHistogrammeTransparenceLissage() : histo.CreateHistogramme());
                    }
                }
            }
            else
            {
                MessageBox.Show("Ouvrez d'abord une image avant d'en créer un histogramme !", "Erreur : image non chargée",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region outils

        private void paramImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ModifImage modif = new ModifImage(this.imageChargée ? this.imagesParOnglet[this.currentIndexOnglet] : null))
            {
                DialogResult dr = modif.ShowDialog();
                if (dr == DialogResult.Yes && this.imageChargée)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    int newHeight = modif.newHeight;
                    int newWidth = modif.newWidth;

                    double angle = modif.angleRotation;

                    Point topLeft = modif.topLeft;
                    Point bottomRight = modif.bottomRight;

                    MyGraphics graph = new MyGraphics(this.imagesParOnglet[this.currentIndexOnglet].Clone())
                    {
                        KeepAspectRatio = modif.b_KeepAspectRatio,
                        InterpolationMode = modif.quality,
                        PixelRemplissage = modif.remplissage
                    };

                    bool changementEffectué = false;

                    if (topLeft != new Point(0, 0) || bottomRight != new Point(this.imagesParOnglet[this.currentIndexOnglet].Height - 1,
                        this.imagesParOnglet[this.currentIndexOnglet].Width - 1))
                    {
                        graph.Clip = new Rectangle((int)topLeft.X, (int)topLeft.Y, (int)(bottomRight.X - topLeft.X), (int)(bottomRight.Y - topLeft.Y));
                        graph.Rognage();
                        changementEffectué = true;
                    }

                    if (newHeight != 0 && newWidth != 0 && (newHeight != this.imagesParOnglet[this.currentIndexOnglet].Height ||
                            newWidth != this.imagesParOnglet[this.currentIndexOnglet].Width))
                    {
                        graph.Redimensionnement(newHeight, newWidth);
                        changementEffectué = true;
                    }

                    if (angle != 0)
                    {
                        graph.Rotation(angle, modif.BordsRemplis);
                        changementEffectué = true;
                    }

                    if (changementEffectué)
                    {
                        this.AddTabPage(graph.MyImage);
                    }
                    else
                    {
                        MessageBox.Show("Aucune modification spécifiée !", "Pas de modification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else if (!imageChargée && dr == DialogResult.Yes)
                {
                    MessageBox.Show("Vous devez ouvrir une image avant d'y effectuer des opérations !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                modif.Dispose();
            }
        }

        private void infoImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.imageChargée)
            {
                Cursor.Current = Cursors.WaitCursor;

                int hue = MyImageStats.GetAverageHue(this.imagesParOnglet[this.currentIndexOnglet]);
                double light = MyImageStats.GetAverageLightness(this.imagesParOnglet[this.currentIndexOnglet]);
                double satur = MyImageStats.GetAverageSaturation(this.imagesParOnglet[this.currentIndexOnglet]);
                double bright = MyImageStats.GetAverageBrightness(this.imagesParOnglet[this.currentIndexOnglet]);
                Pixel colorAverage = MyImageStats.GetAverageColor(this.imagesParOnglet[this.currentIndexOnglet]);
                string typeIm = "Format pivot utilisé pour cette image : " + this.imagesParOnglet[this.currentIndexOnglet].PixelFormat.ToString();

                string infos = $"Informations sur l'image :\n" +
                    $"    - Taille           : Largeur : {this.imagesParOnglet[this.currentIndexOnglet].Width}  |  Hauteur :" +
                    $" {this.imagesParOnglet[this.currentIndexOnglet].Height}\n\n" +
                    $"    - Teinte           : {hue.ToString()}° (0-360°)\n" +
                    $"    - Luminosité  : {(light * 100).ToString("0")}%\n" +
                    $"    - Saturation   : {(satur * 100).ToString("0")}%\n\n" +
                    $"    - Luminance   : {bright.ToString("0")}\n\n" +
                    $"    - Pixel moyen : {colorAverage.ToString()}\n" +
                    $"                              = {Pixel.GetCouleur(colorAverage).ToString()}\n\n" +
                    $"    {typeIm}";

                Cursor.Current = Cursors.Default;

                MessageBox.Show(infos, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Il n'y a pas d'image chargée !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void aideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pour modifier une image, mettez-vous dans l'onglet de l'image à modifier puis choisissez une méthode " +
                "dans les menu déroulants pour la modifier. Un nouvel onglet apparaitra avec l'image changée.\n\n" +
                "Pour sauvegarder une image en particulier appuyez sur le bouton 'SAVE' dans l'onglet de l'image à sauvegarder.\n" +
                @"Vous pouvez vous déplacer dans une image si la case ""Real size"" est cochée", "Aide",
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

    }


    /// <summary>
    /// Zone destinée à afficher une image, peut prendre en paramètre un <see cref="Photoshop3000.MyImage"/>.
    /// </summary>
    public class MyPictureBox : PictureBox
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="MyPictureBox"/>.
        /// </summary>
        public MyPictureBox()
            : base()
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="MyPictureBox"/> à partir d'un <see cref="Photoshop3000.MyImage"/>.
        /// </summary>
        public MyPictureBox(MyImage image)
            : this()
        {
            this.MyImage = image;
        }


        /// <summary>
        /// Obtient ou définit l'<see cref="Image"/> de cet objet sous forme d'un <see cref="Photoshop3000.MyImage"/>
        /// </summary>
        public MyImage MyImage
        {
            get => base.Image != null ? MyImage.FromBitmap(new Bitmap(base.Image)) : null;

            set => base.Image = value?.ToBitmap();
        }
    }

    /// <summary>
    /// TexteBox transparente
    /// </summary>
    public partial class MyTextBox : TextBox
    {
        /// <summary>
        /// TexteBox transparente
        /// </summary>
        public MyTextBox()
            :base()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
        }
    }
}
