using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Photoshop3000
{
    public partial class Décalage : Form
    {
        public double HauteurDécal { get; private set; } = 0.2;

        public double NombreDécal { get; private set; } = 5;

        public bool sin { get; private set; } = false;
        public bool cos { get; private set; } = false;

        public bool cercleTest { get; private set; } = false;

        public bool largeur { get; private set; } = true;

        public int intensitéDécal { get; private set; } = 10;

        public int largeurDécal { get; private set; } = 10;

        public Décalage()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            Cursor.Current = Cursors.Default;
        }

        private void Décalage_Load(object sender, EventArgs e)
        {
            this.textBoxHauteur.Text = this.HauteurDécal.ToString();
            this.textBoxNombre.Text = this.NombreDécal.ToString();
            this.listBoxFct.SetSelected(2, true);
            this.trackBarIntensité.Value = this.intensitéDécal;
            this.trackBarlargeur.Maximum = 100;
            this.trackBarlargeur.Value = this.largeurDécal;
            this.labelIntensité.Text = intensitéDécal.ToString();
            this.labelLargeur.Text = largeurDécal.ToString();
        }



        private void décalage_KeyPress(object sender, KeyPressEventArgs e)
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

        private void hauteur_TextChanged(object sender, EventArgs e)
        {
            string newS = (sender as TextBox).Text;
            newS = newS.Replace(".", ",");
            this.HauteurDécal = double.TryParse(newS, out double value) ? value : 0;
        }
        private void nombre_TextChanged(object sender, EventArgs e)
        {
            string newS = (sender as TextBox).Text;
            newS = newS.Replace(".", ",");
            this.NombreDécal = double.TryParse(newS, out double value) ? value : 0;
        }




        private void CheckBoxLarg_CheckedChanged(object sender, EventArgs e)
        {
            this.largeur = this.checkBoxLarg.Checked;
            this.checkBoxHaut.Checked = !this.checkBoxLarg.Checked;
        }

        private void CheckBoxHaut_CheckedChanged(object sender, EventArgs e)
        {
            this.largeur = !this.checkBoxHaut.Checked;
            this.checkBoxLarg.Checked = !this.checkBoxHaut.Checked;
        }


        private void ListBoxFct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxFct.GetSelected(0))
            {
                this.sin = true;
                this.cos = false;
                this.trackBarIntensité.Enabled = false;
                this.trackBarlargeur.Enabled = false;
                this.textBoxHauteur.Enabled = this.textBoxNombre.Enabled = true;
            }
            if (this.listBoxFct.GetSelected(1))
            {
                this.sin = false;
                this.cos = true;
                this.trackBarIntensité.Enabled = false;
                this.trackBarlargeur.Enabled = false;
                this.textBoxHauteur.Enabled = this.textBoxNombre.Enabled = true;
            }
            if (this.listBoxFct.GetSelected(2))
            {
                this.sin = false;
                this.cos = false;
                this.trackBarIntensité.Enabled = true;
                this.trackBarlargeur.Enabled = true;
                this.textBoxHauteur.Enabled = this.textBoxNombre.Enabled = false;
            }

            if (this.listBoxFct.GetSelected(3))
            {
                this.cercleTest = true;
            }
            else
                this.cercleTest = false;
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            this.intensitéDécal = this.trackBarIntensité.Value;
            this.labelIntensité.Text = this.intensitéDécal.ToString();
        }

        private void TrackBarlargeur_Scroll(object sender, EventArgs e)
        {
            this.largeurDécal = this.trackBarlargeur.Value;
            this.labelLargeur.Text = this.largeurDécal.ToString();
        }


        private bool userChangeLabel = true;
        private void textPourc_textChanged(object sender, EventArgs e)
        {
            if (userChangeLabel)
            {
                userChangeLabel = false;
                (sender as Label).Text += "%";
                userChangeLabel = true;
            }
        }
    }
}
