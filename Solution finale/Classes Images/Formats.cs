using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Photoshop3000
{

    /// <summary>
    /// Formats compatibles d'écriture/lecture de <see cref="MyImage"/>. 
    /// <para/>Fournit des méthodes static valables pour toute la solution.
    /// </summary>
    public static class Formats
    {
        //serialization

        #region marshalling/unmarshalling

        /// <summary>
        /// Convertit un objet en tableau d'octet non managé.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IntPtr SerializeObjectToPtr(object obj)
        {
            if (obj == null || !obj.GetType().IsSerializable)
                return IntPtr.Zero;

            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(obj.GetType()));

            SerializeObjectFromBuffer(obj, ptr);

            return ptr;
        }

        /// <summary>
        /// Convertit un objet en tableau de byte et copie les données dans le pointeur donné.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ptr"></param>
        /// <returns></returns>
        public static void SerializeObjectFromBuffer(object obj, IntPtr ptr)
        {
            if (obj == null || !obj.GetType().IsSerializable)
                return;

            //Les données sont copiées dans le pointeur
            Marshal.StructureToPtr(obj, ptr, false);
        }

        /// <summary>
        /// Convertit un objet en tableau de byte et copie les données dans le tableau donné.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static unsafe void SerializeObjectFromBuffer(object obj, byte[] buffer, int index)
        {
            if (buffer == null || Marshal.SizeOf(obj.GetType()) > buffer.Length + index)
                return;

            fixed (byte* b = buffer)
                SerializeObjectFromBuffer(obj, (IntPtr)b + index);
        }


        /// <summary>
        /// Convertit un tableau de byte non managé en un object.
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object DeserializeObject(IntPtr ptr, Type type)
        {
            if (ptr == IntPtr.Zero)
                return null;

            //copie les données dans le nouvel objet. Note : ce serait pas mal de pouvoir controler si la marshalisation
            //copie ou utilise la ref des données pour créer l'objet. Ex:si on modifie l'objet ça modifie le ptr avec direct.
            return Marshal.PtrToStructure(ptr, type);
        }

        /// <summary>
        /// Convertit un tableau de byte en un object.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="type"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static unsafe object DeserializeObject(byte[] buffer, Type type, int index = 0)
        {
            if (buffer == null)
                return null;

            fixed (byte* buff = buffer)
                return DeserializeObject((IntPtr)buff + index, type);
        }

        #endregion


        //Maths

        /// <summary>
        /// Coef binomial
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static double KParmiN(int n, int k)
        {
            return k == 0 ? 1 : n * KParmiN(n - 1, k - 1) / k;
        }

        /// <summary>
        /// Calcule la probabilité pour cette loi et ces paramètres
        /// </summary>
        /// <param name="n">Nombre d'évènements possible</param>
        /// <param name="k">Le nombre d'évènement pour avoir un succès</param>
        /// <param name="p">La proba uniforme pour chaque évènement</param>
        /// <returns></returns>
        public static double LoiBinomiale(int n, int k, double p)
        {
            return KParmiN(n, k) * Math.Pow(p, k) * Math.Pow(1 - p, n - k);
        }


        //Compression/Decompression

        /// <summary>
        /// Décompresse un tableau d'octet selon la méthode rle 24bits et renvoie le nouveau tableau
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static byte[] RLE24Decompression(byte[] array)
        {
            //A faire
            return array;
        }

        /// <summary>
        /// Compresse un tableau d'octet selon la méthode rle 24bits et renvoie le nouveau tableau
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static byte[] RLE24Compression(byte[] array)
        {
            //A faire
            return array;
        }


        //tab

        /// <summary>
        /// Inverse les lignes du tableau, la 1ere ligne devient la derniere, etc...
        /// </summary>
        /// <returns></returns>
        public static void ReverseLineArray(byte[] array, int height, int startH = 0, int endH = 0)
        {
            int lengthLine = array.Length / height;

            if (endH == 0)
                endH = height;

            unsafe
            {
                fixed (byte* data = array)
                {
                    int mid = (endH - startH) / 2;

                    uint length = (uint)lengthLine;

                    byte* firstLine = data + startH * lengthLine;
                    byte* lastLine = data + (endH - 1) * lengthLine;

                    for (int i = startH; i < mid; i++)
                    {
                        byte[] dataFirstLine = new byte[lengthLine];

                        Marshal.Copy((IntPtr)firstLine, dataFirstLine, 0, lengthLine); //ligne en haut

                        Formats.SafeMemCpy((IntPtr)firstLine, (IntPtr)lastLine, length); //Copie mémoire bas image vers haut

                        Marshal.Copy(dataFirstLine, 0, (IntPtr)lastLine, lengthLine); //Copie mémoire haut image vers bas

                        firstLine += lengthLine;
                        lastLine -= lengthLine;
                    }
                }
            }
        }

        /// <summary>
        /// Si longueur des tab ordres = 4 : rgba, sinon = 3 : rgb (dans cet ordre tjrs). Echange les composants entre eux selon des ordres définis.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="originalOrderRgba"></param>
        /// <param name="destOrderRgba"></param>
        /// <param name="widthIfRgbOnly"></param>
        public static void InvertOrderRgbaArray(byte[] data, byte[] originalOrderRgba, byte[] destOrderRgba, int widthIfRgbOnly = 0)
        {
            //check si les tabs d'ordre sont bons.

            if (data == null || originalOrderRgba == null || destOrderRgba == null)
            {
                throw new ArgumentNullException("Ordres ou données non valides");
            }

            if (originalOrderRgba.Length != destOrderRgba.Length || !(originalOrderRgba.Length == 4 || (originalOrderRgba.Length == 3 && widthIfRgbOnly != 0)))
            {
                throw new ArgumentException("Ordres non valides");
            }

            //Pas la peine de vérifier s'il y a bien un unique chiffre dans chaque case des tableaux.  Si c'est
            //précisé de manière erronée c'est qu'il y a bien une raison (en effet si 2 chiffres se retrouvent
            //dans le même tab, cela revient à dupliquer une des composantes). Il faut seulement vérifier
            //qu'ils sont compris entre 1 et 4

            int lengthPix = originalOrderRgba.Length;
            bool différents = false;

            for (int i = 0; i < lengthPix; i++)
            {
                if (destOrderRgba[i] < 1 || destOrderRgba[i] > lengthPix || originalOrderRgba[i] < 1 || originalOrderRgba[i] > lengthPix)
                    throw new ArgumentException("Tableau non valide, certaines valeurs dépassent les limites autorisées");

                destOrderRgba[i]--; //-1 car les tabs d'ordre commencent à 1.
                originalOrderRgba[i]--;

                if (destOrderRgba[i] != originalOrderRgba[i])
                    différents = true;
            }

            if (!différents) //les tabs sont les memes, pas la peine de faire le code qui suit même s'il est bien opti.
                return;

            unsafe
            {
                fixed (byte* buffFixed = data)
                {
                    byte* buff = buffFixed;

                    byte origPosR = originalOrderRgba[0]; //Pour la performance on met le byte du tab dans un byte
                    byte origPosG = originalOrderRgba[1];
                    byte origPosB = originalOrderRgba[2];

                    byte destPosR = destOrderRgba[0];
                    byte destPosG = destOrderRgba[1];
                    byte destPosB = destOrderRgba[2];

                    if (lengthPix == 4)
                    {
                        byte origPosA = originalOrderRgba[3];
                        byte destPosA = destOrderRgba[3];

                        for (int i = 0; i < data.Length; i += lengthPix)
                        {
                            byte r = buff[origPosR];
                            byte g = buff[origPosG];
                            byte b = buff[origPosB];
                            byte a = buff[origPosA];

                            buff[destPosR] = r;
                            buff[destPosG] = g;
                            buff[destPosB] = b;
                            buff[destPosA] = a;

                            buff += lengthPix;
                        }
                    }
                    else
                    {
                        int pad = Formats.GetPaddingPixel(widthIfRgbOnly, 3);
                        int stride = widthIfRgbOnly * 3 + pad;
                        int height = data.Length / stride;

                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < widthIfRgbOnly; j++)
                            {
                                byte r = buff[origPosR];
                                byte g = buff[origPosG];
                                byte b = buff[origPosB];

                                buff[destPosR] = r;
                                buff[destPosG] = g;
                                buff[destPosB] = b;

                                buff += lengthPix;
                            }

                            buff += pad;
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Extrait un tableau de <see cref="byte"/> d'un tableau de <see cref="byte"/> entre 2 limites
        /// </summary>
        /// <param name="data">Tableau de <see cref="byte"/></param>
        /// <param name="début">Index de début</param>
        /// <param name="fin">Index de fin (non inclus)</param>
        /// <returns></returns>
        public static byte[] ExtractArray(byte[] data, int début, int fin)
        {
            if (data != null && début <= fin && début >= 0 && fin <= data.Length)
            {
                byte[] newArray = new byte[fin - début];

                Array.Copy(data, début, newArray, 0, fin - début);

                return newArray;

                //C# 8.0 => return data[début..fin]; Pas dispo en .net framework
            }

            throw new IndexOutOfRangeException("Ca va pas, tab probablement trop petit");
        }


        //Conversions endian, inutilisées

        #region Conversions endian

        /// <summary>
        /// Convertit un <see cref="int"/> en un tableau de <see cref="byte"/> au format little endian
        /// </summary>
        /// <param name="value">Valeur à convertir</param>
        /// <returns></returns>
        public static unsafe byte[] ConvertIntToLittleEndian(int value)
        {
            byte[] bytes = new byte[4];

            fixed (byte* b = bytes)
            {
                *(int*)b = value; //Un int est auto au format little endian, en tout cas pour les processeurs intel
            }

            return bytes;
        }

        /// <summary>
        /// Convertit un tableau de <see cref="byte"/> au format little endian en <see cref="int"/>
        /// </summary>
        /// <param name="bytes">Tableau de <see cref="byte"/></param>
        /// <returns></returns>
        public static unsafe int ConvertLittleEndianToInt(byte[] bytes)
        {
            fixed (byte* buffer = bytes)
            {
                return *(int*)buffer; //Un int est auto au format little endian
            }
        }


        /// <summary>
        /// Convertit un <see cref="int"/> en un tableau de <see cref="byte"/> au format big endian
        /// </summary>
        /// <param name="value">Valeur à convertir</param>
        /// <returns></returns>
        public static unsafe byte[] ConvertIntToBigEndian(int value)
        {
            byte[] bytes = new byte[4];

            int* buffVal = &value;
            fixed (byte* b = bytes)
            {
                for (int i = 0; i <= 3; i++)
                    b[i] = ((byte*)buffVal)[3 - i];//Un int est auto au format little endian, pas pratique
            }

            return bytes;

        }

        /// <summary>
        /// Convertit un tableau de <see cref="byte"/> au format little endian en <see cref="int"/>
        /// </summary>
        /// <param name="bytes">Tableau de <see cref="byte"/></param>
        /// <returns></returns>
        public static unsafe int ConvertBigEndianToInt(byte[] bytes)
        {
            int val = 0;
            byte* bufferVal = (byte*)&val;

            int max = Math.Min(bytes.Length, 4) - 1;

            fixed (byte* buffer = bytes)
            {
                for (int i = 0; i <= max; i++)
                    bufferVal[max - i] = buffer[i];//Un int est auto au format little endian, pas pratique
            }

            return val;

        }

        #endregion


        //Rect

        /// <summary>
        /// Renvoie le <see cref="Rectangle"/> correspondant à l'intersection avec un <see cref="Size"/>.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="sizeImage"></param>
        /// <param name="inflateRect"></param>
        /// <returns></returns>
        public static Rectangle GetIntersectionWithImage(this Rectangle rect, Size sizeImage, int inflateRect)
        {
            if (rect.Width < 0)
            {
                if (rect.Right < 0)
                {
                    rect.Width = rect.Left;
                    rect.X = 0;
                }
                else
                {
                    int temp = rect.Width;
                    rect.X += temp;
                    rect.Width = -temp;
                }
            }
            if (rect.Height < 0)
            {
                if (rect.Bottom < 0)
                {
                    rect.Height = rect.Top;
                    rect.Y = 0;
                }
                else
                {
                    int temp = rect.Height;
                    rect.Y += temp;
                    rect.Height = -temp;
                }
            }

            rect.Width += inflateRect; //limites comprises dans le rognage
            rect.Height += inflateRect;


            if (rect.Bottom > sizeImage.Height)
            {
                rect.Height = sizeImage.Height - rect.Y;
            }
            if (rect.Right > sizeImage.Width)
            {
                rect.Width = sizeImage.Width - rect.X;
            }

            if (rect.X < 0)
            {
                rect.X = 0;
            }
            if (rect.Y < 0)
            {
                rect.Y = 0;
            }

            return rect;
        }

        /// <summary>
        /// Renvoie le <see cref="Rectangle"/> à l'endroit (avec un Width et Height > 0)
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Rectangle GetRectangleRightWay(this Rectangle rect)
        {
            if (rect.Width < 0)
            {
                int temp = rect.Right;
                rect.Width = -rect.Width;
                rect.X = temp;
            }
            if (rect.Height < 0)
            {
                int temp = rect.Bottom;
                rect.Height = -rect.Height;
                rect.X = temp;
            }

            return rect;
        }


        //padding 

        /// <summary>
        /// Renvoie le nombre d'octets qui composent le padding de fin de ligne en fonction d'une largeur en nombre de pixels.
        /// </summary>
        /// <returns></returns>
        public static int GetPaddingPixel(int width, int pixelLength)
        {
            int pad = width * pixelLength % 4;

            return pad == 0 ? 0 : 4 - pad;
        }


        //Copie

        /*Marshal ne propose pas de fct de copie entre mémoires non managées (note : la classe Buffer en propose mais la fct bug?..)
        * D'après microsoft : "Copies the contents of a source memory block to a destination memory block, 
        * and supports overlapping source and destination memory blocks."
        * D'après microsoft encore, RltCopyMemory serait plus rapide mais plus dangereux car ne gère pas les superposition de mémoire.
        */

        /// <summary>
        /// Copie le nombre d'éléments indiqué d'une adresse mémoire vers une autre. (kernel32)
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="src"></param>
        /// <param name="len">Représente le nombre d'octets à déplacer, si on déplace des int : 1 nombre = 4 de longueur</param>
        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
        public static extern void SafeMemCpy(IntPtr dest, IntPtr src, uint len); //Safe

        //Autres


        /// <summary>
        /// Convertit la valeur en str base2.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ValToStrBase2(int val, int length)
        {
            return Convert.ToString(val, 2).PadLeft(length, '0');
        }


        //Enum extensions

        /// <summary>
        /// Renvoie le destription attribut de cet enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T enumValue)
              where T : struct
        {
            Type type = enumValue.GetType();
            if (!type.IsEnum)
            {
                return "";
            }

            MemberInfo memberInfo = type.GetMember(enumValue.ToString()).First();
            if (memberInfo != null)
            {
                object attrs = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).First();

                if (attrs != null)
                {
                    return ((DescriptionAttribute)attrs).Description;
                }
            }

            return enumValue.ToString(); //Pas de descr
        }

        /// <summary>
        /// Détermine si ce <see cref="Enum"/> est valide ou non.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsDefined(this Enum val)
        {
            return Enum.IsDefined(val.GetType(), val); //check si enum puis si la val est valide.
        }


        /// <summary>
        /// Renvoie le <see cref="System.Drawing.Imaging.PixelFormat"/> équivalent de cet enum.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static System.Drawing.Imaging.PixelFormat ToSystemPixelFormat(this PixelFormat val)
        {
            if (val == PixelFormat.BMP_Rgb24)
            {
                return System.Drawing.Imaging.PixelFormat.Format24bppRgb;
            }
            if (val == PixelFormat.BMP_Argb32)
            {
                return System.Drawing.Imaging.PixelFormat.Format32bppArgb;
            }

            return System.Drawing.Imaging.PixelFormat.DontCare;
        }

        /// <summary>
        /// Renvoie la longueur en octets d'un pixel associé à ce format.
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static int GetPixelLength(this PixelFormat enumValue) => (int)enumValue / 8;


        /// <summary>
        /// Renvoie le <see cref="System.Drawing.Imaging.ImageFormat"/> associé à ce ImageFormat.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static System.Drawing.Imaging.ImageFormat ToSystemImageFormat(this ImageFormat val)
        {
            switch (val)
            {
                case ImageFormat.BMP:
                    return System.Drawing.Imaging.ImageFormat.Bmp;

                case ImageFormat.JPEG:
                    return System.Drawing.Imaging.ImageFormat.Jpeg;

                case ImageFormat.PNG:
                    return System.Drawing.Imaging.ImageFormat.Png;

                default:
                    return System.Drawing.Imaging.ImageFormat.Png;
            }
        }



        //Extensions de fichier

        /// <summary>
        /// Récupère un chemin et l'extension <see cref="ImageFormat"/> qui va avec et renvoie la chaine du chemin avec l'extension correcte
        /// </summary>
        /// <param name="filename">Chemin jusqu'au fichier</param>
        /// <param name="ext">Extension à ajouter au chemin</param>
        /// <returns></returns>
        public static string GetFilenameWithCorrectExtension(string filename, ImageFormat ext)
        {
            return Path.GetDirectoryName(filename) + @"\" + Path.GetFileNameWithoutExtension(filename) + ext.GetDescription();
        }

        /// <summary>
        /// Récupère l'extension sur le chemin d'un fichier
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="chargement">Si on charge une image et que l'extension n'est pas précisée, on regarde dans le dossier de sauvegarde quelle image pourrait correspondre</param>
        /// <returns></returns>
        public static ImageFormat GetExtension(string filename, bool chargement = false)
        {
            string ext = Path.GetExtension(filename);
            if (ext != null && ext != string.Empty)
            {
                switch (ext.ToLower())
                {
                    case ".bmp":
                        if ((chargement && File.Exists(GetFilenameWithCorrectExtension(filename, ImageFormat.BMP))) || !chargement) //On check avant de charger si le fichier existe bien
                            return ImageFormat.BMP;
                        break;
                    case ".csv":
                        if ((chargement && File.Exists(GetFilenameWithCorrectExtension(filename, ImageFormat.CSV))) || !chargement)
                            return ImageFormat.CSV;
                        break;
                    case ".jpg":
                        if ((chargement && File.Exists(GetFilenameWithCorrectExtension(filename, ImageFormat.JPEG))) || !chargement)
                            return ImageFormat.JPEG;
                        break;
                    case ".png":
                        if ((chargement && File.Exists(GetFilenameWithCorrectExtension(filename, ImageFormat.PNG))) || !chargement)
                            return ImageFormat.PNG;
                        break;

                    default:
                        return 0;     //Extension non gérée
                }
            }
            if (chargement) //Pas de précision sur le type, on regarde dans le dossier
            {
                if (File.Exists(GetFilenameWithCorrectExtension(filename, ImageFormat.BMP)))
                    return ImageFormat.BMP;
                if (File.Exists(GetFilenameWithCorrectExtension(filename, ImageFormat.CSV)))
                    return ImageFormat.CSV;

                if (File.Exists(GetFilenameWithCorrectExtension(filename, ImageFormat.JPEG)))
                    return ImageFormat.JPEG;
                if (File.Exists(GetFilenameWithCorrectExtension(filename, ImageFormat.PNG)))
                    return ImageFormat.PNG;
            }
            else
            {
                return ImageFormat.BMP;  //Pas de précision sur le type, on sauvegarde en .bmp par défaut
            }
            return 0;
        }


        //Enums Formats

        /// <summary>
        /// Différents types d'extension d'images supportées pour le chargement et la sauvegarde.
        /// </summary>
        public enum ImageFormat
        {
            /// <summary>
            /// Format Bitmap (.bmp) à 24 BPP (rgb) ou 32 BPP (argb).
            /// </summary>
            [Description(".bmp")]
            BMP = 1,

            /// <summary>
            /// Format d'image bitmap sous format .csv (comma-separated values) -> lisible par Excel 
            /// </summary>
            [Description(".csv")]
            CSV = 2,

            /// <summary>
            /// Format de sauvegarde JPEG (.jpg). Algorithmie gérée par windows
            /// </summary>
            [Description(".jpg")]
            JPEG = 3,

            /// <summary>
            /// Format de sauvegarde PNG. Algorithmie gérée par windows
            /// </summary>
            [Description(".png")]
            PNG = 4,
        }

        /// <summary>
        /// Formats de pixels disponibles (avec transparence ou non pour l'instant)
        /// </summary>
        public enum PixelFormat : short
        {
            //Note : rajouter un format (blanc ou noir 1bpp, gris 8bpp, etc..) serait techniquement compliqué, 
            //les méthodes n'étant adaptées que pour des longueurs de pixels 3 ou 4.

            /// <summary>
            /// RGB
            /// </summary>
            BMP_Rgb24 = BITMAPCONST.BPP_VALUE24,

            /// <summary>
            /// ARGB : Autorise la transparence
            /// </summary>
            BMP_Argb32 = BITMAPCONST.BPP_VALUE32,
        }
    }
}
