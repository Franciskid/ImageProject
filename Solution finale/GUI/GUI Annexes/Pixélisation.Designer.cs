namespace Photoshop3000
{
    partial class Pixellisation
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
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPixel = new System.Windows.Forms.TextBox();
            this.KeepAspectRatio = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.Annuler = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelMax = new System.Windows.Forms.Label();
            this.labelMin = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxPourcentage = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 20;
            this.trackBar1.Location = new System.Drawing.Point(27, 44);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(168, 56);
            this.trackBar1.SmallChange = 20;
            this.trackBar1.TabIndex = 13;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Taille d\'un \'gros\' Pixel";
            // 
            // textBoxPixel
            // 
            this.textBoxPixel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPixel.Location = new System.Drawing.Point(64, 90);
            this.textBoxPixel.Name = "textBoxPixel";
            this.textBoxPixel.Size = new System.Drawing.Size(94, 27);
            this.textBoxPixel.TabIndex = 23;
            this.textBoxPixel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxPixel.TextChanged += new System.EventHandler(this.taille_TextChanged);
            this.textBoxPixel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.taille_KeyPress);
            // 
            // KeepAspectRatio
            // 
            this.KeepAspectRatio.AutoSize = true;
            this.KeepAspectRatio.Checked = true;
            this.KeepAspectRatio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.KeepAspectRatio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeepAspectRatio.Location = new System.Drawing.Point(27, 161);
            this.KeepAspectRatio.Name = "KeepAspectRatio";
            this.KeepAspectRatio.Size = new System.Drawing.Size(175, 24);
            this.KeepAspectRatio.TabIndex = 29;
            this.KeepAspectRatio.Text = "Adaptateur de taille";
            this.toolTip1.SetToolTip(this.KeepAspectRatio, "Modifie la taille d\'un gros Pixel de façon à remplir\r\nl\'image d\'uniquement que de" +
        "s gros pixels.\r\nSinon il y aurait sur les bords des pixels qui n\'auraient\r\npas l" +
        "a même taille");
            this.KeepAspectRatio.UseVisualStyleBackColor = true;
            this.KeepAspectRatio.CheckedChanged += new System.EventHandler(this.KeepAspectRatio_CheckedChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LimeGreen;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(243, 9);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 123);
            this.button1.TabIndex = 30;
            this.button1.Text = "Appliquer à l\'image";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // Annuler
            // 
            this.Annuler.BackColor = System.Drawing.Color.Red;
            this.Annuler.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Annuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Annuler.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Annuler.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Annuler.Location = new System.Drawing.Point(243, 136);
            this.Annuler.Margin = new System.Windows.Forms.Padding(0);
            this.Annuler.Name = "Annuler";
            this.Annuler.Size = new System.Drawing.Size(127, 51);
            this.Annuler.TabIndex = 31;
            this.Annuler.Text = "Annuler";
            this.Annuler.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(230, -7);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 194);
            this.label3.TabIndex = 36;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(230, 130);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 10);
            this.label2.TabIndex = 37;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMax
            // 
            this.labelMax.AutoSize = true;
            this.labelMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMax.Location = new System.Drawing.Point(190, 59);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(36, 20);
            this.labelMax.TabIndex = 38;
            this.labelMax.Text = "100";
            // 
            // labelMin
            // 
            this.labelMin.AutoSize = true;
            this.labelMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMin.Location = new System.Drawing.Point(12, 56);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(18, 20);
            this.labelMin.TabIndex = 39;
            this.labelMin.Text = "1";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(370, -7);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 194);
            this.label4.TabIndex = 40;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(230, 187);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 13);
            this.label5.TabIndex = 41;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(164, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 20);
            this.label6.TabIndex = 42;
            this.label6.Text = "pixels";
            // 
            // textBoxPourcentage
            // 
            this.textBoxPourcentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPourcentage.Location = new System.Drawing.Point(64, 123);
            this.textBoxPourcentage.Name = "textBoxPourcentage";
            this.textBoxPourcentage.Size = new System.Drawing.Size(94, 27);
            this.textBoxPourcentage.TabIndex = 43;
            this.textBoxPourcentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxPourcentage.TextChanged += new System.EventHandler(this.textBoxPourcentage_TextChanged);
            this.textBoxPourcentage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.taille_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(164, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 20);
            this.label7.TabIndex = 44;
            this.label7.Text = "%";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label8.Location = new System.Drawing.Point(230, -1);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(146, 10);
            this.label8.TabIndex = 51;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Pixellisation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 197);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxPourcentage);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxPixel);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelMin);
            this.Controls.Add(this.labelMax);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Annuler);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.KeepAspectRatio);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Pixellisation";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pixellisation";
            this.Load += new System.EventHandler(this.Pixélisation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPixel;
        private System.Windows.Forms.CheckBox KeepAspectRatio;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Annuler;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxPourcentage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}