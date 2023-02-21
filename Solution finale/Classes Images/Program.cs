using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Photoshop3000
{
    static class Program
    {
        public static readonly string ADRESSE_SAUVEGARDE =    //Insérer chemin jusqu'au dossier de sauvegarde contenant les fichiers
             Directory.GetParent(Directory.GetParent(Directory.GetParent(@"...").FullName).FullName).FullName + @"\Sauvegarde\";

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
