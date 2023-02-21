using Microsoft.VisualStudio.TestTools.UnitTesting;
using Photoshop3000.Annexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photoshop3000.Annexes.Tests
{
    [TestClass()]
    public class MyImageStatsTests
    {
        [TestMethod()]
        public void GetAverageColorTest()
        {
            MyImage im = new MyImage(10, 10, Pixel.FromColor(Couleurs.Noir));

            Assert.Equals(Pixel.FromArgb(0, 0, 0), MyImageStats.GetAverageColor(im));

        }
    }
}