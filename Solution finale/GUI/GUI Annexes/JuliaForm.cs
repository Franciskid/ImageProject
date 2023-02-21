using Photoshop3000.Annexes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Photoshop3000.GUI.GUI_Annexes
{
    public partial class JuliaForm : Form
    {
        RectangleD pos;
        MyImage julia;
        bool loaded = false;

        private float xStart = -2, xLength = 4;
        private float yStart = -2, yLength = 4;

        private const int sizeIm = 500;


        public JuliaForm()
        {
            InitializeComponent();

            pos = new RectangleD(xStart, yStart, xLength, yLength);

            Refresh();
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!loaded)
                return;

            using (RognageImage rogn = new RognageImage(julia, 1, 1))
            {
                if (rogn.ShowDialog() == DialogResult.Yes)
                {
                    RectangleF aire = rogn.AireRognage;

                    if (aire.Width / aire.Height < 1)
                    {
                        aire.Width = aire.Height;
                    }
                    else
                    {
                        aire.Height = aire.Width;
                    }

                    double startX = this.pos.X + ((double)(aire.X) / sizeIm) * this.pos.Width;
                    double startY = this.pos.Y + ((double)(aire.Y) / sizeIm) * this.pos.Height;

                    double width = (double)aire.Width / sizeIm * this.pos.Width;
                    double height = (double)aire.Height / sizeIm * this.pos.Height;

                    this.pos = new RectangleD(startX, startY, width, height);
                }
            }

            Refresh();
        }

        private void butt_refresh_Click(object sender, EventArgs e)
        {
            Refresh();

            this.loaded = true;
        }

        private new void Refresh()
        {
            Fractale frac = new Fractale(sizeIm, sizeIm, Fractale.Fractales.Mandelbrot)
            {
                PositionJulia = pos,
                MaxItération = 100,
            };
            frac.Draw();

            this.julia = frac.MyImage;

            this.pb_main.MyImage = this.julia;
        }
    }
}
