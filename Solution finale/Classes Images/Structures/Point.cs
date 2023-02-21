namespace Photoshop3000
{
    /// <summary>
    /// Représente une coordonnée flottante dans un plan à 2 dimensions.
    /// </summary>
    public struct Point
    {
        //Champs et propriétés

        /// <summary>
        /// Point de base (int.min, int.min) = 1 << 31
        /// </summary>
        public static readonly Point Zero = new Point(int.MinValue, int.MinValue);

        private double x, y;

        /// <summary>
        /// Position sur l'axe des abscisses
        /// </summary>
        public double X
        {
            get => this.x;
            set => this.x = value;
        }

        /// <summary>
        /// Position sur l'axe des ordonnées
        /// </summary>
        public double Y
        {
            get => this.y;
            set => this.y = value;
        }


        //Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la structure <see cref="Point"/> avec les coordonnées spécifiées
        /// </summary>
        /// <param name="y">Axe des ordonnées</param>
        /// <param name="x">Axe des abscisses</param>
        public Point(double y, double x)
        {
            this.x = x;
            this.y = y;
        }


        //Opérateurs

        /// <summary>
        /// Renvoie <see langword="true"/> si les 2 <see cref="Point"/> passés en paramètres ont les mêmes coordonnées
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Point left, Point right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        /// <summary>
        /// Renvoie <see langword="true"/> si les 2 <see cref="Point"/> passés en paramètres ont des coordonnées différentes
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Retourne <see langword="true"/> si le <see cref="Point"/> passé en paramètres a les même coordonnées que cette instance
        /// </summary>
        /// <param name="obj">Objet <see cref="Point"/></param>
        /// <returns>this == obj as Point</returns>
        public override bool Equals(object obj)
        {
            return obj is Point p && this == p;
        }


        /// <summary>
        /// Echange les coordonnées en x et y d'un <see cref="Point"/>
        /// </summary>
        public void SwapXY()
        {
            double temp = this.Y;
            this.Y = this.X;
            this.X = temp;
        }
    }
}
