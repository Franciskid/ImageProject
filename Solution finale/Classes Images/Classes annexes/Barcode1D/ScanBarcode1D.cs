using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photoshop3000;

namespace Photoshop3000.Barcode
{
    /// <summary>
    /// Fournit des méthodes pour scanner et manipuler des code-barres GTIN (EAN-13, UPCA, UPCE, EAN-8)
    /// </summary>
    class ScanBarcode1D : Barcode1D
    {
        /// <summary>
        /// Clone de <see cref="Barcode1D.Image"/>.
        /// </summary>
        private MyImage imageToDecode;

        private int margeErreur = 20;

        //Propriétés

        /// <summary>
        /// Renvoie <see langword="true"/> si l'opération de scan à réussie, <see langword="false"/> sinon.
        /// </summary>
        public bool SuccessToScan { get; private set; } = false;

        /// <summary>
        /// Indique si le number system est à 2 ou 3 digits. Peut être à 1 seul digit si UPCA
        /// </summary>
        public bool IsNumberSystem2Digits
        {
            get => this.Data.IsNumberSystem2Digits;
            set => this.Data.IsNumberSystem2Digits = value;
        }

        /// <summary>
        /// Marge d'erreur de la largeur d'une barre du code-barres, représente en % la taille maximale et minimale
        /// d'une barre en fonction de la largeur moyenne des barres du code-barres à décoder. Entre 0% et 30% max.
        /// </summary>
        public int MargeErreur
        {
            get => this.margeErreur;
            set
            {
                if (value >= 0 && value <= 30)
                {
                    this.margeErreur = value;
                }
            }
        }


        //Constructeur

        /// <summary>
        /// Initialise une nouvelle instance en fonction d'un <see cref="MyImage"/> à décoder et optionnellement du <see cref="Barcode1D.BarcodeTypes"/> de cet MyImage.
        /// </summary>
        /// <param name="imageToDecode"></param>
        /// <param name="type"></param>
        public ScanBarcode1D(MyImage imageToDecode, BarcodeTypes type = null)
            : base(imageToDecode, new BarcodeData(type ?? BarcodeTypes.Null))
        {
        }


        //Scan

        /// <summary>
        /// Scan l'image en fonction du type de code barre utilisé.
        /// </summary>
        public void Scan()
        {
            if (this.Image == null)
                return;

            TransformImageToScan();

            if (this.Data.Barcode == null || this.Data.Barcode == BarcodeTypes.Null)
            {
                this.Data.Barcode = FindScanner();
            }

            int tries = 1;

            while (!this.SuccessToScan && tries <= 2)
            {
                byte[] encoded = ReductionLine(GetLineImage(this.imageToDecode.Height / 2));

                byte[] decoded = GetDecodedDigits(encoded);

                if (decoded == null)
                {
                    if (++tries <= 2)
                    {
                        new MyGraphics(this.imageToDecode).EffetMiroir(true); //On tente de décoder l'image dans l'autre sens
                    }
                }
                else
                {
                    this.SuccessToScan = true;
                    InitializeScanData(decoded);
                }
            }

        }


        /// <summary>
        /// Initialise le <see cref="ScanData"/> et check si le <see cref="scanner"/> correspond bien.
        /// </summary>
        /// <param name="decodedDigits"></param>
        private void InitializeScanData(byte[] decodedDigits)
        {
            this.Data.SetScanCode = decodedDigits;
        }



        /// <summary>
        /// Transforme l'image à scanner pour isoler le code-barres
        /// </summary>
        private void TransformImageToScan()
        {
            //Faire un algorithme pour détecter la position du code-barres dans l'image et le centrer dans une nouvelle image rognée.
            this.imageToDecode = base.Image.Clone();
        }

        /// <summary>
        /// Renvoie une ligne de l'image composée de 1 et de 0 à partir d'une hauteur donnée
        /// </summary>
        /// <param name="image"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private byte[] GetLineImage(int height)
        {
            byte[] line = new byte[this.imageToDecode.Width];
            for (int i = 0; i < line.Length; i++)
            {
                line[i] = (byte)(this.imageToDecode[height, i].Moyenne() <= 200 && this.imageToDecode[height, i].A > 0 ? 1 : 0);
            }

            return line;
        }

        /// <summary>
        /// Réduit le surplus d'information d'une ligne d'image et ne renvoie que les barres du codes barres sous forme de 1 et de 0. Un digit pour chaque barre
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private byte[] ReductionLine(byte[] line)
        {
            //long longLength = GetLengthBar(line, out int blankLength);
            int lengthBarR = LengthBar(line, out int blankLength);
            int lengthBlackBar = lengthBarR;// (short)longLength;
            int lengthWhiteBar = lengthBarR;// (short)(longLength >> 16);

            int marginErrorBlack = (int)Math.Round((decimal)lengthBlackBar * this.margeErreur / 100);
            int marginErrorWhite = (int)Math.Round((decimal)lengthWhiteBar * this.margeErreur / 100);

            byte[] bt = new byte[this.Data.Barcode.BarAmount];

            int indexLine = blankLength;

            for (int i = 0; i < bt.Length && indexLine < line.Length; i++)
            {
                byte currentColor = line[indexLine];
                int numberSameBitsInRow = 0;
                int lengthBar = currentColor == 1 ? lengthBlackBar : lengthWhiteBar;
                int marginError = currentColor == 1 ? marginErrorBlack : marginErrorWhite;

                while (indexLine < line.Length)
                {
                    numberSameBitsInRow++;

                    if (numberSameBitsInRow - marginError > lengthBar) //Au dessus marge erreur
                    {
                        if (line[indexLine + 1] == currentColor)
                        {
                            while (line[++indexLine] == currentColor && indexLine < line.Length)
                            {
                                numberSameBitsInRow++;
                            }
                            int numberDigit = (int)Math.Round((Decimal)numberSameBitsInRow / lengthBar);
                            numberDigit = numberDigit >= 5 ? 4 : numberDigit; //4 max, constant

                            for (int k = 0; k < numberDigit; k++)
                            {
                                bt[i] = currentColor;
                                if (k != numberDigit - 1)
                                    i++;
                            }
                            //indexLine -= marginError;
                        }
                        else
                        {
                            bt[i] = currentColor;
                        }
                        break;
                    }
                    else if (numberSameBitsInRow + marginError < lengthBar) //En dessous marge erreur
                    {
                        if (line[++indexLine] != currentColor) //Couleur diff, problème
                        {
                            return null; //Pas valide
                        }
                    }
                    else //Entre les 2 marges d'erreurs
                    {
                        if (line[++indexLine] != currentColor) //Couleur diff, on change
                        {
                            bt[i] = currentColor;
                            break;
                        }

                    }
                }
            }

            return bt;
        }

        /// <summary>
        /// Détermine la taille moyenne d'une barre et renvoie la taille de l'espace blanc avant la 1ere barre noire.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="blank"></param>
        /// <returns></returns>
        private int LengthBar(byte[] line, out int quietZone)
        {
            quietZone = 0;

            int blankLeft = 0;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == 0)
                {
                    quietZone++;
                }
                else
                {
                    break;
                }
            }
            for (int i = line.Length - 1; i >= 0; i--)
            {
                if (line[i] == 0)
                {
                    blankLeft++;
                }
                else
                {
                    break;
                }
            }

            return (int)Math.Round((Decimal)(line.Length - quietZone - blankLeft) / this.Data.Barcode.BarAmount);
        }


        /// <summary>
        /// Essaye de détecter le <see cref="Barcode1D.BarcodeTypes"/> sur cette image
        /// </summary>
        /// <returns></returns>
        private BarcodeTypes FindScanner()
        {
            if (this.imageToDecode == null)
                return "null";

            int blankLeft = 0, blankRight = 0;

            byte[] line = GetLineImage(this.imageToDecode.Height / 2);

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == 0)
                {
                    blankLeft++;
                }
                else
                {
                    break;
                }
            }

            int lengthBar = 0;
            for (int i = blankLeft; i < line.Length; i++)
            {
                if (line[i] == 1)
                {
                    lengthBar++;
                }
                else
                {
                    break;
                }
            }

            for (int i = line.Length - 1; i >= 0; i--)
            {
                if (line[i] == 0)
                {
                    blankRight++;
                }
                else
                {
                    break;
                }
            }

            int estimatedAmountBar = (line.Length - blankLeft - blankRight) / (lengthBar == 0 ? 1 : lengthBar);

            int margeErreur = estimatedAmountBar * 10 / 100; //10%

            List<BarcodeTypes> list = new List<BarcodeTypes>()
            {
                BarcodeTypes.EAN13,
                BarcodeTypes.EAN8,
                BarcodeTypes.UPCA,
                BarcodeTypes.UPCE,
            };

            foreach (BarcodeTypes sc in list)
            {
                if (estimatedAmountBar + margeErreur >= sc.BarAmount && estimatedAmountBar - margeErreur <= sc.BarAmount)
                {
                    return sc;
                }
            }

            return "null";
        }

    }
}

