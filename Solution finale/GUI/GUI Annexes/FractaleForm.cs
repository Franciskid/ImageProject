using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Photoshop3000.Annexes;
using Photoshop3000.GUI.GUI_Annexes;

namespace Photoshop3000
{
    public partial class FractaleForm : Form
    {
        public int height { get; private set; } = 500;
        public int width { get; private set; } = 500;
        public int maxTryColor { get; private set; } = 10;
        public int maxItération { get; private set; } = 200;
        public double Contraste { get; private set; } = 1;
        public double SizeJulia { get; private set; } = 1.75;
        public int TailleMosaique { get; private set; } = 20;
        internal Fractale.Fractales type { get; private set; }
        private Fractale.Fractales typefct { get; set; }
        internal Pixel BackgroundColor => Pixel.FromColor(this.But_Color.BackColor);

        internal NombreComplex userComp = 0;

        public MyImage imageMosaique { get; private set; }


        private int selected = 0;

        private bool firstSelect = false;
        private bool secondSelect = false;


        public FractaleForm(MyImage image)
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            this.imageMosaique = image;

            if (this.imageMosaique != null)
            {
                AjoutImageBouton();
            }
            Cursor.Current = Cursors.Default;

            UpdatePourc();

            this.comboBox_Func.SelectedIndex = 0;
            this.comboBox_Func.DropDownStyle = ComboBoxStyle.DropDownList;
        }



        private void LB_equations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.firstSelect && !this.secondSelect)
            {
                this.firstSelect = true;
                this.TB_iteration.Enabled = true;
                this.TB_height.Enabled = true;
                this.TB_width.Enabled = true;
                this.SaveButton.Enabled = true;
            }

            if (this.secondSelect)
            {
                this.LB_récursiff.SetSelected(this.selected, false);
                this.secondSelect = false;
                this.firstSelect = true;
            }
            this.selected = this.LB_equations.SelectedIndex;

            this.TB_contraste.Enabled = true;
            this.TB_sizeJulia.Enabled = true;

            this.But_imageMosaique.Enabled = false;
            this.TB_tryColor.Enabled = false;
            this.TB_mosaique.Enabled = false;
            this.TB_iteration.Text = "150";

            this.But_Color.Enabled = true;
            this.But_Color.BackColor = Color.Black;

            this.TB_Im.Enabled = this.TB_Re.Enabled = true;// this.selected == this.LB_equations.Items.Count - 1;

            NombreComplex comp = Fractale.FractalesToComp((Fractale.Fractales)this.LB_equations.SelectedIndex);

            this.TB_Im.Text = comp.Im.ToString();
            this.TB_Re.Text = comp.Re.ToString();

            this.type = (Fractale.Fractales)this.selected;
        }

        private void LB_récursiff_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.firstSelect && !this.secondSelect)
            {
                this.secondSelect = true;
                this.TB_iteration.Enabled = true;
                this.TB_height.Enabled = true;
                this.TB_width.Enabled = true;
                this.SaveButton.Enabled = true;
            }

            if (this.firstSelect)
            {
                this.LB_equations.SetSelected(this.selected, false);
                this.firstSelect = false;
                this.secondSelect = true;
            }
            this.selected = this.LB_récursiff.SelectedIndex;
            
            if (this.selected == 10)
            {
                this.But_imageMosaique.Enabled = true;
                this.TB_mosaique.Text = "50";
                this.TB_height.Enabled = false;
                this.TB_width.Enabled = false;
            }
            else
            {
                this.But_imageMosaique.Enabled = false;
                this.TB_mosaique.Text = "30";
                this.TB_height.Enabled = true;
                this.TB_width.Enabled = true;
            }

            if (this.selected >= 8)
            {
                this.TB_iteration.Text = "25";
                this.But_Color.Enabled = this.selected != 10;
            }
            else
            {
                this.But_Color.Enabled = true;
                this.TB_iteration.Text = "6";
            }

            this.But_Color.BackColor = Color.White;

            this.TB_tryColor.Enabled = this.selected >= 8;

            this.TB_mosaique.Enabled = this.selected >= 9;
            this.TB_contraste.Enabled = false;
            this.TB_sizeJulia.Enabled = false;

            this.type = (Fractale.Fractales)(this.selected + Fractale.Fractales.UserEquationZSqrd + 1);
        }



        private void But_imageMosaique_Click(object sender, EventArgs e)
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
                this.imageMosaique = new MyImage(FileName);

                if (this.imageMosaique.Validité || (this.imageMosaique.Width >= 2 && this.imageMosaique.Height >= 2))
                {
                    AjoutImageBouton();
                }
                else
                {
                    MessageBox.Show("L'image n'est pas valide ! Elle est peut-être trop petite (doit etre supérieur à 2 pixels en largeur et hauteur).",
                               "Erreur : Impossible de charger l'image", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void AjoutImageBouton()
        {
            Cursor.Current = Cursors.WaitCursor;

            MyGraphics g = new MyGraphics(this.imageMosaique.Clone())
            {
                InterpolationMode = InterpolationMode.NearestNeighbour,
                KeepAspectRatio = true
            };
            g.Redimensionnement(this.But_imageMosaique.Height, this.But_imageMosaique.Width);

            this.But_imageMosaique.BackgroundImage = g.MyImage.ToBitmap();


            Cursor.Current = Cursors.Default;
        }



        private void TB_height_TextChanged(object sender, EventArgs e)
        {
            this.height = int.TryParse(this.TB_height.Text, out int value) ? value : 0;
        }

        private void TB_width_TextChanged(object sender, EventArgs e)
        {
            this.width = int.TryParse(this.TB_width.Text, out int value) ? value : 0;
        }

        private void TB_Re_TextChanged(object sender, EventArgs e)
        {
            this.userComp.Re = double.TryParse(new string((from s in (sender as TextBox).Text.Replace(".", ",") where !char.IsLetter(s) select s).ToArray()), out double value) ? value : 0;
        }

        private void TB_Im_TextChanged(object sender, EventArgs e)
        {
            this.userComp.Im = double.TryParse(new string((from s in (sender as TextBox).Text.Replace(".", ",") where !char.IsLetter(s) select s).ToArray()), out double value) ? value : 0;
        }


        private void TB_contraste_TextChanged(object sender, EventArgs e)
        {
            string newS = (sender as TextBox).Text;
            newS = newS.Replace(".", ",");

            this.Contraste = float.TryParse(newS, out float value) ? value : 1;
        }

        private void TB_mosaique_TextChanged(object sender, EventArgs e)
        {
            this.TailleMosaique = int.TryParse(this.TB_mosaique.Text, out int value) ? value : 0;
        }

        private void TB_tryColor_TextChanged(object sender, EventArgs e)
        {
            this.maxTryColor = int.TryParse(this.TB_tryColor.Text, out int value) ? value : 0;

            UpdatePourc();
        }

        private void TB_iteration_TextChanged(object sender, EventArgs e)
        {
            this.maxItération = int.TryParse(this.TB_iteration.Text, out int value) ? value : 0;
        }



        private void TB_Im_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-') && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '-') && ((sender as TextBox).Text.LastIndexOf('-') != -1 && (sender as TextBox).Text.IndexOf('-') != -1))
            {
                e.Handled = true;
            }
            
        }

        private void TB_Re_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-') && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '-') && ((sender as TextBox).Text.LastIndexOf('-') != -1 && (sender as TextBox).Text.IndexOf('-') != -1))
            {
                e.Handled = true;
            }
        }

        private void TB_height_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TB_width_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TB_mosaique_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TB_contraste_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void TB_tryColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TB_sizeJulia_TextChanged(object sender, EventArgs e)
        {
            this.SizeJulia = double.TryParse(new string((from s in (sender as TextBox).Text.Replace(".", ",") where !char.IsLetter(s) select s).ToArray()), out double value) ? value : 0;
        }

        private void FractaleForm_Load(object sender, EventArgs e)
        {
            this.TB_height.Text = this.height.ToString();
            this.TB_width.Text = this.width.ToString();
            this.TB_tryColor.Text = this.maxTryColor.ToString();
            this.TB_contraste.Text = this.Contraste.ToString();
            this.TB_iteration.Text = this.maxItération.ToString();
            this.SaveButton.Enabled = false;
            this.But_Color.Enabled = false;
            this.But_Color.BackColor = this.BackgroundColor.ToColor();
        }

        private void comboBox_Func_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.typefct = this.comboBox_Func.SelectedIndex == 0 ? 
                Fractale.Fractales.UserEquationZSqrd : this.comboBox_Func.SelectedIndex == 1 ?
                Fractale.Fractales.UserEquationZSin : Fractale.Fractales.UserEquationNewton;
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (((this.height <= 10 || this.width <= 10) && this.type != Fractale.Fractales.Mosaique_From_Image)|| this.maxItération <= 0
                || (this.type == Fractale.Fractales.Mosaique_From_Image && this.imageMosaique == null))
            {
                MessageBox.Show("La largeur et la hauteur de l'image doivent être supérieures à 10 !\nL'itération doit aussi être supérieure à 0\n" +
                    "Il faut aussi charger une image avant d'en créer une mosaïque", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (this.firstSelect)
                    this.type = this.typefct;

                this.DialogResult = DialogResult.Yes;
            }
        }

        private void But_Color_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.But_Color.BackColor = this.colorDialog1.Color;
            }
        }


        private void UpdatePourc()
        {
            int amountClr = Enum.GetValues(typeof(Couleurs)).Cast<int>().Max() + 1;

            double probaBinoNP = 1 - Formats.LoiBinomiale(this.maxTryColor, 0, (double)1 / amountClr); //Au moins un bon essai, P(X >= 1) = 1 - P(X=0)

            int probaScale100 = (int)Math.Round(probaBinoNP * 100);

            this.label_Pourc.Text = (probaScale100 >= 0 ? probaScale100 : 0) + "%";
        }



        private void button_JuliaForm_Click(object sender, EventArgs e)
        {
            using (JuliaForm julia = new JuliaForm())
            {
                julia.ShowDialog();
            }
        }
    }
}
