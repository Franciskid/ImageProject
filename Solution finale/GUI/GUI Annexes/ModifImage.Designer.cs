using System;
using System.Windows.Forms;

namespace Photoshop3000
{
    partial class ModifImage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModifImage));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.texteRotation = new System.Windows.Forms.Label();
            this.Angle = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.multiplicateurText = new System.Windows.Forms.TextBox();
            this.multiplicateur = new System.Windows.Forms.Label();
            this.hauteur = new System.Windows.Forms.TextBox();
            this.largeur = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.OriginalSize = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.KeepAspectRatio = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Interpolation = new System.Windows.Forms.ListBox();
            this.label19 = new System.Windows.Forms.Label();
            this.couleurLab = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.imageLoadBut = new System.Windows.Forms.Button();
            this.BordsCB = new System.Windows.Forms.CheckBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label20 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ptGauche = new System.Windows.Forms.Label();
            this.PtDroite = new System.Windows.Forms.Label();
            this.gaucheYLab = new System.Windows.Forms.Label();
            this.gaucheXLab = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.leftX = new System.Windows.Forms.TextBox();
            this.rightY = new System.Windows.Forms.TextBox();
            this.rightX = new System.Windows.Forms.TextBox();
            this.leftY = new System.Windows.Forms.TextBox();
            this.degré0 = new System.Windows.Forms.Button();
            this.degré45 = new System.Windows.Forms.Button();
            this.degré90 = new System.Windows.Forms.Button();
            this.degré180 = new System.Windows.Forms.Button();
            this.icoLab = new System.Windows.Forms.Label();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.multi4 = new System.Windows.Forms.Button();
            this.multi2 = new System.Windows.Forms.Button();
            this.multi1 = new System.Windows.Forms.Button();
            this.multi05 = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.labelTailleOrigine = new System.Windows.Forms.Label();
            this.labeltailleRognage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(207)))), ((int)(((byte)(228)))));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(257, -33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 391);
            this.label2.TabIndex = 1;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(207)))), ((int)(((byte)(228)))));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(-8, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(531, 10);
            this.label3.TabIndex = 2;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(279, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 20);
            this.label4.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Red;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label9.Location = new System.Drawing.Point(283, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(214, 2);
            this.label9.TabIndex = 8;
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Red;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(17, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(225, 2);
            this.label7.TabIndex = 9;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Red;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label8.Location = new System.Drawing.Point(17, 195);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(225, 2);
            this.label8.TabIndex = 10;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Red;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label10.Location = new System.Drawing.Point(281, 195);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(216, 2);
            this.label10.TabIndex = 11;
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 20;
            this.trackBar1.Location = new System.Drawing.Point(1, 291);
            this.trackBar1.Maximum = 180;
            this.trackBar1.Minimum = -180;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(253, 56);
            this.trackBar1.SmallChange = 20;
            this.trackBar1.TabIndex = 12;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // texteRotation
            // 
            this.texteRotation.AutoSize = true;
            this.texteRotation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.texteRotation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texteRotation.Location = new System.Drawing.Point(81, 327);
            this.texteRotation.Name = "texteRotation";
            this.texteRotation.Size = new System.Drawing.Size(92, 20);
            this.texteRotation.TabIndex = 13;
            this.texteRotation.Text = "Rotation : 0°";
            // 
            // Angle
            // 
            this.Angle.Location = new System.Drawing.Point(98, 262);
            this.Angle.Name = "Angle";
            this.Angle.Size = new System.Drawing.Size(62, 22);
            this.Angle.TabIndex = 14;
            this.Angle.Text = "0";
            this.Angle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Angle.TextChanged += new System.EventHandler(this.Angle_TextChanged);
            this.Angle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Angle_KeyPress);
            // 
            // label11
            // 
            this.label11.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label11.Location = new System.Drawing.Point(12, 204);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(239, 56);
            this.label11.TabIndex = 15;
            this.label11.Text = "Déplacez le curseur pour indiquer\r\nl\'angle de rotation ou écrivez l\'angle\r\ndans l" +
    "\'emplacement de texte.";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(207)))), ((int)(((byte)(228)))));
            this.label12.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label12.Location = new System.Drawing.Point(-8, 356);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(529, 19);
            this.label12.TabIndex = 16;
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(207)))), ((int)(((byte)(228)))));
            this.label13.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label13.Location = new System.Drawing.Point(-8, -1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(531, 10);
            this.label13.TabIndex = 17;
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(207)))), ((int)(((byte)(228)))));
            this.label14.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label14.Location = new System.Drawing.Point(507, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 366);
            this.label14.TabIndex = 18;
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(207)))), ((int)(((byte)(228)))));
            this.label15.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label15.Location = new System.Drawing.Point(-4, -16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(10, 391);
            this.label15.TabIndex = 19;
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // multiplicateurText
            // 
            this.multiplicateurText.Location = new System.Drawing.Point(98, 56);
            this.multiplicateurText.Name = "multiplicateurText";
            this.multiplicateurText.Size = new System.Drawing.Size(62, 22);
            this.multiplicateurText.TabIndex = 20;
            this.multiplicateurText.Text = "1.0";
            this.multiplicateurText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.multiplicateurText.TextChanged += new System.EventHandler(this.multiplicateurText_TextChanged);
            this.multiplicateurText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.multiplicateurText_KeyPress);
            // 
            // multiplicateur
            // 
            this.multiplicateur.AutoSize = true;
            this.multiplicateur.Location = new System.Drawing.Point(65, 37);
            this.multiplicateur.Name = "multiplicateur";
            this.multiplicateur.Size = new System.Drawing.Size(144, 17);
            this.multiplicateur.TabIndex = 21;
            this.multiplicateur.Text = "Multiplicateur de taille\r\n";
            // 
            // hauteur
            // 
            this.hauteur.Location = new System.Drawing.Point(147, 100);
            this.hauteur.Name = "hauteur";
            this.hauteur.Size = new System.Drawing.Size(62, 22);
            this.hauteur.TabIndex = 22;
            this.hauteur.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.hauteur.TextChanged += new System.EventHandler(this.hauteur_TextChanged);
            this.hauteur.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hauteur_KeyPress);
            // 
            // largeur
            // 
            this.largeur.Location = new System.Drawing.Point(47, 100);
            this.largeur.Name = "largeur";
            this.largeur.Size = new System.Drawing.Size(62, 22);
            this.largeur.TabIndex = 23;
            this.largeur.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.largeur.TextChanged += new System.EventHandler(this.largeur_TextChanged);
            this.largeur.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.largeur_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(150, 81);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 17);
            this.label16.TabIndex = 24;
            this.label16.Text = "Hauteur";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(51, 81);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(58, 17);
            this.label17.TabIndex = 25;
            this.label17.Text = "Largeur";
            // 
            // OriginalSize
            // 
            this.OriginalSize.AutoSize = true;
            this.OriginalSize.Location = new System.Drawing.Point(31, 125);
            this.OriginalSize.Name = "OriginalSize";
            this.OriginalSize.Size = new System.Drawing.Size(108, 17);
            this.OriginalSize.TabIndex = 26;
            this.OriginalSize.Text = "Taille d\'origine :";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(330, 174);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(106, 20);
            this.label18.TabIndex = 27;
            this.label18.Text = "Paramètres";
            // 
            // KeepAspectRatio
            // 
            this.KeepAspectRatio.AutoSize = true;
            this.KeepAspectRatio.Checked = true;
            this.KeepAspectRatio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.KeepAspectRatio.Location = new System.Drawing.Point(298, 197);
            this.KeepAspectRatio.Name = "KeepAspectRatio";
            this.KeepAspectRatio.Size = new System.Drawing.Size(147, 21);
            this.KeepAspectRatio.TabIndex = 28;
            this.KeepAspectRatio.Text = "Keep Aspect Ratio";
            this.toolTip1.SetToolTip(this.KeepAspectRatio, resources.GetString("KeepAspectRatio.ToolTip"));
            this.KeepAspectRatio.UseVisualStyleBackColor = true;
            this.KeepAspectRatio.CheckedChanged += new System.EventHandler(this.KeepAspectRatio_CheckedChanged);
            // 
            // Interpolation
            // 
            this.Interpolation.FormattingEnabled = true;
            this.Interpolation.ItemHeight = 16;
            this.Interpolation.Items.AddRange(new object[] {
            "Voisin le plus proche",
            "Bilinéaire",
            "Bicubique",
            "Windows | Bilinéaire",
            "Windows | Bicubique",
            "WIndows | Voisin le plus proche",
            "Windows | Bilinéaire Haute Qualité",
            "Windows | Bicubique Haute Qualité",
            "Bilinéaire bords (pixel remplissage)",
            "Bilinéaire bords (clr pixel + proche)",
            "Bicubique bords (pixel remplissage)"});
            this.Interpolation.Location = new System.Drawing.Point(276, 241);
            this.Interpolation.Name = "Interpolation";
            this.Interpolation.Size = new System.Drawing.Size(221, 68);
            this.Interpolation.TabIndex = 29;
            this.toolTip1.SetToolTip(this.Interpolation, " Contrôle le type de méthode utilisée pour \r\n       -l\'aggrandissement d\'une imag" +
        "e\r\n       -le rétrécissement d\'une image\r\n       -la rotation d\'une image\r\n");
            this.Interpolation.SelectedIndexChanged += new System.EventHandler(this.Interpolation_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(331, 318);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(88, 34);
            this.label19.TabIndex = 31;
            this.label19.Text = "Couleur de\r\n remplissage";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label19, "Couleur qui occasionnellement remplira les trous laissés par une image tournée ou" +
        " aggrandie");
            // 
            // couleurLab
            // 
            this.couleurLab.BackColor = System.Drawing.Color.Black;
            this.couleurLab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.couleurLab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.couleurLab.Location = new System.Drawing.Point(298, 321);
            this.couleurLab.Name = "couleurLab";
            this.couleurLab.Size = new System.Drawing.Size(31, 26);
            this.couleurLab.TabIndex = 30;
            this.toolTip1.SetToolTip(this.couleurLab, "Cliquez ici pour choisir la couleur de remplissage");
            this.couleurLab.Click += new System.EventHandler(this.couleurLab_Click);
            // 
            // OkButton
            // 
            this.OkButton.BackColor = System.Drawing.Color.LimeGreen;
            this.OkButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.OkButton.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.OkButton.Location = new System.Drawing.Point(260, 369);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(263, 77);
            this.OkButton.TabIndex = 32;
            this.OkButton.Text = "Appliquer à l\'image";
            this.toolTip1.SetToolTip(this.OkButton, "Appliquer à l\'image les modifications indiquées");
            this.OkButton.UseVisualStyleBackColor = false;
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.Color.Red;
            this.CancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CancelButton.Location = new System.Drawing.Point(-9, 369);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(272, 77);
            this.CancelButton.TabIndex = 33;
            this.CancelButton.Text = "Annuler";
            this.toolTip1.SetToolTip(this.CancelButton, "Annuler, aucune modification ne sera effectuée sur l\'image");
            this.CancelButton.UseVisualStyleBackColor = false;
            // 
            // imageLoadBut
            // 
            this.imageLoadBut.BackColor = System.Drawing.Color.DimGray;
            this.imageLoadBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imageLoadBut.Enabled = false;
            this.imageLoadBut.Font = new System.Drawing.Font("MS Reference Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageLoadBut.ForeColor = System.Drawing.Color.White;
            this.imageLoadBut.Location = new System.Drawing.Point(415, 37);
            this.imageLoadBut.Name = "imageLoadBut";
            this.imageLoadBut.Size = new System.Drawing.Size(88, 120);
            this.imageLoadBut.TabIndex = 49;
            this.imageLoadBut.Text = "CLIQUE ICI";
            this.imageLoadBut.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip1.SetToolTip(this.imageLoadBut, "Appuyez sur l\'image pour faire apparaitre l\'image\r\nà rogner. Il vous suffit ensui" +
        "te de cliquer dessus et de \r\nmaintenir pour choisir l\'encadrement qui sera rogné" +
        "\r\n");
            this.imageLoadBut.UseVisualStyleBackColor = false;
            this.imageLoadBut.Click += new System.EventHandler(this.imageLoadBut_Click);
            // 
            // BordsCB
            // 
            this.BordsCB.AutoSize = true;
            this.BordsCB.Location = new System.Drawing.Point(8, 326);
            this.BordsCB.Name = "BordsCB";
            this.BordsCB.Size = new System.Drawing.Size(67, 21);
            this.BordsCB.TabIndex = 59;
            this.BordsCB.Text = "Bords";
            this.toolTip1.SetToolTip(this.BordsCB, resources.GetString("BordsCB.ToolTip"));
            this.BordsCB.UseVisualStyleBackColor = true;
            this.BordsCB.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(343, 9);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(82, 20);
            this.label20.TabIndex = 35;
            this.label20.Text = "Rognage";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(85, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 36;
            this.label5.Text = "Rotation";
            // 
            // ptGauche
            // 
            this.ptGauche.AutoSize = true;
            this.ptGauche.Location = new System.Drawing.Point(273, 43);
            this.ptGauche.Name = "ptGauche";
            this.ptGauche.Size = new System.Drawing.Size(123, 17);
            this.ptGauche.TabIndex = 38;
            this.ptGauche.Text = "Point haut gauche";
            // 
            // PtDroite
            // 
            this.PtDroite.AutoSize = true;
            this.PtDroite.Location = new System.Drawing.Point(273, 105);
            this.PtDroite.Name = "PtDroite";
            this.PtDroite.Size = new System.Drawing.Size(99, 17);
            this.PtDroite.TabIndex = 39;
            this.PtDroite.Text = "Point bas droit\r\n";
            // 
            // gaucheYLab
            // 
            this.gaucheYLab.AutoSize = true;
            this.gaucheYLab.Location = new System.Drawing.Point(347, 69);
            this.gaucheYLab.Name = "gaucheYLab";
            this.gaucheYLab.Size = new System.Drawing.Size(25, 17);
            this.gaucheYLab.TabIndex = 40;
            this.gaucheYLab.Text = "Y :";
            // 
            // gaucheXLab
            // 
            this.gaucheXLab.AutoSize = true;
            this.gaucheXLab.Location = new System.Drawing.Point(273, 69);
            this.gaucheXLab.Name = "gaucheXLab";
            this.gaucheXLab.Size = new System.Drawing.Size(25, 17);
            this.gaucheXLab.TabIndex = 41;
            this.gaucheXLab.Text = "X :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(273, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 17);
            this.label6.TabIndex = 42;
            this.label6.Text = "X :";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(347, 131);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(25, 17);
            this.label21.TabIndex = 43;
            this.label21.Text = "Y :";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Gray;
            this.label22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(205)))), ((int)(((byte)(250)))));
            this.label22.Location = new System.Drawing.Point(270, 97);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(233, 4);
            this.label22.TabIndex = 44;
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // leftX
            // 
            this.leftX.Location = new System.Drawing.Point(301, 66);
            this.leftX.Name = "leftX";
            this.leftX.Size = new System.Drawing.Size(42, 22);
            this.leftX.TabIndex = 45;
            this.leftX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.leftX.TextChanged += new System.EventHandler(this.leftX_TextChanged);
            this.leftX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.leftX_KeyPress);
            // 
            // rightY
            // 
            this.rightY.Location = new System.Drawing.Point(370, 128);
            this.rightY.Name = "rightY";
            this.rightY.Size = new System.Drawing.Size(42, 22);
            this.rightY.TabIndex = 46;
            this.rightY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rightY.TextChanged += new System.EventHandler(this.rightY_TextChanged);
            this.rightY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rightY_KeyPress);
            // 
            // rightX
            // 
            this.rightX.Location = new System.Drawing.Point(301, 128);
            this.rightX.Name = "rightX";
            this.rightX.Size = new System.Drawing.Size(42, 22);
            this.rightX.TabIndex = 47;
            this.rightX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rightX.TextChanged += new System.EventHandler(this.rightX_TextChanged);
            this.rightX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rightX_KeyPress);
            // 
            // leftY
            // 
            this.leftY.Location = new System.Drawing.Point(370, 66);
            this.leftY.Name = "leftY";
            this.leftY.Size = new System.Drawing.Size(42, 22);
            this.leftY.TabIndex = 48;
            this.leftY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.leftY.TextChanged += new System.EventHandler(this.leftY_TextChanged);
            this.leftY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.leftY_KeyPress);
            // 
            // degré0
            // 
            this.degré0.Location = new System.Drawing.Point(7, 262);
            this.degré0.Name = "degré0";
            this.degré0.Size = new System.Drawing.Size(44, 27);
            this.degré0.TabIndex = 50;
            this.degré0.Text = "0°";
            this.degré0.UseVisualStyleBackColor = true;
            this.degré0.Click += new System.EventHandler(this.degré0_Click);
            // 
            // degré45
            // 
            this.degré45.Location = new System.Drawing.Point(50, 262);
            this.degré45.Name = "degré45";
            this.degré45.Size = new System.Drawing.Size(42, 27);
            this.degré45.TabIndex = 51;
            this.degré45.Text = "45°";
            this.degré45.UseVisualStyleBackColor = true;
            this.degré45.Click += new System.EventHandler(this.degré45_Click);
            // 
            // degré90
            // 
            this.degré90.Location = new System.Drawing.Point(164, 262);
            this.degré90.Name = "degré90";
            this.degré90.Size = new System.Drawing.Size(42, 27);
            this.degré90.TabIndex = 52;
            this.degré90.Text = "90°";
            this.degré90.UseVisualStyleBackColor = true;
            this.degré90.Click += new System.EventHandler(this.degré90_Click);
            // 
            // degré180
            // 
            this.degré180.Location = new System.Drawing.Point(207, 262);
            this.degré180.Name = "degré180";
            this.degré180.Size = new System.Drawing.Size(49, 27);
            this.degré180.TabIndex = 53;
            this.degré180.Text = "180°";
            this.degré180.UseVisualStyleBackColor = true;
            this.degré180.Click += new System.EventHandler(this.degré180_Click);
            // 
            // icoLab
            // 
            this.icoLab.Location = new System.Drawing.Point(449, 197);
            this.icoLab.Name = "icoLab";
            this.icoLab.Size = new System.Drawing.Size(52, 41);
            this.icoLab.TabIndex = 54;
            this.toolTip2.SetToolTip(this.icoLab, resources.GetString("icoLab.ToolTip"));
            // 
            // toolTip2
            // 
            this.toolTip2.AutomaticDelay = 50;
            this.toolTip2.AutoPopDelay = 30000;
            this.toolTip2.InitialDelay = 50;
            this.toolTip2.ReshowDelay = 10;
            this.toolTip2.ShowAlways = true;
            // 
            // multi4
            // 
            this.multi4.Location = new System.Drawing.Point(210, 54);
            this.multi4.Name = "multi4";
            this.multi4.Size = new System.Drawing.Size(44, 27);
            this.multi4.TabIndex = 55;
            this.multi4.Text = "x5";
            this.multi4.UseVisualStyleBackColor = true;
            this.multi4.Click += new System.EventHandler(this.multi4_Click);
            // 
            // multi2
            // 
            this.multi2.Location = new System.Drawing.Point(165, 54);
            this.multi2.Name = "multi2";
            this.multi2.Size = new System.Drawing.Size(44, 27);
            this.multi2.TabIndex = 56;
            this.multi2.Text = "x2";
            this.multi2.UseVisualStyleBackColor = true;
            this.multi2.Click += new System.EventHandler(this.multi2_Click);
            // 
            // multi1
            // 
            this.multi1.Location = new System.Drawing.Point(54, 56);
            this.multi1.Name = "multi1";
            this.multi1.Size = new System.Drawing.Size(43, 27);
            this.multi1.TabIndex = 57;
            this.multi1.Text = "x1";
            this.multi1.UseVisualStyleBackColor = true;
            this.multi1.Click += new System.EventHandler(this.multi1_Click);
            // 
            // multi05
            // 
            this.multi05.Location = new System.Drawing.Point(8, 56);
            this.multi05.Name = "multi05";
            this.multi05.Size = new System.Drawing.Size(48, 27);
            this.multi05.TabIndex = 58;
            this.multi05.Text = "x0.5";
            this.multi05.UseVisualStyleBackColor = true;
            this.multi05.Click += new System.EventHandler(this.multi05_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label23.Location = new System.Drawing.Point(307, 221);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(136, 17);
            this.label23.TabIndex = 60;
            this.label23.Text = "Mode d\'interpolation";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(30, 9);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(178, 20);
            this.label24.TabIndex = 61;
            this.label24.Text = "Redimensionnement";
            // 
            // labelTailleOrigine
            // 
            this.labelTailleOrigine.AutoSize = true;
            this.labelTailleOrigine.Location = new System.Drawing.Point(95, 128);
            this.labelTailleOrigine.Name = "labelTailleOrigine";
            this.labelTailleOrigine.Size = new System.Drawing.Size(0, 17);
            this.labelTailleOrigine.TabIndex = 62;
            // 
            // labeltailleRognage
            // 
            this.labeltailleRognage.AutoSize = true;
            this.labeltailleRognage.Location = new System.Drawing.Point(12, 145);
            this.labeltailleRognage.Name = "labeltailleRognage";
            this.labeltailleRognage.Size = new System.Drawing.Size(143, 17);
            this.labeltailleRognage.TabIndex = 63;
            this.labeltailleRognage.Text = "Taille après rognage:";
            // 
            // ModifImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 442);
            this.Controls.Add(this.labeltailleRognage);
            this.Controls.Add(this.labelTailleOrigine);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.BordsCB);
            this.Controls.Add(this.multi05);
            this.Controls.Add(this.multi1);
            this.Controls.Add(this.multi2);
            this.Controls.Add(this.multi4);
            this.Controls.Add(this.icoLab);
            this.Controls.Add(this.degré180);
            this.Controls.Add(this.degré90);
            this.Controls.Add(this.degré45);
            this.Controls.Add(this.degré0);
            this.Controls.Add(this.imageLoadBut);
            this.Controls.Add(this.leftY);
            this.Controls.Add(this.rightX);
            this.Controls.Add(this.rightY);
            this.Controls.Add(this.leftX);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.gaucheXLab);
            this.Controls.Add(this.gaucheYLab);
            this.Controls.Add(this.PtDroite);
            this.Controls.Add(this.ptGauche);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.couleurLab);
            this.Controls.Add(this.Interpolation);
            this.Controls.Add(this.KeepAspectRatio);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.OriginalSize);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.largeur);
            this.Controls.Add(this.hauteur);
            this.Controls.Add(this.multiplicateur);
            this.Controls.Add(this.multiplicateurText);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.Angle);
            this.Controls.Add(this.texteRotation);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModifImage";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modification de la structure d\'image";
            this.Load += new System.EventHandler(this.ModifImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label texteRotation;
        private System.Windows.Forms.TextBox Angle;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private TextBox multiplicateurText;
        private Label multiplicateur;
        private TextBox hauteur;
        private TextBox largeur;
        private Label label16;
        private Label label17;
        private Label OriginalSize;
        private Label label18;
        private CheckBox KeepAspectRatio;
        private ToolTip toolTip1;
        private ListBox Interpolation;
        private ColorDialog colorDialog1;
        private Label couleurLab;
        private Label label19;
        private new Button CancelButton;
        private Label label20;
        private Label label5;
        internal Button OkButton;
        private Label ptGauche;
        private Label PtDroite;
        private Label gaucheYLab;
        private Label gaucheXLab;
        private Label label6;
        private Label label21;
        private Label label22;
        private TextBox leftX;
        private TextBox rightY;
        private TextBox rightX;
        private TextBox leftY;
        private Button imageLoadBut;
        private Button degré0;
        private Button degré45;
        private Button degré90;
        private Button degré180;
        private Label icoLab;
        private ToolTip toolTip2;
        private Button multi4;
        private Button multi2;
        private Button multi1;
        private Button multi05;
        private CheckBox BordsCB;
        private Label label23;
        private Label label24;
        private Label labelTailleOrigine;
        private Label labeltailleRognage;
    }
}