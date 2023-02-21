using System;
using System.Windows.Forms;

namespace Photoshop3000
{
    partial class FractaleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FractaleForm));
            this.SaveButton = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.LB_equations = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_Re = new System.Windows.Forms.TextBox();
            this.TB_Im = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LB_récursiff = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.TB_height = new System.Windows.Forms.TextBox();
            this.TB_width = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.TB_iteration = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.But_imageMosaique = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.TB_tryColor = new System.Windows.Forms.TextBox();
            this.TB_contraste = new System.Windows.Forms.TextBox();
            this.TB_mosaique = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label19 = new System.Windows.Forms.Label();
            this.But_Color = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label_Pourc = new System.Windows.Forms.Label();
            this.TB_sizeJulia = new System.Windows.Forms.TextBox();
            this.comboBox_Func = new System.Windows.Forms.ComboBox();
            this.button_JuliaForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(217)))), ((int)(((byte)(32)))));
            this.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SaveButton.Location = new System.Drawing.Point(458, 308);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(2);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(228, 60);
            this.SaveButton.TabIndex = 11;
            this.SaveButton.Text = "Créer";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Clear
            // 
            this.Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Clear.BackColor = System.Drawing.Color.Red;
            this.Clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Clear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Clear.Location = new System.Drawing.Point(224, 308);
            this.Clear.Margin = new System.Windows.Forms.Padding(2);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(225, 60);
            this.Clear.TabIndex = 12;
            this.Clear.Text = "Annuler";
            this.Clear.UseVisualStyleBackColor = false;
            // 
            // LB_equations
            // 
            this.LB_equations.BackColor = System.Drawing.Color.DimGray;
            this.LB_equations.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB_equations.ForeColor = System.Drawing.SystemColors.Window;
            this.LB_equations.FormattingEnabled = true;
            this.LB_equations.HorizontalScrollbar = true;
            this.LB_equations.ItemHeight = 17;
            this.LB_equations.Items.AddRange(new object[] {
            "Mandelbrot",
            "Lapin",
            "Dentrite",
            "Dentrite division",
            "Ilots",
            "Ilots serpents",
            "Escargot",
            "Escargot révolution",
            "Forme 1",
            "Forme 2",
            "Forme 3",
            "Forme 4",
            "Forme 5",
            "Forme 6"});
            this.LB_equations.Location = new System.Drawing.Point(14, 37);
            this.LB_equations.Margin = new System.Windows.Forms.Padding(2);
            this.LB_equations.Name = "LB_equations";
            this.LB_equations.Size = new System.Drawing.Size(200, 225);
            this.LB_equations.TabIndex = 13;
            this.LB_equations.SelectedIndexChanged += new System.EventHandler(this.LB_equations_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(9, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 30);
            this.label1.TabIndex = 14;
            this.label1.Text = "Choisissez une forme fractale\r\ngénérée à partir d\'une équation";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(20, 274);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Créez votre propre équation :";
            // 
            // TB_Re
            // 
            this.TB_Re.Location = new System.Drawing.Point(23, 308);
            this.TB_Re.Margin = new System.Windows.Forms.Padding(2);
            this.TB_Re.Name = "TB_Re";
            this.TB_Re.Size = new System.Drawing.Size(68, 20);
            this.TB_Re.TabIndex = 16;
            this.TB_Re.Text = "0";
            this.TB_Re.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_Re.TextChanged += new System.EventHandler(this.TB_Re_TextChanged);
            this.TB_Re.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_Re_KeyPress);
            // 
            // TB_Im
            // 
            this.TB_Im.Location = new System.Drawing.Point(109, 308);
            this.TB_Im.Margin = new System.Windows.Forms.Padding(2);
            this.TB_Im.Name = "TB_Im";
            this.TB_Im.Size = new System.Drawing.Size(72, 20);
            this.TB_Im.TabIndex = 17;
            this.TB_Im.Text = "0 i";
            this.TB_Im.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_Im.TextChanged += new System.EventHandler(this.TB_Im_TextChanged);
            this.TB_Im.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_Im_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(39, 291);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "Re :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(132, 291);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 15);
            this.label4.TabIndex = 19;
            this.label4.Text = "Im :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(221, 2);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(235, 30);
            this.label5.TabIndex = 20;
            this.label5.Text = "Ou choisissez des formes générées\r\nde manière récursive";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LB_récursiff
            // 
            this.LB_récursiff.BackColor = System.Drawing.Color.DimGray;
            this.LB_récursiff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB_récursiff.ForeColor = System.Drawing.SystemColors.Window;
            this.LB_récursiff.FormattingEnabled = true;
            this.LB_récursiff.HorizontalScrollbar = true;
            this.LB_récursiff.ItemHeight = 17;
            this.LB_récursiff.Items.AddRange(new object[] {
            "Triangle de Sierpinski",
            "Tapis de Sierpinski",
            "Flocon de Koch",
            "Flocon de Koch en profondeur",
            "Arbre récursif",
            "Arbre récursif 4 côtés",
            "Cercle récursif",
            "Cercle récursif 2",
            "Kaléïdoscope",
            "Mosaïque de kaléïdoscope",
            "Mosaïque à partir d\'une image"});
            this.LB_récursiff.Location = new System.Drawing.Point(224, 37);
            this.LB_récursiff.Margin = new System.Windows.Forms.Padding(2);
            this.LB_récursiff.Name = "LB_récursiff";
            this.LB_récursiff.Size = new System.Drawing.Size(225, 225);
            this.LB_récursiff.TabIndex = 21;
            this.LB_récursiff.SelectedIndexChanged += new System.EventHandler(this.LB_récursiff_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label6.Location = new System.Drawing.Point(455, 2);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 297);
            this.label6.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label7.Location = new System.Drawing.Point(460, 289);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(231, 10);
            this.label7.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.LightGray;
            this.label8.Location = new System.Drawing.Point(463, -1);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(223, 292);
            this.label8.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label9.Location = new System.Drawing.Point(686, -1);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(10, 300);
            this.label9.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label10.Location = new System.Drawing.Point(455, -3);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(231, 10);
            this.label10.TabIndex = 26;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.LightGray;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label11.Location = new System.Drawing.Point(525, 8);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 19);
            this.label11.TabIndex = 27;
            this.label11.Text = "Paramètres";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TB_height
            // 
            this.TB_height.Enabled = false;
            this.TB_height.Location = new System.Drawing.Point(479, 44);
            this.TB_height.Margin = new System.Windows.Forms.Padding(2);
            this.TB_height.Name = "TB_height";
            this.TB_height.Size = new System.Drawing.Size(76, 20);
            this.TB_height.TabIndex = 28;
            this.TB_height.Text = "1000";
            this.TB_height.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.TB_height, "Hauteur de l\'image à créer");
            this.TB_height.TextChanged += new System.EventHandler(this.TB_height_TextChanged);
            this.TB_height.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_height_KeyPress);
            // 
            // TB_width
            // 
            this.TB_width.Enabled = false;
            this.TB_width.Location = new System.Drawing.Point(587, 44);
            this.TB_width.Margin = new System.Windows.Forms.Padding(2);
            this.TB_width.Name = "TB_width";
            this.TB_width.Size = new System.Drawing.Size(76, 20);
            this.TB_width.TabIndex = 29;
            this.TB_width.Text = "1000";
            this.TB_width.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.TB_width, "Largeur de l\'image à créer");
            this.TB_width.TextChanged += new System.EventHandler(this.TB_width_TextChanged);
            this.TB_width.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_width_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.LightGray;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label12.Location = new System.Drawing.Point(490, 27);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 15);
            this.label12.TabIndex = 30;
            this.label12.Text = "Hauteur";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.LightGray;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label13.Location = new System.Drawing.Point(601, 27);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 15);
            this.label13.TabIndex = 31;
            this.label13.Text = "Largeur";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TB_iteration
            // 
            this.TB_iteration.Enabled = false;
            this.TB_iteration.Location = new System.Drawing.Point(587, 73);
            this.TB_iteration.Margin = new System.Windows.Forms.Padding(2);
            this.TB_iteration.Name = "TB_iteration";
            this.TB_iteration.Size = new System.Drawing.Size(76, 20);
            this.TB_iteration.TabIndex = 32;
            this.TB_iteration.Text = "200";
            this.TB_iteration.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_iteration.TextChanged += new System.EventHandler(this.TB_iteration_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.LightGray;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label14.Location = new System.Drawing.Point(467, 74);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 15);
            this.label14.TabIndex = 33;
            this.label14.Text = "Max d\'itération :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label14, "Nombre d\'itération pour la génération d\'une forme récursive\r\nMax d\'itération pour" +
        " la génération d\'une forme fractale de julia\r\nPour un kaléidoscope, c\'est le nom" +
        "bre de forme générées");
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.LightGray;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label15.Location = new System.Drawing.Point(616, 170);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(50, 17);
            this.label15.TabIndex = 34;
            this.label15.Text = " Image ";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label15, "Image pour créer une mosaïque");
            // 
            // But_imageMosaique
            // 
            this.But_imageMosaique.BackColor = System.Drawing.Color.DimGray;
            this.But_imageMosaique.Cursor = System.Windows.Forms.Cursors.Hand;
            this.But_imageMosaique.Enabled = false;
            this.But_imageMosaique.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.But_imageMosaique.ForeColor = System.Drawing.Color.White;
            this.But_imageMosaique.Location = new System.Drawing.Point(602, 189);
            this.But_imageMosaique.Margin = new System.Windows.Forms.Padding(2);
            this.But_imageMosaique.Name = "But_imageMosaique";
            this.But_imageMosaique.Size = new System.Drawing.Size(82, 89);
            this.But_imageMosaique.TabIndex = 50;
            this.But_imageMosaique.Text = "CLIQUE ICI";
            this.But_imageMosaique.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip1.SetToolTip(this.But_imageMosaique, "Image pour créer une mosaïque");
            this.But_imageMosaique.UseVisualStyleBackColor = false;
            this.But_imageMosaique.Click += new System.EventHandler(this.But_imageMosaique_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.LightGray;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label16.Location = new System.Drawing.Point(467, 189);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(116, 15);
            this.label16.TabIndex = 51;
            this.label16.Text = "Nombre mosaïque :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label16, "Nombre de mosaïque en largeur sur l\'image à créer");
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.LightGray;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label17.Location = new System.Drawing.Point(490, 149);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 15);
            this.label17.TabIndex = 52;
            this.label17.Text = "Contraste :";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.LightGray;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label18.Location = new System.Drawing.Point(467, 120);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(123, 15);
            this.label18.TabIndex = 53;
            this.label18.Text = "Max d\'essai couleur :";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label18, resources.GetString("label18.ToolTip"));
            // 
            // TB_tryColor
            // 
            this.TB_tryColor.Enabled = false;
            this.TB_tryColor.Location = new System.Drawing.Point(587, 120);
            this.TB_tryColor.Margin = new System.Windows.Forms.Padding(2);
            this.TB_tryColor.Name = "TB_tryColor";
            this.TB_tryColor.Size = new System.Drawing.Size(76, 20);
            this.TB_tryColor.TabIndex = 54;
            this.TB_tryColor.Text = "10";
            this.TB_tryColor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_tryColor.TextChanged += new System.EventHandler(this.TB_tryColor_TextChanged);
            this.TB_tryColor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_tryColor_KeyPress);
            // 
            // TB_contraste
            // 
            this.TB_contraste.Enabled = false;
            this.TB_contraste.Location = new System.Drawing.Point(486, 166);
            this.TB_contraste.Margin = new System.Windows.Forms.Padding(2);
            this.TB_contraste.Name = "TB_contraste";
            this.TB_contraste.Size = new System.Drawing.Size(76, 20);
            this.TB_contraste.TabIndex = 55;
            this.TB_contraste.Text = "1";
            this.TB_contraste.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_contraste.TextChanged += new System.EventHandler(this.TB_contraste_TextChanged);
            this.TB_contraste.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_contraste_KeyPress);
            // 
            // TB_mosaique
            // 
            this.TB_mosaique.Enabled = false;
            this.TB_mosaique.Location = new System.Drawing.Point(486, 207);
            this.TB_mosaique.Margin = new System.Windows.Forms.Padding(2);
            this.TB_mosaique.Name = "TB_mosaique";
            this.TB_mosaique.Size = new System.Drawing.Size(76, 20);
            this.TB_mosaique.TabIndex = 56;
            this.TB_mosaique.Text = "30";
            this.TB_mosaique.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.TB_mosaique, "Nombre de kaléidoscope en largeur");
            this.TB_mosaique.TextChanged += new System.EventHandler(this.TB_mosaique_TextChanged);
            this.TB_mosaique.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_mosaique_KeyPress);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.LightGray;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label19.Location = new System.Drawing.Point(463, 238);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(122, 15);
            this.label19.TabIndex = 57;
            this.label19.Text = "Couleur arrière plan :";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label19, "Couleur d\'arrière plan pour les formes générées récursivement.\r\nCliquez sur le bo" +
        "uton ci-dessous pour changer la couleur");
            // 
            // But_Color
            // 
            this.But_Color.BackColor = System.Drawing.Color.White;
            this.But_Color.Cursor = System.Windows.Forms.Cursors.Hand;
            this.But_Color.Enabled = false;
            this.But_Color.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.But_Color.FlatAppearance.BorderSize = 5;
            this.But_Color.Location = new System.Drawing.Point(466, 255);
            this.But_Color.Margin = new System.Windows.Forms.Padding(2);
            this.But_Color.Name = "But_Color";
            this.But_Color.Size = new System.Drawing.Size(118, 24);
            this.But_Color.TabIndex = 58;
            this.toolTip1.SetToolTip(this.But_Color, "Cliquez ici pour choisir la couleur de remplissage.\r\nPour les kaléidoscopes, blan" +
        "c est la couleur par défaut et sera ignorée.");
            this.But_Color.UseVisualStyleBackColor = false;
            this.But_Color.Click += new System.EventHandler(this.But_Color_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.LightGray;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label20.Location = new System.Drawing.Point(467, 97);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(115, 15);
            this.label20.TabIndex = 60;
            this.label20.Text = "Taille fractale Julia :";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label20, resources.GetString("label20.ToolTip"));
            // 
            // label_Pourc
            // 
            this.label_Pourc.AutoSize = true;
            this.label_Pourc.BackColor = System.Drawing.Color.LightGray;
            this.label_Pourc.Location = new System.Drawing.Point(613, 142);
            this.label_Pourc.Name = "label_Pourc";
            this.label_Pourc.Size = new System.Drawing.Size(27, 13);
            this.label_Pourc.TabIndex = 59;
            this.label_Pourc.Text = "20%";
            // 
            // TB_sizeJulia
            // 
            this.TB_sizeJulia.Enabled = false;
            this.TB_sizeJulia.Location = new System.Drawing.Point(587, 96);
            this.TB_sizeJulia.Margin = new System.Windows.Forms.Padding(2);
            this.TB_sizeJulia.Name = "TB_sizeJulia";
            this.TB_sizeJulia.Size = new System.Drawing.Size(76, 20);
            this.TB_sizeJulia.TabIndex = 61;
            this.TB_sizeJulia.Text = "1.75";
            this.TB_sizeJulia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_sizeJulia.TextChanged += new System.EventHandler(this.TB_sizeJulia_TextChanged);
            // 
            // comboBox_Func
            // 
            this.comboBox_Func.FormattingEnabled = true;
            this.comboBox_Func.Items.AddRange(new object[] {
            "Z = Z² + C",
            "Z = Sin(Z) * C",
            "Newton fractale"});
            this.comboBox_Func.Location = new System.Drawing.Point(23, 345);
            this.comboBox_Func.Name = "comboBox_Func";
            this.comboBox_Func.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Func.TabIndex = 62;
            this.comboBox_Func.Text = "Fonction utilisée";
            this.comboBox_Func.SelectedIndexChanged += new System.EventHandler(this.comboBox_Func_SelectedIndexChanged);
            // 
            // button_JuliaForm
            // 
            this.button_JuliaForm.AutoSize = true;
            this.button_JuliaForm.BackColor = System.Drawing.Color.DodgerBlue;
            this.button_JuliaForm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_JuliaForm.FlatAppearance.BorderSize = 0;
            this.button_JuliaForm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_JuliaForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_JuliaForm.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_JuliaForm.Location = new System.Drawing.Point(155, 330);
            this.button_JuliaForm.Name = "button_JuliaForm";
            this.button_JuliaForm.Size = new System.Drawing.Size(64, 38);
            this.button_JuliaForm.TabIndex = 63;
            this.button_JuliaForm.Text = "Julia set\r\nBeta";
            this.button_JuliaForm.UseVisualStyleBackColor = false;
            this.button_JuliaForm.Click += new System.EventHandler(this.button_JuliaForm_Click);
            // 
            // FractaleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(695, 378);
            this.Controls.Add(this.button_JuliaForm);
            this.Controls.Add(this.comboBox_Func);
            this.Controls.Add(this.TB_sizeJulia);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label_Pourc);
            this.Controls.Add(this.But_Color);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.TB_mosaique);
            this.Controls.Add(this.TB_contraste);
            this.Controls.Add(this.TB_tryColor);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.But_imageMosaique);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.TB_iteration);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.TB_width);
            this.Controls.Add(this.TB_height);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LB_récursiff);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TB_Im);
            this.Controls.Add(this.TB_Re);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LB_equations);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.SaveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FractaleForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FRACTALES";
            this.Load += new System.EventHandler(this.FractaleForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.ListBox LB_equations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_Re;
        private System.Windows.Forms.TextBox TB_Im;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox LB_récursiff;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TB_height;
        private System.Windows.Forms.TextBox TB_width;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox TB_iteration;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button But_imageMosaique;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox TB_tryColor;
        private System.Windows.Forms.TextBox TB_contraste;
        private System.Windows.Forms.TextBox TB_mosaique;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private ToolTip toolTip1;
        private Label label19;
        private Button But_Color;
        private ColorDialog colorDialog1;
        private Label label_Pourc;
        private Label label20;
        private TextBox TB_sizeJulia;
        private ComboBox comboBox_Func;
        private Button button_JuliaForm;
    }
}