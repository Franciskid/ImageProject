namespace Photoshop3000
{
    partial class RognageImage
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ptOrigine = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.origineXPtLab = new System.Windows.Forms.Label();
            this.origineYPtLab = new System.Windows.Forms.Label();
            this.destinationXPtLab = new System.Windows.Forms.Label();
            this.destinationYPtLab = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(851, 512);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // ptOrigine
            // 
            this.ptOrigine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ptOrigine.AutoSize = true;
            this.ptOrigine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ptOrigine.Location = new System.Drawing.Point(12, 515);
            this.ptOrigine.Name = "ptOrigine";
            this.ptOrigine.Size = new System.Drawing.Size(86, 17);
            this.ptOrigine.TabIndex = 1;
            this.ptOrigine.Text = "Origine       :";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(11, 539);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Destination :";
            // 
            // origineXPtLab
            // 
            this.origineXPtLab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.origineXPtLab.AutoSize = true;
            this.origineXPtLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.origineXPtLab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.origineXPtLab.Location = new System.Drawing.Point(104, 515);
            this.origineXPtLab.Name = "origineXPtLab";
            this.origineXPtLab.Size = new System.Drawing.Size(49, 20);
            this.origineXPtLab.TabIndex = 3;
            this.origineXPtLab.Text = "X : 0";
            // 
            // origineYPtLab
            // 
            this.origineYPtLab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.origineYPtLab.AutoSize = true;
            this.origineYPtLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.origineYPtLab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.origineYPtLab.Location = new System.Drawing.Point(186, 516);
            this.origineYPtLab.Name = "origineYPtLab";
            this.origineYPtLab.Size = new System.Drawing.Size(48, 20);
            this.origineYPtLab.TabIndex = 5;
            this.origineYPtLab.Text = "Y : 0";
            // 
            // destinationXPtLab
            // 
            this.destinationXPtLab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.destinationXPtLab.AutoSize = true;
            this.destinationXPtLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destinationXPtLab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.destinationXPtLab.Location = new System.Drawing.Point(104, 536);
            this.destinationXPtLab.Name = "destinationXPtLab";
            this.destinationXPtLab.Size = new System.Drawing.Size(39, 20);
            this.destinationXPtLab.TabIndex = 6;
            this.destinationXPtLab.Text = "X : ";
            // 
            // destinationYPtLab
            // 
            this.destinationYPtLab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.destinationYPtLab.AutoSize = true;
            this.destinationYPtLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destinationYPtLab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.destinationYPtLab.Location = new System.Drawing.Point(186, 536);
            this.destinationYPtLab.Name = "destinationYPtLab";
            this.destinationYPtLab.Size = new System.Drawing.Size(38, 20);
            this.destinationYPtLab.TabIndex = 7;
            this.destinationYPtLab.Text = "Y : ";
            // 
            // RognageImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(851, 565);
            this.Controls.Add(this.destinationYPtLab);
            this.Controls.Add(this.destinationXPtLab);
            this.Controls.Add(this.origineYPtLab);
            this.Controls.Add(this.origineXPtLab);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ptOrigine);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 100);
            this.Name = "RognageImage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RognageImage";
            this.Load += new System.EventHandler(this.RognageImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label ptOrigine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label origineXPtLab;
        private System.Windows.Forms.Label origineYPtLab;
        private System.Windows.Forms.Label destinationXPtLab;
        private System.Windows.Forms.Label destinationYPtLab;
    }
}