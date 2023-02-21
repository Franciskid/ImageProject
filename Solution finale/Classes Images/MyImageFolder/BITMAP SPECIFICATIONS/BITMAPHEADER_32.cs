using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Photoshop3000
{

    /// <summary>
    /// Suite d'un <see cref="BITMAPHEADER"/> pour les images 32bpp. Les données en big endian sont précisées de la mention 'be'.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = BITMAPCONST.DATA_OFFSET32 - BITMAPCONST.DATA_OFFSET24)]
    [Serializable]
    internal class BITMAPHEADER_32
    {
        public int be_RedMask;
        public int be_GreenMask;
        public int be_BlueMask;
        public int be_AlphaMask;
        public int le_LCS_WINDOWS_COLOR_SPACE;
    }
}
