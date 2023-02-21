using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photoshop3000
{

    /// <summary>
    /// Constantes faisant référence aux index et valeurs des différentes infos dans un fichier .bmp
    /// </summary>
    internal static class BITMAPCONST
    {
        public const int HEADER_INFO_OFFSET = 14;

        public const int DATA_OFFSET24 = 54;
        public const int DATA_OFFSET32 = 122;

        public const int PIXELSPERMETER = 2835;
        public const short PLANES = 1;
        public const int le_WIN = 0x57696e20;
        public const int PALETTE_UNUSED = 0;
        public const int COLOR_IMPORTANT_ALL = 0;

        public const short BPP_VALUE24 = 24;
        public const short BPP_VALUE32 = 32;

        public const int COMPRESSION_VALUE24_UNCOMPRESSED = 0;
        public const int COMPRESSION_VALUE24_COMPRESSED = 4;
        public const int COMPRESSION_VALUE32_UNCOMPRESSED = 3;


        /// <summary>
        /// Hauteur de l'image minimale supportée.
        /// </summary>
        public const int MIN_HEIGHT = 1;

        /// <summary>
        /// Largeur de l'image minimale supportée.
        /// </summary>
        public const int MIN_WIDTH = 1;

    }
}
