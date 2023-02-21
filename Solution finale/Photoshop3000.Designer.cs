using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Solution_finale
{
    partial class Photoshop3000
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Photoshop3000));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sauvegarderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pixélisationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.couleursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.détectionContoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.détectionDesContoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cannyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.renforcementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.affaiblissementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dessinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gravureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repoussageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.faibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aiguisageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contrasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flouToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mouvementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gaussienToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lissageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bruitNumériqueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coloréToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selEtPoivreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grenailleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fractalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.créationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copieImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dessinsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.effetMiroirToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.doubleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.quadrupleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.effetPeintureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stéganographieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paramètresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.param = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Clear = new System.Windows.Forms.Button();
            this.Settings = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SaveButton = new System.Windows.Forms.Button();
            this.Originale = new System.Windows.Forms.TabPage();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.filtresToolStripMenuItem,
            this.fractalesToolStripMenuItem,
            this.créationToolStripMenuItem,
            this.stéganographieToolStripMenuItem,
            this.infosToolStripMenuItem,
            this.paramètresToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(902, 31);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirToolStripMenuItem,
            this.sauvegarderToolStripMenuItem,
            this.quitterToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(71, 27);
            this.fichierToolStripMenuItem.Text = "Fichier";
            // 
            // ouvrirToolStripMenuItem
            // 
            this.ouvrirToolStripMenuItem.Name = "ouvrirToolStripMenuItem";
            this.ouvrirToolStripMenuItem.Size = new System.Drawing.Size(220, 28);
            this.ouvrirToolStripMenuItem.Text = "Ouvrir";
            this.ouvrirToolStripMenuItem.Click += new System.EventHandler(this.ouvrirToolStripMenuItem_Click);
            // 
            // sauvegarderToolStripMenuItem
            // 
            this.sauvegarderToolStripMenuItem.Enabled = false;
            this.sauvegarderToolStripMenuItem.Name = "sauvegarderToolStripMenuItem";
            this.sauvegarderToolStripMenuItem.Size = new System.Drawing.Size(220, 28);
            this.sauvegarderToolStripMenuItem.Text = "Sauvegarder sous";
            this.sauvegarderToolStripMenuItem.Click += new System.EventHandler(this.sauvegarderToolStripMenuItem_Click);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(220, 28);
            this.quitterToolStripMenuItem.Text = "Quitter";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // filtresToolStripMenuItem
            // 
            this.filtresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pixélisationToolStripMenuItem,
            this.couleursToolStripMenuItem,
            this.détectionContoursToolStripMenuItem,
            this.dessinToolStripMenuItem,
            this.gravureToolStripMenuItem,
            this.repoussageToolStripMenuItem,
            this.aiguisageToolStripMenuItem,
            this.contrasteToolStripMenuItem,
            this.flouToolStripMenuItem,
            this.lissageToolStripMenuItem,
            this.bruitNumériqueToolStripMenuItem});
            this.filtresToolStripMenuItem.Name = "filtresToolStripMenuItem";
            this.filtresToolStripMenuItem.Size = new System.Drawing.Size(66, 27);
            this.filtresToolStripMenuItem.Text = "Filtres";
            // 
            // pixélisationToolStripMenuItem
            // 
            this.pixélisationToolStripMenuItem.Name = "pixélisationToolStripMenuItem";
            this.pixélisationToolStripMenuItem.Size = new System.Drawing.Size(281, 28);
            this.pixélisationToolStripMenuItem.Text = "Pixélisation";
            this.pixélisationToolStripMenuItem.Click += new System.EventHandler(this.pixélisationToolStripMenuItem_Click);
            // 
            // couleursToolStripMenuItem
            // 
            this.couleursToolStripMenuItem.Name = "couleursToolStripMenuItem";
            this.couleursToolStripMenuItem.Size = new System.Drawing.Size(281, 28);
            this.couleursToolStripMenuItem.Text = "Modification des couleurs";
            this.couleursToolStripMenuItem.Click += new System.EventHandler(this.couleursToolStripMenuItem_Click);
            // 
            // détectionContoursToolStripMenuItem
            // 
            this.détectionContoursToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.détectionDesContoursToolStripMenuItem,
            this.sobelToolStripMenuItem1,
            this.cannyToolStripMenuItem1,
            this.renforcementToolStripMenuItem,
            this.affaiblissementToolStripMenuItem});
            this.détectionContoursToolStripMenuItem.Name = "détectionContoursToolStripMenuItem";
            this.détectionContoursToolStripMenuItem.Size = new System.Drawing.Size(281, 28);
            this.détectionContoursToolStripMenuItem.Text = "Contours";
            // 
            // détectionDesContoursToolStripMenuItem
            // 
            this.détectionDesContoursToolStripMenuItem.Name = "détectionDesContoursToolStripMenuItem";
            this.détectionDesContoursToolStripMenuItem.Size = new System.Drawing.Size(263, 28);
            this.détectionDesContoursToolStripMenuItem.Text = "Détection des contours";
            this.détectionDesContoursToolStripMenuItem.Click += new System.EventHandler(this.détectionDesContoursToolStripMenuItem_Click);
            // 
            // sobelToolStripMenuItem1
            // 
            this.sobelToolStripMenuItem1.Name = "sobelToolStripMenuItem1";
            this.sobelToolStripMenuItem1.Size = new System.Drawing.Size(263, 28);
            this.sobelToolStripMenuItem1.Text = "Sobel";
            this.sobelToolStripMenuItem1.Click += new System.EventHandler(this.sobelToolStripMenuItem_Click);
            // 
            // cannyToolStripMenuItem1
            // 
            this.cannyToolStripMenuItem1.Name = "cannyToolStripMenuItem1";
            this.cannyToolStripMenuItem1.Size = new System.Drawing.Size(263, 28);
            this.cannyToolStripMenuItem1.Text = "Canny";
            this.cannyToolStripMenuItem1.Click += new System.EventHandler(this.cannyToolStripMenuItem_Click);
            // 
            // renforcementToolStripMenuItem
            // 
            this.renforcementToolStripMenuItem.Name = "renforcementToolStripMenuItem";
            this.renforcementToolStripMenuItem.Size = new System.Drawing.Size(263, 28);
            this.renforcementToolStripMenuItem.Text = "Renforcement";
            this.renforcementToolStripMenuItem.Click += new System.EventHandler(this.renforcementToolStripMenuItem_Click);
            // 
            // affaiblissementToolStripMenuItem
            // 
            this.affaiblissementToolStripMenuItem.Name = "affaiblissementToolStripMenuItem";
            this.affaiblissementToolStripMenuItem.Size = new System.Drawing.Size(263, 28);
            this.affaiblissementToolStripMenuItem.Text = "Affaiblissement";
            this.affaiblissementToolStripMenuItem.Click += new System.EventHandler(this.affaiblissementToolStripMenuItem_Click);
            // 
            // dessinToolStripMenuItem
            // 
            this.dessinToolStripMenuItem.Name = "dessinToolStripMenuItem";
            this.dessinToolStripMenuItem.Size = new System.Drawing.Size(281, 28);
            this.dessinToolStripMenuItem.Text = "Dessin";
            this.dessinToolStripMenuItem.Click += new System.EventHandler(this.dessinToolStripMenuItem_Click);
            // 
            // gravureToolStripMenuItem
            // 
            this.gravureToolStripMenuItem.Name = "gravureToolStripMenuItem";
            this.gravureToolStripMenuItem.Size = new System.Drawing.Size(281, 28);
            this.gravureToolStripMenuItem.Text = "Gravure";
            this.gravureToolStripMenuItem.Click += new System.EventHandler(this.gravureToolStripMenuItem_Click);
            // 
            // repoussageToolStripMenuItem
            // 
            this.repoussageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.faibleToolStripMenuItem,
            this.fortToolStripMenuItem});
            this.repoussageToolStripMenuItem.Name = "repoussageToolStripMenuItem";
            this.repoussageToolStripMenuItem.Size = new System.Drawing.Size(281, 28);
            this.repoussageToolStripMenuItem.Text = "Repoussage";
            // 
            // faibleToolStripMenuItem
            // 
            this.faibleToolStripMenuItem.Name = "faibleToolStripMenuItem";
            this.faibleToolStripMenuItem.Size = new System.Drawing.Size(129, 28);
            this.faibleToolStripMenuItem.Text = "Faible";
            this.faibleToolStripMenuItem.Click += new System.EventHandler(this.faibleToolStripMenuItem_Click);
            // 
            // fortToolStripMenuItem
            // 
            this.fortToolStripMenuItem.Name = "fortToolStripMenuItem";
            this.fortToolStripMenuItem.Size = new System.Drawing.Size(129, 28);
            this.fortToolStripMenuItem.Text = "Fort";
            this.fortToolStripMenuItem.Click += new System.EventHandler(this.fortToolStripMenuItem_Click);
            // 
            // aiguisageToolStripMenuItem
            // 
            this.aiguisageToolStripMenuItem.Name = "aiguisageToolStripMenuItem";
            this.aiguisageToolStripMenuItem.Size = new System.Drawing.Size(281, 28);
            this.aiguisageToolStripMenuItem.Text = "Aiguisage";
            this.aiguisageToolStripMenuItem.Click += new System.EventHandler(this.aiguisageToolStripMenuItem_Click);
            // 
            // contrasteToolStripMenuItem
            // 
            this.contrasteToolStripMenuItem.Name = "contrasteToolStripMenuItem";
            this.contrasteToolStripMenuItem.Size = new System.Drawing.Size(281, 28);
            this.contrasteToolStripMenuItem.Text = "Contraste";
            this.contrasteToolStripMenuItem.Click += new System.EventHandler(this.contrasteToolStripMenuItem_Click);
            // 
            // flouToolStripMenuItem
            // 
            this.flouToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalToolStripMenuItem,
            this.mouvementToolStripMenuItem,
            this.gaussienToolStripMenuItem});
            this.flouToolStripMenuItem.Name = "flouToolStripMenuItem";
            this.flouToolStripMenuItem.Size = new System.Drawing.Size(281, 28);
            this.flouToolStripMenuItem.Text = "Flou";
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(178, 28);
            this.normalToolStripMenuItem.Text = "Normal";
            this.normalToolStripMenuItem.Click += new System.EventHandler(this.normalToolStripMenuItem_Click);
            // 
            // mouvementToolStripMenuItem
            // 
            this.mouvementToolStripMenuItem.Name = "mouvementToolStripMenuItem";
            this.mouvementToolStripMenuItem.Size = new System.Drawing.Size(178, 28);
            this.mouvementToolStripMenuItem.Text = "Mouvement";
            this.mouvementToolStripMenuItem.Click += new System.EventHandler(this.mouvementToolStripMenuItem_Click);
            // 
            // gaussienToolStripMenuItem
            // 
            this.gaussienToolStripMenuItem.Name = "gaussienToolStripMenuItem";
            this.gaussienToolStripMenuItem.Size = new System.Drawing.Size(178, 28);
            this.gaussienToolStripMenuItem.Text = "Gaussien";
            this.gaussienToolStripMenuItem.Click += new System.EventHandler(this.gaussienToolStripMenuItem_Click);
            // 
            // lissageToolStripMenuItem
            // 
            this.lissageToolStripMenuItem.Name = "lissageToolStripMenuItem";
            this.lissageToolStripMenuItem.Size = new System.Drawing.Size(281, 28);
            this.lissageToolStripMenuItem.Text = "Lissage";
            this.lissageToolStripMenuItem.Click += new System.EventHandler(this.lissageToolStripMenuItem_Click);
            // 
            // bruitNumériqueToolStripMenuItem
            // 
            this.bruitNumériqueToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.coloréToolStripMenuItem,
            this.selEtPoivreToolStripMenuItem,
            this.grenailleToolStripMenuItem});
            this.bruitNumériqueToolStripMenuItem.Name = "bruitNumériqueToolStripMenuItem";
            this.bruitNumériqueToolStripMenuItem.Size = new System.Drawing.Size(281, 28);
            this.bruitNumériqueToolStripMenuItem.Text = "Bruit numérique";
            // 
            // coloréToolStripMenuItem
            // 
            this.coloréToolStripMenuItem.Name = "coloréToolStripMenuItem";
            this.coloréToolStripMenuItem.Size = new System.Drawing.Size(180, 28);
            this.coloréToolStripMenuItem.Text = "Coloré";
            this.coloréToolStripMenuItem.Click += new System.EventHandler(this.coloréToolStripMenuItem_Click);
            // 
            // selEtPoivreToolStripMenuItem
            // 
            this.selEtPoivreToolStripMenuItem.Name = "selEtPoivreToolStripMenuItem";
            this.selEtPoivreToolStripMenuItem.Size = new System.Drawing.Size(180, 28);
            this.selEtPoivreToolStripMenuItem.Text = "Sel et poivre";
            this.selEtPoivreToolStripMenuItem.Click += new System.EventHandler(this.selEtPoivreToolStripMenuItem_Click);
            // 
            // grenailleToolStripMenuItem
            // 
            this.grenailleToolStripMenuItem.Name = "grenailleToolStripMenuItem";
            this.grenailleToolStripMenuItem.Size = new System.Drawing.Size(180, 28);
            this.grenailleToolStripMenuItem.Text = "Grenaille";
            this.grenailleToolStripMenuItem.Click += new System.EventHandler(this.grenailleToolStripMenuItem_Click);
            // 
            // fractalesToolStripMenuItem
            // 
            this.fractalesToolStripMenuItem.Name = "fractalesToolStripMenuItem";
            this.fractalesToolStripMenuItem.Size = new System.Drawing.Size(88, 27);
            this.fractalesToolStripMenuItem.Text = "Fractales";
            this.fractalesToolStripMenuItem.Click += new System.EventHandler(this.fractalesToolStripMenuItem_Click);
            // 
            // créationToolStripMenuItem
            // 
            this.créationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copieImageToolStripMenuItem,
            this.dessinsToolStripMenuItem,
            this.effetMiroirToolStripMenuItem1,
            this.effetPeintureToolStripMenuItem});
            this.créationToolStripMenuItem.Name = "créationToolStripMenuItem";
            this.créationToolStripMenuItem.Size = new System.Drawing.Size(87, 27);
            this.créationToolStripMenuItem.Text = "Création";
            // 
            // copieImageToolStripMenuItem
            // 
            this.copieImageToolStripMenuItem.Name = "copieImageToolStripMenuItem";
            this.copieImageToolStripMenuItem.Size = new System.Drawing.Size(349, 28);
            this.copieImageToolStripMenuItem.Text = "Copier une image dans une image";
            this.copieImageToolStripMenuItem.Click += new System.EventHandler(this.copieImageToolStripMenuItem_Click);
            // 
            // dessinsToolStripMenuItem
            // 
            this.dessinsToolStripMenuItem.Name = "dessinsToolStripMenuItem";
            this.dessinsToolStripMenuItem.Size = new System.Drawing.Size(349, 28);
            this.dessinsToolStripMenuItem.Text = "Dessins";
            this.dessinsToolStripMenuItem.Click += new System.EventHandler(this.dessinsToolStripMenuItem_Click);
            // 
            // effetMiroirToolStripMenuItem1
            // 
            this.effetMiroirToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simpleToolStripMenuItem1,
            this.doubleToolStripMenuItem1,
            this.quadrupleToolStripMenuItem1});
            this.effetMiroirToolStripMenuItem1.Name = "effetMiroirToolStripMenuItem1";
            this.effetMiroirToolStripMenuItem1.Size = new System.Drawing.Size(349, 28);
            this.effetMiroirToolStripMenuItem1.Text = "Effet miroir";
            // 
            // simpleToolStripMenuItem1
            // 
            this.simpleToolStripMenuItem1.Name = "simpleToolStripMenuItem1";
            this.simpleToolStripMenuItem1.Size = new System.Drawing.Size(167, 28);
            this.simpleToolStripMenuItem1.Text = "Simple";
            this.simpleToolStripMenuItem1.Click += new System.EventHandler(this.simpleToolStripMenuItem_Click);
            // 
            // doubleToolStripMenuItem1
            // 
            this.doubleToolStripMenuItem1.Name = "doubleToolStripMenuItem1";
            this.doubleToolStripMenuItem1.Size = new System.Drawing.Size(167, 28);
            this.doubleToolStripMenuItem1.Text = "Double";
            this.doubleToolStripMenuItem1.Click += new System.EventHandler(this.doubleToolStripMenuItem_Click);
            // 
            // quadrupleToolStripMenuItem1
            // 
            this.quadrupleToolStripMenuItem1.Name = "quadrupleToolStripMenuItem1";
            this.quadrupleToolStripMenuItem1.Size = new System.Drawing.Size(167, 28);
            this.quadrupleToolStripMenuItem1.Text = "Quadruple";
            this.quadrupleToolStripMenuItem1.Click += new System.EventHandler(this.quadrupleToolStripMenuItem_Click);
            // 
            // effetPeintureToolStripMenuItem
            // 
            this.effetPeintureToolStripMenuItem.Name = "effetPeintureToolStripMenuItem";
            this.effetPeintureToolStripMenuItem.Size = new System.Drawing.Size(349, 28);
            this.effetPeintureToolStripMenuItem.Text = "Effet peinture";
            this.effetPeintureToolStripMenuItem.Click += new System.EventHandler(this.effetPeintureToolStripMenuItem_Click);
            // 
            // stéganographieToolStripMenuItem
            // 
            this.stéganographieToolStripMenuItem.Name = "stéganographieToolStripMenuItem";
            this.stéganographieToolStripMenuItem.Size = new System.Drawing.Size(142, 27);
            this.stéganographieToolStripMenuItem.Text = "Stéganographie";
            this.stéganographieToolStripMenuItem.Click += new System.EventHandler(this.stéganographieToolStripMenuItem_Click);
            // 
            // infosToolStripMenuItem
            // 
            this.infosToolStripMenuItem.Name = "infosToolStripMenuItem";
            this.infosToolStripMenuItem.Size = new System.Drawing.Size(132, 27);
            this.infosToolStripMenuItem.Text = "Histogrammes";
            this.infosToolStripMenuItem.Click += new System.EventHandler(this.infosToolStripMenuItem_Click);
            // 
            // paramètresToolStripMenuItem
            // 
            this.paramètresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.param,
            this.settingsToolStripMenuItem,
            this.aideToolStripMenuItem});
            this.paramètresToolStripMenuItem.Name = "paramètresToolStripMenuItem";
            this.paramètresToolStripMenuItem.Size = new System.Drawing.Size(66, 27);
            this.paramètresToolStripMenuItem.Text = "Outils";
            // 
            // param
            // 
            this.param.Name = "param";
            this.param.Size = new System.Drawing.Size(295, 28);
            this.param.Text = "Paramètres de modification";
            this.param.Click += new System.EventHandler(this.paramToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(295, 28);
            this.settingsToolStripMenuItem.Text = "Informations sur l\'image";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // aideToolStripMenuItem
            // 
            this.aideToolStripMenuItem.Name = "aideToolStripMenuItem";
            this.aideToolStripMenuItem.Size = new System.Drawing.Size(295, 28);
            this.aideToolStripMenuItem.Text = "Aide";
            this.aideToolStripMenuItem.Click += new System.EventHandler(this.aideToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Clear
            // 
            this.Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Clear.BackColor = System.Drawing.Color.Red;
            this.Clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Clear.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Clear.Location = new System.Drawing.Point(765, 433);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(125, 100);
            this.Clear.TabIndex = 2;
            this.Clear.Text = "Clear";
            this.toolTip1.SetToolTip(this.Clear, "Supprime l\'image en cours\r\nainsi que les images modifiées.");
            this.Clear.UseVisualStyleBackColor = false;
            this.Clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // Settings
            // 
            this.Settings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Settings.BackColor = System.Drawing.Color.DimGray;
            this.Settings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Settings.FlatAppearance.BorderSize = 0;
            this.Settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Settings.Font = new System.Drawing.Font("Comic Sans MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Settings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Settings.Location = new System.Drawing.Point(765, 31);
            this.Settings.Margin = new System.Windows.Forms.Padding(0);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(125, 67);
            this.Settings.TabIndex = 3;
            this.Settings.Text = "Settings";
            this.Settings.UseVisualStyleBackColor = false;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox1.BackColor = System.Drawing.Color.DimGray;
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Mode Nuit",
            "Real Size"});
            this.checkedListBox1.Location = new System.Drawing.Point(765, 97);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(125, 68);
            this.checkedListBox1.TabIndex = 9;
            this.checkedListBox1.ThreeDCheckBoxes = true;
            this.toolTip1.SetToolTip(this.checkedListBox1, resources.GetString("checkedListBox1.ToolTip"));
            this.checkedListBox1.Visible = false;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 300;
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.BackColor = System.Drawing.SystemColors.Desktop;
            this.toolTip1.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolTip1.InitialDelay = 300;
            this.toolTip1.ReshowDelay = 60;
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(217)))), ((int)(((byte)(32)))));
            this.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SaveButton.Location = new System.Drawing.Point(765, 101);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(125, 326);
            this.SaveButton.TabIndex = 10;
            this.SaveButton.Text = "Save";
            this.toolTip1.SetToolTip(this.SaveButton, "Sauvegarde l\'image sélectionnée\r\n(pour sélectionner l\'image, se mettre\r\ndans l\'on" +
        "glet de l\'image à sauvegarder)");
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Originale
            // 
            this.Originale.AutoScroll = true;
            this.Originale.BackColor = System.Drawing.Color.Gainsboro;
            this.Originale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Originale.Location = new System.Drawing.Point(4, 25);
            this.Originale.Name = "Originale";
            this.Originale.Padding = new System.Windows.Forms.Padding(3);
            this.Originale.Size = new System.Drawing.Size(734, 473);
            this.Originale.TabIndex = 0;
            this.Originale.Text = "  Image chargée  ";
            // 
            // Tabs
            // 
            this.Tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tabs.Controls.Add(this.Originale);
            this.Tabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.Tabs.Location = new System.Drawing.Point(12, 31);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(742, 502);
            this.Tabs.TabIndex = 1;
            this.Tabs.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            this.Tabs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            // 
            // Photoshop3000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(902, 545);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.Settings);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.SaveButton);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 350);
            this.Name = "Photoshop3000";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Photoshop Evolution";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.Tabs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion


        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ouvrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sauvegarderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fractalesToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private List<System.Windows.Forms.PictureBox> PictureBoxParOnglet = new List<PictureBox>();
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem détectionContoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem détectionDesContoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobelToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cannyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem renforcementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem affaiblissementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dessinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gravureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repoussageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem faibleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aiguisageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contrasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flouToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mouvementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gaussienToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lissageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bruitNumériqueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coloréToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selEtPoivreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grenailleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pixélisationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stéganographieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paramètresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button Settings;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private ToolTip toolTip1;
        private ToolStripMenuItem param;
        private Button SaveButton;
        private ToolStripMenuItem aideToolStripMenuItem;
        private TabPage Originale;
        private TabControl Tabs;
        private ToolStripMenuItem couleursToolStripMenuItem;
        private ToolStripMenuItem créationToolStripMenuItem;
        private ToolStripMenuItem dessinsToolStripMenuItem;
        private ToolStripMenuItem effetMiroirToolStripMenuItem1;
        private ToolStripMenuItem simpleToolStripMenuItem1;
        private ToolStripMenuItem doubleToolStripMenuItem1;
        private ToolStripMenuItem quadrupleToolStripMenuItem1;
        private ToolStripMenuItem effetPeintureToolStripMenuItem;
        private ToolStripMenuItem copieImageToolStripMenuItem;

        public class CouleurMenuDéroulant : ProfessionalColorTable
        {
            private Color BG = Color.FromArgb(60, 60, 60);

            private Color Hover = Color.Gray;

            public CouleurMenuDéroulant(bool modeNuit)
            {
                if (!modeNuit)
                {
                    this.BG = Color.WhiteSmoke;
                }
            }

            public override Color ToolStripDropDownBackground
            {
                get
                {
                    return BG;
                }
            }

            public override Color ImageMarginGradientBegin
            {
                get
                {
                    return BG;
                }
            }

            public override Color ImageMarginGradientMiddle
            {
                get
                {
                    return BG;
                }
            }

            public override Color ImageMarginGradientEnd
            {
                get
                {
                    return BG;
                }
            }

            public override Color MenuBorder
            {
                get
                {
                    return Color.Black;
                }
            }

            public override Color MenuItemBorder
            {
                get
                {
                    return Color.Black;
                }
            }

            public override Color MenuItemSelected
            {
                get
                {
                    return Hover;
                }
            }

            public override Color MenuStripGradientBegin
            {
                get
                {
                    return BG;
                }
            }

            public override Color MenuStripGradientEnd
            {
                get
                {
                    return BG;
                }
            }

            public override Color MenuItemSelectedGradientBegin
            {
                get
                {
                    return Hover;
                }
            }

            public override Color MenuItemSelectedGradientEnd
            {
                get
                {
                    return Hover;
                }
            }

            public override Color MenuItemPressedGradientBegin
            {
                get
                {
                    return BG;
                }
            }

            public override Color MenuItemPressedGradientEnd
            {
                get
                {
                    return BG;
                }
            }
        }


        class PictureBoxMove : PictureBox
        {
            protected override void OnPaint(PaintEventArgs pe)
            {
                if (this.Image != null)
                {
                    //calculate how much the image needs to be shifted in order to be in the center
                    int xOffset = (this.Width - this.Image.Width) / 2;

                    //shift all drawings to the amount calculated
                    pe.Graphics.TranslateTransform(xOffset, 0);
                }
                base.OnPaint(pe);
            }
        }
    }
}

