using System;
using System.Windows.Forms;

namespace Photoshop3000
{
    public partial class ModifImage : Form
    {
        private bool imageChargée = false;
        private double redimensionMultiplicateur = 1;

        public int newHeight = 0, newWidth = 0;
        private int height = 0, width = 0;

        private bool userRognage = false, userResize = false, userRotation = false;

        private double newRatioHeight = 1, newRatioWidth = 1;

        public Point topLeft = new Point(0, 0);
        public Point bottomRight = new Point(0, 0);

        public InterpolationMode quality = InterpolationMode.Bicubique;

        public Pixel remplissage = Pixel.Zero;

        public bool b_KeepAspectRatio = true;

        public bool BordsRemplis = false;

        public double angleRotation = 0;

        private MyImage imageLoad;


        public ModifImage(MyImage image)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.imageChargée = image != null;
            this.imageLoad = image;

            this.newHeight = this.height = imageChargée ? image.Height : 0;
            this.newWidth = this.width = imageChargée ? image.Width : 0;
            InitializeComponent();

            Cursor.Current = Cursors.Default;

            this.colorDialog1.CustomColors = new int[] { System.Drawing.Color.FromArgb(251658240).ToArgb(),
            System.Drawing.Color.Black.ToArgb()};
        }


        #region Rotation

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.texteRotation.Text = $"Rotation : {this.trackBar1.Value.ToString()}°";
            this.Angle.Text = this.trackBar1.Value.ToString();
            this.angleRotation = this.trackBar1.Value;
        }

        private void Angle_TextChanged(object sender, EventArgs e)
        {
            this.trackBar1.Value = int.TryParse(this.Angle.Text, out int value) ? (value % 360) > 180 ? (value % 360) - 360 : 
                (value % 360) < -180 ? 360 + (value % 360) : value == 180 ? 180 : value == -180 ? -180 : value % 180 : 0;
            this.texteRotation.Text = $"Rotation : {this.trackBar1.Value.ToString()}°";
            this.angleRotation = this.trackBar1.Value;
        }

        private void Angle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '-') && ((sender as TextBox).Text.LastIndexOf('-') != -1 && (sender as TextBox).Text.IndexOf('-') != -1))
            {
                e.Handled = true;
            }

        }


        private void degré0_Click(object sender, EventArgs e)
        {
            this.Angle.Text = (0).ToString();
        }

        private void degré45_Click(object sender, EventArgs e)
        {
            this.Angle.Text = (45).ToString();
        }

        private void degré90_Click(object sender, EventArgs e)
        {
            this.Angle.Text = (90).ToString();
        }

        private void degré180_Click(object sender, EventArgs e)
        {
            this.Angle.Text = (180).ToString();
        }

        #endregion
        

        #region Redimensionnement

        private void multiplicateurText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)ConsoleKey.Enter)
            {
                this.DialogResult = DialogResult.Yes;
            }

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('-') != -1)
            {
                e.Handled = true;
            }
        }

        private void multiplicateurText_TextChanged(object sender, EventArgs e)
        {
            this.redimensionMultiplicateur = double.TryParse((sender as TextBox).Text.Replace(".", ","), out double value) ? value : 0;

            this.newWidth = this.redimensionMultiplicateur != 0 ? (int)(this.width * this.redimensionMultiplicateur) : this.width;
            this.newHeight = this.redimensionMultiplicateur != 0 ? (int)(this.height * this.redimensionMultiplicateur) : this.height;

            //this.newRatioHeight = this.redimensionMultiplicateur;
            //this.newRatioWidth = this.redimensionMultiplicateur;

            this.largeur.Text = newWidth.ToString();
            this.hauteur.Text = newHeight.ToString();
        }



        private void largeur_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void largeur_TextChanged(object sender, EventArgs e)
        {
            this.newWidth = int.TryParse((sender as TextBox).Text, out int value) ? value : 0;

            //this.newRatioWidth = this.newWidth != 0 ? this.width / this.newWidth : 0;

            this.labeltailleRognage.Text = $"Taille après rognage : {this.newWidth}x{this.newHeight}";

            //this.rightX.Text = (this.newWidth - 1).ToString();
        }


        private void hauteur_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void hauteur_TextChanged(object sender, EventArgs e)
        {
            this.newHeight = int.TryParse((sender as TextBox).Text, out int value) ? value : 0;

            //this.newRatioHeight = this.newHeight != 0 ? this.height / this.newHeight : 0;

            this.labeltailleRognage.Text = $"Taille après rognage : {this.newWidth}x{this.newHeight}";

            //this.rightY.Text = (this.newHeight - 1).ToString();
        }



        private void multi05_Click(object sender, EventArgs e)
        {
            this.multiplicateurText.Text = "0.5";
        }

        private void multi1_Click(object sender, EventArgs e)
        {
            this.multiplicateurText.Text = "1.0";
        }

        private void multi2_Click(object sender, EventArgs e)
        {
            this.multiplicateurText.Text = "2.0";
        }

        private void multi4_Click(object sender, EventArgs e)
        {
            this.multiplicateurText.Text = "5.0";
        }

        #endregion


        #region Rognage

        private void leftX_TextChanged(object sender, EventArgs e)
        {
            this.topLeft.X = int.TryParse((sender as TextBox).Text, out int value) ? value : 0;
            //this.newWidth -= (int)this.topLeft.X;
            //this.largeur.Text = newWidth.ToString();
        }
        private void leftX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void leftY_TextChanged(object sender, EventArgs e)
        {
            this.topLeft.Y = int.TryParse((sender as TextBox).Text, out int value) ? value : 0;
            //this.newHeight -= (int)this.topLeft.Y;
            //this.hauteur.Text = newHeight.ToString();
        }
        private void leftY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void rightX_TextChanged(object sender, EventArgs e)
        {
            this.bottomRight.X = int.TryParse((sender as TextBox).Text, out int value) && value != 0 ? value : this.width - 1;
            //this.newWidth -= this.width - (int)this.bottomRight.X - 1;
            ////this.largeur.Text = newWidth.ToString();
            //if (this.userChange) 
            //    //this.rightX.Text = bottomRight.X.ToString();
        }
        private void rightX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void rightY_TextChanged(object sender, EventArgs e)
        {
            this.bottomRight.Y = int.TryParse((sender as TextBox).Text, out int value) && value != 0 ? value : this.height - 1;
            //this.newHeight -= this.height - (int)this.bottomRight.Y - 1;
            //this.hauteur.Text = newHeight.ToString();
            //if (this.userChange)
            //    this.rightY.Text = bottomRight.Y.ToString();
        }
        private void rightY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion


        
        #region Paramètres

        private void couleurLab_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.couleurLab.BackColor = this.colorDialog1.Color;
                this.remplissage = Pixel.FromColor(this.colorDialog1.Color); //Le colorDialog ne prend pas en compte la transparence... 
            }                         // Même le choix d'un ajout manuel d'une couleur avec A != 255 renvoie la couleur avec A = 255
        }


        private void KeepAspectRatio_CheckedChanged(object sender, EventArgs e)
        {
            b_KeepAspectRatio = this.KeepAspectRatio.Checked;
        }


        private bool userChange = true;
        private void Interpolation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.userChange)
            {
                this.userChange = false;
                if (this.BordsRemplis)
                {
                    if (this.Interpolation.SelectedIndex != 0 && this.Interpolation.SelectedIndex != 2)
                    {
                        if (this.quality == InterpolationMode.Bicubique)
                        {
                            this.Interpolation.SetSelected(2, true);
                        }
                        else
                        {
                            this.Interpolation.SetSelected(0, true);
                        }
                    }
                }
                this.userChange = true;

                if (this.Interpolation.SelectedIndex <= 2)
                {
                    this.quality = (InterpolationMode)this.Interpolation.SelectedIndex;
                }
                else if (this.Interpolation.SelectedIndex >= 3 && this.Interpolation.SelectedIndex <= 7)
                {
                    this.quality = (InterpolationMode)(this.Interpolation.SelectedIndex + (InterpolationMode.Windows_Bilinear - 1 - InterpolationMode.Bicubique));
                }
                else
                {
                    this.quality = (InterpolationMode)(this.Interpolation.SelectedIndex - (InterpolationMode.Windows_HighQualityBicubic + 1 - InterpolationMode.Windows_Bilinear));
                }
            }
        }


        #endregion


        private void checkBox1_CheckedChanged(object sender, EventArgs e) //Bords
        {
            this.BordsRemplis = this.BordsCB.Checked;
            if (this.Interpolation.SelectedIndex != 2 && this.Interpolation.SelectedIndex != 0 && this.BordsRemplis) //Bili
                this.Interpolation.SetSelected(2, true); //Bicu
        }

        private void imageLoadBut_Click(object sender, EventArgs e)
        {
            using (RognageImage rognage = new RognageImage(this.imageLoad, newRatioHeight, newRatioWidth))
            {
                if (rognage.ShowDialog() == DialogResult.Yes)
                {
                    this.topLeft = rognage.realOrigine;
                    this.bottomRight = rognage.realDest;

                    this.leftX.Text = topLeft.X.ToString();
                    this.leftY.Text = topLeft.Y.ToString();
                    this.rightX.Text = bottomRight.X.ToString();
                    this.rightY.Text = bottomRight.Y.ToString();
                }
            }
        }

        private void ModifImage_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.OriginalSize.Text = $"Taille d'origine : {this.width}x{this.height}";

            if (this.imageChargée)
            {
                this.imageLoadBut.Enabled = true;
                MyGraphics g = new MyGraphics(this.imageLoad.Clone())
                {
                    KeepAspectRatio = false
                };
                g.Redimensionnement(this.imageLoadBut.Height, this.imageLoadBut.Width);
                this.imageLoadBut.BackgroundImage = g.MyImage.ToBitmap();
            }
            this.Interpolation.SelectedIndex = 1;

            this.largeur.Text = this.width.ToString();
            this.hauteur.Text = this.height.ToString();

            this.userChange = false;

            this.leftX.Text = this.imageLoad == null ? "///" : "0";
            this.leftY.Text = this.imageLoad == null ? "///" : "0";

            this.rightX.Text = this.imageLoad == null ? "///" : (this.width - 1).ToString();
            this.rightY.Text = this.imageLoad == null ? "///" : (this.height - 1).ToString();

            this.userChange = true;


            this.icoLab.Image = System.Drawing.Bitmap.FromHicon(System.Drawing.SystemIcons.Information.Handle);

            this.Interpolation.SetSelected(this.quality == InterpolationMode.Bicubique ? 2 :
                this.quality == InterpolationMode.Bilineaire ? 1 : 0, true);

            Cursor.Current = Cursors.Default;
        }

    }
}
