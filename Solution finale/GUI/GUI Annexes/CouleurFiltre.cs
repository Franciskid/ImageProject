using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Photoshop3000
{
    public partial class CouleurFiltre : Form
    {
        public bool Rouge { get; private set; } = false;
        public bool Vert { get; private set; } = false;
        public bool Bleu { get; private set; } = false;
        public bool Sépia { get; private set; } = false;

        public bool Inversion { get; private set; } = false;

        public bool NoirEtBlanc => this.pages[0].Enabled;
        public int NoirEtBlancValue => this.pages[0].Value;

        public bool TransfoLum => this.pages[1].Enabled;
        public int TransfoLumValue => this.pages[1].Value;

        public bool TransfoCouleur => this.pages[2].Enabled;
        public int TransfoCouleurValue => this.pages[2].Value;


        public bool RougeIntensité => this.pages[3].Enabled;
        public bool RougeIntensitéHigher => this.pages[3].CheckBoxActivated;
        public int RougeIntensitéValue => this.pages[3].Value * 255 / 100;

        public bool VertIntensité => this.pages[4].Enabled;
        public bool VertIntensitéHigher => this.pages[4].CheckBoxActivated;
        public int VertIntensitéValue => this.pages[4].Value * 255 / 100;

        public bool BleuIntensité => this.pages[5].Enabled;
        public bool BleuIntensitéHigher => this.pages[5].CheckBoxActivated;
        public int BleuIntensitéValue => this.pages[5].Value * 255 / 100;



        /// <summary>
        /// Indique si c'est bien l'utilisateur qui a changé les index
        /// </summary>
        private bool userListBoxChange = false;

        private ListPage pages;

        public CouleurFiltre()
        {
            Cursor.Current = Cursors.WaitCursor;

            InitializeComponent();

            this.OkButton.Enabled = false;
            this.icoLab.Image = Bitmap.FromHicon(SystemIcons.Information.Handle);

            Cursor.Current = Cursors.Default;

            this.pages = new ListPage(this)
            {
                new UniquePage("Noir et Blanc Intensité")
                {
                    Value = 0,
                    Enabled = false,
                    CheckBoxVisible = false,
                    ToolTip = "Transforme les pixels en des nuances de gris, de noir et de blanc " +
                "en fonction d'un facteur %.\r\n100 % revient à changer la photo qu'en noir et qu'en blanc," +
                "\r\n0 % revient à mettre la photo en nuance de gris normalement",
                },
                new UniquePage("Luminosité Intensité")
                {
                    Value = 50,
                    Enabled = false,
                    CheckBoxVisible = false,
                    ToolTip = "Transforme la luminosité de l'image.\r\nOn définit 50% comme " +
                "étant la luminosité actuelle.\r\n0 % revient à mettre l'image en noir.\r\n100 % revient à la changer en blanc."
                },
                new UniquePage("Couleur Intensité")
                {
                    Value = 100,
                    Enabled = false,
                    CheckBoxVisible = false,
                    ToolTip = "Atténue les couleurs d'une image en fonction d'un %.\n" +
                "100% est l'intensité de couleur actuelle, 0% est l'image en noir et blanc d'intensité 0% (cf 'Noir et Blanc')",
                },

                new UniquePage("Rouge intensité")
                {
                    Value = 0,
                    Enabled = false,
                    CheckBoxVisible = true,
                    CheckBoxActivated = true,
                    ToolTip = "Intensité à partir de laquelle les pixels ne sont pas changés en gris.",
                },
                new UniquePage("Vert intensité")
                {
                    Value = 0,
                    Enabled = false,
                    CheckBoxVisible = true,
                    CheckBoxActivated = true,
                    ToolTip = "Intensité à partir de laquelle les pixels ne sont pas changés en gris.",
                },
                new UniquePage("Bleu Intensité")
                {
                    Value = 0,
                    Enabled = false,
                    CheckBoxVisible = true,
                    CheckBoxActivated = true,
                    ToolTip = "Intensité à partir de laquelle les pixels ne sont pas changés en gris.",
                },
            };

            this.pages.GoToIndex(0);
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int lastSelectedIndex = (typeof(ListBox).GetProperty("FocusedIndex", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(this.listBox1, null) as int?) ?? -1;

            if (this.listBox1.SelectedIndices.Count == 0)
            {
                this.OkButton.Enabled = false;
            }
            else if (this.userListBoxChange)
            {
                this.OkButton.Enabled = true;
                this.userListBoxChange = false; //Sinon on rentre dans une boucle infinie à chaque chgmt de selections

                if (lastSelectedIndex == 0 && this.listBox1.GetSelected(0)) //On ne peut pas utiliser le mode noir et blanc avec les modes de supression de couleur RGB
                {
                    this.listBox1.SetSelected(1, false);
                    this.listBox1.SetSelected(2, false);
                    this.listBox1.SetSelected(3, false);
                    this.listBox1.SetSelected(0, true);
                }
                else if (lastSelectedIndex < 4 || !this.listBox1.GetSelected(0))
                {
                    this.listBox1.SetSelected(0, false);
                }
            }

            this.pages.EnableOrDisablePage(0, this.listBox1.GetSelected(0));
            for (int i = 6; i <= 10; i++)
                this.pages.EnableOrDisablePage(i - 5, this.listBox1.GetSelected(i));

            if (this.listBox1.GetSelected(0) && lastSelectedIndex == 0)
                this.pages.GoToIndex(0);
            else if (lastSelectedIndex >= 6 && lastSelectedIndex <= 10)
                this.pages.GoToIndex(lastSelectedIndex - 5);
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.userListBoxChange = true;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.trackbar.Value = int.TryParse(this.textBox.Text, out int value) ? value < 0 ? 0 : value > 100 ? 100 : value : 0;
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.pages.EnumeratorNewValue(this.trackbar.Value);
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.pages.CheckChange(this.checkBox.Checked);
        }


        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Rouge = this.listBox1.GetSelected(1);
            this.Vert = this.listBox1.GetSelected(2);
            this.Bleu = this.listBox1.GetSelected(3);
            this.Inversion = this.listBox1.GetSelected(4);

            this.Sépia = this.listBox1.GetSelected(5);

            this.pages.EnableOrDisablePage(0, this.listBox1.GetSelected(0));
            this.pages.EnableOrDisablePage(1, this.listBox1.GetSelected(6));
            this.pages.EnableOrDisablePage(2, this.listBox1.GetSelected(7));
            this.pages.EnableOrDisablePage(3, this.listBox1.GetSelected(8));
            this.pages.EnableOrDisablePage(4, this.listBox1.GetSelected(9));
            this.pages.EnableOrDisablePage(5, this.listBox1.GetSelected(10));

        }

        private void OkButton_EnabledChanged(object sender, EventArgs e)
        {
            this.OkButton.BackColor = this.OkButton.BackColor == Color.LimeGreen ? Color.FromArgb(80, 100, 80) : Color.LimeGreen;
        }



        private void button1_Click(object sender, EventArgs e) //forwards
        {
            this.pages.Forward();
        }


        private void button2_Click(object sender, EventArgs e) //backwards
        {
            this.pages.Backward();
        }



        private class ListPage : List<UniquePage>
        {
            private CouleurFiltre form;

            private int currIterator;

            public ListPage(CouleurFiltre form)
            {
                this.form = form;

                currIterator = 0;
            }

            public void EnableOrDisablePage(int position, bool state)
            {
                this[position].Enabled = state;

                if (position == currIterator)
                    this.EnableDisableButtons(state);
            }

            private void EnableDisableButtons(bool state)
            {
                this.form.trackbar.Enabled = this.form.textBox.Enabled = this.form.lab0.Enabled =
                    this.form.lab100.Enabled = this.form.labIntensité.Enabled = this.form.checkBox.Enabled = state;

                this.form.labelBackgroundColor.BackColor = this.form.trackbar.BackColor =
                    this.form.textBox.BackColor = this.form.lab0.BackColor = this.form.lab100.BackColor =
                    this.form.labIntensité.BackColor = this.form.checkBox.BackColor = state ? SystemColors.Control : SystemColors.ControlLight;

            }



            public void Forward()
            {
                if (this.currIterator != this.Count - 1)
                    DisplayPage(this.currIterator + 1);
            }

            public void Backward()
            {
                if (this.currIterator != 0)
                    DisplayPage(this.currIterator - 1);
            }

            private void DisplayPage(int index)
            {
                this.currIterator = index;

                this.form.buttonBack.Enabled = index != 0;
                this.form.buttonForward.Enabled = index != this.Count - 1;
                this.form.labIntensité.Text = this[index].Titre;
                this.form.textBox.Text = this[index].Value.ToString();

                this.form.checkBox.Checked = this[index].CheckBoxActivated;
                this.form.checkBox.Visible = this[index].CheckBoxVisible;
                this.form.checkBox.Text = "Plus grand";

                string txt = this[index].ToolTip;

                this.form.toolTip1.SetToolTip(this.form.trackbar, txt);

                this.form.toolTip1.SetToolTip(this.form.textBox, txt);

                this.form.toolTip1.SetToolTip(this.form.labIntensité, txt);

                EnableDisableButtons(this[index].Enabled);
            }

            public void GoToIndex(int index)
            {
                DisplayPage(index);
            }


            public void EnumeratorNewValue(int value)
            {
                this[this.currIterator].Value = value;
                this.form.textBox.Text = value.ToString();
            }

            public void CheckChange(bool state)
            {
                this[this.currIterator].CheckBoxActivated = state;
            }

        }

        private class UniquePage
        {
            public string ToolTip { get; set; } = "";

            public string Titre { get; set; } = "";

            public bool Enabled { get; set; } = false;

            public bool CheckBoxActivated { get; set; } = false;


            public bool CheckBoxVisible { get; set; } = false;

            public int Value { get; set; } = 0;

            public UniquePage(string titre)
            {
                this.Titre = titre;
            }

        }

    }

}
