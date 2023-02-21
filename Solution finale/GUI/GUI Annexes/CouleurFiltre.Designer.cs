using System.Windows.Forms;

namespace Photoshop3000
{
    partial class CouleurFiltre
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CouleurFiltre));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.trackbar = new System.Windows.Forms.TrackBar();
            this.labIntensité = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lab100 = new System.Windows.Forms.Label();
            this.lab0 = new System.Windows.Forms.Label();
            this.icoLab = new System.Windows.Forms.Label();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.labelBackgroundColor = new System.Windows.Forms.Label();
            this.checkBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Items.AddRange(new object[] {
            "Noir et Blanc",
            "Filtre Rouge",
            "Filtre Vert",
            "Filtre Bleu",
            "Inversion Couleurs",
            "Sépia",
            "Transformation luminosité",
            "Atténuation des couleurs",
            "Griser à partir du Rouge",
            "Griser à partir du Vert",
            "Griser à partir du Bleu"});
            this.listBox1.Location = new System.Drawing.Point(9, 41);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(229, 184);
            this.listBox1.TabIndex = 0;
            this.toolTip1.SetToolTip(this.listBox1, "Filtres à appliquer à l\'image.\r\nPlusieurs choix sont possibles pour\r\nles filtres " +
        "de couleurs.");
            this.listBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseClick);
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // trackbar
            // 
            this.trackbar.Location = new System.Drawing.Point(45, 248);
            this.trackbar.Maximum = 100;
            this.trackbar.Name = "trackbar";
            this.trackbar.Size = new System.Drawing.Size(154, 56);
            this.trackbar.TabIndex = 2;
            this.toolTip1.SetToolTip(this.trackbar, resources.GetString("trackbar.ToolTip"));
            this.trackbar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // labIntensité
            // 
            this.labIntensité.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labIntensité.Location = new System.Drawing.Point(9, 225);
            this.labIntensité.Name = "labIntensité";
            this.labIntensité.Size = new System.Drawing.Size(229, 20);
            this.labIntensité.TabIndex = 3;
            this.labIntensité.Text = "Noir et blanc intensité";
            this.labIntensité.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.labIntensité, resources.GetString("labIntensité.ToolTip"));
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(10, -4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 46);
            this.label2.TabIndex = 4;
            this.label2.Text = "Choisissez le(s) type(s) de \r\nfiltre(s) à appliquer à l\'image";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.OkButton.Location = new System.Drawing.Point(256, 0);
            this.OkButton.Margin = new System.Windows.Forms.Padding(0);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(169, 213);
            this.OkButton.TabIndex = 33;
            this.OkButton.Text = "Appliquer\r\nà \r\nl\'image";
            this.toolTip1.SetToolTip(this.OkButton, "Appliquer à l\'image les modifications indiquées");
            this.OkButton.UseVisualStyleBackColor = false;
            this.OkButton.EnabledChanged += new System.EventHandler(this.OkButton_EnabledChanged);
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.Color.Red;
            this.CancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CancelButton.Location = new System.Drawing.Point(257, 225);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(168, 128);
            this.CancelButton.TabIndex = 34;
            this.CancelButton.Text = "Annuler";
            this.toolTip1.SetToolTip(this.CancelButton, "Annuler, aucune modification ne sera effectuée sur l\'image");
            this.CancelButton.UseVisualStyleBackColor = false;
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(70, 294);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(107, 22);
            this.textBox.TabIndex = 41;
            this.textBox.Text = "0";
            this.textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.textBox, resources.GetString("textBox.ToolTip"));
            this.textBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(241, -4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 387);
            this.label3.TabIndex = 35;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(247, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 12);
            this.label4.TabIndex = 36;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(-9, 353);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(453, 10);
            this.label5.TabIndex = 37;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(428, -4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 387);
            this.label6.TabIndex = 38;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label8.Location = new System.Drawing.Point(-3, -4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 377);
            this.label8.TabIndex = 40;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lab100
            // 
            this.lab100.AutoSize = true;
            this.lab100.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab100.Location = new System.Drawing.Point(194, 258);
            this.lab100.Name = "lab100";
            this.lab100.Size = new System.Drawing.Size(45, 18);
            this.lab100.TabIndex = 42;
            this.lab100.Text = "100%";
            // 
            // lab0
            // 
            this.lab0.AutoSize = true;
            this.lab0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab0.Location = new System.Drawing.Point(13, 258);
            this.lab0.Name = "lab0";
            this.lab0.Size = new System.Drawing.Size(29, 18);
            this.lab0.TabIndex = 43;
            this.lab0.Text = "0%";
            // 
            // icoLab
            // 
            this.icoLab.Location = new System.Drawing.Point(209, 9);
            this.icoLab.Name = "icoLab";
            this.icoLab.Size = new System.Drawing.Size(30, 28);
            this.icoLab.TabIndex = 44;
            this.toolTip2.SetToolTip(this.icoLab, resources.GetString("icoLab.ToolTip"));
            // 
            // toolTip2
            // 
            this.toolTip2.AutomaticDelay = 0;
            this.toolTip2.AutoPopDelay = 10000;
            this.toolTip2.InitialDelay = 50;
            this.toolTip2.ReshowDelay = 30;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label11.Location = new System.Drawing.Point(-3, 213);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(259, 12);
            this.label11.TabIndex = 45;
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonForward
            // 
            this.buttonForward.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonForward.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonForward.Location = new System.Drawing.Point(183, 284);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(52, 40);
            this.buttonForward.TabIndex = 46;
            this.buttonForward.Text = ">>";
            this.buttonForward.UseVisualStyleBackColor = false;
            this.buttonForward.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonBack.Enabled = false;
            this.buttonBack.Location = new System.Drawing.Point(12, 284);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(52, 40);
            this.buttonBack.TabIndex = 47;
            this.buttonBack.Text = "<<";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelBackgroundColor
            // 
            this.labelBackgroundColor.BackColor = System.Drawing.SystemColors.Control;
            this.labelBackgroundColor.Location = new System.Drawing.Point(9, 225);
            this.labelBackgroundColor.Margin = new System.Windows.Forms.Padding(0);
            this.labelBackgroundColor.Name = "labelBackgroundColor";
            this.labelBackgroundColor.Size = new System.Drawing.Size(234, 100);
            this.labelBackgroundColor.TabIndex = 48;
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(70, 328);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(98, 21);
            this.checkBox.TabIndex = 49;
            this.checkBox.Text = "checkBox1";
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // CouleurFiltre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 361);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonForward);
            this.Controls.Add(this.labIntensité);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.icoLab);
            this.Controls.Add(this.lab100);
            this.Controls.Add(this.lab0);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackbar);
            this.Controls.Add(this.labelBackgroundColor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CouleurFiltre";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modification des couleurs";
            ((System.ComponentModel.ISupportInitialize)(this.trackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TrackBar trackbar;
        private System.Windows.Forms.Label labIntensité;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button OkButton;
        private new System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label lab100;
        private System.Windows.Forms.Label lab0;
        private System.Windows.Forms.Label icoLab;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonForward;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Label labelBackgroundColor;
        private System.Windows.Forms.CheckBox checkBox;
    }
}