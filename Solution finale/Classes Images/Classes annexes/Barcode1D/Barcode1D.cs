using Photoshop3000;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using Point = Photoshop3000.Point;

namespace Photoshop3000.Barcode
{
    /// <summary>
    /// Classe de base des codes-barres à 1 dimension.
    /// </summary>
    abstract class Barcode1D
    {
        //Propriétés

        /// <summary>
        /// Contient toutes les informations liées au code-barres
        /// </summary>
        public BarcodeData Data { get; private set; }

        /// <summary>
        /// Renvoie l'image de ce <see cref="Barcode1D"/>
        /// </summary>
        public MyImage Image { get; protected set; }


        //Constructeurs

        /// <summary>
        /// Constructeur pour décoder un code-barres
        /// </summary>
        /// <param name="toScan"></param>
        /// <param name="data"></param>
        protected Barcode1D(MyImage toScan, BarcodeData data)
        {
            this.Image = toScan;

            this.Data = data;
        }


        //Méthodes publiques/protégées

        /// <summary>
        /// Convertit ce <see cref="Barcode1D"/> sous une autre forme spécifiée et renvoie le succès ou non de l'opération à travers un message d'erreur.
        /// </summary>
        /// <param name="scannerToConvertTo">Scanner vers lequel convertir les données</param>
        /// <param name="manufacturerOpt">Si on convertit depuis un EAN8, il faut forcément le code du producteur à 5 chiffres, ne pas remplir sinon.</param>
        /// <returns></returns>
        public string ConvertBarcode(BarcodeTypes scannerToConvertTo, string manufacturerOpt = "")
        {
            return this.Data.ConvertBarcode(scannerToConvertTo, manufacturerOpt);
        }


        /// <summary>
        /// Renvoie le <see cref="BarcodeData"/> sous forme d'un tableau de byte composé de 0 et de 1 encodé selon les normes du <see cref="BarcodeTypes"/>
        /// </summary>
        /// <returns></returns>
        protected byte[] GetEncodedBits()
        {
            if (!this.Data.EstValide)
                return null;

            byte[] bytes = new byte[this.Data.Barcode.BarAmount];

            //Guards

            for (int i = this.Data.Barcode.LGuard_POS; i < this.Data.Barcode.LGuard_LENGTH + this.Data.Barcode.LGuard_POS; i++)
            {
                bytes[i] = (byte)((this.Data.Barcode.LGuard_VALUE & (1 << (i - this.Data.Barcode.LGuard_POS))) >> (i - this.Data.Barcode.LGuard_POS));
            }
            for (int i = this.Data.Barcode.CGuard_POS; i < this.Data.Barcode.CGuard_LENGTH + this.Data.Barcode.CGuard_POS; i++)
            {
                bytes[i] = (byte)((this.Data.Barcode.CGuard_VALUE & (1 << (i - this.Data.Barcode.CGuard_POS))) >> (i - this.Data.Barcode.CGuard_POS));
            }
            for (int i = this.Data.Barcode.RGuard_POS; i < this.Data.Barcode.RGuard_LENGTH + this.Data.Barcode.RGuard_POS; i++)
            {
                bytes[i] = (byte)((this.Data.Barcode.RGuard_VALUE & (1 << (i - this.Data.Barcode.RGuard_POS))) >> (i - this.Data.Barcode.RGuard_POS));
            }


            string stringToEncode = this.Data.GetScanCodeToEncode;

            int firstDigit = Convert.ToInt32(this.Data.FirstDigit);
            int lastDigit = Convert.ToInt32(this.Data.ScanCode[this.Data.ScanCode.Length - 1].ToString());

            int startLeft = this.Data.Barcode.LGuard_LENGTH + this.Data.Barcode.LGuard_POS;

            for (int i = 0; i < stringToEncode.Length; i++)
            {
                int code = Convert.ToInt32(stringToEncode[i].ToString());

                string oneDigitRpz;

                if (this.Data.Barcode == "upce")
                {
                    oneDigitRpz = ConvertByteRepresentationToString(ValeursGTIN[ParitéUPCE[lastDigit][firstDigit][i]][code]);
                }
                else if (i < this.Data.Barcode.MidDigit)
                {
                    oneDigitRpz = ConvertByteRepresentationToString(ValeursGTIN[Parité[firstDigit][i]][code]);
                }
                else
                {
                    oneDigitRpz = ConvertByteRepresentationToString(ValeursGTIN[2][code]);
                }

                for (int k = oneDigitRpz.Length - this.Data.Barcode.BarPerDigit; k <= this.Data.Barcode.BarPerDigit; k++)
                {
                    if (i < this.Data.Barcode.MidDigit)
                    {
                        bytes[startLeft + k - 1 + (i * this.Data.Barcode.BarPerDigit)] = (byte)(oneDigitRpz[k].ToString() == "1" ? 1 : 0);
                    }
                    else
                    {
                        bytes[startLeft + this.Data.Barcode.CGuard_LENGTH + k - 1 + (i * this.Data.Barcode.BarPerDigit)] = (byte)(oneDigitRpz[k].ToString() == "1" ? 1 : 0);
                    }
                }

            }

            return bytes;
        }


        /// <summary>
        /// Renvoie un tableau de byte contenant les digits décodés à partir du tableau de byte de 0 et de 1 encodé selon les normes du <see cref="BarcodeTypes"/>
        /// </summary>
        /// <returns></returns>
        protected byte[] GetDecodedDigits(byte[] line)
        {
            if (CheckValidité(line))
            {
                byte[] encodedDigits = GetEncodedDigits(line);

                byte[] decodedDigits = new byte[this.Data.Barcode.Capacity];

                byte systemFirstDigit = 0;

                if (this.Data.Barcode != "upce") //upca, ean13, ean8
                {
                    int numberChange = 0;
                    do
                    {
                        decodedDigits[0] = systemFirstDigit;

                        for (int i = 1; i < decodedDigits.Length; i++)
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                if (i <= this.Data.Barcode.MidDigit && encodedDigits[i] == ValeursGTIN[Parité[decodedDigits[0]][i - 1]][j]) //pair ou impair gauche
                                {
                                    decodedDigits[i] = (byte)j;
                                    numberChange++;
                                    break;
                                }
                                else if (i > this.Data.Barcode.MidDigit && encodedDigits[i] == ValeursGTIN[2][j]) //Toujours meme encodage à droite
                                {
                                    decodedDigits[i] = (byte)j;
                                    numberChange++;
                                    break;
                                }

                            }
                        }

                        if (numberChange == decodedDigits.Length - 1 && TestCheckDigit(decodedDigits))
                        {
                            break; //On pourrait check si le checkDigit est correcte mais à partir du moment où on a tous les changements effectués, ce n'est plus la peine (note:en fait si)
                        }

                        numberChange = 0;
                        systemFirstDigit++;

                    }
                    while (systemFirstDigit < 10);
                }
                else //Méthode de décodage différente pour upce
                {
                    byte checkDigit = 0;
                    for (int systemNumb = 0; systemNumb < 2; systemNumb++)
                    {
                        decodedDigits[1] = (byte)systemNumb;
                        int numberChange = 0;
                        bool checkModulo;
                        do
                        {
                            checkModulo = false;
                            decodedDigits[decodedDigits.Length - 1] = checkDigit;
                            for (int i = 1; i < decodedDigits.Length - 1 - 1; i++)
                            {
                                int indexChanges = 0;
                                for (int j = 0; j < 10; j++)
                                {
                                    if (encodedDigits[i] == ValeursGTIN[ParitéUPCE[checkDigit][decodedDigits[1]][i - 1]][j]) //pair ou impair gauche
                                    {
                                        decodedDigits[i + 1] = (byte)j;
                                        numberChange++;
                                        indexChanges++;
                                        break;
                                    }
                                }

                                if (indexChanges == 0) //Aucun changements, ce n'est pas le bon systemNumb
                                {
                                    break;
                                }

                            }

                            if (numberChange != 6)
                            {
                                numberChange = 0;
                            }
                            else
                            {
                                checkModulo = TestCheckDigit(decodedDigits);
                            }

                        }
                        while (!checkModulo && ++checkDigit < 10);

                        if (numberChange != 6 && systemNumb == 0) //On regarde si tous les changement sont bien été effectués, sinon le résultat est forcément faux.
                        {
                            checkDigit = 0;
                        }
                        else if (TestCheckDigit(decodedDigits))
                        {
                            break;
                        }
                        else if (systemNumb == 0)
                        {
                            checkDigit = 0;
                        }

                        systemFirstDigit = checkDigit; //Pour savoir si valide après. Si checkdigit >= 10, pas valide
                    }
                }

                if (systemFirstDigit < 10) //Check si tout s'est bien passé
                {
                    return decodedDigits;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }


        /// <summary>
        /// Renvoie le check digit, valeurs paires = * 1, valeurs impaires = * 3.
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        protected static int FindCheckDigit(byte[] digits)
        {
            int valeur = 0;

            for (int i = 0; i < digits.Length - 1; i++)
            {
                valeur += i % 2 == 0 ? digits[i] : digits[i] * 3;
            }

            return valeur % 10 == 0 ? 0 : 10 - valeur % 10;
        }


        /// <summary>
        /// Convertit les 8 premiers bits d'un int en leur équivalent en string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static string ConvertByteRepresentationToString(int value)
        {
            const int MAX = 7;

            string s = "";
            for (int i = 0; i <= MAX; i++)
            {
                s += ((value & (1 << (MAX - i))) >> MAX - i) == 1 ? "1" : "0";
            }

            return s;
        }


        //Méthodes privées

        /// <summary>
        /// Renvoie un tableau contenant les digits encodés à partir de la ligne du code-barres composée de 0 et de 1 telle qu'elle.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private byte[] GetEncodedDigits(byte[] line)
        {
            byte[] encodedDigits = new byte[this.Data.Barcode.Capacity];

            byte[] lineWithoutGuard = RemoveGuard(line);

            for (int i = 0, indexInDigit = 0, digit = 1; i < lineWithoutGuard.Length; i++)
            {
                encodedDigits[digit] |= (byte)(lineWithoutGuard[i] << (this.Data.Barcode.BarPerDigit - 1 - indexInDigit++));

                if (indexInDigit == this.Data.Barcode.BarPerDigit)
                {
                    indexInDigit = 0;
                    digit++;
                }
            }

            return encodedDigits;
        }


        /// <summary>
        /// Détermine si le code barre récupéré est valide ou non en fonction de son type.
        /// </summary>
        /// <param name="bars"></param>
        /// <returns></returns>
        private bool CheckValidité(byte[] bars)
        {
            if (bars != null && bars.Length == this.Data.Barcode.BarAmount)
            {
                int LGuard = 0;
                for (int i = this.Data.Barcode.LGuard_POS; i < this.Data.Barcode.LGuard_POS + this.Data.Barcode.LGuard_LENGTH; i++)
                    LGuard |= bars[i] << (this.Data.Barcode.LGuard_POS + this.Data.Barcode.LGuard_LENGTH - 1 - i);

                int cGuard = 0;
                for (int i = this.Data.Barcode.CGuard_POS; i < this.Data.Barcode.CGuard_POS + this.Data.Barcode.CGuard_LENGTH; i++)
                    cGuard |= bars[i] << (this.Data.Barcode.CGuard_POS + this.Data.Barcode.CGuard_LENGTH - 1 - i);

                int rGuard = 0;
                for (int i = this.Data.Barcode.RGuard_POS; i < this.Data.Barcode.RGuard_POS + this.Data.Barcode.RGuard_LENGTH; i++)
                    rGuard |= bars[i] << (this.Data.Barcode.RGuard_POS + this.Data.Barcode.RGuard_LENGTH - 1 - i);

                return this.Data.Barcode.LGuard_VALUE == LGuard && this.Data.Barcode.CGuard_VALUE == cGuard && this.Data.Barcode.RGuard_VALUE == rGuard;
            }

            return false;
        }


        /// <summary>
        /// Détermine si le module des digits est bien égale au dernier digit de ce code barre.
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        private static bool TestCheckDigit(byte[] digits)
        {
            return FindCheckDigit(digits) == digits[digits.Length - 1];
        }


        /// <summary>
        /// Renvoie la ligne de barres sans les barres guard
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private byte[] RemoveGuard(byte[] line)
        {
            if (line.Length != this.Data.Barcode.BarAmount)
                return null;

            byte[] bt = new byte[line.Length - (this.Data.Barcode.LGuard_LENGTH + this.Data.Barcode.CGuard_LENGTH + this.Data.Barcode.RGuard_LENGTH)];

            for (int i = this.Data.Barcode.LGuard_POS + this.Data.Barcode.LGuard_LENGTH; i < line.Length - this.Data.Barcode.RGuard_LENGTH; i++)
            {
                if (i < this.Data.Barcode.CGuard_POS) //Mid guard
                {
                    bt[i - this.Data.Barcode.LGuard_LENGTH] = line[i];
                }
                if (i >= this.Data.Barcode.CGuard_POS + this.Data.Barcode.CGuard_LENGTH)
                {
                    bt[i - (this.Data.Barcode.CGuard_LENGTH + this.Data.Barcode.LGuard_LENGTH)] = line[i];
                }
            }

            return bt;
        }


        /// <summary>
        /// Convertit un string en tableau de bits (chaque bits est dans une case du tableau de byte)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static byte[] ConvertStringToByteArray(string str)
        {
            byte[] bt = new byte[str.Length];
            for (int i = 0; i < bt.Length; i++)
                bt[i] = Convert.ToByte(str[i].ToString());

            return bt;
        }


        //Parité et valeur des codes à 7 bits

        #region Parités et valeurs des codes à 7 bits

        /// <summary>
        /// Valeur des digits GTIN, gauche impaire = 0, gauche paire = 1, droite = 2. Pos du byte = valeur
        /// </summary>
        private static readonly byte[][] ValeursGTIN = new byte[][]
        {
            new byte[]
            {
                0b_000_1101,
                0b_001_1001,
                0b_001_0011,
                0b_011_1101,
                0b_010_0011,
                0b_011_0001,
                0b_010_1111,
                0b_011_1011,
                0b_011_0111,
                0b_000_1011,
            },
            new byte[]
            {
                0b_010_0111,
                0b_011_0011,
                0b_001_1011,
                0b_010_0001,
                0b_001_1101,
                0b_011_1001,
                0b_000_0101,
                0b_001_0001,
                0b_000_1001,
                0b_000_0111,
            },
            new byte[]
            {
                0b_111_0010,
                0b_110_0110,
                0b_110_1100,
                0b_100_0010,
                0b_101_1100,
                0b_100_1110,
                0b_101_0000,
                0b_100_0100,
                0b_100_1000,
                0b_111_0100,
            }
        };

        /// <summary>
        /// Parité UPC-A et EAN-13 en fonction du 1er digit
        /// </summary>
        private static readonly byte[][] Parité = new byte[][]
        {
            new byte[]
            {
                0, 0, 0, 0, 0, 0
            },
            new byte[]
            {
                0, 0, 1, 0, 1, 1
            },
            new byte[]
            {
                0, 1, 1, 0, 0, 1
            },
            new byte[]
            {
                0, 0, 1, 1, 1, 0
            },
            new byte[]
            {
                0, 1, 0, 0, 1, 1
            },
            new byte[]
            {
                0, 1, 1, 0, 0, 1
            },
            new byte[]
            {
                0, 1, 1, 1, 0, 0
            },
            new byte[]
            {
                0, 1, 0, 1, 0, 1
            },
            new byte[]
            {
                0, 1, 0, 1, 1, 0
            },
            new byte[]
            {
                0, 1, 1, 0, 1, 0
            },

        };

        /// <summary>
        /// Parité UPC-E en fonction du dernier puis du 1er digit
        /// </summary>
        private static readonly byte[][][] ParitéUPCE = new byte[][][]
        {
            new byte[][]
            {
                new byte[]
                {
                    1, 1, 1, 0, 0, 0
                },
                new byte[]
                {
                    0, 0, 0, 1, 1, 1
                },
            },


            new byte[][]
            {
                new byte[]
                {
                    1, 1, 0, 1, 0, 0
                },
                new byte[]
                {
                    0, 0, 1, 0, 1, 1
                },
            },


            new byte[][]
            {
                new byte[]
                {
                    1, 1, 0, 0, 1, 0
                },
                new byte[]
                {
                    0, 0, 1, 1, 0, 1
                },
            },


            new byte[][]
            {
                new byte[]
                {
                    1, 1, 0, 0, 0, 1
                },
                new byte[]
                {
                    0, 0, 1, 1, 1, 0
                },
            },


            new byte[][]
            {
                new byte[]
                {
                    1, 0, 1, 1, 0, 0
                },
                new byte[]
                {
                    0, 1, 0, 0, 1, 1
                },
            },

            new byte[][]
            {
                new byte[]
                {
                    1, 0, 0, 1, 1, 0
                },
                new byte[]
                {
                    0, 1, 1, 0, 0, 1
                },
            },


            new byte[][]
            {
                new byte[]
                {
                    1, 0, 0, 0, 1, 1
                },
                new byte[]
                {
                    0, 1, 1, 1, 0, 0
                },
            },


            new byte[][]
            {
                new byte[]
                {
                    1, 0, 1, 0, 1, 0
                },
                new byte[]
                {
                    0, 1, 0, 1, 0, 1
                },
            },


            new byte[][]
            {
                new byte[]
                {
                    1, 0, 1, 0, 0, 1
                },
                new byte[]
                {
                    0, 1, 0, 1, 1, 0
                },
            },


            new byte[][]
            {
                new byte[]
                {
                    1, 0, 0, 1, 0, 1
                },
                new byte[]
                {
                    0, 1, 1, 0, 1, 0
                },
            },


        };

        #endregion


        //Class Scanners et ScanData

        #region barcodetype et barcodedata

        /// <summary>
        /// Types de code-barres unidimensionnels valides pour la création et le décodage.
        /// </summary>
        public class BarcodeTypes
        {
            #region Constantes

            //EAN-13 + UPC-A (const pas précisées pour UPC lui sont valables)
            private const byte EAN13_Capacity = 13; //UpcA et EAN, même principe +/- au décodage
            private const byte EAN13_BarAmount = 95;
            private const byte EAN_BarPerDigit = 7;
            private const byte EAN13_MidDigit = 6;


            private const byte EAN_RLGuard_VALUE = 0b101;
            private const byte EAN_LGuard_POS = 0;
            private const byte EAN13_RGuard_POS = 92;
            private const byte EAN_RLGuard_LENGTH = 3;

            private const byte EAN_CGuard_VALUE = 0b01010;
            private const byte EAN13_CGuard_POS = 45;
            private const byte EAN_CGuard_LENGTH = 5;


            //EAN-8 (voir EAN si const manquante)
            private const byte EAN8_Capacity = 9;
            private const byte EAN8_BarAmount = 67;
            private const byte EAN8_MidDigit = 4;

            private const byte EAN8_RGuard_POS = 64;
            private const byte EAN8_CGuard_POS = 31;


            //UPC-E
            private const byte UPCE_BarAmount = 51;
            private const byte UPCE_Capacity = 9;

            private const byte UPCE_RGuard_POS = 50;
            private const byte UPCE_RGuard_VALUE = 1;
            private const byte UPCE_RGuard_LENGTH = 1;

            #endregion

            //Propriétés public

            /// <summary>
            /// Représentation du type de code barres sous forme d'un string
            /// </summary>
            public string StringRepresentation { get; private set; }

            public byte Capacity { get; private set; }
            public byte BarAmount { get; private set; }
            public byte BarPerDigit { get; private set; }
            public byte MidDigit { get; private set; }

            public byte RGuard_VALUE { get; private set; }
            public byte RGuard_POS { get; private set; }
            public byte RGuard_LENGTH { get; private set; }

            public byte LGuard_VALUE { get; private set; }
            public byte LGuard_POS { get; private set; }
            public byte LGuard_LENGTH { get; private set; }

            public byte CGuard_VALUE { get; private set; }
            public byte CGuard_POS { get; private set; }
            public byte CGuard_LENGTH { get; private set; }


            //Constructeur privé

            private BarcodeTypes(string value) => this.StringRepresentation = value;


            //Constructeurs static

            /// <summary>
            /// Code barres non valide
            /// </summary>
            public static BarcodeTypes Null
            {
                get
                {
                    return new BarcodeTypes("null");
                }
            }


            /// <summary>
            /// Scan 1D, GTIN-13 ou EAN-13 (European Article Numbering à 13 digits)
            /// </summary>
            public static BarcodeTypes EAN13
            {
                get
                {
                    return new BarcodeTypes("ean13")
                    {
                        Capacity = EAN13_Capacity,
                        BarAmount = EAN13_BarAmount,
                        BarPerDigit = EAN_BarPerDigit,
                        MidDigit = EAN13_MidDigit,

                        RGuard_VALUE = EAN_RLGuard_VALUE,
                        RGuard_LENGTH = EAN_RLGuard_LENGTH,
                        RGuard_POS = EAN13_RGuard_POS,

                        LGuard_POS = EAN_LGuard_POS,
                        LGuard_VALUE = EAN_RLGuard_VALUE,
                        LGuard_LENGTH = EAN_RLGuard_LENGTH,

                        CGuard_VALUE = EAN_CGuard_VALUE,
                        CGuard_POS = EAN13_CGuard_POS,
                        CGuard_LENGTH = EAN_CGuard_LENGTH,
                    };
                }
            }


            /// <summary>
            /// Scan 1D, GTIN-12 ou UPC-A (Universal Product Code à 12 digits)
            /// </summary>
            public static BarcodeTypes UPCA
            {
                get
                {
                    BarcodeTypes scan = EAN13;
                    scan.StringRepresentation = "upca";
                    return scan;
                }
            }


            /// <summary>
            /// Scan 1D, GTIN-8 ou EAN-8 (European Article Numbering à 8 digits)
            /// </summary>
            public static BarcodeTypes EAN8
            {
                get
                {
                    return new BarcodeTypes("ean8")
                    {
                        Capacity = EAN8_Capacity,
                        BarAmount = EAN8_BarAmount,
                        BarPerDigit = EAN_BarPerDigit,
                        MidDigit = EAN8_MidDigit,

                        RGuard_VALUE = EAN_RLGuard_VALUE,
                        RGuard_LENGTH = EAN_RLGuard_LENGTH,
                        RGuard_POS = EAN8_RGuard_POS,

                        LGuard_POS = EAN_LGuard_POS,
                        LGuard_VALUE = EAN_RLGuard_VALUE,
                        LGuard_LENGTH = EAN_RLGuard_LENGTH,

                        CGuard_VALUE = EAN_CGuard_VALUE,
                        CGuard_POS = EAN8_CGuard_POS,
                        CGuard_LENGTH = EAN_CGuard_LENGTH,
                    };
                }
            }


            /// <summary>
            /// Scan 1D, UPC-E (Universal Product Code à 8 digits), convertible en UPC-A
            /// </summary>
            public static BarcodeTypes UPCE
            {
                get
                {
                    return new BarcodeTypes("upce")
                    {
                        Capacity = UPCE_Capacity,
                        BarAmount = UPCE_BarAmount,
                        BarPerDigit = EAN_BarPerDigit,
                        MidDigit = EAN13_MidDigit,

                        RGuard_VALUE = UPCE_RGuard_VALUE,
                        RGuard_LENGTH = UPCE_RGuard_LENGTH,
                        RGuard_POS = UPCE_RGuard_POS,

                        LGuard_POS = EAN_LGuard_POS,
                        LGuard_VALUE = EAN_RLGuard_VALUE,
                        LGuard_LENGTH = EAN_RLGuard_LENGTH,

                        CGuard_VALUE = EAN_CGuard_VALUE,
                        CGuard_POS = EAN13_CGuard_POS,
                        CGuard_LENGTH = EAN_CGuard_LENGTH,
                    };
                }
            }


            //Opérateurs

            public static bool operator ==(BarcodeTypes scanner1D_1, BarcodeTypes scanner1D_2) => scanner1D_1.StringRepresentation == scanner1D_2.StringRepresentation;

            public static bool operator !=(BarcodeTypes scanner1D_1, BarcodeTypes scanner1D_2) => !(scanner1D_1 == scanner1D_2);

            public static bool operator ==(BarcodeTypes scanner1D, string scan) => scanner1D == new BarcodeTypes(scan);

            public static bool operator !=(BarcodeTypes scanner1D, string scan) => !(scanner1D == scan);

            public static implicit operator BarcodeTypes(string str)
            {
                return GetScannerFromStringName(str);
            }

            private static BarcodeTypes GetScannerFromStringName(string str)
            {
                if (str == EAN13.StringRepresentation)
                    return EAN13;
                if (str == UPCA.StringRepresentation)
                    return UPCA;
                if (str == UPCE.StringRepresentation)
                    return UPCE;
                if (str == EAN8.StringRepresentation)
                    return EAN8;

                return Null;
            }
        }

        /// <summary>
        /// Traite et donne accès aux informations contenues dans un code-barres à 1 dimension.
        /// </summary>
        public class BarcodeData
        {
            //Champs

            /// <summary>
            /// Contient le code du produit en entier, systemNumber et checkDigit compris.
            /// </summary>
            private string scanCode;

            private BarcodeTypes scanner;

            private int numberSystem2Digits = 2;


            //Propriétés

            /// <summary>
            /// Renvoie la validité du scancode
            /// </summary>
            public bool EstValide
            {
                get
                {
                    if (this.ScanCode == null || this.scanner == null)
                    {
                        return false;
                    }
                    if (this.scanner == BarcodeTypes.EAN13)
                    {
                        return this.scanCode.Length == 13;
                    }
                    if (this.scanner == BarcodeTypes.EAN8)
                    {
                        return this.scanCode.Length == 8;
                    }
                    if (this.scanner == BarcodeTypes.UPCA)
                    {
                        return this.scanCode.Length == 12;
                    }
                    if (this.scanner == BarcodeTypes.UPCE)
                    {
                        return this.scanCode.Length == 8 && (this.scanCode.FirstOrDefault().ToString() == "0" || this.scanCode.FirstOrDefault().ToString() == "1");
                    }

                    return false;
                }
            }


            /// <summary>
            /// Indique si le number system est à 2 ou 3 digits. Peut être à 1 seul digit si UPCA
            /// </summary>
            public bool IsNumberSystem2Digits
            {
                get => this.numberSystem2Digits == 2;
                set
                {
                    if (this.scanner != "upca" && this.scanner != "upce")
                    {
                        this.numberSystem2Digits = value ? 2 : 3;
                    }
                    else
                    {
                        this.numberSystem2Digits = 1;
                    }
                }
            }


            /// <summary>
            /// Renvoie le premier digit tu code utilisé notamment pour l'encodage et le calcul du check digit.
            /// </summary>
            public string FirstDigit
            {
                get
                {
                    if (this.scanner != null && this.ScanCode != null)
                    {
                        return this.scanner == BarcodeTypes.UPCA || this.scanner == BarcodeTypes.EAN8 ? "0" : this.scanCode.FirstOrDefault().ToString();
                    }

                    return null;
                }
            }

            /// <summary>
            /// 1, 2 ou 3 digits, représente le pays d'où est commercialisé le produit
            /// </summary>
            public string NumberSystem
            {
                get
                {
                    if (this.scanCode != null && this.scanCode != string.Empty)
                    {
                        string str = "";

                        if (this.scanner == BarcodeTypes.EAN13 || this.scanner == BarcodeTypes.UPCA || this.scanner == BarcodeTypes.EAN8)
                        {
                            for (int i = 0; i < this.numberSystem2Digits; i++)
                            {
                                str += this.scanCode[i].ToString();
                            }
                        }
                        else if (this.scanner == BarcodeTypes.UPCE)
                        {
                            str = this.scanCode[0].ToString();
                        }

                        return str;
                    }

                    return null;
                }
            }


            /// <summary>
            /// Le code lié au producteur qui vend le produit, donné par l'autorité liée au number system
            /// </summary>
            public string ManufacturerCode
            {
                get
                {
                    if (this.scanCode != null && this.scanCode != string.Empty)
                    {
                        string str = "";

                        if (this.scanner == BarcodeTypes.EAN13 || this.scanner == BarcodeTypes.UPCA)
                        {
                            for (int i = this.numberSystem2Digits; i < this.numberSystem2Digits + 5; i++)
                            {
                                str += this.scanCode[i].ToString();
                            }

                        }
                        else if (this.scanner == BarcodeTypes.UPCE)
                        {
                            str = GetUPCAManufactProductFromUPCE().Substring(0, 5);
                        }
                        return str;
                    }

                    return null;
                }
            }

            /// <summary>
            /// Code du produit donné par le fabriquant.
            /// </summary>
            public string ProductCode
            {
                get
                {
                    if (this.scanCode != null && this.scanCode != string.Empty)
                    {
                        string str = "";

                        if (this.scanner == BarcodeTypes.EAN13 || this.scanner == BarcodeTypes.UPCA)
                        {
                            int start = this.scanCode.Length == 13 ? 7 : 6;
                            for (int i = start; i < this.scanCode.Length - 1; i++)
                            {
                                str += this.scanCode[i].ToString();
                            }

                        }
                        else if (this.scanner == BarcodeTypes.EAN8)
                        {
                            for (int i = this.scanCode.Length - 1 - (this.IsNumberSystem2Digits ? 5 : 4); i < this.scanCode.Length - 1; i++)
                            {
                                str += this.scanCode[i].ToString();
                            }
                        }
                        else if (this.scanner == BarcodeTypes.UPCE)
                        {
                            str = GetUPCAManufactProductFromUPCE().Substring(5, 5);
                        }

                        return str;
                    }

                    return null;
                }
            }

            /// <summary>
            /// Check digit
            /// </summary>
            public string CheckDigit
            {
                get
                {
                    if (this.scanCode != null && this.scanCode != string.Empty)
                    {
                        return this.scanCode[this.scanCode.Length - 1].ToString();
                    }

                    return null;
                }
            }


            /// <summary>
            /// Code en entier du code-barres
            /// </summary>
            public string ScanCode
            {
                get
                {
                    if (this.scanCode != null && this.scanCode != string.Empty)
                    {
                        return this.scanCode;
                    }

                    return null;
                }
                set
                {
                    if (!CheckDigitOnly(value = value.Replace("-", "")))
                        return;

                    if (this.scanner == BarcodeTypes.UPCA)
                    {
                        if (value.Length == 11) //UPCA
                        {
                            value = value.Insert(0, "0");
                            value += "0";
                            value += Barcode1D.FindCheckDigit(ConvertStringToByteArray(value));
                            value = value.Remove(value.Length - 2, 1);
                            value = value.Remove(0, 1);

                            this.scanCode = value;
                        }
                        else if (value.Length == 12)
                        {
                            value = value.Insert(0, "0");

                            int moduloCheck = Barcode1D.FindCheckDigit(ConvertStringToByteArray(value));

                            value = value.Remove(0, 1);

                            if (moduloCheck.ToString() == value.LastOrDefault().ToString())
                            {
                                this.scanCode = value;
                            }
                            else
                            {
                                this.scanCode = value.Remove(value.Length - 1, 1) + moduloCheck.ToString();
                            }
                        }
                        else if (value.Length == 13)
                        {
                            if (value.First().ToString() == "0")
                            {
                                this.scanCode = value.Remove(0, 1);
                            }
                            else
                            {
                                this.scanCode = value;
                                this.scanner = BarcodeTypes.EAN13;
                            }
                        }
                    }
                    else if (this.scanner == BarcodeTypes.UPCE)
                    {
                        if (value.Length == 9)
                        {
                            value = value.Remove(0, 1);
                        }
                        else if (value.Length == 7)
                        {
                            value += FindCheckDigit(ConvertStringToByteArray("0" + value + "0"));
                        }
                        this.scanCode = value;
                    }
                    else if (this.scanner == BarcodeTypes.EAN13)
                    {
                        if (value.Length == 12)
                        {
                            value += FindCheckDigit(ConvertStringToByteArray(value + "0"));
                        }
                        else if (value.Length == 11)
                        {
                            value = "0" + value + FindCheckDigit(ConvertStringToByteArray("0" + value + "0"));
                        }
                        this.scanCode = value;
                    }
                    else if (this.scanner == BarcodeTypes.EAN8)
                    {
                        if (value.Length == 9)
                        {
                            value = value.Remove(0, 1);
                        }
                        else if (value.Length == 7)
                        {
                            value += FindCheckDigit(ConvertStringToByteArray("0" + value + "0"));
                        }
                        this.scanCode = value;
                    }
                    else
                    {
                        this.scanCode = value;
                    }

                    this.Barcode = this.scanner; //Check

                }
            }


            /// <summary>
            /// Renvoie le code en string qui peut être encodé.
            /// </summary>
            public string GetScanCodeToEncode
            {
                get
                {
                    if (this.scanner != null && this.ScanCode != null)
                    {
                        string stringToEncode = this.scanCode;

                        if (this.scanner == BarcodeTypes.EAN13)
                        {
                            stringToEncode = stringToEncode.Remove(0, 1);
                        }
                        else if (this.scanner == BarcodeTypes.UPCE)
                        {
                            stringToEncode = stringToEncode.Remove(0, 1);
                            stringToEncode = stringToEncode.Remove(stringToEncode.Length - 1);
                        }

                        return stringToEncode;
                    }

                    return null;
                }
            }


            /// <summary>
            /// Type de <see cref="BarcodeTypes"/> de ce <see cref="Barcode1D"/>
            /// </summary>
            public BarcodeTypes Barcode
            {
                get => this.scanner;
                set
                {
                    if (value == BarcodeTypes.Null)
                    {
                        this.scanner = value;
                    }
                    else if (this.ScanCode != null) //Si initialisation du scanner après l'initialisation du code
                    {
                        if (value == BarcodeTypes.UPCA) //upca non valide mais ean13 valide
                        {
                            if (this.scanCode.Length == 13 && this.scanCode.FirstOrDefault().ToString() != "0")
                            {
                                this.scanner = BarcodeTypes.EAN13;
                                this.numberSystem2Digits = this.numberSystem2Digits == 1 ? 2 : this.numberSystem2Digits;
                            }
                            else if (this.scanCode.Length == 12)
                            {
                                this.scanner = value;
                                this.numberSystem2Digits = 1;
                            }
                        }
                        else if (value == BarcodeTypes.EAN13)
                        {
                            if (this.scanCode.Length == 12)
                            {
                                this.scanCode = this.scanCode.Insert(0, "0");
                            }

                            this.scanner = value;
                            this.numberSystem2Digits = 2;
                        }
                        else
                        {
                            this.scanner = value;
                        }
                    }
                    else
                    {
                        this.scanner = value;
                    }
                }

            }


            public byte[] SetScanCode
            {
                set
                {
                    if (value != null)
                    {
                        string sValue = "";
                        for (int i = 0; i < value.Length; i++)
                        {
                            sValue += value[i].ToString();
                        }

                        this.ScanCode = sValue;
                    }
                }
            }



            //Constructeur 

            public BarcodeData(BarcodeTypes scanner)
            {
                this.scanner = scanner ?? BarcodeTypes.Null;

                this.numberSystem2Digits = this.scanner == BarcodeTypes.UPCA || this.scanner == BarcodeTypes.UPCE ? 1 : 2;
            }

            public BarcodeData(string code, BarcodeTypes scanner)
                : this(scanner)
            {
                this.ScanCode = code;
            }

            public BarcodeData(string systemNumber, string manufacturerNumber, string productCode, BarcodeTypes scanner)
                : this(systemNumber + manufacturerNumber + productCode, scanner)
            {
            }


            //Check digit only

            /// <summary>
            /// Renvoie true si le string entré en paramètre est un nombre positif.
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            private static bool CheckDigitOnly(string str)
            {
                //1er check, le string peut être plus long qu'un 'long' donc on est pas sûr que ce ne
                //soit pas un nbr meme si on arrive pas à la convertir en nbr. Examen plus poussé nécessaire
                if (str != null)
                {
                    if (ulong.TryParse(str, out _))
                    {
                        return true;
                    }
                    else if (long.TryParse(str, out _)) //Si le nbr est négatif, il est inexploitable
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                const int maxULongLength = 19; //En fait c'est 20 mais ça évite que le nombre soit encore trop grand pour le convertir en long

                string[] strs = new string[str.Length / maxULongLength + (str.Length % maxULongLength == 0 ? 0 : 1)];

                for (int i = 0; i < strs.Length - 1; i++)
                {
                    strs[i] = str.Substring(i * maxULongLength, maxULongLength);
                }
                strs[strs.Length - 1] = str.Substring(maxULongLength * (strs.Length - 1));

                return strs.All(s => ulong.TryParse(s, out _));
            }


            //Convertisseur

            /// <summary>
            /// Convertit ce <see cref="BarcodeData"/> sous une autre forme spécifiée. Attention, les transformations sont 
            /// irreversibles, en cas d'échec il faut manuellement convertir vers l'ancien barcodeType.
            /// </summary>
            /// <param name="scannerToConvertTo">Code-barres vers lequel convertir ce <see cref="BarcodeData"/></param>
            /// <param name="manufacturerOpt">Code du producteur, nécéssaire si on convertit depuis un EAN8</param>
            /// <returns>Renvoie <see langword="null"/> si la conversion a réussi, sinon un message d'erreur.</returns>
            public string ConvertBarcode(BarcodeTypes scannerToConvertTo, string manufacturerOpt = "")
            {
                string success = "Echec de la conversion, le BarcodeData n'est pas valide ou le code-barres vers lequel " +
                    "le convertir est le même qu'actuellement";

                if (scannerToConvertTo != null && this.EstValide && this.Barcode != scannerToConvertTo)
                {
                    if (scannerToConvertTo == BarcodeTypes.EAN8)
                    {
                        if (this.scanner == BarcodeTypes.UPCE)
                        {
                            success = this.ConvertToUPCAFromUPCE();
                        }
                        if (this.scanner == BarcodeTypes.UPCA)
                        {
                            success = ConvertToEAN13FromUPCA();
                        }

                        if (success == null)
                        {
                            success = this.ConvertToEAN8FromEAN13();
                        }
                    }
                    else if (scannerToConvertTo == BarcodeTypes.EAN13 || scannerToConvertTo == BarcodeTypes.UPCA)
                    {
                        if (this.scanner == BarcodeTypes.EAN8)
                        {
                            success = this.ConvertToEAN13FromEAN8(manufacturerOpt);

                            if (success == null && scannerToConvertTo == BarcodeTypes.UPCA)
                            {
                                success = ConvertToUPCAFromEAN13();
                            }
                        }
                        else if (this.scanner == BarcodeTypes.UPCE)
                        {
                            success = this.ConvertToUPCAFromUPCE();

                            if (success == null && scannerToConvertTo == BarcodeTypes.EAN13)
                            {
                                success = ConvertToEAN13FromUPCA();
                            }
                        }
                        else if (this.scanner == BarcodeTypes.EAN13)
                        {
                            success = ConvertToUPCAFromEAN13();
                        }
                        else if (this.scanner == BarcodeTypes.UPCA)
                        {
                            success = ConvertToEAN13FromUPCA();
                        }
                    }
                    else if (scannerToConvertTo == BarcodeTypes.UPCE)
                    {
                        if (this.scanner == BarcodeTypes.EAN13 || this.scanner == BarcodeTypes.UPCA)
                        {
                            if (scannerToConvertTo == BarcodeTypes.EAN13)
                            {
                                success = ConvertToUPCAFromEAN13();
                            }
                            if (success == null)
                            {
                                success = this.ConvertToUPCEFromUPCA();
                            }
                        }
                        else
                        {
                            success = this.ConvertToEAN13FromEAN8(manufacturerOpt);

                            if (success == null)
                            {
                                success = this.ConvertToUPCEFromUPCA();
                            }
                        }
                    }
                }

                return success;
            }



            /// <summary>
            /// Convertit le scanner EAN8 sous la forme d'un EAN13 à partir du code du producteur. Renvoie un bool indiquant le succès ou non de l'opération.
            /// </summary>
            /// <param name="manufacturerCode"></param>
            /// <returns></returns>
            private string ConvertToEAN13FromEAN8(string manufacturerCode)
            {
                if (this.scanner == BarcodeTypes.EAN8)
                {
                    int length = 7 - this.numberSystem2Digits;
                    if (manufacturerCode.Length == length)
                    {
                        this.scanCode = this.scanCode.Insert(this.IsNumberSystem2Digits ? 2 : 3, manufacturerCode);
                        this.scanner = BarcodeTypes.EAN13;
                        return null;
                    }
                    else
                    {
                        return $"Impossible de convertir en EAN13 car le code du producteur entré est invalide : " +
                            $"\nTaille requise : {length}\nTaille du code entré en paramètre : {manufacturerCode.Length}";
                    }
                }


                return "Erreur interne";
            }

            /// <summary>
            /// Convertit le scanner UPCA sous la forme d'un EAN13. Renvoie un bool indiquant le succès ou non de l'opération.
            /// </summary>
            private string ConvertToEAN13FromUPCA()
            {
                if (this.Barcode == BarcodeTypes.UPCA)
                {
                    this.scanCode = this.scanCode.Insert(0, "0");
                    this.numberSystem2Digits = 2;
                    this.Barcode = BarcodeTypes.EAN13;

                    return null;
                }

                return "Erreur interne";
            }


            /// <summary>
            /// Convertit le scanner EAN13 sous la forme d'un UPCA. Renvoie un bool indiquant le succès ou non de l'opération.
            /// </summary>
            private string ConvertToUPCAFromEAN13()
            {
                if (this.scanner == BarcodeTypes.EAN13 && scanCode.FirstOrDefault().ToString() == "0")
                {
                    this.scanCode = this.scanCode.Remove(0, 1);
                    this.numberSystem2Digits = 1;
                    this.Barcode = BarcodeTypes.UPCA;

                    return null;
                }
                else if (this.scanner == BarcodeTypes.EAN13)
                {
                    return "Impossible de convertir en UPCA car le 1er digit est différent de 0";
                }

                return "Erreur interne";
            }

            /// <summary>
            /// Convertit le scanner UPCE sous la forme d'un UPCA. Renvoie un bool indiquant le succès ou non de l'opération.
            /// </summary>
            private string ConvertToUPCAFromUPCE()
            {
                string upca = GetUPCAManufactProductFromUPCE();

                upca = upca.Insert(0, this.scanCode.FirstOrDefault().ToString()) + this.CheckDigit;

                this.scanner = BarcodeTypes.UPCA;
                this.ScanCode = upca;

                return null;
            }

            /// <summary>
            /// Renvoie le code du producteur et le code du produit d'un UPCE
            /// </summary>
            /// <returns></returns>
            private string GetUPCAManufactProductFromUPCE()
            {
                //4 formats possibles pour la conversion, pour retrouver le bon format il suffit de regarder le dernier digit
                //ABX00-00HIJ <= ABHIJX pour X = 0, X = 1, X = 2
                //ABX00-000IJ <= ABXIJ3 pour X >= 3 et X <= 9
                //ABCD0-0000J <= ABCDJ4
                //ABCDE-0000X <= ABCDEX pour X >= 5 et X <= 9

                string code = this.scanCode.Substring(1, 6);
                byte lastDigit = (byte)char.GetNumericValue(code.LastOrDefault());

                string upca = "";

                if (lastDigit <= 2)
                {
                    upca += code.Substring(0, 2);
                    upca += lastDigit;
                    upca += "0000";
                    upca += code.Substring(2, 3);
                }
                else if (lastDigit == 3)
                {
                    upca += code.Substring(0, 3);
                    upca += "00000";
                    upca += code.Substring(3, 2);
                }
                else if (lastDigit == 4)
                {
                    upca += code.Substring(0, 4);
                    upca += "00000";
                    upca += code[code.Length - 2];
                }
                else
                {
                    upca += code.Substring(0, 5);
                    upca += "0000";
                    upca += lastDigit;
                }

                return upca;
            }


            /// <summary>
            /// Convertit le scanner UPCA sous la forme d'un UPCE. Renvoie un bool indiquant le succès ou non de l'opération.
            /// </summary>
            private string ConvertToUPCEFromUPCA()
            {
                //4 formats possibles pour la conversion = 16 possibilités
                //ABX00-00HIJ => ABHIJX pour X >= 0 et X <= 2
                //ABX00-000IJ => ABXIJ3 pour X >= 3 et X <= 9
                //ABCD0-0000J => ABCDJ4
                //ABCDE-0000X => ABCDEX pour X >= 5 et X <= 9

                if (this.numberSystem2Digits != 1) //Format UPCA, EAN13 avec systemNumb à 1 digit
                {
                    return "Erreur, impossible de convertir en UPCE";
                }

                string code = this.ManufacturerCode + this.ProductCode;

                for (int i = 0; i < ConvertUPCaToUPCeFormat.Length; i++)
                {
                    for (int x = ConvertUPCaToUPCeRange[i][0]; x <= ConvertUPCaToUPCeRange[i][1]; x++)
                    {
                        string codeFormatX = "";
                        string format = ConvertUPCaToUPCeFormat[i][0].Replace('X', x.ToString().ToCharArray().FirstOrDefault());

                        for (int j = 0; j < format.Length; j++)
                        {
                            if (format[j].ToString() == code[j].ToString() || (code[j] != 0 && format[j] >= 'A') || code[j].ToString() == "0")
                            {
                                codeFormatX += format[j];
                            }
                            else
                            {
                                codeFormatX = "";
                                break; //Echec
                            }
                        }

                        if (format == codeFormatX) //On a trouvé le bon format, on retraduit en digits mnt
                        {
                            string newFormat = ConvertUPCaToUPCeFormat[i][1].Replace('X', x.ToString().ToCharArray().FirstOrDefault());
                            string newString = "";

                            for (int l = 0; l < newFormat.Length; l++)
                            {
                                if (char.IsDigit(newFormat[l]))
                                {
                                    newString += newFormat[l].ToString();
                                }
                                else if (newFormat[l] >= 'A')
                                {
                                    newString += code[newFormat[l] - 'A'];
                                }
                            }
                            newString = newString.Insert(0, "0");
                            newString += this.CheckDigit;

                            this.scanner = BarcodeTypes.UPCE;
                            this.scanCode = newString;

                            return null;
                        }

                    }

                }

                return "Erreur, impossible de convertir en UPCE, le code UPCA fournit n'est pas compatible avec le format UPCE.";
            }


            /// <summary>
            /// Convertit un <see cref="BarcodeTypes.EAN13"/> sous la forme EAN8. Renvoie un bool indiquant le succès ou non de l'opération.
            /// </summary>
            /// <returns></returns>
            private string ConvertToEAN8FromEAN13()
            {
                if (this.scanner == BarcodeTypes.EAN13 && this.numberSystem2Digits == 2)
                {
                    int startIndex = this.numberSystem2Digits;
                    int endIndex = 7 - startIndex;
                    this.scanCode = this.scanCode.Remove(startIndex, endIndex);

                    this.scanner = BarcodeTypes.EAN8;

                    return null;
                }
                else if (this.numberSystem2Digits == 3)
                {
                    return "Impossible de convertir en EAN8 car le system number, composé actuellement de 3 digit, est trop long";
                }

                return "Erreur interne";
            }


            /// <summary>
            /// 4 formats possibles UPCA convertibles en UPCE
            /// </summary>
            private static readonly string[][] ConvertUPCaToUPCeFormat = new string[][]
            {
                new string[] { "ABX0000HIJ", "ABHIJX" }, //upca , upce
                new string[] { "ABX00000IJ", "ABXIJ3" },
                new string[] { "ABCD00000J", "ABCDJ4" },
                new string[] { "ABCDE0000X", "ABCDEX" },
            };

            /// <summary>
            /// Intervalles de la variable 'X' valides pour les 4 formats UPCA convertibles en UPCE
            /// </summary>
            private static readonly int[][] ConvertUPCaToUPCeRange = new int[][]
            {
                new int[] { 0, 2 },
                new int[] { 3, 9 },
                new int[] { 0, 0 }, //Pas de X, on teste une seule fois
                new int[] { 5, 9 },
            };

            public override string ToString()
            {
                return $"Code : {this.scanCode}\r\n" +
                    $"Code système : {this.NumberSystem}\r\n" +
                    $"Code manufactureur : {this.ManufacturerCode}\r\n" +
                    $"Code Produit : {this.ProductCode}\r\n" +
                    $"Type de code-barres : {this.scanner.StringRepresentation}\r\n";
            }
        }

        #endregion

    }
}