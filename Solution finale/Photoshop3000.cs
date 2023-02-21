using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Solution_finale
{
    public partial class Photoshop3000 : Form
    {
        private List<MyImage> imagesParOnglet = new List<MyImage>();
        private List<Panel> panelsParOnglet = new List<Panel>();

        private string FileName;

        private bool imageChargée = false;
        private bool realSize = false;
        private bool modeNuit = false;

        private int currentIndexImage = 0;

        private int numéroModif = 1;

        private System.Drawing.Point imageLocation = new System.Drawing.Point();
        private bool imageIsMoving = false;


        public Photoshop3000()
        {
            InitializeComponent();
            this.menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CouleurMenuDéroulant(this.modeNuit));
            this.Originale.BackgroundImage = AddImageHelperImagePrincipale();
            this.Originale.BackgroundImageLayout = ImageLayout.Center;
        }


        #region Open et Save file

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Program.ADRESSE_SAUVEGARDE;
            openFileDialog1.Filter = "Fichiers bitmap|*.bmp|Fichiers csv|*.csv|Fichiers bmp et csv|*.csv;*.bmp";
            openFileDialog1.FilterIndex = 3;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = "";


            DialogResult fichier = openFileDialog1.ShowDialog();
            FileName = openFileDialog1.FileName;


            if (File.Exists(FileName) && fichier == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                MyImage im = new MyImage(FileName);

                if (!im.BitmapValide || im.GetHeight < 2 || im.GetWidth < 2)
                {
                    MessageBox.Show("L'image est trop petite, doit etre supérieur à 2 pixels en largeur et hauteur", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (this.Tabs.TabPages.Count == 1)
                {
                    this.imageChargée = false;
                    this.imagesParOnglet.Clear();
                    this.imagesParOnglet.Add(new MyImage(FileName));
                    ModificationActive();
                }
                else
                {
                    if (ClearEverything("Voulez vous vraiment tout supprimer ?") != DialogResult.Yes)
                    {
                        return;
                    }
                    this.imagesParOnglet.Add(new MyImage(FileName));
                    ModificationActive();
                }
                this.imageChargée = true;
                this.sauvegarderToolStripMenuItem.Enabled = true;
                this.Originale.BackgroundImage = null;
                Cursor.Current = Cursors.Default;
            }
        }

        private void sauvegarderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.imageChargée)
                SaveDialog(this.imagesParOnglet[Tabs.TabPages.Count - 1]); //Le dernier
        }
        private void SaveDialog(MyImage toSave)
        {
            saveFileDialog1.InitialDirectory = Program.ADRESSE_SAUVEGARDE;
            saveFileDialog1.Filter = "Fichiers bitmap|*.bmp|Fichiers csv|*.csv|Fichiers bmp et csv|*.csv;*.bmp";
            saveFileDialog1.FilterIndex = 3;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = Path.GetFileName(FileName);

            DialogResult SaveFile = saveFileDialog1.ShowDialog();


            if (SaveFile == DialogResult.OK && Directory.Exists(Path.GetDirectoryName(saveFileDialog1.FileName)))
            {
                Cursor.Current = Cursors.WaitCursor;
                toSave.Save(saveFileDialog1.FileName);
                Cursor.Current = Cursors.Default;
            }

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (this.imageChargée)
                SaveDialog(this.imagesParOnglet[this.Tabs.SelectedIndex]);
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!imageChargée || MessageBox.Show("Ëtes-vous sûr de vouloir quitter l'application ?", "Quitter",
                       MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Dispose();
                Environment.Exit(0);
            }
        }


        #endregion



        #region Filtre

        private int pixélisation = 10;
        private bool adaptationTaille = true;


        private void ApplicationMéthodesFiltre(int méthode)
        {
            if (this.imageChargée)
            {
                Cursor.Current = Cursors.WaitCursor;
                MyGraphics g = new MyGraphics(this.imagesParOnglet[Tabs.SelectedIndex].GetCopie());
                switch (méthode)
                {
                    case 2:
                        g.FiltreSobel();
                        break;
                    case 3:
                        g.FiltreCanny();
                        break;

                    #region Filtres de matrice de convolution

                    case 1:
                        g.FiltreDetectionContours();
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

                    #endregion

                    #region bruit numérique

                    case 16:
                        g.AddNoise(20);
                        break;
                    case 17:
                        g.AddPepperNoise(10);
                        break;
                    case 18:
                        g.AddShotNoise(10);
                        break;

                    #endregion

                    case 19:
                        g.KeepAspectRatio = this.adaptationTaille;
                        g.Pixélisation(this.pixélisation);
                        break;

                    #region effet miroir

                    case 20:
                        g.EffetMiroir();
                        break;

                    case 21:
                        g.EffetMiroirDoubleOuQuadruple(false);
                        break;

                    case 22:
                        g.EffetMiroirDoubleOuQuadruple(true);
                        break;

                    #endregion

                    case 23:
                        g.CouleurPeinture();
                        break;

                }

                this.imagesParOnglet.Add(g.GetMyImage);
                ModificationActive(Tabs.TabPages.Count);
            }
            else
            {
                MessageBox.Show("Ouvrez d'abord une image avant de la modifier !", "Erreur : image non chargée",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void détectionDesContoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(1);
        }

        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(2);
        }

        private void cannyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(3);
        }

        private void renforcementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(4);
        }

        private void affaiblissementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(5);
        }

        private void dessinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(6);
        }

        private void gravureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(7);
        }

        private void faibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(8);
        }

        private void fortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(9);
        }

        private void aiguisageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(10);
        }

        private void contrasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(11);
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(12);
        }

        private void mouvementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(13);
        }

        private void gaussienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(14);
        }

        private void lissageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(15);
        }

        private void coloréToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(16);
        }

        private void selEtPoivreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(17);
        }

        private void grenailleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(18);
        }


        private void pixélisationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.imageChargée)
            {
                using (Pixélisation pixel = new Pixélisation(this.imagesParOnglet[this.currentIndexImage].GetWidth))
                {
                    if (pixel.ShowDialog() == DialogResult.Yes)
                    {
                        this.pixélisation = pixel.taillePixel;
                        this.adaptationTaille = pixel.adaptateur;
                        ApplicationMéthodesFiltre(19);
                    }
                }
            }
            else
            {
                MessageBox.Show("Ouvrez d'abord une image avant de la modifier !", "Erreur : image non chargée",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                        MyGraphics g = new MyGraphics(this.imagesParOnglet[this.currentIndexImage].GetCopie());

                        if (colorFilter.NoirEtBlanc)
                        {
                            g.TransformationNoirEtBlanc(colorFilter.GrisIntensité);
                        }
                        else if (colorFilter.Rouge || colorFilter.Vert || colorFilter.Bleu)
                        {
                            g.TransformationRGB(colorFilter.Rouge, colorFilter.Vert, colorFilter.Bleu);
                        }

                        if (colorFilter.inversion)
                            g.InversionCouleurs();

                        if (colorFilter.Sépia)
                            g.TransformationSépia();

                        if (colorFilter.transfoLum)
                            g.TransformationLuminositéPerçue(colorFilter.LumChgmtIntensité);

                        this.imagesParOnglet.Add(g.GetMyImage);
                        ModificationActive(this.Tabs.TabPages.Count);

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
            using (FractaleForm frac = new FractaleForm(this.imageChargée ? this.imagesParOnglet[this.currentIndexImage] : null))
            {
                if (frac.ShowDialog() == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Fractale fractale;
                    if (frac.type == Fractale.Fractales.Mosaïque_From_Image)
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
                            BackgroundColor = frac.backgroundColor
                        };
                    }
                    fractale.CreateFractale();

                    this.imagesParOnglet.Add(fractale.GetMyImage);

                    if (this.imageChargée)
                    {
                        ModificationActive(Tabs.TabPages.Count);
                    }
                    else
                    {
                        ModificationActive();
                        this.imageChargée = true;
                        this.sauvegarderToolStripMenuItem.Enabled = true;
                        this.Originale.BackgroundImage = null;
                        Cursor.Current = Cursors.Default;
                    }

                }

                frac.Dispose();
            }
        }

        #endregion


        #region Création

        private void effetPeintureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(23);
        }

        //Miroir
        private void simpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(20);
        }

        private void doubleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(21);
        }

        private void quadrupleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationMéthodesFiltre(22);
        }


        private void dessinsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Dessin drawing = new Dessin(this.imageChargée ? this.imagesParOnglet[this.currentIndexImage] : null))
            {
                if (drawing.ShowDialog() == DialogResult.Yes)
                {

                }
            }
        }

        private void copieImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.imageChargée)
            {
                using (CopieImageForm copie = new CopieImageForm(this.imagesParOnglet[this.currentIndexImage]))
                {
                    if (copie.ShowDialog() == DialogResult.Yes)
                    {
                        MyGraphics g = new MyGraphics(copie.GetImage);
                        MyGraphics gReal = new MyGraphics(this.imagesParOnglet[this.currentIndexImage].GetCopie());

                        g.KeepAspectRatio = false;
                        g.Redimensionnement(copie.HeightImage, copie.WidthImage, InterpolationMode.Bicubique);

                        gReal.CopieImageDansImage(copie.GetImage, copie.GetPointOrigine);


                        this.imagesParOnglet.Add(gReal.GetMyImage);
                        ModificationActive(Tabs.TabPages.Count);
                    }

                    copie.Dispose();
                }
            }
            else
                MessageBox.Show("Ouvrez d'abord une image !", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                        MyImage myImage = new MyImage(this.imagesParOnglet[this.currentIndexImage].GetHeight, this.imagesParOnglet[this.currentIndexImage].GetWidth);
                        MyGraphics g = new MyGraphics(this.imagesParOnglet[this.currentIndexImage].GetCopie());

                        if (ste.HideImage)
                        {
                            if (ste.AdapatSize)
                            {
                                double ratioW = (double)myImage.GetWidth / ste.imageToHide.GetWidth;
                                double ratioH = (double)myImage.GetHeight / ste.imageToHide.GetHeight;

                                double rapportChgmtTaille = ratioH > ratioW ? ratioW : ratioH;

                                new MyGraphics(ste.imageToHide).Redimensionnement(rapportChgmtTaille);
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

                            if (this.imagesParOnglet[this.currentIndexImage] == g.GetMyImage) //La fct renvoie la meme image si txt trop long
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

                        this.imagesParOnglet.Add(g.GetMyImage);
                        ModificationActive(Tabs.TabPages.Count);
                    }
                }
            }
            else
                MessageBox.Show("Ouvrez d'abord une image !", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                        MyImageStatistiques histo = new MyImageStatistiques(this.imagesParOnglet[this.currentIndexImage], histoForm.hauteur, histoForm.largeur);
                        HistogrammeMode mode = histoForm.color ? HistogrammeMode.Echelle_Couleurs : HistogrammeMode.Echelle_Gris;

                        this.imagesParOnglet.Add(histo.CreateHistogramme(mode, histoForm.remplissage));

                        ModificationActive(Tabs.TabPages.Count);
                    }
                }
            }
            else
                MessageBox.Show("Ouvrez d'abord une image avant d'en créer un histogramme !", "Erreur : image non chargée",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion


        #region outil tool

        private void paramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ModifImage modif = new ModifImage(this.imageChargée ? this.imagesParOnglet[this.currentIndexImage] : null))
            {
                DialogResult d = modif.ShowDialog();
                if (d == DialogResult.Yes && this.imageChargée)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    int newHeight = modif.newHeight;
                    int newWidth = modif.newWidth;

                    double angle = modif.angleRotation;

                    Point topLeft = modif.topLeft;
                    Point bottomRight = modif.bottomRight;

                    MyGraphics graph = new MyGraphics(this.imagesParOnglet[this.currentIndexImage].GetCopie())
                    {
                        KeepAspectRatio = modif.b_KeepAspectRatio,
                        Quality = modif.quality,
                        PixelRemplissage = modif.remplissage
                    };

                    bool changementEffectué = false;

                    if (topLeft != new Point(0, 0) || bottomRight != new Point(this.imagesParOnglet[this.currentIndexImage].GetHeight - 1,
                        this.imagesParOnglet[this.currentIndexImage].GetWidth - 1))
                    {
                        graph.Rognage(topLeft, bottomRight);
                        changementEffectué = true;
                    }

                    if (newHeight != 0 && newWidth != 0 && (newHeight != this.imagesParOnglet[this.currentIndexImage].GetHeight ||
                            newWidth != this.imagesParOnglet[this.currentIndexImage].GetWidth))
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
                        this.imagesParOnglet.Add(graph.GetMyImage);
                        ModificationActive(Tabs.TabPages.Count);
                    }
                    else
                    {
                        MessageBox.Show("Aucune modification n'a été effectué sur l'image !", "Pas de modification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else if (!imageChargée && d == DialogResult.Yes)
                {
                    MessageBox.Show("Vous devez ouvrir une image avant d'y effectuer des opérations !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                modif.Dispose();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.imageChargée)
            {
                Cursor.Current = Cursors.WaitCursor;

                int hue = MyImageStatistiques.GetAverageHue(this.imagesParOnglet[this.currentIndexImage]);
                double light = MyImageStatistiques.GetAverageLightness(this.imagesParOnglet[this.currentIndexImage]);
                double satur = MyImageStatistiques.GetAverageSaturation(this.imagesParOnglet[this.currentIndexImage]);
                double bright = MyImageStatistiques.GetAverageBrightness(this.imagesParOnglet[this.currentIndexImage]);
                Pixel colorAverage = MyImageStatistiques.GetAverageColor(this.imagesParOnglet[this.currentIndexImage]);

                string infos = $"Informations sur l'image :\n" +
                    $"    - Taille           : Largeur : {this.imagesParOnglet[this.currentIndexImage].GetWidth}  |  Hauteur :" +
                    $" {this.imagesParOnglet[this.currentIndexImage].GetHeight}\n\n" +
                    $"    - Teinte           : {hue.ToString()}° (0-360°)\n" +
                    $"    - Luminosité  : {(light * 100).ToString("0")}%\n" +
                    $"    - Saturation   : {(satur * 100).ToString("0")}%\n\n" +
                    $"    - Luminance   : {bright.ToString("0")}\n\n" +
                    $"    - Pixel moyen : {colorAverage.ToString()}\n" +
                    $"                              = {Pixel.GetCouleur(colorAverage).ToString()}";

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
                "Pour sauvegarder une image en particulier appuyez sur le bouton 'SAVE' dans l'onglet de l'image à sauvegarder.", "Aide",
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion



        #region Paramètres internes

        private void ModificationActive(int onglet = 0)
        {
            if (this.imageChargée)
            {
                TabPage page = new TabPage("Modification Image n°" + numéroModif++ + "     ")
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(3, 3, 3, 3),
                    Padding = new Padding(2, 2, 2, 2),
                    BackColor = Color.Gainsboro,
                };

                this.Tabs.TabPages.Add(page);
                this.Tabs.SelectedTab = this.Tabs.TabPages[onglet];
            }
            this.PictureBoxParOnglet.Add(new PictureBox());
            this.PictureBoxParOnglet[onglet].BackColor = System.Drawing.Color.Gainsboro;
            this.PictureBoxParOnglet[onglet].Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBoxParOnglet[onglet].Location = new System.Drawing.Point(3, 3);
            this.PictureBoxParOnglet[onglet].Name = Tabs.TabPages.Count.ToString();
            this.PictureBoxParOnglet[onglet].Size = new System.Drawing.Size(689, 383);
            this.PictureBoxParOnglet[onglet].SizeMode = this.realSize ? PictureBoxSizeMode.CenterImage : PictureBoxSizeMode.Zoom;
            this.PictureBoxParOnglet[onglet].TabIndex = onglet;
            this.PictureBoxParOnglet[onglet].TabStop = false;
            this.PictureBoxParOnglet[onglet].Image = this.imagesParOnglet[onglet].ToBitmap();

            this.PictureBoxParOnglet[onglet].MouseDown += this.pictureBox_MouseDown;
            this.PictureBoxParOnglet[onglet].MouseMove += this.pictureBox_MouseMove;
            this.PictureBoxParOnglet[onglet].MouseUp += this.pictureBox_MouseUp;
            
            this.Tabs.TabPages[onglet].Controls.Add(this.PictureBoxParOnglet[onglet]);
            this.currentIndexImage = onglet;
            Cursor.Current = Cursors.Default;
        }


        private void clear_Click(object sender, EventArgs e)
        {
            ClearEverything("Vous avez plusieurs onglets ouverts, voulez-vous vraiment tous les supprimer ?");
        }

        private DialogResult ClearEverything(string MessageErreur)
        {
            DialogResult result = DialogResult.None;
            if (Tabs.TabCount > 1)
            {
                result = MessageBox.Show("Vous avez plusieurs onglets ouverts ! Voulez-vous vraiment tous les supprimer ?", "Attention !",
                       MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    this.imagesParOnglet = new List<MyImage>();
                    for (int i = Tabs.TabCount - 1; i > 0; i--)
                    {
                        this.PictureBoxParOnglet.RemoveAt(i);
                    }
                    SupprimerOnglets();
                    this.PictureBoxParOnglet[0].Image = AddImageHelperImagePrincipale();
                    this.sauvegarderToolStripMenuItem.Enabled = false;
                    this.imageChargée = false;
                }
            }
            else if (this.imageChargée)
            {
                this.imagesParOnglet = new List<MyImage>();
                SupprimerOnglets();
                for (int i = 1; i < Tabs.TabCount; i++)
                {
                    this.PictureBoxParOnglet.RemoveAt(i);
                }
                this.PictureBoxParOnglet[0].Image = AddImageHelperImagePrincipale();
                this.sauvegarderToolStripMenuItem.Enabled = false;
                this.imageChargée = false;
            }

            return result;
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            this.checkedListBox1.Visible = !this.checkedListBox1.Visible;
            this.SaveButton.Location = new System.Drawing.Point(this.SaveButton.Location.X, this.SaveButton.Location.Y + (this.checkedListBox1.Height * (this.checkedListBox1.Visible ? 1 : -1)));

            this.SaveButton.Height = this.SaveButton.Height + this.checkedListBox1.Height * (this.checkedListBox1.Visible ? -1 : 1);
        }


        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            List<string> checkedItems = new List<string>();

            List<string> uncheckedItems = new List<string>();

            if (e.NewValue == CheckState.Checked)
                checkedItems.Add(checkedListBox1.Items[e.Index].ToString());
            else
                uncheckedItems.Add(checkedListBox1.Items[e.Index].ToString());

            foreach (string s in checkedItems)
            {
                if (s == "Mode Nuit")
                {
                    this.modeNuit = true;
                    menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CouleurMenuDéroulant(this.modeNuit));
                    this.Originale.BackColor = Color.Black;

                    foreach (var pb in PictureBoxParOnglet)
                        pb.BackColor = Color.FromArgb(31, 31, 31);

                    this.menuStrip1.BackColor = Color.FromArgb(31, 31, 31);
                    this.BackColor = Color.FromArgb(31, 31, 31);
                    this.Settings.BackColor = Color.FromArgb(61, 61, 61);
                    this.checkedListBox1.BackColor = Color.FromArgb(61, 61, 61);
                    this.menuStrip1.ForeColor = Color.White;
                }
                if (s == "Real Size")
                {
                    foreach (var pb in PictureBoxParOnglet)
                        pb.SizeMode = PictureBoxSizeMode.CenterImage;
                    this.realSize = true;
                }
            }

            foreach (string s in uncheckedItems)
            {
                if (s == "Mode Nuit")
                {
                    this.modeNuit = false;
                    menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CouleurMenuDéroulant(this.modeNuit));
                    this.Originale.BackColor = Color.White;

                    foreach (var pb in PictureBoxParOnglet)
                        pb.BackColor = Color.Gainsboro;

                    this.menuStrip1.BackColor = Color.WhiteSmoke;
                    this.BackColor = Color.WhiteSmoke;
                    this.Settings.BackColor = Color.DimGray;
                    this.checkedListBox1.BackColor = Color.DimGray;
                    this.menuStrip1.ForeColor = Color.Black;
                }
                if (s == "Real Size")
                {
                    foreach (var pb in PictureBoxParOnglet)
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                    this.realSize = false;
                }
            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush couleurTexte = new SolidBrush(Color.Black);

            if (!modeNuit)
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
            if (e.Index != 0) // Add button to the last TabPage only
            {
                MyImage closeImage = new MyImage(@"C:\Users\François\Documents\C#\Solution finale\Solution finale\close.bmp");
                new MyGraphics(closeImage).Redimensionnement(20, 20);
                e.Graphics.DrawImage(closeImage.ToBitmap(),
                    (tabRect.Right - closeImage.GetWidth),
                    tabRect.Top + (tabRect.Height - closeImage.GetHeight) / 2);
                TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font,
                    tabRect, tabPage.ForeColor, TextFormatFlags.Left);
            }
            else
            {
                Rectangle paddedBounds = e.Bounds;
                paddedBounds.Inflate(-2, -2);
                e.Graphics.DrawString(this.Tabs.TabPages[e.Index].Text, this.Font, couleurTexte, paddedBounds);
            }

        }


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
                    this.PictureBoxParOnglet.RemoveAt(i);
                    this.imagesParOnglet.RemoveAt(i);
                    this.Tabs.SelectedTab = this.Tabs.TabPages[i - 1];
                    if (this.Tabs.TabCount == 1)
                        this.numéroModif = 1;
                    break;
                }
            }
            this.currentIndexImage = this.Tabs.SelectedIndex;
        }

        private void SupprimerOnglets()
        {
            for (int i = Tabs.TabCount - 1; i > 0; i--)
            {
                this.Tabs.TabPages.RemoveAt(i);
            }
            this.currentIndexImage = 0;
            this.numéroModif = 1;
        }



        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.imageLocation = e.Location;
                this.imageIsMoving = true;
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.imageIsMoving && this.realSize && 
                (this.PictureBoxParOnglet[this.currentIndexImage].Image.Width > this.PictureBoxParOnglet[this.currentIndexImage].Width ||
                    this.PictureBoxParOnglet[this.currentIndexImage].Image.Height > this.PictureBoxParOnglet[this.currentIndexImage].Height))
            {

            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {

            this.imageIsMoving = false;
        }

        #endregion



        private Bitmap AddImageHelperImagePrincipale()
        {
            int height = this.PictureBoxParOnglet.Count != 0 ? this.PictureBoxParOnglet[0].Height : this.Originale.Height - this.Originale.Padding.Vertical;
            int width = this.PictureBoxParOnglet.Count != 0 ? this.PictureBoxParOnglet[0].Width : this.Originale.Width - this.Originale.Padding.Horizontal;

            Bitmap bmp = new Bitmap(width, height);
            Graphics.FromImage(bmp).DrawString(" Veuillez charger\n\rune image depuis\r\n le menu 'fichier'",
                new Font("Times New Roman", 40, FontStyle.Bold), Brushes.Black, new System.Drawing.Point(65, 70));
            Graphics.FromImage(bmp).DrawString("ou créez-en une depuis les menus\r\n         'Fractales' et 'Création'",
                new Font("Times New Roman", 17, FontStyle.Regular), Brushes.Black, new System.Drawing.Point(110, 270));

            return bmp;
        }
    }
}
