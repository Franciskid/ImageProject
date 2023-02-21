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
    public partial class Pixellisation : Form
    {
        private int width;

        public int taillePixel = 1;

        public bool adaptateur = true;

        public Pixellisation(int width)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.width = width;
            InitializeComponent();
            Cursor.Current = Cursors.Default;
        }

        private void Pixélisation_Load(object sender, EventArgs e)
        {
            this.trackBar1.Maximum = this.width;
            this.trackBar1.Value = this.taillePixel = this.width * 10 / 100;
            this.textBoxPixel.Text = taillePixel.ToString();
            this.KeepAspectRatio.Checked = true;
            this.labelMax.Text = this.width.ToString();
        }



        private void taille_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private bool userChange = true;

        private void taille_TextChanged(object sender, EventArgs e)
        {
            if (this.userChange)
            {
                this.userChange = false;

                this.taillePixel = int.TryParse((sender as TextBox).Text, out int value) ? value : 1;

                this.textBoxPourcentage.Text = Math.Ceiling(this.taillePixel * 100 / this.width > 100 ? 100 : this.taillePixel * 100.0 / this.width).ToString();

                this.trackBar1.Value = this.taillePixel > this.width ? this.width : this.taillePixel;

                this.userChange = true;
            }
        }

        private void textBoxPourcentage_TextChanged(object sender, EventArgs e)
        {
            if (this.userChange)
            {
                this.userChange = false;

                this.taillePixel = int.TryParse((sender as TextBox).Text, out int value) ? value * this.width / 100 : 1;

                this.textBoxPixel.Text = (this.taillePixel > this.width ? this.width : this.taillePixel).ToString();

                this.trackBar1.Value = this.taillePixel > this.width ? this.width : this.taillePixel;

                this.userChange = true;
            }
        }


        private void KeepAspectRatio_CheckedChanged(object sender, EventArgs e)
        {
            this.adaptateur = this.KeepAspectRatio.Checked;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.taillePixel = (sender as TrackBar).Value;
            this.textBoxPixel.Text = this.taillePixel.ToString();
        }

    }
}
