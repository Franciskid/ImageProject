using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Photoshop3000
{
    /// <summary>
    /// Permet de créer, de lire et de sauvegarder un fichier .bmp, .csv (au format bitmap), .jpg (windows) ou .png (windows).
    /// <para/>Peut prendre en charge n'importe quel type d'image au format de pixel 32 bpp ou 24 bpp.
    /// </summary>
    public class MyImage
    {
        //Champs

        /// <summary>
        /// Données de pixels de l'image. Le format dépend de <see cref="pixelFormat"/>.
        /// </summary>
        private byte[] data;

        /// <summary>
        /// Longueur en byte d'un pixel
        /// </summary>
        private int lengthPixel;

        /// <summary>
        /// Format pivot utilisé.
        /// </summary>
        private Formats.PixelFormat pixelFormat;


        //Propriétés

        /// <summary>
        /// Format pivot utilisé. Modifiable via <see cref="ConvertPixelFormat(Formats.PixelFormat)"/>.
        /// </summary>
        public Formats.PixelFormat PixelFormat
        {
            get
            {
                return this.pixelFormat;
            }
            private set
            {
                this.pixelFormat = value.IsDefined() ? value : Formats.PixelFormat.BMP_Rgb24;

                this.lengthPixel = this.pixelFormat == Formats.PixelFormat.BMP_Rgb24 ? 3 : 4;

                this.Stride = this.Width * this.lengthPixel + GetPadding();
            }
        }

        /// <summary>
        /// Renvoie la largeur en pixels du <see cref="MyImage"/>
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Renvoie la hauteur en pixels du <see cref="MyImage"/>
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Renvoie le nombre d'octets qui composent une ligne de donnée du <see cref="MyImage"/> (padding compris)
        /// </summary>
        public int Stride { get; private set; }

        /// <summary>
        /// Indique la validité du <see cref="MyImage"/>.
        /// </summary>
        public bool Validité => this.data != null && this.Height >= BITMAPCONST.MIN_HEIGHT && this.Width >= BITMAPCONST.MIN_WIDTH;


        /// <summary>
        /// Obtient ou définit le <see cref="Pixel"/> à partir des coordonnées spécifiées
        /// </summary>
        /// <param name="y">Ligne</param>
        /// <param name="x">Colonne</param>
        public unsafe Pixel this[int y, int x]
        {
            get
            {
                try //serait mieux de checker direct si on dépasse le tab, car l'exception n'est forcément pas levée au bon endroit.
                {
                    if (y >= this.Height || x >= this.Width)
                        throw new IndexOutOfRangeException();

                    fixed (byte* buff = this.data)
                    {
                        return this.lengthPixel == 4 ? Pixel.FromArgb(*(int*)(buff + GetPosition(y, x))) :
                            Pixel.FromArgb(*(int*)(buff + GetPosition(y, x)) | -16777216); // & ~(255 << 24) , | 255 << 24
                    }
                }
                catch (Exception e)
                {
                    throw new IndexOutOfRangeException($"Erreur interne : {e.Message} \nmaxWidth = { this.Width } , maxHeight = { this.Height }\n" +
                      $"  ligne : {y} ,  colonne : {x}", e);
                }
            }

            set
            {
                try 
                {
                    if (y >= this.Height || x >= this.Width)
                        throw new IndexOutOfRangeException();

                    fixed (byte* buff = this.data)
                    {
                        byte* pixel = buff + GetPosition(y, x);

                        if (this.lengthPixel == 4)
                        {
                            *(int*)pixel = value.ToArgb();
                        }
                        else
                        {
                            *pixel = value.B;
                            *++pixel = value.G;
                            *++pixel = value.R;
                        }

                    }
                }
                catch (Exception e)
                {
                    throw new IndexOutOfRangeException($"Erreur interne : {e.Message} \nmaxWidth = { this.Width } , maxHeight = { this.Height }\n" +
                      $"  ligne : {y} ,  colonne : {x}", e);
                }
            }
        }

        /// <summary>
        /// Renvoie la position de la première composante d'un pixel dans le tableau de donnée en fonction de ses coordonnées dans l'image.
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private int GetPosition(int y, int x) => y * this.Stride + x * this.lengthPixel;


        //Constructeurs

        /// <summary>
        /// Constructeur vide, l'image ne sera pas valide.
        /// </summary>
        private MyImage()
        {
        }

        /// <summary>
        /// Crée une nouvelle instance de la classe <see cref="MyImage"/> à partir du nom de fichier indiqué.
        /// </summary>
        /// <param name="filename">Chemin</param>
        /// <exception cref="ArgumentException">Fichier null.</exception>
        /// <exception cref="FileNotFoundException">Le fichier n'existe pas ou n'a pas été trouvé.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Levée quand l'image est trop petite (0 généralement).</exception>
        /// <exception cref="BadImageFormatException">Levée quand le format d'image n'est pas valide ou n'est pas supporté.</exception>
        public MyImage(string filename)
        {
            FileInfo info;
            try //FileInfo va gérer toutes les exceptions pour nous.
            {
                info = new FileInfo(Path.GetFullPath(filename));

                if (!info.Exists)
                {
                    throw new FileNotFoundException("Echec de chargement : le fichier indiqué n'existe pas... Choisissez un autre fichier");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erreur : nom de fichier invalide.", e);
            }


            Formats.ImageFormat ext = Formats.GetExtension(info.FullName, true);

            if (!ext.IsDefined()) //Priorité pour l'enum Extension, si ce n'est pas supporté alors on arrete tout meme si le filename possède une extension correcte.
            {
                throw new FileLoadException("Echec de chargement : Extension de fichier non supportée. Choisissez un autre fichier\n\r\n\rSeuls les fichiers .bmp, .csv (au format bitmap), .jpg et .png peuvent être chargés.");
            }

            bool isHidden = (info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden; //Fichiers masqués, enum.HasFlag(enum)
            if (isHidden)
            {
                info.Attributes &= ~FileAttributes.Hidden;
            }

            InitializeMyImageFromFile(this, info.FullName, ext);

            if (isHidden) //On masque le fichier si il était masqué au départ
            {
                info.Attributes |= FileAttributes.Hidden;
            }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="MyImage"/> à partir de nouvelles dimensions de hauteur et de largeur.
        /// <para/> L'image est automatiquement initialisée en noire (+ transparente si 32 bpp)
        /// </summary>
        /// <param name="height">Hauteur</param>
        /// <param name="width">Largeur</param>
        /// <param name="format">Format</param>
        /// <exception cref="ArgumentOutOfRangeException">Levée quand l'image est trop petite (0 généralement).</exception>
        public MyImage(int height, int width, Formats.PixelFormat format = Formats.PixelFormat.BMP_Rgb24)
        {
            this.PixelFormat = format;

            ChangeSize(height, width);
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="MyImage"/> à partir de nouvelles dimensions de hauteur et de largeur. 
        /// <para/>L'image est remplie par la couleur spécifiée
        /// </summary>
        /// <param name="height">Hauteur</param>
        /// <param name="width">Largeur</param>
        /// <param name="couleurRemplissage">Pixel de remplissage de l'image</param>
        /// <param name="format">Format</param>
        /// <exception cref="ArgumentOutOfRangeException">Levée quand l'image est trop petite (0 généralement).</exception>
        public MyImage(int height, int width, Pixel couleurRemplissage, Formats.PixelFormat format = Formats.PixelFormat.BMP_Rgb24)
            : this(height, width, format)
        {
            new MyGraphics(this).Remplissage(couleurRemplissage);
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="MyImage"/> à partir d'une copie d'un <see cref="MyImage"/> à redimensionner. 
        /// </summary>
        /// <param name="image">Image à redimensionner</param>
        /// <param name="newHeight">Hauteur de la nouvelle image.</param>
        /// <param name="newWidth">Largeur de la nouvelle image.</param>
        /// <exception cref="ArgumentOutOfRangeException">Levée quand l'image est trop petite (0 généralement).</exception>
        public MyImage(MyImage image, int newHeight, int newWidth)
        {
            if (image == null || !image.Validité || newHeight < BITMAPCONST.MIN_HEIGHT || newWidth < BITMAPCONST.MIN_WIDTH)
                throw new ArgumentOutOfRangeException("Impossible de redimensionner l'image, paramètres de redimensionnement non valides");
            
            if (newHeight == this.Height && newWidth == this.Width) //Meme dimensions, on clone simplement
            {
                InitializeMyImageFromDataArray(image.ToBGRArray(), image.Width, image.PixelFormat, false);
            }
            else //On redimensionne un clone de l'image et on initialise l'image avec
            {
                MyImage clone = image.Clone();
                new MyGraphics(clone).Redimensionnement(newHeight, newWidth);

                InitializeMyImageFromDataArray(clone.ToBGRArray(), clone.Width, clone.PixelFormat, true);
            }

        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="MyImage"/> à partir d'un <see cref="Bitmap"/>
        /// </summary>
        /// <param name="bmp">bitmap</param>
        /// <exception cref="FormatException">Levée quand le format de pixel n'est pas valide.</exception>
        /// <exception cref="ArgumentNullException">Levée quand le bitmap entré en paramètre est null</exception>
        /// <exception cref="ArgumentOutOfRangeException">Levée quand l'image est trop petite (0 généralement).</exception>
        public static MyImage FromBitmap(Bitmap bmp)
        {
            if (bmp == null)
                throw new Exception("Bitmap non valide... Null entré en paramètre");

            MyImage image = new MyImage();

            image.InitializeMyImageFromBitmap(bmp);

            return image;
        }


        //Conversions

        /// <summary>
        /// Renvoie le <see cref="MyImage"/> sous forme d'un <see cref="Bitmap"/>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidCastException">Levée quand l'image n'est pas valide.</exception>
        public Bitmap ToBitmap()
        {
            if (!this.Validité)
                throw new InvalidCastException("L'image n'est pas valide, impossible de la convertir en bitmap");
            
            Bitmap bmp = new Bitmap(this.Width, this.Height, this.PixelFormat.ToSystemPixelFormat());

            //on lock la mémoire du bmp pour pouvoir y copier le tab de byte
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.WriteOnly, bmp.PixelFormat);

            //on copie les données dans la première ligne de mémoire du bitmap
            Marshal.Copy(this.data, 0, bmpData.Scan0, this.data.Length);

            bmp.UnlockBits(bmpData);

            return bmp;
        }

        /// <summary>
        /// Réalise une copie profonde de ce <see cref="MyImage"/> et renvoie le nouvel objet <see cref="MyImage"/> résultant.
        /// </summary>
        /// <exception cref="InvalidCastException">Levée quand l'image n'est pas valide.</exception>
        public MyImage Clone()
        {
            if (!this.Validité)
            {
                throw new InvalidCastException("L'image n'est pas valide, impossible de la cloner");
            }

            MyImage image = new MyImage
            {
                Width = this.Width,
                Height = this.Height,
                PixelFormat = this.pixelFormat
            };

            image.InitializeDataFromDataArray(this.data);

            return image;
        }

        /// <summary>
        /// Renvoie le tableau de données bgr ou bgra au <see cref="Formats.PixelFormat"/> .bmp de cette instance.
        /// </summary>
        /// <returns></returns>
        public byte[] ToBGRArray()
        {
            return this.data;
        }


        //Opérateurs 

        /// <summary>
        /// Renvoie <see langword="true"></see> si tous les Pixels des 2 images sont les mêmes ou si les 2 images sont <see langword="null"/>
        /// </summary>
        /// <param name="a">image 1</param>
        /// <param name="b">image 2</param>
        /// <returns></returns>
        public static bool operator ==(MyImage a, MyImage b)
        {
            if (((a as object) == null && (b as object) == null) || (a as object) == (b as object)) //meme ref ou null.
                return true;
            if (a?.Height != b?.Height || a?.Width != b?.Width) // Taille différente
                return false;
            if (a.pixelFormat != b.pixelFormat) // Format différent
                return false;

            return a.data.SequenceEqual(b.data, EqualityComparer<byte>.Default);
        }

        /// <summary>
        /// Renvoie <see langword="false"></see> si tous les Pixels des 2 images sont les mêmes ou si les 2 images sont <see langword="null"/>
        /// </summary>
        /// <param name="a">image 1</param>
        /// <param name="b">image 2</param>
        /// <returns></returns>
        public static bool operator !=(MyImage a, MyImage b) => !(a == b);


        //Méthodes publiques

        /// <summary>
        /// Permet de changer la taille d'un <see cref="MyImage"/>. Réinitialise le tableau de <see cref="Pixel"/>
        /// </summary>
        /// <param name="newHeight"></param>
        /// <param name="newWidth"></param>
        /// <exception cref="ArgumentOutOfRangeException">Tailles non valides.</exception>
        public void ChangeSize(int newHeight, int newWidth)
        {
            if (newHeight < BITMAPCONST.MIN_HEIGHT || newWidth < BITMAPCONST.MIN_WIDTH)
                throw new ArgumentOutOfRangeException($"Dimensions trop petites... Doivent être supérieures ou égales à {BITMAPCONST.MIN_WIDTH}x{BITMAPCONST.MIN_HEIGHT}");
            
            //calcul des nouvelles dimensions
            this.Height = newHeight;
            this.Width = newWidth;
            this.PixelFormat = this.pixelFormat; //calcul du stride, ce serait mieux de le faire dans Width

            this.data = InitializeData();
        }

        /// <summary>
        /// Change le tableau de données de ce <see cref="MyImage"/>. Doit forcément être conforme au format de données actuel.
        /// </summary>
        /// <param name="données"></param>
        /// <exception cref="ArgumentNullException">Tableau null ou = 0</exception>
        /// <exception cref="FormatException">Tableau non valide (trop petit probablement).</exception>
        public void ChangeData(byte[] données)
        {
            try
            {
                InitializeMyImageFromDataArray(données, this.Width, this.pixelFormat, true);
            }
            catch (Exception e)
            {
                throw new Exception("Tableau non valide...", e);
            }
        }

        /// <summary>
        /// Convertit l'image au format spécifié, des données peuvent alors être supprimées : 32 -> 24  = perte alpha, 24 -> 32 = alpha=>255
        /// </summary>
        /// <param name="destFormat">Format vers lequel convertir le <see cref="MyImage"/></param>
        /// <exception cref="ArgumentException">Format de destination invalide</exception>
        public unsafe void ConvertPixelFormat(Formats.PixelFormat destFormat)
        {
            if (!destFormat.IsDefined()) //invalide
                throw new ArgumentException("Format de destination invalide");

            if (destFormat == this.PixelFormat)//Même format qu'actuellement
                return;

            this.PixelFormat = destFormat;

            if (!this.Validité)
                return;

            byte[] destData = InitializeData();

            int padding = Formats.GetPaddingPixel(this.Width, 3);

            int scanLine = 0;

            //On alterne entre pointeurs int et byte pour de meilleures performances au lieu de copier entre byte*
            //Comme la longueur des tabs est toujours un multiple de 4 il n'y a aucun risque de violation de mémoire.
            fixed (byte* bufferDest = destData)
            fixed (byte* bufferSrc = this.data)
            {
                if (destFormat == Formats.PixelFormat.BMP_Rgb24)
                {
                    byte* destBuffer = bufferDest;
                    int* srcBufferInt = (int*)bufferSrc;

                    while (scanLine++ < this.Height)
                    {
                        for (int index = 0; index < this.Width; ++index)
                        {
                            *(int*)destBuffer = *srcBufferInt & ~(0xFF << 24); //dernier byte à 0

                            destBuffer += 3;
                            srcBufferInt += 1; //format src = 32 bpp, donc on incr de 1 <=> byte* += 4

                            //*destBuffer = 0;
                        }

                        destBuffer += padding;
                    }
                }
                else if (destFormat == Formats.PixelFormat.BMP_Argb32)
                {
                    byte* srcBuffer = bufferSrc;
                    int* destBufferInt = (int*)bufferDest;

                    while (scanLine++ < this.Height)
                    {
                        for (int index = 0; index < this.Width; ++index)
                        {
                            *destBufferInt = *(int*)srcBuffer | (0xFF << Pixel.ArgbAlphaShift); // | alpha

                            destBufferInt += 1;
                            srcBuffer += 3;
                        }

                        srcBuffer += padding;
                    }
                }
            }

            this.data = destData;
        }

        /// <summary>
        /// Renvoie le nombre d'octets qui composent le padding de fin de ligne de cette image.
        /// </summary>
        /// <returns></returns>
        public int GetPadding() => Formats.GetPaddingPixel(this.Width, this.pixelFormat.GetPixelLength());


        //Init

        #region Initialisation du tableau de données

        /// <summary>
        /// Renvoie un tableau de données au format bitmap à partir des paramètres pré-définis de ce <see cref="MyImage"/>.
        /// </summary>
        private byte[] InitializeData()
        {
            long length = (long)this.Height * this.Stride;

            try
            {
                return new byte[length];
            }
            catch (Exception e)
            {
                throw new Exception($"L'image ne peut pas être initialisée car trop grande (taille : {length}) ...", e);
            }
        }

        /// <summary>
        /// Initialise et remplit la matrice de données à partir d'un tableau de <see cref="byte"/> au format bitmap.
        /// </summary>
        /// <param name="données">Tableau contenant les infos liées aux pixels de l'image au format bitmap</param>
        private void InitializeDataFromDataArray(byte[] données)
        {
            this.data = InitializeData();

            if (données != null)
            {
                Array.Copy(données, 0, this.data, 0, this.data.Length);
            }
        }

        /// <summary>
        /// Initialise le MyImage à partir d'un Bitmap système.
        /// </summary>
        /// <param name="bmp"></param>
        private void InitializeMyImageFromBitmap(Bitmap bmp)
        {
            if (bmp.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb &&
                bmp.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb &&
                bmp.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppRgb)
            {
                throw new Exception("Impossible de charger l'image. Le format n'est pas pris en charge.");
            }
            this.Height = bmp.Height;
            this.Width = bmp.Width;

            //L'objectif est de récupérer le tableau de byte en mémoire non managée du bitmap

            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), //Lock de la mémoire non managée
                ImageLockMode.ReadOnly, bmp.PixelFormat);

            int length = bmpData.Stride * bmpData.Height;
            this.data = new byte[length];

            // copie des valeurs rgb ou argb dans le tab
            Marshal.Copy(bmpData.Scan0, this.data, 0, length);

            this.PixelFormat = bmpData.Stride / bmp.Width == 3 ? Formats.PixelFormat.BMP_Rgb24 : Formats.PixelFormat.BMP_Argb32;

            bmp.UnlockBits(bmpData); //Déverouille ce bitmap
        }

        /// <summary>
        /// Change le tableau de données de ce <see cref="MyImage"/>. Vérifie que le tableau est bien conforme aux paramètres donnés.
        /// </summary>
        /// <param name="données"></param>
        /// <param name="imageWidth"></param>
        /// <param name="pf"></param>
        /// <param name="copyDirectly">le tab de donnée de cette instance devient celui entré en paramètre, sinon il est copié.</param>
        /// <exception cref="ArgumentNullException">Tableau null ou = 0</exception>
        /// <exception cref="FormatException">Tableau non valide (trop petit probablement).</exception>
        private void InitializeMyImageFromDataArray(byte[] données, int imageWidth, Formats.PixelFormat pf, bool copyDirectly = false)
        {
            if (données == null || données.Length == 0)
            {
                throw new ArgumentNullException("Tableau de donnée non valide......");
            }

            this.Width = imageWidth;
            this.PixelFormat = pf;

            this.Height = données.Length / (imageWidth * this.lengthPixel + GetPadding());

            if (données.Length < this.Stride * this.Height || this.Height < BITMAPCONST.MIN_HEIGHT || this.Width < BITMAPCONST.MIN_WIDTH)
            {
                throw new FormatException("Tableau de donnée entré non valide (ou trop petit)");
            }

            if (copyDirectly)
            {
                this.data = données;
            }
            else
            {
                InitializeDataFromDataArray(données);
            }
        }

        #endregion


        //Création et sauvegarde d'un fichier

        #region Chargement

        /// <summary>
        /// Crée le <see cref="MyImage"/> à partir du nom du fichier de l'image à récupérer.
        /// </summary>
        /// <param name="image">Image à initialiser</param>
        /// <param name="filename">Nom du fichier</param>
        /// <param name="ext"></param>
        /// <exception cref="ArgumentOutOfRangeException">Levée quand l'image est trop petite (0 généralement).</exception>
        /// <exception cref="BadImageFormatException">Levée quand le format d'image n'est pas valide ou n'est pas supporté.</exception>
        private static void InitializeMyImageFromFile(MyImage image, string filename, Formats.ImageFormat ext)
        {
            bool loadSuccess = false;

            if (ext == Formats.ImageFormat.BMP || ext == Formats.ImageFormat.CSV)
            {
                try
                {
                    InitializeMyImageFromBitmapArray(image, CreateBitmapArrayFromFile(filename, ext));

                    loadSuccess = true;
                }
                catch (Exception) { }
            }

            //Si on a pas réussi à charger l'image en .bmp (peut etre du au format de compression, aux mask non valides...) on fait lire
            //l'image par la classe Bitmap et on charge le MyImage depuis cette instance Bitmap si le format est pris en charge.
            if (!loadSuccess && (ext != Formats.ImageFormat.CSV)) //jpg , png, bmp (si fail)
            {
                using (Bitmap bmp = new Bitmap(filename))
                {
                    if (bmp.Height < BITMAPCONST.MIN_HEIGHT || bmp.Width < BITMAPCONST.MIN_WIDTH)
                    {
                        throw new ArgumentOutOfRangeException("Image non supportée, trop petite");
                    }

                    image.InitializeMyImageFromBitmap(bmp);
                }
            }
        }

        /// <summary>
        /// Renvoie le fichier d'image sous forme d'un tableau de <see cref="byte"/>.
        /// </summary>
        /// <param name="filename">Chemin d'accès au fichier</param>
        /// <param name="imF"></param>
        /// <returns></returns>
        private static byte[] CreateBitmapArrayFromFile(string filename, Formats.ImageFormat imF)
        {
            byte[] returnBytes;

            if (imF == Formats.ImageFormat.CSV)
            {
                StreamReader sr = new StreamReader(filename);
                string chars = sr.ReadToEnd();
                sr.Dispose();  //On libère le stream, permet de ne pas bloquer l'image

                //returnBytes = chars.Split(';').Select(byte.Parse).ToArray();

                List<byte> onlyDigit = new List<byte>(); //Lent mais on peut lire n'importe quel fichier sans risque d'erreur

                string num = "";

                foreach (char c in chars)
                {
                    if (!char.IsDigit(c) && num != "")
                    {
                        onlyDigit.Add(byte.Parse(num));
                        num = "";
                    }
                    else if (char.IsDigit(c))
                    {
                        num += c;
                    }
                }

                returnBytes = onlyDigit.ToArray();
            }
            else
            {
                returnBytes = File.ReadAllBytes(filename);
            }

            return returnBytes;
        }


        /// <summary>
        /// Initialise le <see cref="MyImage"/> à partir d'un tableau au format bitmap 24 ou 32bpp
        /// </summary>
        /// <param name="image">Image à initialiser</param>
        /// <param name="dataFromFile"></param>
        /// <exception cref="ArgumentException">Tab non valide</exception>
        private static void InitializeMyImageFromBitmapArray(MyImage image, byte[] dataFromFile)
        {
            int sizeHeader = Marshal.SizeOf(typeof(BITMAPHEADER));
            if (dataFromFile.Length < sizeHeader)
            {
                throw new ArgumentException("Image invalide");
            }

            BITMAPHEADER header = (BITMAPHEADER)Formats.DeserializeObject(dataFromFile, typeof(BITMAPHEADER));
            BITMAPHEADER_32 header32 = null;

            if (header.le_BitCount == (int)Formats.PixelFormat.BMP_Argb32)
            {
                if (dataFromFile.Length < sizeHeader + Marshal.SizeOf(typeof(BITMAPHEADER_32)))
                {
                    throw new ArgumentException("Image non valide");
                }

                //Note: En fait la valeur qui nous intéresse est header.le_DataOffset, or si cette valeur est
                //différente de ce qui est attendu on ne pourra pas initialiser BITMAPHEADER_32BPP qui a une 
                //taille pré-définie. Il faudrait donc pouvoir modifier dynamiquement la taille de la struct (comment ?)
                header32 = (BITMAPHEADER_32)Formats.DeserializeObject(dataFromFile, typeof(BITMAPHEADER_32), sizeHeader);
            }

            if (!ConformitéBitmapHeaders(header, header32, out bool isCompressed)) //check si conforme en fct du bpp, seules les données 24bpp et 32 bpp sont acceptées
            {
                throw new ArgumentException("Image non valide, erreur, header non conforme.");
            }

            image.Height = header.le_Height;
            image.Width = header.le_Width;

            if (image.Height < BITMAPCONST.MIN_HEIGHT || image.Width < BITMAPCONST.MIN_WIDTH)
            {
                throw new ArgumentException("Image non supportée, trop petite");
            }

            image.PixelFormat = (Formats.PixelFormat)header.le_BitCount; //format pivot, 24 bpp ou 32 bpp (pour l'instant)


            byte[] pixelsDataFromFile = Formats.ExtractArray(dataFromFile, header.le_DataOffset, dataFromFile.Length);

            if (isCompressed) //décompression rle 24bpp, on extrait les données de pixels qu'on décompresse ensuite
                pixelsDataFromFile = Formats.RLE24Decompression(pixelsDataFromFile);
            
            byte[] realPixelsData;
            if (pixelsDataFromFile.Length != header.le_SizeImage) //Check si la taille des données est conforme ou non.
            {
                realPixelsData = new byte[header.le_SizeImage];

                Array.Copy(pixelsDataFromFile, 0, realPixelsData, 0, Math.Min(pixelsDataFromFile.Length, header.le_SizeImage));
            }
            else
            {
                realPixelsData = pixelsDataFromFile;
            }

            Formats.ReverseLineArray(realPixelsData, image.Height); //on inverse les lignes de données en hauteur

            if (image.PixelFormat == Formats.PixelFormat.BMP_Argb32)
            {
                //On prend en compte l'ordre des données de pixels rgba donné dans le headerInfo, on intervertit
                //donc les composants pour qu'un pixel soit de la forme int = argb (format little endian)
                //(on ne sauvegarde seulement que dans cet ordre d'ailleurs)
                //2x plus rapide pour inverser l'ordre (lorsque nécessaire) que gdi+, sinon 2/3x plus lent.
                Formats.InvertOrderRgbaArray(realPixelsData, GetOrderRgba(header32), new byte[4] { (Pixel.ArgbRedShift / 8) + 1,
                        (Pixel.ArgbGreenShift / 8) + 1, (Pixel.ArgbBlueShift / 8) + 1, (Pixel.ArgbAlphaShift / 8) + 1 }, 0);
            }
            else
            {
                Formats.InvertOrderRgbaArray(realPixelsData, new byte[3] { 3, 2, 1 }, new byte[3] { (Pixel.ArgbRedShift / 8) + 1,
                        (Pixel.ArgbGreenShift / 8) + 1, (Pixel.ArgbBlueShift / 8) + 1 }, image.Width); //On fait correspondre l'ordre ac les const de la classe Pixel
            }

            image.data = realPixelsData;
        }


        /// <summary>
        /// Regarde la conformité des header par rapport aux constantes de <see cref="BITMAPCONST"/>. Header32 bpp peut etre null.
        /// <see langword="OUT"/> un bool indiquant si le fichier est compressé ou non..
        /// </summary>
        private static bool ConformitéBitmapHeaders(BITMAPHEADER header, BITMAPHEADER_32 header32, out bool compressed)
        {
            bool planes = header.le_Planes == BITMAPCONST.PLANES;

            compressed = false;

            if (header.le_BitCount == 24)
            {
                compressed = header.le_Compression == BITMAPCONST.COMPRESSION_VALUE24_COMPRESSED;

                return planes && (header.le_Compression == BITMAPCONST.COMPRESSION_VALUE24_UNCOMPRESSED || compressed) &&
                    header.le_DataOffset == BITMAPCONST.DATA_OFFSET24;
            }
            else if (header.le_BitCount == 32)
            {
                compressed = header.le_Compression != BITMAPCONST.COMPRESSION_VALUE32_UNCOMPRESSED;

                return planes && !compressed && header.le_DataOffset == BITMAPCONST.DATA_OFFSET32 &&
                    (header32 == null || header32.le_LCS_WINDOWS_COLOR_SPACE == BITMAPCONST.le_WIN);
            }

            return false;
        }

        /// <summary>
        /// Renvoie l'ordre pour chaque composant dans chaque byte (1, 2, 3, 4 => rgba format little endian). 32 bpp uniquement.
        /// Habituellement va renvoyer { 3, 2, 1, 4 }.
        /// </summary>
        /// <param name="header32"></param>
        /// <returns></returns>
        private static byte[] GetOrderRgba(BITMAPHEADER_32 header32)
        {
            byte[] ordre = new byte[4]; //rgba

            int[] headerVal = new int[] { header32.be_RedMask, header32.be_GreenMask, header32.be_BlueMask, header32.be_AlphaMask };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ((byte)(headerVal[i] >> 8 * j) == byte.MaxValue)
                        ordre[i] = (byte)(j + 1);
                }
                if (ordre[i] == 0)//check si tout s'est bien passé, on renvoie null si erreur (= fichier pas forcément corrompu mais
                    return null;  //il faudrait alors ignorer un composant et MyImage n'est pas conçu pour pouvoir en ignorer un)
            }

            return ordre;
        }

        #endregion


        #region Sauvegarde

        /// <summary>
        /// Sauvegarde ce <see cref="MyImage"/> dans le fichier sélectionné. Overwrite si le fichier existe déjà.
        /// <para/>On sauvegarde en prenant en compte le format de pixels <see cref="PixelFormat"/> spécifié.
        /// </summary>
        /// <param name="filename">Chemin de sauvegarde</param>
        /// <exception cref="InvalidCastException">Le fichier n'existe pas ou n'a pas été trouvé.</exception>
        /// <exception cref="ArgumentException">Levée quand le nom du fichier est null.</exception>
        /// <exception cref="DirectoryNotFoundException">Levée quand le dossier de sauvegarde est introuvable.</exception>
        /// <exception cref="FileLoadException">Levée quand le format d'image n'est pas valide ou n'est pas supporté.</exception>
        public void Save(string filename)
        {
            Save(filename, new MyImageSaveFormat()
            {
                ImageFormat = Formats.GetExtension(filename, false),
                PixelFormat = this.pixelFormat,
                Compression_bmp_24bpp = false
            });
        }

        /// <summary>
        /// Sauvegarde ce <see cref="MyImage"/> dans le fichier sélectionné au format indiqué. Overwrite si le fichier existe déjà.
        /// <para/>On sauvegarde en prenant en compte le format de pixels <see cref="PixelFormat"/> spécifié.
        /// </summary>
        /// <param name="filename">Chemin de sauvegarde</param>
        /// <param name="format">Format de sauvegarde</param>
        /// <exception cref="InvalidCastException">Le fichier n'existe pas ou n'a pas été trouvé.</exception>
        /// <exception cref="ArgumentException">Levée quand le nom du fichier est null.</exception>
        /// <exception cref="DirectoryNotFoundException">Levée quand le dossier de sauvegarde est introuvable.</exception>
        /// <exception cref="FileLoadException">Levée quand le format d'image n'est pas valide ou n'est pas supporté.</exception>
        public void Save(string filename, MyImageSaveFormat format)
        {
            if (!this.Validité)
                throw new InvalidCastException("Impossible de sauvegarder l'image, elle n'est pas valide !");
            
            if (filename == null || !format.ImageFormat.IsDefined())
                throw new ArgumentException($"Impossible de sauvegarder, le format n'est pas pris en charge ou le chemin " +
                    $"de sauvegarde n'est pas précisé");
            
            filename = Formats.GetFilenameWithCorrectExtension(Path.GetFullPath(filename), format.ImageFormat);

            if (!Directory.Exists(Path.GetDirectoryName(filename)))
                throw new DirectoryNotFoundException($"Impossible de sauvegarder, le dossier de sauvegarde n'existe pas");
            

            if (format.ImageFormat == Formats.ImageFormat.JPEG || format.ImageFormat == Formats.ImageFormat.PNG)
            {
                if (format.ImageFormat == Formats.ImageFormat.PNG && format.PixelFormat != this.PixelFormat && format.PixelFormat.IsDefined())
                {
                    MyImage image = Clone();
                    image.ConvertPixelFormat(format.PixelFormat);

                    image.ToBitmap().Save(filename, format.ImageFormat.ToSystemImageFormat());
                }
                else
                {
                    ToBitmap().Save(filename, format.ImageFormat.ToSystemImageFormat());
                }
            }
            else if (format.ImageFormat == Formats.ImageFormat.BMP || format.ImageFormat == Formats.ImageFormat.CSV)
            {
                byte[] buff = CreateBitmapArray(this, format);

                if (format.ImageFormat == Formats.ImageFormat.BMP)
                {
                    File.WriteAllBytes(filename, buff);
                }
                else //Pour le format csv on sépare simplement chaque octet par un point-virgule ';'.
                {
                    File.WriteAllText(filename, string.Join(";", buff) + ";");
                }
            }
            else
                throw new FileLoadException("Impossible de sauvegarder : Le format de sauvegarde n'est pas pris en charge.");
        }


        /// <summary>
        /// Convertit le tableau de données de pixels en un tableau de byte au format bitmap .bmp
        /// </summary>
        /// <returns></returns>
        private static byte[] CreateBitmapArray(MyImage image, MyImageSaveFormat format)
        {
            MyImage im = image.Clone();
            im.ConvertPixelFormat(format.PixelFormat);

            byte[] pixelsData = im.ToBGRArray();
            int realDataL = pixelsData.Length;

            Formats.ReverseLineArray(pixelsData, image.Height);

            if (format.IsCompressionPossible)
                pixelsData = Formats.RLE24Compression(pixelsData);
            
            int headerLength = format.PixelFormat == Formats.PixelFormat.BMP_Rgb24 ? BITMAPCONST.DATA_OFFSET24 : BITMAPCONST.DATA_OFFSET32;

            byte[] buffer = new byte[pixelsData.Length + headerLength];

            SerializeHeaderInfo(buffer, format, realDataL, pixelsData.Length, image.Width, image.Height);

            //if (ConformitéBitmapHeaders((BITMAPHEADER)Formats.DeserializeObject(buffer, typeof(BITMAPHEADER)), null, out _)) //Inutile
            Array.Copy(pixelsData, 0, buffer, headerLength, Math.Min(buffer.Length - headerLength, pixelsData.Length));

            return buffer;
        }

        /// <summary>
        /// Copie dans ce tableau le header info associé au format de sauvegarde et aux différentes variables indiquées.
        /// </summary>
        private static void SerializeHeaderInfo(byte[] data, MyImageSaveFormat format, int realDataL, int formatDataL, int width, int height)
        {
            bool _32bpp = format.PixelFormat == Formats.PixelFormat.BMP_Argb32;

            int headerLength = _32bpp ? BITMAPCONST.DATA_OFFSET32 : BITMAPCONST.DATA_OFFSET24;

            int compression = _32bpp ? BITMAPCONST.COMPRESSION_VALUE32_UNCOMPRESSED : format.Compression_bmp_24bpp ?
                BITMAPCONST.COMPRESSION_VALUE24_COMPRESSED : BITMAPCONST.COMPRESSION_VALUE24_UNCOMPRESSED;

            Formats.SerializeObjectFromBuffer(new BITMAPHEADER
            {
                le_FileSize = headerLength + formatDataL,
                le_DataOffset = headerLength,
                le_Size = headerLength - BITMAPCONST.HEADER_INFO_OFFSET,
                le_Width = width,
                le_Height = height,
                le_Planes = BITMAPCONST.PLANES,
                le_BitCount = (short)format.PixelFormat,
                le_Compression = compression,
                le_SizeImage = realDataL,
                le_XPelsPerMeter = BITMAPCONST.PIXELSPERMETER,
                le_YPelsPerMeter = BITMAPCONST.PIXELSPERMETER,
                le_Palette = BITMAPCONST.PALETTE_UNUSED,
                le_ClrImportant = BITMAPCONST.COLOR_IMPORTANT_ALL,
            },
            data, 0);

            if (_32bpp)
            {
                Formats.SerializeObjectFromBuffer(new BITMAPHEADER_32
                {
                    be_RedMask = 0xFF << Pixel.ArgbRedShift,
                    be_GreenMask = 0xFF << Pixel.ArgbGreenShift,
                    be_BlueMask = 0xFF << Pixel.ArgbBlueShift,
                    be_AlphaMask = 0xFF << Pixel.ArgbAlphaShift,
                    le_LCS_WINDOWS_COLOR_SPACE = BITMAPCONST.le_WIN,
                },
                data, Marshal.SizeOf(typeof(BITMAPHEADER)));
            }
        }

        #endregion

    }
}
