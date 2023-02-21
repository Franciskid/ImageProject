using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photoshop3000
{
    struct RectangleD
    {
        private double x, y;

        private double width, height;

        public bool Empty => this.x + this.y + this.width + this.height == 0;

        public double Height => this.height;
        public double Width => this.width;

        public double X => this.x;
        public double Y => this.y;

        public RectangleD(double x, double y, double width, double height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

    }
}
