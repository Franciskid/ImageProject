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
    public partial class Dessin : Form
    {
        public MyImage imageOnLoad { get; private set; }

        private List<Point> coordonnées = new List<Point>();
        int nombrePointNecessaires = 0;


        private bool imageChargée = true;

        private bool remplissageFormes = true;

        bool tailleChangée = false;
        int newHeight = 500;
        int newWidth = 500;

        int sizeElement = 0;

        bool b_remplissage = false;
        Pixel remplissage = (Pixel)Couleurs.Blanc;
        Pixel currentBGColor = (Pixel)Couleurs.Blanc;


        public Dessin(MyImage myImage)
        {
            Cursor.Current = Cursors.WaitCursor;

            InitializeComponent();
            this.imageOnLoad = myImage;
            this.imageChargée = this.imageOnLoad != null;

            if (!this.imageChargée)
            {
                this.imageOnLoad = new MyImage(this.newHeight, this.newWidth, this.remplissage);
            }

            Cursor.Current = Cursors.Default;
        }

        private void Dessin_Load(object sender, EventArgs e)
        {
            if (this.imageChargée)
            {
                this.textBoxHeight.Enabled = this.textBoxWidth.Enabled = false;

                this.textBoxHeight.Text = this.imageOnLoad.Height.ToString();
                this.textBoxWidth.Text = this.imageOnLoad.Width.ToString();
                this.tailleChangée = false;
            }

            this.pictureBoxMain.Image = this.imageOnLoad.ToBitmap();

            this.icoLab.Image = Icon.FromHandle(SystemIcons.Information.Handle).ToBitmap();

            this.textBoxHeight.Text = this.newHeight.ToString();
            this.textBoxWidth.Text = this.newWidth.ToString();

            this.remplissage = Pixel.Zero;
            this.buttonColor.BackColor = this.remplissage.ToColor();

            this.trackBarTaille.Maximum = Math.Min(this.imageOnLoad.Height, this.imageOnLoad.Width);
            this.trackBarTaille.Minimum = Math.Min(this.trackBarTaille.Maximum - 1, 10);

            int value = (this.trackBarTaille.Maximum + this.trackBarTaille.Minimum) / 4;
            this.trackBarTaille.Value = value >= this.trackBarTaille.Minimum ? value : this.trackBarTaille.Minimum + 1;

            this.labelTaille.Text = this.trackBarTaille.Value.ToString();
            this.sizeElement = this.trackBarTaille.Value;

            this.labelPointsOKChangeText(0);
        }



        private void ButtonColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.buttonColor.BackColor = this.colorDialog1.Color;

                this.remplissage = Pixel.FromColor(this.colorDialog1.Color);

                this.buttonActualiser.Enabled = true;
            }
        }


        private void ButtonActualiser_Click(object sender, EventArgs e)
        {
            InternalActualisation();
        }

        private void InternalActualisation()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (this.tailleChangée)
            {
                MyImage newSize = new MyImage(this.newHeight, this.newWidth, this.currentBGColor);
                new MyGraphics(newSize).DrawImage(this.imageOnLoad, new Point(0, 0));
                this.imageOnLoad = newSize;

                this.tailleChangée = false;

                this.trackBarTaille.Maximum = Math.Min(this.imageOnLoad.Height, this.imageOnLoad.Width);
                this.trackBarTaille.Minimum = Math.Min(this.trackBarTaille.Maximum - 1, 1);

                this.trackBarTaille.Value = (this.trackBarTaille.Maximum + this.trackBarTaille.Minimum) / 4;
                this.labelTaille.Text = this.trackBarTaille.Value.ToString();
                this.sizeElement = this.trackBarTaille.Value;
            }
            if (this.b_remplissage)
            {
                this.b_remplissage = false;
                this.imageOnLoad = new MyImage(this.imageOnLoad.Height, this.imageOnLoad.Width, this.remplissage);
                this.currentBGColor = this.remplissage;
            }

            if (this.nombrePointNecessaires == 0 && (this.secondListBoxSelec || this.firstListBoxSelec))
            {
                MyGraphics g = new MyGraphics(this.imageOnLoad);

                if (this.firstListBoxSelec)
                {
                    switch (this.selecIndex)
                    {
                        case 0:
                            g.DrawLineBresenham(this.coordonnées[0], this.coordonnées[1], this.remplissage);
                            break;

                        case 1:
                            g.DrawLineWu(this.coordonnées[0], this.coordonnées[1], this.remplissage);
                            break;

                        case 2://Carré
                            if (this.remplissageFormes)
                            {
                                g.FillCarréMid(this.coordonnées[0], this.sizeElement, this.remplissage);
                            }
                            else
                            {
                                g.DrawCarréMid(this.coordonnées[0], this.sizeElement, this.remplissage);
                            }
                            break;

                        case 3://Rectangle
                            if (this.remplissageFormes)
                            {
                                g.FillRectangle(this.coordonnées[0], this.coordonnées[1], this.remplissage);
                            }
                            else
                            {
                                g.DrawRectangle(this.coordonnées[0], this.coordonnées[1], this.remplissage);
                            }
                            break;

                        case 4://hexagone
                            if (this.remplissageFormes)
                            {
                                g.FillHexagone(this.coordonnées[0], this.sizeElement, this.remplissage);
                            }
                            else
                            {
                                g.DrawHexagone(this.coordonnées[0], this.sizeElement, this.remplissage);
                            }
                            break;

                        case 5://pentagone
                            if (this.remplissageFormes)
                            {
                                g.FillPentagone(this.coordonnées[0], this.sizeElement, this.remplissage);
                            }
                            else
                            {
                                g.DrawPentagone(this.coordonnées[0], this.sizeElement, this.remplissage);
                            }
                            break;
                    }
                }
                else
                {
                    switch (this.selecIndex)
                    {
                        case 0://Cercle bres
                            if (this.remplissageFormes)
                            {
                                g.FillCircle(this.coordonnées[0], this.sizeElement, this.remplissage);
                            }
                            else
                            {
                                g.DrawCircleBresenham(this.coordonnées[0], this.sizeElement, this.remplissage);
                            }
                            break;

                        case 1: //3 points
                            if (this.remplissageFormes)
                            {
                                g.FillTriangle(this.coordonnées[0], this.coordonnées[1], this.coordonnées[2], this.remplissage);
                            }
                            else
                            {
                                g.DrawLineBresenham(this.coordonnées[0], this.coordonnées[1], this.remplissage);
                                g.DrawLineBresenham(this.coordonnées[1], this.coordonnées[2], this.remplissage);
                                g.DrawLineBresenham(this.coordonnées[2], this.coordonnées[0], this.remplissage);
                            }
                            break;

                        case 2://4 points
                            if (this.remplissageFormes)
                            {
                                g.FillForme_4(this.coordonnées[0], this.coordonnées[1], this.coordonnées[2],
                                    this.coordonnées[3], this.remplissage);
                            }
                            else
                            {
                                g.DrawLineBresenham(this.coordonnées[0], this.coordonnées[1], this.remplissage);
                                g.DrawLineBresenham(this.coordonnées[1], this.coordonnées[2], this.remplissage);
                                g.DrawLineBresenham(this.coordonnées[2], this.coordonnées[3], this.remplissage);
                                g.DrawLineBresenham(this.coordonnées[3], this.coordonnées[0], this.remplissage);
                            }
                            break;

                        case 3://5 points
                            if (this.remplissageFormes)
                            {
                                g.FillForme_5(this.coordonnées[0], this.coordonnées[1], this.coordonnées[2],
                                    this.coordonnées[3], this.coordonnées[4], this.remplissage);
                            }
                            else
                            {
                                g.DrawLineBresenham(this.coordonnées[0], this.coordonnées[1], this.remplissage);
                                g.DrawLineBresenham(this.coordonnées[1], this.coordonnées[2], this.remplissage);
                                g.DrawLineBresenham(this.coordonnées[2], this.coordonnées[3], this.remplissage);
                                g.DrawLineBresenham(this.coordonnées[3], this.coordonnées[4], this.remplissage);
                                g.DrawLineBresenham(this.coordonnées[4], this.coordonnées[0], this.remplissage);
                            }
                            break;

                        case 4://6 points
                            if (this.remplissageFormes)
                            {
                                g.FillForme_6(this.coordonnées[0], this.coordonnées[1], this.coordonnées[2],
                                    this.coordonnées[3], this.coordonnées[4], this.coordonnées[5], this.remplissage);
                            }
                            else
                            {
                                g.DrawLineBresenham(this.coordonnées[0], this.coordonnées[1], this.remplissage);
                                g.DrawLineBresenham(this.coordonnées[1], this.coordonnées[2], this.remplissage);
                                g.DrawLineBresenham(this.coordonnées[2], this.coordonnées[3], this.remplissage);
                                g.DrawLineBresenham(this.coordonnées[3], this.coordonnées[4], this.remplissage);
                                g.DrawLineBresenham(this.coordonnées[4], this.coordonnées[5], this.remplissage);
                                g.DrawLineBresenham(this.coordonnées[5], this.coordonnées[0], this.remplissage);
                            }
                            break;

                    }
                }
            }

            this.pictureBoxMain.Image = this.imageOnLoad.ToBitmap();

            Cursor.Current = Cursors.Default;
            if (!(this.nombrePointNecessaires == 0 && (this.secondListBoxSelec || this.firstListBoxSelec)))
                this.buttonActualiser.Enabled = false;
        }


        private void TextBoxHeight_TextChanged(object sender, EventArgs e)
        {
            this.newHeight = int.TryParse(this.textBoxHeight.Text, out int val) ? val : 500;

            if (this.newHeight != this.imageOnLoad.Height)
            {
                this.tailleChangée = true;
                this.buttonActualiser.Enabled = true;
            }
        }
        private void TextBoxWidth_TextChanged(object sender, EventArgs e)
        {
            this.newWidth = int.TryParse(this.textBoxWidth.Text, out int val) ? val : 500;

            if (this.newWidth != this.imageOnLoad.Width)
            {
                this.tailleChangée = true;
                this.buttonActualiser.Enabled = true;
            }
        }


        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void TrackBarTaille_Scroll(object sender, EventArgs e)
        {
            this.sizeElement = this.trackBarTaille.Value;
            this.labelTaille.Text = this.sizeElement.ToString();
        }



        private void IcoLab_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Choisissez des modifications à appliquer à une copie d'une image " +
                      "ou à votre propre création puis appuyez sur le bouton 'actualiser'." +
                      "Chaque modification est irréversible.Une fois que l'image vous plait, " +
                      "appuyez sur le bouton 'Sortir et appliquer...' pour créer l'image." +
                      "\nPour dessiner une forme sur l'image, cliquez sur la forme correspondante" +
                      "dans les 2 listes et effectuez un clique gauche sur l'image pour choisir les " +
                      "coordonnées à partir desquelles la forme est copiée. Si les coordonnées sont correctes," +
                      "un texte s'allume en vert pour l'indiquer.", "Aide", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        #region listBox

        private bool userChange = true;

        private bool firstListBoxSelec = false;
        private bool secondListBoxSelec = false;

        private int selecIndex = -1;
        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.userChange)
            {
                if (this.secondListBoxSelec)
                {
                    this.userChange = false;
                    this.listBox2.SetSelected(selecIndex, false);
                    this.userChange = true;
                }

                this.secondListBoxSelec = false;
                this.firstListBoxSelec = true;
                this.selecIndex = this.listBox1.SelectedIndex;

                ChangementSelectionForme();
            }
        }
        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.userChange)
            {
                if (this.firstListBoxSelec)
                {
                    this.userChange = false;
                    this.listBox1.SetSelected(selecIndex, false);
                    this.userChange = true;
                }

                this.secondListBoxSelec = true;
                this.firstListBoxSelec = false;
                this.selecIndex = this.listBox2.SelectedIndex;

                if (this.selecIndex == 5)
                {
                    this.secondListBoxSelec = false;
                    this.userChange = false;
                    this.listBox2.SetSelected(selecIndex, false);
                    this.userChange = true;
                    this.selecIndex = -1;
                }

                ChangementSelectionForme();
            }
        }

        private void ChangementSelectionForme()
        {
            this.coordonnées = new List<Point>();
            if (this.firstListBoxSelec || this.secondListBoxSelec) //Nb points nécéssaires pour une forme donnée
            {
                switch (this.selecIndex)
                {
                    case 0:
                        this.nombrePointNecessaires = this.firstListBoxSelec ? 2 : 1;
                        break;
                    case 1:
                        this.nombrePointNecessaires = this.firstListBoxSelec ? 2 : 3;
                        break;
                    case 2:
                        this.nombrePointNecessaires = this.firstListBoxSelec ? 1 : 4;
                        break;
                    case 3:
                        this.nombrePointNecessaires = this.firstListBoxSelec ? 2 : 5;
                        break;
                    case 4:
                        this.nombrePointNecessaires = this.firstListBoxSelec ? 1 : 6;
                        break;
                    case 5:
                        this.nombrePointNecessaires = this.firstListBoxSelec ? 1 : 5;
                        break;
                }

                this.listBoxPoints.Enabled = true;
            }
            else
            {
                this.nombrePointNecessaires = 0;
                this.listBoxPoints.Enabled = false;
            }

            this.listBoxPoints.Items.Clear();
            this.coordonnées = new List<Point>();
            for (int i = 1; i <= this.nombrePointNecessaires; i++)
            {
                this.listBoxPoints.Items.Add($"Point {i}");
                this.coordonnées.Add(Point.Zero);
            }
            if (this.nombrePointNecessaires >= 1)
            {
                this.listBoxPoints.SetSelected(0, true);
            }
            else
                this.selectedPoint = -1;

            this.labelPointsOKChangeText(this.nombrePointNecessaires);
        }


        private void labelPointsOKChangeText(int reste)
        {
            this.labelPointsOK.Text = $"Reste {reste} point(s) \n\rà placer";

            this.labelPointsOK.ForeColor = reste == 0 ? Color.LimeGreen : Color.DarkRed;

            if (reste == 0 && (this.firstListBoxSelec || this.secondListBoxSelec))
            {
                this.buttonActualiser.Enabled = true;
            }
        }

        #endregion


        private int selectedPoint = -1;
        private void PictureBoxMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.selectedPoint >= 0)
            {
                double ratioW = (double)this.pictureBoxMain.Width / this.imageOnLoad.Width;
                double ratioH = (double)this.pictureBoxMain.Height / this.imageOnLoad.Height;

                double rapportRealSizePBSize = ratioH > ratioW ? ratioW : ratioH;

                int hauteurEnTrop = (int)((this.pictureBoxMain.Height - this.imageOnLoad.Height * rapportRealSizePBSize) / 2);
                int largeurEnTrop = (int)((this.pictureBoxMain.Width - this.imageOnLoad.Width * rapportRealSizePBSize) / 2);

                if (e.X >= largeurEnTrop && e.X <= this.pictureBoxMain.Width - largeurEnTrop &&
                    e.Y >= hauteurEnTrop && e.Y <= this.pictureBoxMain.Height - hauteurEnTrop)
                {
                    if (this.coordonnées[this.selectedPoint] == Point.Zero)
                    {
                        labelPointsOKChangeText(--this.nombrePointNecessaires);
                    }

                    this.coordonnées[this.selectedPoint] = new Point((e.Y - hauteurEnTrop) / rapportRealSizePBSize,
                        (e.X - largeurEnTrop) / rapportRealSizePBSize);

                    this.listBoxPoints.Items[this.selectedPoint] = $"Point {this.selectedPoint + 1} | X = " +
                        $"{(int)this.coordonnées[this.selectedPoint].X} , Y = {(int)this.coordonnées[this.selectedPoint].Y}";

                    int temp = this.selectedPoint;
                    this.listBoxPoints.SetSelected(temp, false);

                    if (temp < this.listBoxPoints.Items.Count - 1)
                    {
                        this.listBoxPoints.SetSelected(temp + 1, true);
                    }
                    else
                        this.listBoxPoints.SetSelected(0, true);

                }

            }
        }


        private void ListBoxPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedPoint = this.listBoxPoints.SelectedIndex;
        }

        private void CheckBoxRemplissage_CheckedChanged(object sender, EventArgs e)
        {
            this.remplissageFormes = this.checkBoxRemplissage.Checked;
        }

        private void CheckBoxRemplIm_CheckedChanged(object sender, EventArgs e)
        {
            this.b_remplissage = this.checkBoxRemplIm.Checked;
            if (this.b_remplissage)
            {
                this.buttonActualiser.Enabled = true;
            }
        }
    }
}
