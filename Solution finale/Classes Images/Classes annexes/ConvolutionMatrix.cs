namespace Photoshop3000.Annexes
{
    /// <summary>
    /// Classe contenant des matrices de convolution pouvant s'appliquer à un <see cref="Filtre"/>
    /// </summary>
    internal class ConvolutionMatrix
    {
        //Champs et propriétés

        private readonly int width;
        private readonly int height;

        private float[][] matrix;

        /// <summary>
        /// Correctif lors du calcul de filtrage
        /// </summary>
        public int Correctif { get; private set; }

        /// <summary>
        /// Methode de filtrage : 0 = classique, 1 = bords ignorés, 2 = bords laissés tels quels.
        /// </summary>
        public int MethodeFiltrage { get; private set; }

        /// <summary>
        /// Types de matrice de convolution dispo.
        /// </summary>
        private static readonly float[][][] TypesMatrix = new float[][][]
        {
            #region Flou
            new float[][]
            {
                new float[] {0, 1, 1, 1, 0},
                new float[] {1, 1, 1, 1, 1},
                new float[] {1, 1, 1, 1, 1},
                new float[] {1, 1, 1, 1, 1},
                new float[] {0, 1, 1, 1, 0},
            },
            #endregion

            #region Flou de Mouvement
            new float[][]
            {  //Pas optimale pour la vitesse de calcul mais meilleur résultat pour une image floue jusqu'à présent
                new float[] {1, 0, 0, 0, 0, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0, 0, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0, 0, 0, 0, 0},
                new float[] {0, 0, 0, 1, 0, 0, 0, 0, 0},
                new float[] {0, 0, 0, 0, 1, 0, 0, 0, 0},
                new float[] {0, 0, 0, 0, 0, 1, 0, 0, 0},
                new float[] {0, 0, 0, 0, 0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 0, 0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 0, 0, 0, 0, 1},
            },
            #endregion

            #region Flou Gaussien 
            new float[][]
            {
                new float[] {0.077847f, 0.123317f, 0.077847f},
                new float[] {0.123317f, 0.195346f, 0.123317f},
                new float[] {0.077847f, 0.123317f, 0.077847f},
            },
            #endregion

            #region Repoussage Léger
            new float[][]
            {
                new float[] {0,1,0},
                new float[] {0,0,0},
                new float[] {0,-1,0},
            },
            #endregion

            #region Repoussage fort
            new float[][]
            {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 0, 0, 0},
                new float[] {0, 0, 0, -1, 0},
                new float[] {0, 0, 0, 0, -1},
            },
            #endregion
            
            #region Bords Renforcement
            new float[][]
            {
                new float[] {0,0,0},
                new float[] {-1,1,0},
                new float[] {0,0,0},
            },
            #endregion

            #region Bords Affaiblissement
            new float[][]
            {
                new float[] {-1, 0, 0, 0, 0},
                new float[] {0, -2, 0, 0, 0},
                new float[] {0, 0, 6, 0, 0},
                new float[] {0, 0, 0, -2, 0},
                new float[] {0, 0, 0, 0, -1},
            },
            #endregion

            #region Bords Detection
            new float[][]
            {
                new float[] {0,1,0},
                new float[] {1,-4,1},
                new float[] {0,1,0},
            },
            #endregion

            #region Sobel Y
            new float[][]
            {
                new float[] {-1,-2,-1},
                new float[] {0,0,0},
                new float[] {1,2,1},
            },
            #endregion

            #region Sobel X
            new float[][]
            {
                new float[] {-1,0,1},
                new float[] {-2,0,2},
                new float[] {-1,0,1},
            },
            #endregion

            #region Contraste
            new float[][]
            {
                new float[] {-2,-1,-0},
                new float[] {-1, 1, 1},
                new float[] {0, 1, 2},
            },
            #endregion

            #region Aiguiser
            new float[][]
            {
                new float[] {-1,-1,-1},
                new float[] {-1, 9,-1},
                new float[] {-1,-1,-1},
            },
            #endregion

            #region Dessin
            new float[][]
            {
                new float[] {0, 0, -0.3f, 0, 0},
                new float[] {0, 0,-1, 0, 0},
                new float[] {-0.3f, -1, 5.27f,-1, -0.3f},
                new float[] {0, 0,-1, 0, 0},
                new float[] {0, 0, -0.3f, 0, 0},
            },
            #endregion

            #region Lissage
            new float[][]
            {
                new float[] {2, 4, 5, 4, 2},
                new float[] {4, 9, 12, 9, 4},
                new float[] {5, 12, 15, 12, 5},
                new float[] {4, 9, 12, 9, 4},
                new float[] {2, 4, 5, 4, 2},
            },
            #endregion

            #region Test
            new float[][]
            {
                //Pas optimale pour la vitesse de calcul mais meilleur résultat pour une image floue jusqu'à présent
                new float[]{.393f, .349f, .272f, 0, 0},
                        new float[]{.769f, .686f, .534f, 0, 0},
                        new float[]{.189f, .168f, .131f, 0, 0},
                        new float[]{0, 0, 0, 1, 0},
                        new float[]{0, 0, 0, 0, 1}
            },
            #endregion

        };

        /// <summary>
        /// Récupère une copie de la matrice de convolution initialisée
        /// </summary>
        /// <returns></returns>
        public float[][] GetMatrix()
        {
            float[][] returnMatrix = new float[this.height][];

            for (int i = 0; i < this.height; i++)
            {
                returnMatrix[i] = new float[this.width];

                for (int j = 0; j < returnMatrix[i].Length; j++)
                {
                    returnMatrix[i][j] = this.matrix[i][j];
                }
            }

            return returnMatrix;
        }

        /// <summary>
        /// Set la matrice de convolution avec la matrice entrée en paramètre
        /// </summary>
        /// <param name="matrixConv"></param>
        private void SetMatrix(float[][] matrixConv)
        {
            this.matrix = new float[height][];
            for (int i = 0; i < height; i++)
            {
                this.matrix[i] = new float[width];

                for (int j = 0; j < this.matrix[i].Length; j++)
                {
                    this.matrix[i][j] = matrixConv[i][j];
                }
            }
        }



        //Constructeurs

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ConvolutionMatrix"/> à partir d'un tableau de tableau de <see cref="float"/>
        /// </summary>
        /// <param name="matrix">Matrice de convolution carrée</param>
        /// <param name="correctif">Valeur à ajouter à chaque pixel</param>
        public ConvolutionMatrix(float[][] matrix, int correctif = 0)
        {
            this.height = this.width = matrix.Length;
            this.Correctif = correctif;

            SetMatrix(matrix);
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ConvolutionMatrix"/> à partir d'un type <see cref="ImageFiltre"/> de <see cref="ConvolutionMatrix"/> spécifié
        /// </summary>
        /// <param name="t">Type de matrice de convolution</param>
        public ConvolutionMatrix(ImageFiltre t)
        {
            this.height = this.width = TypesMatrix[(int)t].Length; //Matrice carré, tous les tab d'un même tab font la meme longueur

            this.Correctif = 0;
            this.MethodeFiltrage = 0;

            if (t == ImageFiltre.Repoussage_Léger || t == ImageFiltre.Repoussage_Fort)
            {
                this.Correctif = 128;
            }
            if (t == ImageFiltre.Dessin)
            {
                this.MethodeFiltrage = 1;
            }

            SetMatrix(TypesMatrix[(int)t]);
        }


        /// <summary>
        /// Différents types de matrices pour le filtrage
        /// </summary>
        public enum ImageFiltre
        {
            /// <summary>
            /// Matrice de convolution 5x5 
            /// </summary>
            Flou = 0,

            /// <summary>
            /// Matrice de convolution 9x9
            /// </summary>
            Flou_De_Mouvement,

            /// <summary>
            /// Matrice de convolution 3x3 
            /// </summary>
            FlouGaussien,

            /// <summary>
            /// Matrice de convolution 3x3 
            /// </summary>
            Repoussage_Léger,

            /// <summary>
            /// Matrice de convolution 5x5 
            /// </summary>
            Repoussage_Fort,

            /// <summary>
            /// Matrice de convolution 3x3 
            /// </summary>
            Bords_Renforcement,

            /// <summary>
            /// Matrice de convolution 5x5 
            /// </summary>
            Bords_Affaiblissement,

            /// <summary>
            /// Matrice de convolution 3x3 
            /// </summary>
            Bords_Détection,

            /// <summary>
            /// Matrice de convolution 3x3 
            /// </summary>
            Sobel_Vertical,

            /// <summary>
            /// Matrice de convolution 3x3 
            /// </summary>
            Sobel_Horizontal,

            /// <summary>
            /// Matrice de convolution 3x3 
            /// </summary>
            Contraste,

            /// <summary>
            /// Matrice de convolution 3x3 
            /// </summary>
            Aiguiser,

            /// <summary>
            /// Matrice de convolution 5x5 
            /// </summary>
            Dessin,

            /// <summary>
            /// Matrice de convolution 5x5 
            /// </summary>
            Lissage,

            /// <summary>
            /// Test, à ignorer
            /// </summary>
            Test,
        }
    }
}