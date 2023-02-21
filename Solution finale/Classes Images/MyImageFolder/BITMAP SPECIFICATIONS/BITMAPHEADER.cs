using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Photoshop3000
{

    /// <summary>
    /// Header et header info d'un bitmap. Toutes les données sont en little endian.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)] //pack = 1 => alignement pour que le 1er short n'utilise pas 2 byte de padding
    [Serializable]
    internal class BITMAPHEADER
    {
        //HEADER
        private readonly byte Id_B = (int)'B'; //B, 66
        private readonly byte Id_M = (int)'M'; //M, 77
        public int le_FileSize;
        private readonly int Unused; //on le met pour garder l'ordre des byte. Sinon on doit préciser l'offset pour chaque champ avec LayoutKind.Explicit.
        public int le_DataOffset;

        //HEADERINFO 24BPP
        public int le_Size;
        public int le_Width;
        public int le_Height;
        public short le_Planes;
        public short le_BitCount;
        public int le_Compression;
        public int le_SizeImage;
        public int le_XPelsPerMeter;
        public int le_YPelsPerMeter;
        public int le_Palette;
        public int le_ClrImportant;
    }

}
