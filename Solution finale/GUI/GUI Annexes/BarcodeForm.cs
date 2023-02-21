using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Photoshop3000;
using Photoshop3000.Barcode;

namespace Photoshop3000.GUI.GUI_Annexes
{
    public partial class BarcodeForm : Form
    {
        private bool Qr => this.tabControl1.SelectedIndex != 1;

        public MyImage MyImage { get; private set; }


        public BarcodeForm(MyImage im)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.listBox_Encodage.SelectedIndex = 0;
            this.listBox_Ecc.SelectedIndex = 0;
            this.listBox_types.SelectedIndex = 0;

            this.pb_barcode.SizeMode = PictureBoxSizeMode.Zoom;

            this.pb_barcode.MyImage = this.MyImage = im;
        }


        private void butt_Scan_Click(object sender, EventArgs e)
        {
            if (this.MyImage != null)
            {
                if (this.Qr)
                {
                    try
                    {
                        ScanQRCode scan = new ScanQRCode(this.MyImage);

                        this.textBox_resultsQr.TextAlign = HorizontalAlignment.Left;

                        this.textBox_resultsQr.Text = scan.ToString();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Echec du scan : " + error.Message);
                    }
                }
                else
                {
                    try
                    {
                        ScanBarcode1D scan = new ScanBarcode1D(this.MyImage);
                        scan.Scan();

                        this.textBox_resultsQr.TextAlign = HorizontalAlignment.Left;

                        this.textBox_resultsBarc.Text = scan.Data.ToString();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Echec du scan : " + error.Message);
                    }
                }
            }
        }

        private void butt_Gen_Click(object sender, EventArgs e)
        {
            if (this.Qr)
            {
                string àEncoder = this.textBox_message.Text;

                QRCode.EncoderMode mode = (QRCode.EncoderMode)(this.listBox_Encodage.SelectedIndex - 1);

                QRCode.ErrorCorrectionLevel error = (QRCode.ErrorCorrectionLevel)this.listBox_Ecc.SelectedIndex;

                if (this.checkBox_Boost.Checked)
                    error |= QRCode.ErrorCorrectionLevel.Boost;

                if (this.checkBox_Eci.Checked)
                    mode |= QRCode.EncoderMode.Eci;

                QRCode.EciMode eciMode = this.checkBox_Eci.Checked ? QRCode.EciMode.UTF8 : QRCode.EciMode.Auto;

                int version = (int)this.numericUpDown_Version.Value;
                int mask = (int)this.numericUpDown_Mask.Value;

                Pixel fore = Pixel.FromColor(this.button_ClrFront.BackColor);
                Pixel back = Pixel.FromColor(this.button_ClrBack.BackColor);
                if (this.checkBox_Transp.Checked)
                    back = Pixel.FromArgb(0, back.R, back.G, back.B);

                int bordure = (int)this.numericUpDown_bordure.Value;

                try
                {
                    if (this.checkBox_OnIm.Checked)
                    {
                        Rectangle rect = Formats.GetRectangleRightWay(new Rectangle((int)this.numericUpDown_PosOrigX.Value, (int)this.numericUpDown_PosOrigY.Value,
                            (int)this.numericUpDown_PosWidth.Value, (int)this.numericUpDown_PosHeight.Value));

                        QRCodeHelper.GenerateQRCode(àEncoder, mode, eciMode, error, version, mask,
                           this.MyImage, rect, fore, back, bordure, true);
                    }
                    else
                    {
                        Rectangle rect = new Rectangle(0, 0, Convert.ToInt32(tb_Width.Text), Convert.ToInt32(tb_Height.Text));

                        this.MyImage = QRCodeHelper.GenerateQRCode(àEncoder, mode, eciMode, error, version, mask, rect,
                            fore, back, bordure, false);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Echec de la génération du qr code : " + ex.Message);
                }

            }
            else
            {
                string sys = this.maskedTextBox_Sys.Text;
                string prod = this.maskedTextBox_Prod.Text;
                string man = this.maskedTextBox_Man.Text;

                Barcode1D.BarcodeTypes type = null;
                switch (this.listBox_types.SelectedIndex)
                {
                    case 0:
                        type = Barcode1D.BarcodeTypes.EAN13;
                        break;
                    case 1:
                        type = Barcode1D.BarcodeTypes.UPCA;
                        break;
                    case 2:
                        type = Barcode1D.BarcodeTypes.EAN8;
                        break;
                    case 3:
                        type = Barcode1D.BarcodeTypes.UPCE;
                        break;
                }

                try
                {
                    Barcode1D.BarcodeData data = new Barcode1D.BarcodeData(sys, man, prod, type);

                    DrawBarcode1D draw = new DrawBarcode1D(data)
                    {
                        Size = new Size(Convert.ToInt32(this.tb_Width.Text), Convert.ToInt32(this.tb_Height.Text)),
                        BarWidth = (int)this.numericUpDown_BarWidth.Value,
                        ForegroundClr = Pixel.FromColor(this.button_ClrFront.BackColor),
                        BackgroundClr = Pixel.FromColor(this.button_ClrBack.BackColor),
                        DrawFlecheEAN = this.checkBox_drawFleche.Checked,
                        DrawText = this.checkBox_drawNumb.Checked,
                    };

                    draw.Draw();

                    this.MyImage = draw.Image;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Echec de la génération du code-barres : " + ex.Message);
                }
            }

            this.pb_barcode.MyImage = this.MyImage;
        }

        private void button_ClrFront_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.button_ClrFront.BackColor = this.colorDialog1.Color;
            }
        }

        private void button_ClrBack_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.button_ClrBack.BackColor = this.colorDialog1.Color;
            }
        }

        private void button_charge_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.InitialDirectory = Program.ADRESSE_SAUVEGARDE;
            this.openFileDialog1.Filter = "Fichiers BMP|*.bmp|Fichiers PNG|*.png|Fichiers JPEG|*.jpeg|Fichiers CSV" +
                "|*.csv|Fichiers BMP et CSV|*.bmp;*.csv|Tous les fichiers|*.bmp;*.csv;*.jpeg;*.png";
            this.openFileDialog1.FilterIndex = 5;
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.FileName = "";


            DialogResult fichier = this.openFileDialog1.ShowDialog();
            string FileName = this.openFileDialog1.FileName;


            if (File.Exists(FileName) && fichier == DialogResult.OK)
            {
                MyImage im = new MyImage(FileName);

                if (this.pb_barcode.MyImage == null || MessageBox.Show("Charger cette image et supprimer" +
                    " l'image en cours ?", "Attention !", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (im.Validité || (im.Width >= 2 && im.Height >= 2))
                    {
                        this.pb_barcode.MyImage = this.MyImage = im;

                        this.numericUpDown_PosWidth.Maximum = this.MyImage.Width;
                        this.numericUpDown_PosWidth.Value = this.MyImage.Width;

                        this.numericUpDown_PosHeight.Maximum = this.MyImage.Height;
                        this.numericUpDown_PosHeight.Value = this.MyImage.Height;

                        ChangeStateEnableNumericUpDown(this.checkBox_OnIm.Checked);
                    }
                    else
                    {
                        MessageBox.Show("L'image n'est pas valide ! Elle est peut-être trop petite (doit etre supérieur à 2 pixels en largeur et hauteur).",
                                   "Erreur : Impossible de charger l'image", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void pb_barcode_Click(object sender, EventArgs e)
        {
            if (checkBox_OnIm.Checked && this.pb_barcode.MyImage != null)
            {
                using (RognageImage rognage = new RognageImage(this.pb_barcode.MyImage, 1, 1))
                {
                    if (rognage.ShowDialog() == DialogResult.Yes)
                    {
                        Rectangle rect = Formats.GetRectangleRightWay(rognage.AireRognage);

                        this.numericUpDown_PosOrigX.Value = rect.X;
                        this.numericUpDown_PosOrigY.Value = rect.Y;
                        this.numericUpDown_PosWidth.Value = rect.Width;
                        this.numericUpDown_PosHeight.Value = rect.Height;
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez verifier qu'une image est bien chargée et que la case \n" + 
                    $@"""{this.checkBox_OnIm.Text}"" {Environment.NewLine}est bien cochée pour pouvoir dessiner dessus.");
            }
        }

        private void TabChangedPage(object o, EventArgs e)
        {
            this.butt_Gen.Enabled = this.tabControl1.SelectedIndex != 2;

            this.butt_Gen.Text = "Générer " + (this.tabControl1.SelectedIndex == 0 ? "Qr-Code" : "code-barres");

            this.butt_Scan.Text = "Scanner l'image chargée" + (this.tabControl1.SelectedIndex == 0 ? " (qr-code)" : " (code-barres)");
        }

        private void numericUpDown_PosWidth_ValueChanged(object sender, EventArgs e)
        {
            //if (this.checkBox_OnIm.Checked)
            //{
            //    this.tb_Width.Text = Math.Abs((int)(this.numericUpDown_PosWidth.Value - this.numericUpDown_PosOrigX.Value)).ToString();
            //    this.tb_Height.Text = Math.Abs((int)(this.numericUpDown_PosHeight.Value - this.numericUpDown_PosOrigY.Value)).ToString();
            //}
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            this.MyImage = this.pb_barcode.MyImage = null;

            ChangeStateEnableNumericUpDown(false);
        }

        private void checkBox_OnIm_CheckedChanged(object sender, EventArgs e)
        {
            ChangeStateEnableNumericUpDown(this.MyImage != null && this.checkBox_OnIm.Checked);
        }

        private void ChangeStateEnableNumericUpDown(bool val)
        {
            this.numericUpDown_PosHeight.Enabled = this.numericUpDown_PosWidth.Enabled =
                this.numericUpDown_PosOrigX.Enabled = this.numericUpDown_PosOrigY.Enabled = val;

            this.tb_Height.Enabled = this.tb_Width.Enabled = !val;
        }
    }

}
