using System;
using System.Drawing;

namespace Photoshop3000.Barcode
{
    /// <summary>
    /// Encapsule une surface de dessin relative à un QR Code.
    /// </summary>
    class QRCode
    {
        //Champs et propriétés

        /// <summary>
        /// Données de ce qrcode.
        /// </summary>
        public QRCodeData Data { get; private set; }

        private sbyte[,] matrix;


        /// <summary>
        /// Taille du qrcode
        /// </summary>
        public int Size => this.matrix.GetLength(0); //Mat carré

        /// <summary>
        /// <see langword="true"/> : 1 (noir)  | <see langword="false"/> : 0 (blanc)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool? this[int x, int y]
        {
            get => this.matrix[x, y] == -1 ? (bool?)null : this.matrix[x, y] == 1;
            
            set => this.matrix[x, y] = (sbyte)(value == null ? -1 : (bool)value ? 1 : 0);
        }


        //Constructeur

        /// <summary>
        /// Initialise le qrcode avec un <see cref="QRCodeData"/>.
        /// </summary>
        /// <param name="data"></param>
        public QRCode(QRCodeData data)
            : this(VersionToSize(data.Version))
        {
            SetData(data);
        }

        /// <summary>
        /// Initialise le qrcode en fonction de sa taille. <see cref="Data"/> est null.
        /// </summary>
        /// <param name="size"></param>
        public QRCode(int size)
        {
            InitializeMatrix(size);
        }


        //Méthodes

        /// <summary>
        /// Dessine ce qrcode sur l'image entrée en paramètre.
        /// </summary>
        /// <param name="graphic">Le graphic dans lequel dessiner le qrcode</param>
        public void ToImage(MyGraphics graphic)
        {
            ToImage(graphic, Pixel.FromColor(Couleurs.Noir), Pixel.FromColor(Couleurs.Blanc), 2);
        }

        /// <summary>
        /// Dessine ce qrcode sur l'image entrée en paramètre.
        /// </summary>
        /// <param name="graphic">Le graphic dans lequel dessiner le qrcode</param>
        /// <param name="fG">Couleur de premier plan du qrcode (noir)</param>
        /// <param name="bG">Couleur de second plan du qrcode (blanc)</param>
        /// <param name="bordureIntérieure"></param>
        public void ToImage(MyGraphics graphic, Pixel fG, Pixel bG, int bordureIntérieure)
        {
            if (bordureIntérieure < 0) bordureIntérieure = 2; //bordure par défaut

            int size = bordureIntérieure * 2 + this.Size; //taille + les bords * 2

            MyImage qr = new MyImage(size, size, bG, Formats.PixelFormat.BMP_Argb32);

            for (int i = 0; i < this.Size; i++)
            {
                for (int j = 0; j < this.Size; j++)
                {
                    qr[j + bordureIntérieure, i + bordureIntérieure] = this[i, j] == true ? fG : this[i, j] == false ? bG : Pixel.FromArgb(0);
                }
            }


            graphic.DrawImage(qr, -1); //-1 = opacité de chaque pixel de l'image
        }


        /// <summary>
        /// Initialise la matrice avec une taille donnée.
        /// </summary>
        /// <param name="size"></param>
        private void InitializeMatrix(int size)
        {
            this.matrix = new sbyte[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    this[i, j] = null;
        }

        /// <summary>
        /// Met la matrice à null.
        /// </summary>
        public void Reset()
        {
            InitializeMatrix(this.Size);
        }


        /// <summary>
        /// Set les données de ce qrcode.
        /// </summary>
        /// <param name="data"></param>
        public void SetData(QRCodeData data)
        {
            this.Data = data;
        }


        /// <summary>
        /// Convertit une version (1-40) en taille de module.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static int VersionToSize(int version) => 21 + (version - 1) * 4;

        /// <summary>
        /// Convertit une taille (en module) en version (1-40)
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static int SizeToVersion(int size) => (size - 21) / 4 + 1;



        /// <summary>
        /// Niveau de correction d'erreur.
        /// </summary>
        public enum ErrorCorrectionLevel
        {
            /// <summary>
            /// 7%
            /// </summary>
            L = 0,

            /// <summary>
            /// 15%
            /// </summary>
            M = 1,

            /// <summary>
            /// 25%
            /// </summary>
            Q = 2,

            /// <summary>
            /// 30%
            /// </summary>
            H = 3,

            /// <summary>
            /// Boost le niveau de correction choisi tout en gardant la même version que celle choisie.
            /// </summary>
            Boost = 4,
        }

        /// <summary>
        /// Mode d'encodage
        /// </summary>
        public enum EncoderMode
        {
            /// <summary>
            /// Chiffres
            /// </summary>
            Numeric = 0,

            /// <summary>
            /// 1 à 9, A à Z et ' '   '$'   '%'   '*'   '+'   '-'   '.'   '/'   ':'
            /// </summary>
            AlphaNumeric = 1,

            /// <summary>
            /// 256 premiers char du format utf-8
            /// </summary>
            Byte = 2,

            /// <summary>
            /// Charactères japonais. 
            /// Implémenté mais problème de format, Impossible de convertir de Unicode -> Shift JIS
            /// </summary>
            Kanji = 3,

            /// <summary>
            /// Extended channel interpretation
            /// </summary>
            Eci = 4,

            /// <summary>
            /// Laisser le programme decider quel mode est le plus approprié.
            /// </summary>
            Auto = -1
        }

        /// <summary>
        /// Mode eci : extended channel interpretation, pas encore totalement fonctionnel.
        /// </summary>
        public enum EciMode
        {
            Auto = 0,

            Shift_JIS = 20,

            UTF8 = 26,

            US_ASCII = 27,
        }
    }


    static class QRCodeHelper
    {
        /// <summary>
        /// Génère un QrCode à partir des paramètres donnés et le dessine dans un <see cref="MyImage"/>. Ne supporte pas 
        /// l'optimisation par segmentation des modes d'encodages.
        /// </summary>
        /// <param name="message">Message à encoder</param>
        /// <param name="mode">Mode d'encodage (utiliser <see cref="QRCode.EncoderMode.Auto"/> si on est pas sûr)</param>
        /// <param name="eci"></param>
        /// <param name="level">Niveau de correction à utiliser (utiliser '<see cref="QRCode.ErrorCorrectionLevel.L"/> | <see cref="QRCode.ErrorCorrectionLevel.Boost"/>' si on est pas sûr)</param>
        /// <param name="version">Version à utiliser (-1 pour la meilleure)</param>
        /// <param name="mask">Mask à utiliser (-1 pour le meilleur)</param>
        /// <param name="rect">Taille et forme du qr code</param>
        /// <param name="fg">Couleur des modules du qr code (noir recommandé)</param>
        /// <param name="bg">Couleur d'arrière plan (blanc recommandé)</param>
        /// <param name="bordure">Taille en module des bords entre l'image et le qr code (1 ou 2 recommandées)</param>
        /// <param name="srcA">Source alpha, false est mieux</param>
        /// <returns></returns>
        public static MyImage GenerateQRCode(string message, QRCode.EncoderMode mode, QRCode.EciMode eci,
            QRCode.ErrorCorrectionLevel level, int version, int mask, Rectangle rect, Pixel fg, Pixel bg, int bordure, bool srcA)
        {
            MyImage im = new MyImage(rect.Height, rect.Width, bg, Formats.PixelFormat.BMP_Argb32);

            GenerateQRCode(message, mode, eci, level, version, mask, im, rect, fg, bg, bordure, srcA);

            return im;
        }

        /// <summary>
        /// Génère un QrCode à partir des paramètres donnés et le dessine dans le <see cref="MyImage"/> donné.
        /// </summary>
        /// <param name="message">Message à encoder</param>
        /// <param name="mode">Mode d'encodage (utiliser <see cref="QRCode.EncoderMode.Auto"/> si on est pas sûr)</param>
        /// <param name="eci"></param>
        /// <param name="level">Niveau de correction à utiliser (utiliser '<see cref="QRCode.ErrorCorrectionLevel.L"/> | <see cref="QRCode.ErrorCorrectionLevel.Boost"/>' si on est pas sûr)</param>
        /// <param name="version">Version à utiliser (-1 pour la meilleure)</param>
        /// <param name="mask">Mask à utiliser (-1 pour le meilleur)</param>
        /// <param name="image">Image sur laquelle dessiner le qr code</param>
        /// <param name="rect">Taille et position du qrcode dans l'image</param>
        /// <param name="fg">Couleur des modules du qr code (noir recommandé)</param>
        /// <param name="bg">Couleur d'arrière plan (blanc recommandé)</param>
        /// <param name="bordure">Taille en module des bords entre l'image et le qr code (1 ou 2 recommandées)</param>
        /// <param name="srcA">Source alpha, true est mieux</param>
        /// <returns></returns>
        public static void GenerateQRCode(string message, QRCode.EncoderMode mode, QRCode.EciMode eci,
            QRCode.ErrorCorrectionLevel level, int version, int mask, MyImage image, Rectangle rect, Pixel fg, Pixel bg, int bordure, bool srcA)
        {
            QRCodeData data = new QRCodeData(level, version);
            data.Encode(message, mode, eci);

            QRCode qrcode = DrawQRCode.GenerateQRCode(data, mask);

            MyGraphics g = new MyGraphics(image)
            {
                InterpolationMode = InterpolationMode.NearestNeighbour,
                KeepAspectRatio = false,
                SourceAlpha = srcA,
                Clip = rect
            };

            qrcode.ToImage(g, fg, bg, bordure);
        }


        /// <summary>
        /// Scan une image contenant un QrCode et renvoie le message contenu dans ce QrCode.
        /// </summary>
        /// <param name="image">Image à scanner</param>
        /// <returns></returns>
        public static string ScanQrCode(MyImage image)
        {
            ScanQRCode scan = new ScanQRCode(image);

            return scan.Message;
        }
    }
}
