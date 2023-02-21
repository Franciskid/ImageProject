using System;
using System.Drawing;
using System.Linq;

namespace Photoshop3000
{
    /// <summary>
    /// Représente un pixel A-R-G-B d'une image, composé de 3 nuances de couleurs : rouges, vertes et bleues et d'une transparence Alpha
    /// </summary>
    public struct Pixel //Struct car comme ça pas de problème d'objets avec la même ref, cette struct ne contient que 4 octets (rapide à copier)
    {
        //Champs et propriétés

        /// <summary>
        /// Décalage alpha
        /// </summary>
        public const int ArgbAlphaShift = 24; //on peut changer la valeur de ces constantes pour interchanger les composantes argb de l'image.
                                              //(changement effectué au chargement de l'image uniquement, pas à la création).
        /// <summary>
        /// Décalage rouge
        /// </summary>
        public const int ArgbRedShift = 16;

        /// <summary>
        /// Décalage vert
        /// </summary>
        public const int ArgbGreenShift = 8;

        /// <summary>
        /// Décalage bleu
        /// </summary>
        public const int ArgbBlueShift = 0;


        private readonly int argb;


        /// <summary>
        /// Composante rouge
        /// </summary>
        public byte R => (byte)(this.argb >> ArgbRedShift);

        /// <summary>
        /// Composante verte
        /// </summary>
        public byte G => (byte)(this.argb >> ArgbGreenShift);

        /// <summary>
        /// Composante bleue
        /// </summary>
        public byte B => (byte)(this.argb >> ArgbBlueShift);

        /// <summary>
        /// Transparence Alpha (0-255)
        /// </summary>
        public byte A => (byte)(this.argb >> ArgbAlphaShift);


        /// <summary>
        /// Pixel de base, noir + opaque.
        /// </summary>
        public static readonly Pixel Zero = new Pixel(0xff << ArgbAlphaShift);


        //Constructeurs

        /// <summary>
        /// Initialise une nouvelle instance de la structure <see cref="Pixel"/> à partir de composants rouge vert et bleu et d'une transparence alpha
        /// </summary>
        /// <param name="argb">rouge</param>
        private Pixel(int argb)
        {
            this.argb = argb;
        }


        /// <summary>
        /// Renvoie une nouvelle instance de la structure <see cref="Pixel"/> à partir de composants rouge vert et bleu
        /// </summary>
        public static Pixel FromArgb(int R, int G, int B)
        {
            return new Pixel(MakeARGB(255, R, G, B));
        }

        /// <summary>
        /// Renvoie une nouvelle instance de la structure <see cref="Pixel"/> à partir d'une transparence et de composants rouge vert et bleu
        /// </summary>
        /// <param name="Alpha"></param>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Pixel FromArgb(int Alpha, int R, int G, int B)
        {
            return new Pixel(MakeARGB(Alpha, R, G, B));
        }

        /// <summary>
        /// Renvoie une nouvelle instance de la structure <see cref="Pixel"/> à partir d'un entier à 4 octets au format litte 
        /// endian contenant dans cet ordre le bleu, le vert, le rouge et la transparence
        /// </summary>
        /// <param name="argb"></param>
        /// <returns></returns>
        public static Pixel FromArgb(int argb)
        {
            return new Pixel(argb);
        }


        /// <summary>
        /// Renvoie une nouvelle instance de la structure <see cref="Pixel"/> à partir d'une couleur <see cref="Couleurs"/>
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Pixel FromColor(Couleurs color)
        {
            if (!color.IsDefined())
                return Pixel.Zero;

            int[] rgb = CouleursTableau[(int)color];

            return new Pixel(MakeARGB(255, rgb[0], rgb[1], rgb[2]));
        }

        /// <summary>
        /// Renvoie une nouvelle instance de la structure <see cref="Pixel"/> à partir d'une <see cref="Color"/>
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Pixel FromColor(Color color)
        {
            return new Pixel(MakeARGB(color.A, color.R, color.G, color.B));
        }

        /// <summary>
        /// Renvoie une nouvelle instance de la structure <see cref="Pixel"/> à partir des composants TSV ou TSL (teinte, saturation, luminosité/valeur), HSL ou HSV en anglais
        /// </summary>
        /// <param name="hue">Teinte en degré % 360</param>
        /// <param name="sat">Saturation par rapport à 1</param>
        /// <param name="lum">Luminosité par rapport à 1</param>
        /// <returns></returns>
        public static Pixel FromHSL(int hue, float sat, float lum)
        {
            byte r;
            byte g;
            byte b;

            if (sat == 0)
            {
                r = g = b = (byte)(lum * 255);
            }
            else
            {
                float v1, v2;
                float h = (float)hue / 360;

                v2 = (lum < 0.5) ? (lum * (1 + sat)) : (lum + sat - (lum * sat));
                v1 = 2 * lum - v2;

                r = (byte)(255 * FromHueToRGB(v1, v2, h + (1.0f / 3)));
                g = (byte)(255 * FromHueToRGB(v1, v2, h));
                b = (byte)(255 * FromHueToRGB(v1, v2, h - (1.0f / 3)));
            }

            return Pixel.FromArgb(r, g, b);
        }


        //Opérateurs

        /// <summary>
        /// Renvoie <see langword="true"/> si les 2 pixels ont les mêmes composantes R-G-B
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static unsafe bool operator ==(Pixel a, Pixel b)
        {
            return a.argb == b.argb;
        }

        /// <summary>
        /// Renvoie <see langword="false"/> si les 2 pixels ont les mêmes composantes R-G-B
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Pixel a, Pixel b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Renvoie le Pixel équivalent de ce Couleurs enum.
        /// </summary>
        /// <param name="c"></param>
        public static explicit operator Pixel(Couleurs c)
        {
            return Pixel.FromColor(c);
        }


        //Méthodes publiques

        /// <summary>
        /// Renvoie la moyenne des couleurs du Pixel
        /// </summary>
        public int Moyenne()
        {
            return (this.R + this.B + this.G) / 3;
        }


        /// <summary>
        /// Renvoie un pixel en son équivalent en gris
        /// </summary>
        /// <param name="intensité"></param>
        /// <returns></returns>
        public Pixel TransformationGris(int intensité)
        {
            int somme = Moyenne();
            if (somme > 255 / 2.0) //Moitié haute, on augmente l'intensité des composants du pixel
            {
                somme += 255 * intensité / 100;
            }
            else //En dessous on les baisses
            {
                somme -= 255 * intensité / 100;
            }
            if (somme > 255) somme = 255;
            else if (somme < 0) somme = 0;

            return new Pixel(MakeARGB(this.A, somme, somme, somme));
        }

        /// <summary>
        /// Renvoie un pixel en augmentant la luminosité en fonction d'un facteur intensité
        /// </summary>
        /// <param name="intensité">Pourcentage d'intensité</param>
        /// <returns></returns>
        public Pixel TransformationLuminosité(int intensité)
        {
            int augmentation = 255 * intensité / 100;

            int red = this.R + augmentation, green = this.G + augmentation, blue = this.B + augmentation;
            red = Math.Min(Math.Max(red, 0), 255);
            green = Math.Min(Math.Max(green, 0), 255);
            blue = Math.Min(Math.Max(blue, 0), 255);

            return new Pixel(MakeARGB(this.A, red, green, blue));
        }

        /// <summary>
        /// Renvoie le <see cref="Pixel"/> avec les couleurs inversées
        /// </summary>
        /// <returns></returns>
        public Pixel InversionCouleur()
        {
            return new Pixel(MakeARGB(this.A, 255 - this.R, 255 - this.G, 255 - this.B));
        }



        /// <summary>
        /// Renvoie un pixel en tonalités sépia
        /// </summary>
        /// <returns></returns>
        public Pixel TransformationSépia()
        {
            byte b = (byte)Math.Min(Math.Max((int)(0.272 * this.R + 0.534 * this.G + 0.131 * this.B), 0), 255);  //Bleu
            byte g = (byte)Math.Min(Math.Max((int)(0.349 * this.R + 0.686 * this.G + 0.168 * this.B), 0), 255);  //Vert
            byte r = (byte)Math.Min(Math.Max((int)(0.393 * this.R + 0.769 * this.G + 0.189 * this.B), 0), 255);  //Rouge

            return new Pixel(MakeARGB(this.A, r, g, b));
        }


        /// <summary>
        /// Ajoute une transparence alpha à un pixel, le pixel résultant a une transparence A = 255.
        /// </summary>
        /// <param name="alpha">Transparence, 0 à 1</param>
        /// <param name="backgroundPixel">Pixel de devant</param>
        /// <param name="foregroundPixel">Pixel de derrière</param>
        /// <returns></returns>
        public static Pixel AddTransparence(Pixel backgroundPixel, Pixel foregroundPixel, float alpha)
        {
            float inverse = 1.0f - alpha;
            return new Pixel(MakeARGB(byte.MaxValue, (byte)(foregroundPixel.R * alpha + backgroundPixel.R * inverse),
                (byte)(foregroundPixel.G * alpha + backgroundPixel.G * inverse),
                (byte)(foregroundPixel.B * alpha + backgroundPixel.B * inverse)));
        }


        /// <summary>
        /// Récupère la <see cref="Couleurs"/> la plus proche du <see cref="Pixel"/> entré en paramètre
        /// </summary>
        /// <param name="p">Pixel</param>
        /// <returns></returns>
        public static Couleurs GetCouleur(Pixel p)
        {
            int miniDistance = int.MaxValue;
            int indexMiniDistance = 0;
            for (int i = 0; i < CouleursTableau.Length; i++)
            {
                int[] rgb = CouleursTableau[i];
                int distance = Math.Abs(p.R - rgb[0]) + Math.Abs(p.G - rgb[1]) + Math.Abs(p.B - rgb[2]);
                if (distance < miniDistance)
                {
                    miniDistance = distance;
                    indexMiniDistance = i;
                }
            }

            return (Couleurs)indexMiniDistance;
        }


        /// <summary>
        /// Renvoie en % la différence de couleur entre ces 2 pixels. Prend en compte la composante alpha.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static int Divergence(Pixel p1, Pixel p2)
        {
            byte diffR = (byte)Math.Abs(p1.R - p2.R);
            byte diffG = (byte)Math.Abs(p1.G - p2.G);
            byte diffB = (byte)Math.Abs(p1.B - p2.B);
            byte diffA = (byte)Math.Abs(p1.A - p2.A);

            return ((diffR * 100 / 255) + (diffA * 100 / 255) + (diffG * 100 / 255) + (diffB * 100 / 255)) / 4;
        }


        /// <summary>
        /// Renvoie la moyenne entre 2 pixels
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Pixel MoyennePixels(Pixel p1, Pixel p2)
        {
            return AddTransparence(p1, p2, 50);
        }



        /// <summary>
        /// Récupère la luminosité perçue du Pixel
        /// </summary>
        /// <returns></returns>
        public float Brightness()
        {
            return 0.2126f * this.R + 0.7152f * this.G + 0.0722f * this.B;
        }


        /// <summary>
        /// Récupère la teinte d'un Pixel
        /// </summary>
        /// <returns></returns>
        public int Hue()
        {
            if (this.R == this.G && this.G == this.B)
                return 0;

            float r = (float)this.R / 255.0f;
            float g = (float)this.G / 255.0f;
            float b = (float)this.B / 255.0f;

            float min = Math.Min(Math.Min(r, g), b);
            float max = Math.Max(Math.Max(r, g), b);


            float hue = 0f;
            if (max == r)
            {
                hue = (g - b) / (max - min);

            }
            else if (max == g)
            {
                hue = 2f + ((b - r) / (max - min));

            }
            else
            {
                hue = 4f + ((r - g) / (max - min));
            }

            hue *= 60;
            if (hue < 0) hue += 360;

            return (int)Math.Round(hue);
        }

        /// <summary>
        /// Récupère la luminosité du Pixel
        /// </summary>
        /// <returns></returns>
        public float Lightness()
        {
            float r = (float)this.R / 255.0f;
            float g = (float)this.G / 255.0f;
            float b = (float)this.B / 255.0f;

            float min = Math.Min(Math.Min(r, g), b);
            float max = Math.Max(Math.Max(r, g), b);

            return (max + min) / 2;
        }

        /// <summary>
        /// Récupère la saturation du Pixel
        /// </summary>
        /// <returns></returns>
        public float Saturation()
        {
            float r = (float)this.R / 255.0f;
            float g = (float)this.G / 255.0f;
            float b = (float)this.B / 255.0f;

            float min = Math.Min(Math.Min(r, g), b);
            float max = Math.Max(Math.Max(r, g), b);

            if (min == max)
                return 0f;

            if ((max + min) / 2 <= .5f)
            {
                return (max - min) / (max + min);
            }
            else
            {
                return (max - min) / (2f - max - min);
            }
        }


        private static float FromHueToRGB(float p, float q, float t)
        {
            if (t < 0) t++;
            else if (t > 1) t--;

            if (t < 1f / 6) return p + ((q - p) * 6 * t);
            if (t < 1f / 2) return q;
            if (t < 2f / 3) return p + ((q - p) * (2f / 3 - t) * 6);
            return p;
        }


        /// <summary>
        /// Renvoie une chaine de charactère décrivant le <see cref="Pixel"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"| A({this.A}) R({this.R}) G({this.G}) B({this.B}) |";
        }

        /// <summary>
        /// Renvoie une valeur 32 bits de cette instance
        /// </summary>
        /// <returns></returns>
        public int ToArgb()
        {
            return this.argb;
        }

        /// <summary>
        /// Renvoie l'équivalent 32 bits de ces 4 valeurs
        /// </summary>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static int MakeARGB(int a, int r, int g, int b)
        {
            return ((byte)a << ArgbAlphaShift) | ((byte)r << ArgbRedShift) | ((byte)g << ArgbGreenShift) | ((byte)b << ArgbBlueShift);
        }


        /// <summary>
        /// Renvoie le <see cref="Pixel"/> sous forme d'un <see cref="Color"/>
        /// </summary>
        /// <returns></returns>
        public Color ToColor()
        {
            //return Color.FromArgb(*(int*)this.Argb);
            return Color.FromArgb(this.argb);
        }


        /// <summary>
        /// R-G-B
        /// </summary>
        private static readonly int[][] CouleursTableau = new int[][]
        {
            //Blanc
            new int[]
            {
                255, 255, 255
            },
            
            //Gris clair
            new int[]
            {
                224, 224, 224
            },

            //Argent
            new int[]
            {
                192, 192, 192
            },

            //Gris
            new int[]
            {
                128, 128, 128
            },
            
            //Gris foncé
            new int[]
            {
                70, 70, 70
            },

            //Gris très foncé
            new int[]
            {
                34, 34, 34
            },
            
            //Noir
            new int[]
            {
                0, 0, 0
            },
            
            //Violet
            new int[]
            {
                138, 43, 226
            },

            //Violet2
            new int[]
            {
                102, 0, 51
            },

            //Indigo
            new int[]
            {
                46, 0, 79
            },
            
            //Bleu marine
            new int[]
            {
                0, 0, 128
            },
            
            //Bleu
            new int[]
            {
                0, 0, 255
            },

            //Bleu 2
            new int[]
            {
                15, 69, 165
            },

            //Cyan
            new int[]
            {
                0, 255, 255
            },
            
            //Bleu ciel
            new int[]
            {
                119, 180, 254
            },

            //Bleu ciel 2
            new int[]
            {
                10, 150, 189
            },
            
            //Bleu ciel 3
            new int[]
            {
                80, 106, 193
            },

            //Bleu clair
            new int[]
            {
                153, 204, 254
            },
            
            //Bleu canard
            new int[]
            {
                0, 128, 128
            },

            //Vert foncé
            new int[]
            {
                0, 128, 0
            },

            //Vert 
            new int[]
            {
                0, 255, 0
            },

            //Vert 2
            new int[]
            {
                41, 120, 0
            },

            //Vert 3
            new int[]
            {
                145, 191, 90
            },
            
            //Vert 4
            new int[]
            {
                100, 160, 60
            },

            //Vert clair
            new int[]
            {
                144, 238, 178
            },

            //Vert jaune
            new int[]
            {
                229, 255, 204
            },
            
            //Beige
            new int[]
            {
                255, 255, 190
            },

            //Beige 2
            new int[]
            {
                222, 208, 208
            },

            //Jaune
            new int[]
            {
                255, 255, 0
            },
            
            //Jaune 2
            new int[]
            {
                240, 225, 114
            },

            //Jaune Kaki
            new int[]
            {
                102, 102, 0
            },

            //Jaune orangé
            new int[]
            {
                255, 204, 0
            },

            //Orange Foncé
            new int[]
            {
                204, 102, 0
            },

            //Orange
            new int[]
            {
                255, 102, 0
            },
            
            //Orange Rosé
            new int[]
            {
                255, 204, 253
            },
            
            //Rose clair
            new int[]
            {
                255, 192, 203
            },

            //Rose
            new int[]
            {
                255, 102, 180
            },

            //Rose foncé
            new int[]
            {
                255,20,147
            },

            //Marron
            new int[]
            {
                88, 40, 0
            },
            
            //Magenta
            new int[]
            {
                255, 0, 255
            },
            
            //Rouge
            new int[]
            {
                255, 0, 0
            },

            //Rouge Foncé
            new int[]
            {
                153, 0, 0
            },

        };

    }

    /// <summary>
    /// Types de couleurs de base. ~40 couleurs disponibles
    /// </summary>
    public enum Couleurs
    {
        Blanc,

        Gris_Clair,
        Argent,
        Gris,
        Gris_Foncé,
        Noir_Clair,

        Noir,

        Violet,
        Violet2,
        Indigo,
        Bleu_Marine,
        Bleu,
        Bleu2,
        Cyan,
        Bleu_Ciel1,
        Bleu_Ciel2,
        Bleu_Ciel3,
        Bleu_Clair,
        Bleu_Canard,
        Vert_Foncé,
        Vert,
        Vert2,
        Vert3,
        Vert4,
        Vert_Clair,
        Vert_Jaune,
        Beige,
        Beige2,
        Jaune,
        Jaune2,
        Jaune_Kaki,
        Jaune_Orangé,
        Orange_Foncé,
        Orange,
        Orange_Rosé,
        Rose_Clair,
        Rose,
        Rose_Foncé,
        Marron,
        Magenta,
        Rouge,
        Rouge_Foncé
    }
}
