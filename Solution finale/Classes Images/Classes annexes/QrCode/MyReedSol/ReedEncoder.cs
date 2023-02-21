using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photoshop3000.QrCode.MyReedSol
{
    class ReedEncoder
    {
        private GaloisField gf;
        public ReedEncoder()
        {
            var gf = new GaloisField();
        }

        public static byte[] Encoder(byte[] message, int lengthError)
        {
            return null;
        }
    }




    /// <summary>
    /// Représente un corps fini à 256 éléments
    /// </summary>
    class GaloisField
    {
        private const int basePoly = 285;

        private byte[] log = new byte[256];
        private byte[] antiLog = new byte[256];

        public GaloisField()
        {
            InitializePuissances();
        }

        private void InitializePuissances()
        {
            int val = 1;

            for (int i = 0; i < this.log.Length; i++)
            {
                this.log[i] = (byte)val;

                val *= 2;

                if (val >= 256)
                {
                    val ^= basePoly;
                }
            }

            for (int i = 0; i < antiLog.Length; i++)
            {
                antiLog[this.log[i]] = (byte)i;
            }
            antiLog[1] = 0;
        }


    }

    class PolynomialGF
    {
        private GaloisField gf;


        public PolynomialGF(int taille)
        {

        }

        private static PolynomialGF CreateGeneratorPoly(int taille)
        {
            //(x - 2^0)..(x-2^(taille-1))
            //=(x*2^0 - 2^0)..(x*2^0 - 2^n-1)

            return null;
        }
    }

    class MonomialGF
    {
        private int xCoeff;
        private int alphaCoeff;

        private int alpha;
    }
}
