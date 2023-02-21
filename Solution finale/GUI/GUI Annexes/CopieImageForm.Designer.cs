using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Photoshop3000
{
    partial class CopieImageForm
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
            this.pB_static = new System.Windows.Forms.PictureBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SaveButton = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.textBoxOrigineY = new System.Windows.Forms.TextBox();
            this.textBoxOrigineX = new System.Windows.Forms.TextBox();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDestinY = new System.Windows.Forms.TextBox();
            this.textBoxDestinX = new System.Windows.Forms.TextBox();
            this.checkBoxEtirer = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonHelper = new System.Windows.Forms.Button();
            this.trackBarOpacité = new System.Windows.Forms.TrackBar();
            this.labelOpacité = new System.Windows.Forms.Label();
            this.buttonCentrer = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pB_Moving = new Photoshop3000.SizeablePictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pB_static)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacité)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_Moving)).BeginInit();
            this.SuspendLayout();
            // 
            // pB_static
            // 
            this.pB_static.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pB_static.BackColor = System.Drawing.Color.LightGray;
            this.pB_static.Location = new System.Drawing.Point(244, 12);
            this.pB_static.Margin = new System.Windows.Forms.Padding(0);
            this.pB_static.Name = "pB_static";
            this.pB_static.Size = new System.Drawing.Size(620, 529);
            this.pB_static.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pB_static.TabIndex = 0;
            this.pB_static.TabStop = false;
            this.pB_static.SizeChanged += new System.EventHandler(this.pictureboxStatic_changesize);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonOpen.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpen.Location = new System.Drawing.Point(7, 291);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(206, 77);
            this.buttonOpen.TabIndex = 2;
            this.buttonOpen.Text = "Cliquez ici pour\r\nouvrir une image\r\nà copier";
            this.buttonOpen.UseVisualStyleBackColor = false;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(217)))), ((int)(((byte)(32)))));
            this.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.SaveButton.Enabled = false;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SaveButton.Location = new System.Drawing.Point(7, 387);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(206, 74);
            this.SaveButton.TabIndex = 12;
            this.SaveButton.Text = "Copier";
            this.SaveButton.UseVisualStyleBackColor = false;
            // 
            // Clear
            // 
            this.Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Clear.BackColor = System.Drawing.Color.Red;
            this.Clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Clear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Clear.Location = new System.Drawing.Point(7, 467);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(206, 74);
            this.Clear.TabIndex = 13;
            this.Clear.Text = "Annuler";
            this.Clear.UseVisualStyleBackColor = false;
            // 
            // textBoxOrigineY
            // 
            this.textBoxOrigineY.Enabled = false;
            this.textBoxOrigineY.Location = new System.Drawing.Point(113, 80);
            this.textBoxOrigineY.Name = "textBoxOrigineY";
            this.textBoxOrigineY.Size = new System.Drawing.Size(100, 22);
            this.textBoxOrigineY.TabIndex = 14;
            this.textBoxOrigineY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxOrigineY.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBoxOrigineY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.taille_KeyPress);
            // 
            // textBoxOrigineX
            // 
            this.textBoxOrigineX.Enabled = false;
            this.textBoxOrigineX.Location = new System.Drawing.Point(7, 80);
            this.textBoxOrigineX.Name = "textBoxOrigineX";
            this.textBoxOrigineX.Size = new System.Drawing.Size(100, 22);
            this.textBoxOrigineX.TabIndex = 15;
            this.textBoxOrigineX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxOrigineX.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBoxOrigineX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.taille_KeyPress);
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(4, 4);
            this.labelHeight.Margin = new System.Windows.Forms.Padding(0);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(111, 17);
            this.labelHeight.TabIndex = 16;
            this.labelHeight.Text = "Image hauteur : ";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(4, 28);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(107, 17);
            this.labelWidth.TabIndex = 17;
            this.labelWidth.Text = "Image largeur : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "Point origine X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(113, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 17);
            this.label4.TabIndex = 19;
            this.label4.Text = "Point origine Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "Point destin. X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(113, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 17);
            this.label6.TabIndex = 21;
            this.label6.Text = "Point destin. Y";
            // 
            // textBoxDestinY
            // 
            this.textBoxDestinY.Enabled = false;
            this.textBoxDestinY.Location = new System.Drawing.Point(113, 139);
            this.textBoxDestinY.Name = "textBoxDestinY";
            this.textBoxDestinY.Size = new System.Drawing.Size(100, 22);
            this.textBoxDestinY.TabIndex = 22;
            this.textBoxDestinY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxDestinY.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBoxDestinY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.taille_KeyPress);
            // 
            // textBoxDestinX
            // 
            this.textBoxDestinX.Enabled = false;
            this.textBoxDestinX.Location = new System.Drawing.Point(7, 139);
            this.textBoxDestinX.Name = "textBoxDestinX";
            this.textBoxDestinX.Size = new System.Drawing.Size(100, 22);
            this.textBoxDestinX.TabIndex = 23;
            this.textBoxDestinX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxDestinX.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBoxDestinX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.taille_KeyPress);
            // 
            // checkBoxEtirer
            // 
            this.checkBoxEtirer.AutoSize = true;
            this.checkBoxEtirer.Enabled = false;
            this.checkBoxEtirer.Location = new System.Drawing.Point(12, 177);
            this.checkBoxEtirer.Name = "checkBoxEtirer";
            this.checkBoxEtirer.Size = new System.Drawing.Size(112, 21);
            this.checkBoxEtirer.TabIndex = 24;
            this.checkBoxEtirer.Text = "Etirer l\'image";
            this.toolTip1.SetToolTip(this.checkBoxEtirer, "Etire l\'image à copier quand la taille de l\'image\r\nest modifiée pour remplir l\'es" +
        "pace disponible\r\n");
            this.checkBoxEtirer.UseVisualStyleBackColor = true;
            this.checkBoxEtirer.CheckedChanged += new System.EventHandler(this.checkBoxEtirer_CheckedChanged);
            // 
            // buttonHelper
            // 
            this.buttonHelper.BackColor = System.Drawing.SystemColors.Control;
            this.buttonHelper.FlatAppearance.BorderSize = 0;
            this.buttonHelper.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHelper.Location = new System.Drawing.Point(159, 4);
            this.buttonHelper.Name = "buttonHelper";
            this.buttonHelper.Size = new System.Drawing.Size(54, 45);
            this.buttonHelper.TabIndex = 25;
            this.toolTip1.SetToolTip(this.buttonHelper, "Aide");
            this.buttonHelper.UseVisualStyleBackColor = false;
            this.buttonHelper.Click += new System.EventHandler(this.buttonHelper_Click);
            // 
            // trackBarOpacité
            // 
            this.trackBarOpacité.Enabled = false;
            this.trackBarOpacité.Location = new System.Drawing.Point(10, 220);
            this.trackBarOpacité.Maximum = 100;
            this.trackBarOpacité.Name = "trackBarOpacité";
            this.trackBarOpacité.Size = new System.Drawing.Size(201, 56);
            this.trackBarOpacité.TabIndex = 29;
            this.toolTip1.SetToolTip(this.trackBarOpacité, "Opacité de l\'image à copier");
            this.trackBarOpacité.Value = 100;
            this.trackBarOpacité.Scroll += new System.EventHandler(this.trackBarOpacité_Scroll);
            // 
            // labelOpacité
            // 
            this.labelOpacité.AutoSize = true;
            this.labelOpacité.Enabled = false;
            this.labelOpacité.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOpacité.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.labelOpacité.Location = new System.Drawing.Point(48, 258);
            this.labelOpacité.Name = "labelOpacité";
            this.labelOpacité.Size = new System.Drawing.Size(108, 18);
            this.labelOpacité.TabIndex = 30;
            this.labelOpacité.Text = "Opacité : 100";
            this.toolTip1.SetToolTip(this.labelOpacité, "Opaciter de l\'image à copier. 100 revient à ne pas avoir de transparence");
            // 
            // buttonCentrer
            // 
            this.buttonCentrer.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonCentrer.Enabled = false;
            this.buttonCentrer.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCentrer.Location = new System.Drawing.Point(130, 169);
            this.buttonCentrer.Name = "buttonCentrer";
            this.buttonCentrer.Size = new System.Drawing.Size(75, 35);
            this.buttonCentrer.TabIndex = 26;
            this.buttonCentrer.Text = "Centrer";
            this.buttonCentrer.UseVisualStyleBackColor = false;
            this.buttonCentrer.Click += new System.EventHandler(this.buttonCentrer_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.LightGray;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(223, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(653, 564);
            this.label2.TabIndex = 27;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(217, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 564);
            this.label1.TabIndex = 28;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label7.Location = new System.Drawing.Point(8, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(203, 2);
            this.label7.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label8.Location = new System.Drawing.Point(7, 284);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(205, 2);
            this.label8.TabIndex = 32;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label9.Location = new System.Drawing.Point(210, 207);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(2, 77);
            this.label9.TabIndex = 33;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label10.Location = new System.Drawing.Point(7, 207);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(2, 77);
            this.label10.TabIndex = 34;
            // 
            // pB_Moving
            // 
            this.pB_Moving.BackColor = System.Drawing.Color.Transparent;
            this.pB_Moving.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pB_Moving.Location = new System.Drawing.Point(476, 205);
            this.pB_Moving.MinimumSize = new System.Drawing.Size(40, 20);
            this.pB_Moving.Name = "pB_Moving";
            this.pB_Moving.Size = new System.Drawing.Size(98, 71);
            this.pB_Moving.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pB_Moving.TabIndex = 1;
            this.pB_Moving.TabStop = false;
            this.pB_Moving.Visible = false;
            this.pB_Moving.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pB_Moving.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pB_Moving.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            this.pB_Moving.Resize += new System.EventHandler(this.pictureboxMove_changesize);
            // 
            // CopieImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 553);
            this.Controls.Add(this.pB_Moving);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labelOpacité);
            this.Controls.Add(this.trackBarOpacité);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pB_static);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCentrer);
            this.Controls.Add(this.buttonHelper);
            this.Controls.Add(this.checkBoxEtirer);
            this.Controls.Add(this.textBoxDestinX);
            this.Controls.Add(this.textBoxDestinY);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.labelHeight);
            this.Controls.Add(this.textBoxOrigineX);
            this.Controls.Add(this.textBoxOrigineY);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.buttonOpen);
            this.MinimumSize = new System.Drawing.Size(700, 600);
            this.Name = "CopieImageForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copie d\'une image dans une image";
            this.Load += new System.EventHandler(this.CopieImageForm_Load);
            this.SizeChanged += new EventHandler(this.CopieImageForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pB_static)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacité)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_Moving)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.PictureBox pB_static;
        private SizeablePictureBox pB_Moving;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.TextBox textBoxOrigineY;
        private System.Windows.Forms.TextBox textBoxOrigineX;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelWidth;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox textBoxDestinY;
        private TextBox textBoxDestinX;
        private CheckBox checkBoxEtirer;
        private ToolTip toolTip1;
        private Button buttonHelper;
        private Button buttonCentrer;
        private Label label2;
        private Label label1;
        private TrackBar trackBarOpacité;
        private Label labelOpacité;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
    }

    
    internal class SizeablePictureBox : PictureBox
    {
        public SizeablePictureBox()
        {
            this.ResizeRedraw = true;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.poignéeSize = (int)(Math.Min(Math.Max((double)Math.Min(this.Size.Width, this.Size.Height) / 3.5, 12), 18));
            var rc = new Rectangle(this.ClientSize.Width - poignéeSize, this.ClientSize.Height - poignéeSize, poignéeSize, poignéeSize);
            ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x84)
            {  //WM_NCHITTEST, voir https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest
                var pos = this.PointToClient(new System.Drawing.Point(m.LParam.ToInt32()));
                if (pos.X >= this.ClientSize.Width - poignéeSize && pos.Y >= this.ClientSize.Height - poignéeSize)
                    m.Result = new IntPtr(17);  // HT_BOTTOMRIGHT, In the lower-right corner of a border of a resizable window (the user can click the mouse to resize the window diagonally).
            }
        }
        private int poignéeSize = 16;
    }
}