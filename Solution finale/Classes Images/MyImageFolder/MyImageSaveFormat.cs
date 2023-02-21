using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photoshop3000
{

    /// <summary>
    /// Classe pour préciser le format de sauvegarde d'un MyImage.
    /// </summary>
    public class MyImageSaveFormat
    {
        /// <summary>
        /// Format avec lequel sauvegarder l'image. Prioritaire sur le format de pixel.
        /// </summary>
        public Formats.ImageFormat ImageFormat { get; set; }

        /// <summary>
        /// Format de pixel dans lequel sauvegarder les données. 2ème dans l'ordre de priorité.
        /// </summary>
        public Formats.PixelFormat PixelFormat { get; set; }

        /// <summary>
        /// Uniquement destiné aux images .bmp 24bpp.
        /// </summary>
        public bool Compression_bmp_24bpp { get; set; }

        /// <summary>
        /// Indique s'il faut compresser l'image ou non.
        /// </summary>
        public bool IsCompressionPossible => this.Compression_bmp_24bpp && this.PixelFormat == Formats.PixelFormat.BMP_Rgb24;
    }

}
