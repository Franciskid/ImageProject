using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Photoshop3000
{
    public partial class StéganographieForm : Form
    {
        public MyImage imageToHide { get; private set; }

        public bool HideImage { get; private set; } = false;
        public bool GetImage { get; private set; } = false;

        public bool HideTexte { get; private set; } = false;
        public bool GetTexte { get; private set; } = false;

        public int BitsToHide { get; private set; } = 4;

        public bool AdapatSize { get; private set; } = false;

        public string mdp { get; private set; } = "";
        public string texteToHide { get; private set; } = "";

        public int longueurTexte { get; private set; } = 0;

        private bool AddEndText { get; set; } = false;

        public StéganographieForm()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            Cursor.Current = Cursors.Default;
        }



        private void StéganographieForm_Load(object sender, EventArgs e)
        {
            this.imageButton.Enabled = this.TextButton.Enabled = false;
            this.trackBar1.Value = this.trackBar2.Value = 4;
        }


        //Image

        private void ImageBut_Click(object sender, EventArgs e)
        {
            if (this.HideImage)
            {
                openFileDialog1.InitialDirectory = Program.ADRESSE_SAUVEGARDE;
                openFileDialog1.Filter = "Fichiers BMP|*.bmp|Fichiers PNG|*.png|Fichiers JPEG|*.jpeg|Fichiers CSV" +
                "|*.csv|Fichiers BMP et CSV|*.bmp;*.csv|Tous les fichiers|*.bmp;*.csv;*.jpeg;*.png";
                openFileDialog1.FilterIndex = 5;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.FileName = "";


                DialogResult fichier = openFileDialog1.ShowDialog();
                string FileName = openFileDialog1.FileName;


                if (File.Exists(FileName) && fichier == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    this.imageToHide = new MyImage(FileName);

                    if (this.imageToHide.Validité)
                    {
                        MyGraphics g = new MyGraphics(this.imageToHide.Clone())
                        {
                            InterpolationMode = InterpolationMode.NearestNeighbour,
                            KeepAspectRatio = true
                        };
                        g.Redimensionnement(this.ImageBut.Height, this.ImageBut.Width);

                        this.ImageBut.BackgroundImage = g.MyImage.ToBitmap();

                        ActiverAprèsImage();

                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show("L'image n'est pas valide !","Erreur : Impossible de charger l'image", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ActiverAprèsImage()
        {
            if ((this.ImageBut.BackgroundImage != null && this.HideImage) || this.GetImage)
            {
                if (this.GetImage)
                {
                    this.Path_HideImage3.BackColor = Color.Black;
                    this.Path_HideImage4.BackColor = Color.Black;
                    this.Path_HideImage5.BackColor = Color.Black;
                    this.Path_HideImage6.BackColor = Color.Black;

                    this.imageButton.Enabled = false;
                    this.label14.Enabled = false;
                    this.adapterCB.Enabled = false;
                }
                else
                {
                    this.Path_HideImage3.BackColor = Color.LimeGreen;
                    this.Path_HideImage4.BackColor = Color.LimeGreen;
                    this.Path_HideImage5.BackColor = Color.LimeGreen;
                    this.Path_HideImage6.BackColor = Color.LimeGreen;

                    this.imageButton.Enabled = true;
                    this.label14.Enabled = true;
                    this.adapterCB.Enabled = true;
                }


                this.trackBar1.Enabled = true;
                this.bits_Lab.Enabled = true;
                this.Path_Image1.BackColor = Color.LimeGreen;
                this.Path_Image2.BackColor = Color.LimeGreen;
            }
            else
            {
                this.Path_HideImage3.BackColor = Color.Black;
                this.Path_HideImage4.BackColor = Color.Black;
                this.Path_HideImage5.BackColor = Color.Black;
                this.Path_HideImage6.BackColor = Color.Black;

                if (!this.GetImage)
                {
                    this.trackBar1.Enabled = false;
                    this.bits_Lab.Enabled = false;
                    this.Path_Image1.BackColor = Color.Black;
                    this.Path_Image2.BackColor = Color.Black;
                }

                this.imageButton.Enabled = false;
                this.label14.Enabled = false;
                this.adapterCB.Enabled = false;
            }
        }

        private void CacherImageCB_CheckedChanged(object sender, EventArgs e)
        {
            this.HideImage = CacherImageCB.Checked;

            if (this.HideImage)
            {
                this.GetImageCB.Checked = false;

                this.Path_HideImage1.BackColor = Color.LimeGreen;
                this.Path_HideImage2.BackColor = Color.LimeGreen;

                this.Path_GetImage1.BackColor = Color.Black;
                this.Path_GetImage2.BackColor = Color.Black;
                this.Path_GetImage3.BackColor = Color.Black;
                this.Path_GetImage4.BackColor = Color.Black;

                this.imageButton.Text = "CACHER  IMAGE";

                ActiverAprèsImage();

                this.ImageBut.Enabled = true;
            }
            else
            {
                this.Path_HideImage1.BackColor = Color.Black;
                this.Path_HideImage2.BackColor = Color.Black;

                this.imageButton.Text = "IMAGE";

                ActiverAprèsImage();

                this.ImageBut.Enabled = false;
            }

        }

        private void GetImageCB_CheckedChanged(object sender, EventArgs e)
        {
            this.GetImage = GetImageCB.Checked;

            if (this.GetImage)
            {
                this.CacherImageCB.Checked = false;

                this.Path_GetImage1.BackColor = Color.LimeGreen;
                this.Path_GetImage2.BackColor = Color.LimeGreen;
                this.Path_GetImage3.BackColor = Color.LimeGreen;
                this.Path_GetImage4.BackColor = Color.LimeGreen;

                this.Path_HideImage1.BackColor = Color.Black;
                this.Path_HideImage2.BackColor = Color.Black;
                this.Path_HideImage3.BackColor = Color.Black;
                this.Path_HideImage4.BackColor = Color.Black;
                this.Path_HideImage5.BackColor = Color.Black;
                this.Path_HideImage6.BackColor = Color.Black;

                this.imageButton.Text = "RETROUVER IMAGE";

                ActiverAprèsImage();

                this.label14.Enabled = true;
                this.imageButton.Enabled = true;
            }
            else
            {
                this.Path_GetImage1.BackColor = Color.Black;
                this.Path_GetImage2.BackColor = Color.Black;
                this.Path_GetImage3.BackColor = Color.Black;
                this.Path_GetImage4.BackColor = Color.Black;

                this.imageButton.Text = "IMAGE";

                ActiverAprèsImage();
            }

        }

        private void adapterCB_CheckedChanged(object sender, EventArgs e)
        {
            this.AdapatSize = this.adapterCB.Checked;
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.BitsToHide = this.trackBar1.Value;
            this.bits_Lab.Text = this.BitsToHide.ToString();
        }


        //Texte

        private void TextToHideTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 255)
            {
                e.Handled = true;
            }
        }

        private void LengthTextTB_KeyPress(object s, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void HideText_CheckedChanged(object sender, EventArgs e)
        {
            this.HideTexte = this.HideText.Checked;

            if (this.HideTexte)
            {
                this.getTexteCB.Checked = false;

                this.HideTexte = true;

                this.Path_HideTxt1.BackColor = Color.FromArgb(150, 50, 230);
                this.Path_HideTxt2.BackColor = Color.FromArgb(150, 50, 230);
                this.Path_HideTxt3.BackColor = Color.FromArgb(150, 50, 230);
                this.Path_HideTxt4.BackColor = Color.FromArgb(150, 50, 230);
                this.Path_HideTxt5.BackColor = Color.FromArgb(150, 50, 230);
                this.Path_Text1.BackColor = Color.FromArgb(150, 50, 230);

                ActivationAprèsMdp();

                this.TextButton.Text = "CACHER TEXTE";

                this.addEndCB.Enabled = true;
                this.passwordTB.Enabled = true;
                this.label20.Enabled = true;
                this.TextToHideTB.Enabled = true;
            }
            else
            {
                this.HideTexte = false;

                this.Path_HideTxt1.BackColor = Color.Black;
                this.Path_HideTxt2.BackColor = Color.Black;
                this.Path_HideTxt3.BackColor = Color.Black;
                this.Path_HideTxt4.BackColor = Color.Black;
                this.Path_HideTxt5.BackColor = Color.Black;

                this.Path_Text1.BackColor = Color.Black;
                this.Path_Text2.BackColor = Color.Black;
                this.Path_Text3.BackColor = Color.Black;
                this.Path_Text4.BackColor = Color.Black;
                this.Path_Text5.BackColor = Color.Black;

                this.TextButton.Text = "TEXTE";

                ActivationAprèsMdp();

                this.addEndCB.Enabled = false;
                this.passwordTB.Enabled = false;
                this.label20.Enabled = false;
                this.TextToHideTB.Enabled = false;
            }
        }

        private void getTexteCB_CheckedChanged(object sender, EventArgs e)
        {
            this.GetTexte = this.getTexteCB.Checked;

            if (this.GetTexte)
            {
                this.HideText.Checked = false;

                this.GetTexte = true;

                this.Path_GetText1.BackColor = Color.FromArgb(150, 50, 230);
                this.Path_GetText2.BackColor = Color.FromArgb(150, 50, 230);
                this.Path_GetText3.BackColor = Color.FromArgb(150, 50, 230);
                this.Path_GetText4.BackColor = Color.FromArgb(150, 50, 230);
                this.Path_GetText5.BackColor = Color.FromArgb(150, 50, 230);

                this.Path_Text1.BackColor = Color.FromArgb(150, 50, 230);

                ActivationAprèsMdp();

                this.TextButton.Text = "RETROUVER TEXTE";

                this.LengthTextTB.Enabled = true;
                this.label19.Enabled = true;
                this.passwordTB.Enabled = true;
                this.label20.Enabled = true;
            }
            else
            {
                this.GetTexte = false;

                this.Path_GetText1.BackColor = Color.Black;
                this.Path_GetText2.BackColor = Color.Black;
                this.Path_GetText3.BackColor = Color.Black;
                this.Path_GetText4.BackColor = Color.Black;
                this.Path_GetText5.BackColor = Color.Black;


                this.Path_Text1.BackColor = Color.Black;
                this.Path_Text2.BackColor = Color.Black;
                this.Path_Text3.BackColor = Color.Black;
                this.Path_Text4.BackColor = Color.Black;
                this.Path_Text5.BackColor = Color.Black;

                ActivationAprèsMdp();

                this.TextButton.Text = "TEXTE";

                this.passwordTB.Enabled = false;
                this.label20.Enabled = false;
                this.LengthTextTB.Enabled = false;
                this.label19.Enabled = false;
            }
        }

        private void passwordTB_TextChanged(object sender, EventArgs e)
        {
            ActivationAprèsMdp();
        }

        private void ActivationAprèsMdp()
        {
            if (passwordTB.Text.Length > 0 && (this.GetTexte || this.HideTexte))
            {
                this.Path_Text2.BackColor = Color.FromArgb(150, 50, 230);
                this.Path_Text3.BackColor = Color.FromArgb(150, 50, 230);
                this.Path_Text4.BackColor = Color.FromArgb(150, 50, 230);
                this.Path_Text5.BackColor = Color.FromArgb(150, 50, 230);

                this.label29.Enabled = true;
                this.bitsHideTxtLab.Enabled = true;
                this.trackBar2.Enabled = true;
                this.TextButton.Enabled = true;
            }
            else
            {
                this.Path_Text2.BackColor = Color.Black;
                this.Path_Text3.BackColor = Color.Black;
                this.Path_Text4.BackColor = Color.Black;
                this.Path_Text5.BackColor = Color.Black;

                this.label29.Enabled = false;
                this.bitsHideTxtLab.Enabled = false;
                this.trackBar2.Enabled = false;
                this.TextButton.Enabled = false;
            }
        }


        private void TextButton_Click(object sender, EventArgs e)
        {
            this.mdp = this.passwordTB.Text;

            this.HideImage = false;
            this.GetImage = false;

            this.longueurTexte = int.TryParse(this.LengthTextTB.Text, out int value) ? value : 0;

            this.texteToHide = this.TextToHideTB.Text + (this.AddEndText ? '\u0003'.ToString() : "");
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (this.trackBar2.Value % 2 != 0 && this.trackBar2.Value != 1)
            {
                if (this.BitsToHide > this.trackBar2.Value)
                    this.trackBar2.Value--;
                else
                    this.trackBar2.Value++;
            }
            if (this.trackBar2.Value == 6)
            {
                if (this.BitsToHide > this.trackBar2.Value)
                    this.trackBar2.Value = 4;
                else
                    this.trackBar2.Value = 8;
            }

            this.BitsToHide = this.trackBar2.Value;
            this.bitsHideTxtLab.Text = this.BitsToHide.ToString();
        }


        private void addEndCB_CheckedChanged(object sender, EventArgs e)
        {
            this.AddEndText = this.addEndCB.Checked;
        }

        private void imageButton_Click(object sender, EventArgs e)
        {
            this.HideTexte = false;
            this.GetTexte = false;
        }
    }
}
