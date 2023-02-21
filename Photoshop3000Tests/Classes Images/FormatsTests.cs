using Microsoft.VisualStudio.TestTools.UnitTesting;
using Photoshop3000;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photoshop3000.Tests
{
    [TestClass()]
    public class FormatsTests
    {
        [TestMethod()]
        public void LoiBinomialeTest()
        {
            double proba = Formats.LoiBinomiale(0, 0, 0);

            Assert.AreEqual(0, proba);
        }

        [TestMethod()]
        public void ExtractArrayTest()
        {
            byte[] arr = { 1, 2, 3, 4, 5, 6 };

            var extArr = Formats.ExtractArray(arr, 0, 3);

            bool isTrue = true;
            for (int i = 0; i < extArr.Length; i++)
                isTrue &= arr[i] == extArr[i];

            Assert.IsTrue(isTrue);
        }


        [TestMethod()]
        public void GetPaddingPixelTest()
        {
            int width = 3;
            int lengthPixel = 3;

            int padding = 3;

            Assert.AreEqual(padding, Formats.GetPaddingPixel(width, lengthPixel));
        }
    }
}