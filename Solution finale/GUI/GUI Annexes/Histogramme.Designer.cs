namespace Photoshop3000
{
    partial class Histogramme
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
            this.rempliCheckBox = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelValMin = new System.Windows.Forms.Label();
            this.labelValMax = new System.Windows.Forms.Label();
            this.largeurTB = new System.Windows.Forms.TextBox();
            this.hauteurTB = new System.Windows.Forms.TextBox();
            this.GoBut = new System.Windows.Forms.Button();
            this.AnnulerBut = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.trackBarMin = new System.Windows.Forms.TrackBar();
            this.trackBarMax = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMax)).BeginInit();
            this.SuspendLayout();
            // 
            // rempliCheckBox
            // 
            this.rempliCheckBox.AutoSize = true;
            this.rempliCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rempliCheckBox.Location = new System.Drawing.Point(15, 118);
            this.rempliCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rempliCheckBox.Name = "rempliCheckBox";
            this.rempliCheckBox.Size = new System.Drawing.Size(116, 22);
            this.rempliCheckBox.TabIndex = 0;
            this.rempliCheckBox.Text = "Remplissage";
            this.toolTip1.SetToolTip(this.rempliCheckBox, "Rempli le dessous des (de la) courbe(s)");
            this.rempliCheckBox.UseVisualStyleBackColor = true;
            this.rempliCheckBox.CheckedChanged += new System.EventHandler(this.rempliCheckBox_CheckedChanged);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Items.AddRange(new object[] {
            "Histogramme de la couleur grise",
            "Histogramme couleurs RGB | Mode 1",
            "Histogramme couleurs RGB | Mode 2",
            "Histogramme couleurs RGB | Mode 3"});
            this.listBox1.Location = new System.Drawing.Point(5, 146);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(354, 104);
            this.listBox1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.listBox1, "Type d\'histogramme");
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Largeur";
            this.toolTip1.SetToolTip(this.label1, "Largeur en pixel d el\'histogramme à réaliser");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hauteur";
            this.toolTip1.SetToolTip(this.label2, "Hauteur en pixel d el\'histogramme à réaliser");
            // 
            // labelValMin
            // 
            this.labelValMin.AutoSize = true;
            this.labelValMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelValMin.Location = new System.Drawing.Point(188, -1);
            this.labelValMin.Name = "labelValMin";
            this.labelValMin.Size = new System.Drawing.Size(113, 20);
            this.labelValMin.TabIndex = 45;
            this.labelValMin.Text = "Valeur Min : 0";
            this.toolTip1.SetToolTip(this.labelValMin, "Largeur en pixel d el\'histogramme à réaliser");
            // 
            // labelValMax
            // 
            this.labelValMax.AutoSize = true;
            this.labelValMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelValMax.Location = new System.Drawing.Point(188, 62);
            this.labelValMax.Name = "labelValMax";
            this.labelValMax.Size = new System.Drawing.Size(135, 20);
            this.labelValMax.TabIndex = 46;
            this.labelValMax.Text = "Valeur Max : 255";
            this.toolTip1.SetToolTip(this.labelValMax, "Largeur en pixel d el\'histogramme à réaliser");
            // 
            // largeurTB
            // 
            this.largeurTB.Location = new System.Drawing.Point(92, 9);
            this.largeurTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.largeurTB.Name = "largeurTB";
            this.largeurTB.Size = new System.Drawing.Size(67, 22);
            this.largeurTB.TabIndex = 4;
            this.largeurTB.TextChanged += new System.EventHandler(this.largeurTB_TextChanged);
            this.largeurTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.largeurTB_KeyPress);
            // 
            // hauteurTB
            // 
            this.hauteurTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hauteurTB.Location = new System.Drawing.Point(92, 39);
            this.hauteurTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.hauteurTB.Name = "hauteurTB";
            this.hauteurTB.Size = new System.Drawing.Size(67, 24);
            this.hauteurTB.TabIndex = 5;
            this.hauteurTB.TextChanged += new System.EventHandler(this.hauteurTB_TextChanged);
            this.hauteurTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hauteurTB_KeyPress);
            // 
            // GoBut
            // 
            this.GoBut.BackColor = System.Drawing.Color.LimeGreen;
            this.GoBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GoBut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GoBut.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GoBut.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.GoBut.Location = new System.Drawing.Point(172, 255);
            this.GoBut.Margin = new System.Windows.Forms.Padding(0);
            this.GoBut.Name = "GoBut";
            this.GoBut.Size = new System.Drawing.Size(180, 96);
            this.GoBut.TabIndex = 6;
            this.GoBut.Text = "CRÉER";
            this.GoBut.UseVisualStyleBackColor = false;
            this.GoBut.Click += new System.EventHandler(this.GoBut_Click);
            // 
            // AnnulerBut
            // 
            this.AnnulerBut.BackColor = System.Drawing.Color.Red;
            this.AnnulerBut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AnnulerBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AnnulerBut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AnnulerBut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AnnulerBut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AnnulerBut.Location = new System.Drawing.Point(12, 256);
            this.AnnulerBut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AnnulerBut.Name = "AnnulerBut";
            this.AnnulerBut.Size = new System.Drawing.Size(156, 96);
            this.AnnulerBut.TabIndex = 7;
            this.AnnulerBut.Text = "ANNULER";
            this.AnnulerBut.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(0, 242);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(364, 14);
            this.label4.TabIndex = 37;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(159, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 108);
            this.label3.TabIndex = 38;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(0, 255);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 108);
            this.label5.TabIndex = 39;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Location = new System.Drawing.Point(353, 242);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 121);
            this.label6.TabIndex = 40;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label7.Location = new System.Drawing.Point(92, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 39);
            this.label7.TabIndex = 41;
            this.label7.Click += new System.EventHandler(this.Label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 34);
            this.label8.TabIndex = 42;
            this.label8.Text = "   Couleur\r\nArrière Plan ";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label9.Location = new System.Drawing.Point(5, 351);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(360, 12);
            this.label9.TabIndex = 43;
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBarMin
            // 
            this.trackBarMin.Location = new System.Drawing.Point(165, 22);
            this.trackBarMin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBarMin.Maximum = 255;
            this.trackBarMin.Name = "trackBarMin";
            this.trackBarMin.Size = new System.Drawing.Size(199, 56);
            this.trackBarMin.TabIndex = 44;
            this.trackBarMin.Scroll += new System.EventHandler(this.trackBarMin_Scroll);
            // 
            // trackBarMax
            // 
            this.trackBarMax.Location = new System.Drawing.Point(165, 84);
            this.trackBarMax.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBarMax.Maximum = 255;
            this.trackBarMax.Name = "trackBarMax";
            this.trackBarMax.Size = new System.Drawing.Size(199, 56);
            this.trackBarMax.TabIndex = 47;
            this.trackBarMax.Value = 255;
            this.trackBarMax.Scroll += new System.EventHandler(this.trackBarMax_Scroll);
            // 
            // Histogramme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 366);
            this.Controls.Add(this.trackBarMax);
            this.Controls.Add(this.labelValMax);
            this.Controls.Add(this.labelValMin);
            this.Controls.Add(this.trackBarMin);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AnnulerBut);
            this.Controls.Add(this.GoBut);
            this.Controls.Add(this.hauteurTB);
            this.Controls.Add(this.largeurTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.rempliCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Histogramme";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Histogramme";
            this.Load += new System.EventHandler(this.Histogramme_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox rempliCheckBox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox largeurTB;
        private System.Windows.Forms.TextBox hauteurTB;
        private System.Windows.Forms.Button GoBut;
        private System.Windows.Forms.Button AnnulerBut;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar trackBarMin;
        private System.Windows.Forms.Label labelValMin;
        private System.Windows.Forms.Label labelValMax;
        private System.Windows.Forms.TrackBar trackBarMax;
    }
}