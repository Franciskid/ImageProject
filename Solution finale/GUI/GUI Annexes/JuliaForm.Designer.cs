namespace Photoshop3000.GUI.GUI_Annexes
{
    partial class JuliaForm
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
            this.pb_main = new Photoshop3000.MyPictureBox();
            this.butt_zoomIn = new System.Windows.Forms.Button();
            this.butt_refresh = new System.Windows.Forms.Button();
            this.butt_del = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb_main)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_main
            // 
            this.pb_main.Location = new System.Drawing.Point(0, 0);
            this.pb_main.MyImage = null;
            this.pb_main.Name = "pb_main";
            this.pb_main.Size = new System.Drawing.Size(500, 500);
            this.pb_main.TabIndex = 0;
            this.pb_main.TabStop = false;
            // 
            // butt_zoomIn
            // 
            this.butt_zoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butt_zoomIn.BackColor = System.Drawing.Color.DarkGray;
            this.butt_zoomIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butt_zoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butt_zoomIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butt_zoomIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.butt_zoomIn.Location = new System.Drawing.Point(505, 416);
            this.butt_zoomIn.Margin = new System.Windows.Forms.Padding(2);
            this.butt_zoomIn.Name = "butt_zoomIn";
            this.butt_zoomIn.Size = new System.Drawing.Size(268, 84);
            this.butt_zoomIn.TabIndex = 12;
            this.butt_zoomIn.Text = "Zoom In";
            this.butt_zoomIn.UseVisualStyleBackColor = false;
            this.butt_zoomIn.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // butt_refresh
            // 
            this.butt_refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butt_refresh.BackColor = System.Drawing.Color.Gray;
            this.butt_refresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butt_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butt_refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butt_refresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.butt_refresh.Location = new System.Drawing.Point(505, 328);
            this.butt_refresh.Margin = new System.Windows.Forms.Padding(2);
            this.butt_refresh.Name = "butt_refresh";
            this.butt_refresh.Size = new System.Drawing.Size(133, 84);
            this.butt_refresh.TabIndex = 14;
            this.butt_refresh.Text = "Refresh";
            this.butt_refresh.UseVisualStyleBackColor = false;
            this.butt_refresh.Click += new System.EventHandler(this.butt_refresh_Click);
            // 
            // butt_del
            // 
            this.butt_del.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butt_del.BackColor = System.Drawing.Color.Gray;
            this.butt_del.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butt_del.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butt_del.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butt_del.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.butt_del.Location = new System.Drawing.Point(640, 328);
            this.butt_del.Margin = new System.Windows.Forms.Padding(2);
            this.butt_del.Name = "butt_del";
            this.butt_del.Size = new System.Drawing.Size(133, 84);
            this.butt_del.TabIndex = 15;
            this.butt_del.Text = "Delete";
            this.butt_del.UseVisualStyleBackColor = false;
            // 
            // JuliaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 500);
            this.Controls.Add(this.butt_del);
            this.Controls.Add(this.butt_refresh);
            this.Controls.Add(this.butt_zoomIn);
            this.Controls.Add(this.pb_main);
            this.Name = "JuliaForm";
            this.Text = "JuliaForm";
            ((System.ComponentModel.ISupportInitialize)(this.pb_main)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MyPictureBox pb_main;
        private System.Windows.Forms.Button butt_zoomIn;
        private System.Windows.Forms.Button butt_refresh;
        private System.Windows.Forms.Button butt_del;
    }
}