namespace Photoshop3000
{
    partial class Dessin
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
            this.pictureBoxMain = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.buttonActualiser = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.buttonColor = new System.Windows.Forms.Button();
            this.icoLab = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.trackBarTaille = new System.Windows.Forms.TrackBar();
            this.label14 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.labelTaille = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.labelPointsOK = new System.Windows.Forms.Label();
            this.listBoxPoints = new System.Windows.Forms.ListBox();
            this.checkBoxRemplissage = new System.Windows.Forms.CheckBox();
            this.checkBoxRemplIm = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTaille)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxMain
            // 
            this.pictureBoxMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxMain.Location = new System.Drawing.Point(462, 81);
            this.pictureBoxMain.Name = "pictureBoxMain";
            this.pictureBoxMain.Size = new System.Drawing.Size(769, 582);
            this.pictureBoxMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxMain.TabIndex = 0;
            this.pictureBoxMain.TabStop = false;
            this.pictureBoxMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMain_MouseDown);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(112, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 42);
            this.label1.TabIndex = 1;
            this.label1.Text = "Paramètres";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(437, 688);
            this.label2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(434, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 690);
            this.label3.TabIndex = 3;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OkButton.BackColor = System.Drawing.Color.LimeGreen;
            this.OkButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.OkButton.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.OkButton.Location = new System.Drawing.Point(201, 581);
            this.OkButton.Margin = new System.Windows.Forms.Padding(0);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(230, 95);
            this.OkButton.TabIndex = 33;
            this.OkButton.Text = "Sortir et appliquer tous les changements";
            this.OkButton.UseVisualStyleBackColor = false;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.BackColor = System.Drawing.Color.Red;
            this.CancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CancelButton.Location = new System.Drawing.Point(3, 581);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(192, 95);
            this.CancelButton.TabIndex = 34;
            this.CancelButton.Text = "Sortir et annuler tous les changements";
            this.CancelButton.UseVisualStyleBackColor = false;
            // 
            // buttonActualiser
            // 
            this.buttonActualiser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonActualiser.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonActualiser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonActualiser.Enabled = false;
            this.buttonActualiser.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.buttonActualiser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonActualiser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonActualiser.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonActualiser.Location = new System.Drawing.Point(1096, 10);
            this.buttonActualiser.Name = "buttonActualiser";
            this.buttonActualiser.Size = new System.Drawing.Size(124, 54);
            this.buttonActualiser.TabIndex = 35;
            this.buttonActualiser.Text = "Actualiser";
            this.buttonActualiser.UseVisualStyleBackColor = false;
            this.buttonActualiser.Click += new System.EventHandler(this.ButtonActualiser_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.Red;
            this.label4.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gainsboro;
            this.label4.Location = new System.Drawing.Point(532, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(558, 54);
            this.label4.TabIndex = 36;
            this.label4.Text = "Preview de l\'image";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(93, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(186, 18);
            this.label5.TabIndex = 37;
            this.label5.Text = "Couleur de remplissage";
            this.toolTip1.SetToolTip(this.label5, "Remplit l\'élément sélectionné dans les listes ci dessous\r\nSi aucun élément n\'est " +
        "sélectionné, remplit alors l\'image.");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(-1, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 20);
            this.label6.TabIndex = 38;
            this.label6.Text = "Hauteur :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(207, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 20);
            this.label7.TabIndex = 39;
            this.label7.Text = "Largeur :";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(308, 69);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(67, 22);
            this.textBoxWidth.TabIndex = 40;
            this.textBoxWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxWidth.TextChanged += new System.EventHandler(this.TextBoxWidth_TextChanged);
            this.textBoxWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(107, 69);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(67, 22);
            this.textBoxHeight.TabIndex = 41;
            this.textBoxHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxHeight.TextChanged += new System.EventHandler(this.TextBoxHeight_TextChanged);
            this.textBoxHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // buttonColor
            // 
            this.buttonColor.BackColor = System.Drawing.Color.White;
            this.buttonColor.Location = new System.Drawing.Point(16, 105);
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size(71, 41);
            this.buttonColor.TabIndex = 42;
            this.buttonColor.UseVisualStyleBackColor = false;
            this.buttonColor.Click += new System.EventHandler(this.ButtonColor_Click);
            // 
            // icoLab
            // 
            this.icoLab.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.icoLab.Location = new System.Drawing.Point(474, 16);
            this.icoLab.Name = "icoLab";
            this.icoLab.Size = new System.Drawing.Size(52, 48);
            this.icoLab.TabIndex = 55;
            this.toolTip1.SetToolTip(this.icoLab, "Cliquez ici [INFOS]");
            this.icoLab.Click += new System.EventHandler(this.IcoLab_Click);
            // 
            // trackBarTaille
            // 
            this.trackBarTaille.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.trackBarTaille.BackColor = System.Drawing.Color.DimGray;
            this.trackBarTaille.Location = new System.Drawing.Point(61, 517);
            this.trackBarTaille.Name = "trackBarTaille";
            this.trackBarTaille.Size = new System.Drawing.Size(314, 56);
            this.trackBarTaille.TabIndex = 62;
            this.toolTip1.SetToolTip(this.trackBarTaille, "Taille de l\'élément sélectionné (carré, cercle, hexa/penta.gone)");
            this.trackBarTaille.Scroll += new System.EventHandler(this.TrackBarTaille_Scroll);
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label14.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label14.Location = new System.Drawing.Point(79, 491);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(180, 23);
            this.label14.TabIndex = 63;
            this.label14.Text = "Taille de l\'élément :";
            this.toolTip1.SetToolTip(this.label14, "Taille de l\'élément sélectionné (carré, cercle, hexa/penta.gone)");
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label8.Location = new System.Drawing.Point(444, 64);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(800, 17);
            this.label8.TabIndex = 56;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label9.Location = new System.Drawing.Point(444, 660);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(800, 14);
            this.label9.TabIndex = 57;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label10.Location = new System.Drawing.Point(445, 80);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 580);
            this.label10.TabIndex = 58;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label11.Location = new System.Drawing.Point(1229, 80);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 580);
            this.label11.TabIndex = 59;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label12.Location = new System.Drawing.Point(444, 0);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(800, 67);
            this.label12.TabIndex = 60;
            // 
            // labelTaille
            // 
            this.labelTaille.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTaille.AutoSize = true;
            this.labelTaille.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelTaille.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTaille.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelTaille.Location = new System.Drawing.Point(288, 491);
            this.labelTaille.Name = "labelTaille";
            this.labelTaille.Size = new System.Drawing.Size(61, 18);
            this.labelTaille.TabIndex = 64;
            this.labelTaille.Text = "label15";
            this.labelTaille.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.DimGray;
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Items.AddRange(new object[] {
            "Tirer un trait (Bresenham)",
            "Tirer un trait (antialiasing)",
            "Dessiner un carré",
            "Dessiner un rectangle",
            "Dessiner un hexagone",
            "Dessiner un pentagone"});
            this.listBox1.Location = new System.Drawing.Point(222, 196);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(209, 164);
            this.listBox1.TabIndex = 69;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // listBox2
            // 
            this.listBox2.BackColor = System.Drawing.Color.DimGray;
            this.listBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox2.ForeColor = System.Drawing.SystemColors.Window;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.ItemHeight = 20;
            this.listBox2.Items.AddRange(new object[] {
            "Dessiner un cercle (Bresenham)",
            "Dessiner un triangle",
            "Forme de 4 points",
            "Forme de 5 points",
            "Forme de 6 points",
            "Désélectionner tout"});
            this.listBox2.Location = new System.Drawing.Point(3, 196);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(213, 164);
            this.listBox2.TabIndex = 70;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.ListBox2_SelectedIndexChanged);
            // 
            // labelPointsOK
            // 
            this.labelPointsOK.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelPointsOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPointsOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelPointsOK.Location = new System.Drawing.Point(268, 368);
            this.labelPointsOK.Name = "labelPointsOK";
            this.labelPointsOK.Size = new System.Drawing.Size(163, 55);
            this.labelPointsOK.TabIndex = 71;
            this.labelPointsOK.Text = "Reste 1 point à \r\nplacer sur l\'image";
            // 
            // listBoxPoints
            // 
            this.listBoxPoints.BackColor = System.Drawing.Color.DimGray;
            this.listBoxPoints.Enabled = false;
            this.listBoxPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPoints.ForeColor = System.Drawing.SystemColors.Window;
            this.listBoxPoints.FormattingEnabled = true;
            this.listBoxPoints.ItemHeight = 20;
            this.listBoxPoints.Items.AddRange(new object[] {
            "Appuyez pour placer le Point 1",
            "Appuyez pour placer le Point 2",
            "Appuyez pour placer le Point 3",
            "Appuyez pour placer le Point 4",
            "Appuyez pour placer le Point 5",
            "Appuyez pour placer le Point 6"});
            this.listBoxPoints.Location = new System.Drawing.Point(3, 358);
            this.listBoxPoints.Name = "listBoxPoints";
            this.listBoxPoints.Size = new System.Drawing.Size(251, 144);
            this.listBoxPoints.TabIndex = 72;
            this.listBoxPoints.SelectedIndexChanged += new System.EventHandler(this.ListBoxPoints_SelectedIndexChanged);
            // 
            // checkBoxRemplissage
            // 
            this.checkBoxRemplissage.AutoSize = true;
            this.checkBoxRemplissage.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.checkBoxRemplissage.Checked = true;
            this.checkBoxRemplissage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRemplissage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxRemplissage.Location = new System.Drawing.Point(260, 426);
            this.checkBoxRemplissage.Name = "checkBoxRemplissage";
            this.checkBoxRemplissage.Size = new System.Drawing.Size(149, 38);
            this.checkBoxRemplissage.TabIndex = 73;
            this.checkBoxRemplissage.Text = "Remplir la forme\r\n sélectionnée ";
            this.checkBoxRemplissage.UseVisualStyleBackColor = false;
            this.checkBoxRemplissage.CheckedChanged += new System.EventHandler(this.CheckBoxRemplissage_CheckedChanged);
            // 
            // checkBoxRemplIm
            // 
            this.checkBoxRemplIm.AutoSize = true;
            this.checkBoxRemplIm.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.checkBoxRemplIm.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxRemplIm.Location = new System.Drawing.Point(33, 152);
            this.checkBoxRemplIm.Name = "checkBoxRemplIm";
            this.checkBoxRemplIm.Size = new System.Drawing.Size(386, 21);
            this.checkBoxRemplIm.TabIndex = 74;
            this.checkBoxRemplIm.Text = "Remplir toute l\'image par la couleur sélectionnée";
            this.checkBoxRemplIm.UseVisualStyleBackColor = false;
            this.checkBoxRemplIm.CheckedChanged += new System.EventHandler(this.CheckBoxRemplIm_CheckedChanged);
            // 
            // Dessin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1245, 675);
            this.Controls.Add(this.checkBoxRemplIm);
            this.Controls.Add(this.checkBoxRemplissage);
            this.Controls.Add(this.listBoxPoints);
            this.Controls.Add(this.labelPointsOK);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.labelTaille);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.trackBarTaille);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.icoLab);
            this.Controls.Add(this.buttonColor);
            this.Controls.Add(this.textBoxHeight);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonActualiser);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBoxMain);
            this.Controls.Add(this.label12);
            this.MinimumSize = new System.Drawing.Size(920, 722);
            this.Name = "Dessin";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dessin";
            this.Load += new System.EventHandler(this.Dessin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTaille)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button OkButton;
        private new System.Windows.Forms.Button CancelButton;
        internal System.Windows.Forms.Button buttonActualiser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonColor;
        private System.Windows.Forms.Label icoLab;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TrackBar trackBarTaille;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label labelTaille;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label labelPointsOK;
        private System.Windows.Forms.ListBox listBoxPoints;
        private System.Windows.Forms.CheckBox checkBoxRemplissage;
        private System.Windows.Forms.CheckBox checkBoxRemplIm;
    }
}