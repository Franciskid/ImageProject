using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Photoshop3000
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sauvegarderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.couleurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test15x15ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fractalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barcodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.créationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copieImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dessinsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.effetMiroirToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleLargeurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.doubleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.doubleHauteurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quadrupleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.moitiéHauteurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moitiéLargeurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.effetPeintureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.méthodeNormaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.méthodeAiguisageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.méthodeContrasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.effetPeintureSurHuileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.effetPeintureÀLhuileContrasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bruitNumériqueToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.coloréToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.selEtPoivreToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.grenailleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pixélisationToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.décallageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triDesCouleursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parCôtéToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parCoinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.CheckLBSettings = new System.Windows.Forms.CheckedListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SaveButton = new System.Windows.Forms.Button();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.menuStrip1.SuspendLayout();
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
            this.barcodesToolStripMenuItem,
            this.créationToolStripMenuItem,
            this.stéganographieToolStripMenuItem,
            this.infosToolStripMenuItem,
            this.paramètresToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(790, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirToolStripMenuItem,
            this.sauvegarderToolStripMenuItem,
            this.quitterToolStripMenuItem});
            this.fichierToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.fichierToolStripMenuItem.Text = "Fichier ";
            // 
            // ouvrirToolStripMenuItem
            // 
            this.ouvrirToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.ouvrirToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ouvrirToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ouvrirToolStripMenuItem.Image")));
            this.ouvrirToolStripMenuItem.Name = "ouvrirToolStripMenuItem";
            this.ouvrirToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.ouvrirToolStripMenuItem.Text = "Ouvrir";
            this.ouvrirToolStripMenuItem.Click += new System.EventHandler(this.ouvrirToolStripMenuItem_Click);
            // 
            // sauvegarderToolStripMenuItem
            // 
            this.sauvegarderToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.sauvegarderToolStripMenuItem.Enabled = false;
            this.sauvegarderToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sauvegarderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sauvegarderToolStripMenuItem.Image")));
            this.sauvegarderToolStripMenuItem.Name = "sauvegarderToolStripMenuItem";
            this.sauvegarderToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.sauvegarderToolStripMenuItem.Text = "Enregistrer sous";
            this.sauvegarderToolStripMenuItem.Click += new System.EventHandler(this.sauvegarderToolStripMenuItem_Click);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(235)))), ((int)(((byte)(236)))));
            this.quitterToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.quitterToolStripMenuItem.Text = "Quitter";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // filtresToolStripMenuItem
            // 
            this.filtresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.couleursToolStripMenuItem,
            this.détectionContoursToolStripMenuItem,
            this.dessinToolStripMenuItem,
            this.gravureToolStripMenuItem,
            this.repoussageToolStripMenuItem,
            this.aiguisageToolStripMenuItem,
            this.contrasteToolStripMenuItem,
            this.flouToolStripMenuItem,
            this.lissageToolStripMenuItem,
            this.couleurToolStripMenuItem,
            this.test15x15ToolStripMenuItem});
            this.filtresToolStripMenuItem.Name = "filtresToolStripMenuItem";
            this.filtresToolStripMenuItem.Size = new System.Drawing.Size(69, 24);
            this.filtresToolStripMenuItem.Text = "Filtres   ";
            // 
            // couleursToolStripMenuItem
            // 
            this.couleursToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("couleursToolStripMenuItem.Image")));
            this.couleursToolStripMenuItem.Name = "couleursToolStripMenuItem";
            this.couleursToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.couleursToolStripMenuItem.Text = "Modification des couleurs ";
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
            this.détectionContoursToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.détectionContoursToolStripMenuItem.Text = "Contours";
            // 
            // détectionDesContoursToolStripMenuItem
            // 
            this.détectionDesContoursToolStripMenuItem.Name = "détectionDesContoursToolStripMenuItem";
            this.détectionDesContoursToolStripMenuItem.Size = new System.Drawing.Size(220, 24);
            this.détectionDesContoursToolStripMenuItem.Text = "Détection des contours";
            this.détectionDesContoursToolStripMenuItem.Click += new System.EventHandler(this.détectionDesContoursToolStripMenuItem_Click);
            // 
            // sobelToolStripMenuItem1
            // 
            this.sobelToolStripMenuItem1.Name = "sobelToolStripMenuItem1";
            this.sobelToolStripMenuItem1.Size = new System.Drawing.Size(220, 24);
            this.sobelToolStripMenuItem1.Text = "Sobel";
            this.sobelToolStripMenuItem1.Click += new System.EventHandler(this.sobelToolStripMenuItem_Click);
            // 
            // cannyToolStripMenuItem1
            // 
            this.cannyToolStripMenuItem1.Name = "cannyToolStripMenuItem1";
            this.cannyToolStripMenuItem1.Size = new System.Drawing.Size(220, 24);
            this.cannyToolStripMenuItem1.Text = "Canny";
            this.cannyToolStripMenuItem1.Click += new System.EventHandler(this.cannyToolStripMenuItem_Click);
            // 
            // renforcementToolStripMenuItem
            // 
            this.renforcementToolStripMenuItem.Name = "renforcementToolStripMenuItem";
            this.renforcementToolStripMenuItem.Size = new System.Drawing.Size(220, 24);
            this.renforcementToolStripMenuItem.Text = "Renforcement";
            this.renforcementToolStripMenuItem.Click += new System.EventHandler(this.renforcementToolStripMenuItem_Click);
            // 
            // affaiblissementToolStripMenuItem
            // 
            this.affaiblissementToolStripMenuItem.Name = "affaiblissementToolStripMenuItem";
            this.affaiblissementToolStripMenuItem.Size = new System.Drawing.Size(220, 24);
            this.affaiblissementToolStripMenuItem.Text = "Affaiblissement";
            this.affaiblissementToolStripMenuItem.Click += new System.EventHandler(this.affaiblissementToolStripMenuItem_Click);
            // 
            // dessinToolStripMenuItem
            // 
            this.dessinToolStripMenuItem.Name = "dessinToolStripMenuItem";
            this.dessinToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.dessinToolStripMenuItem.Text = "Dessin";
            this.dessinToolStripMenuItem.Click += new System.EventHandler(this.dessinToolStripMenuItem_Click);
            // 
            // gravureToolStripMenuItem
            // 
            this.gravureToolStripMenuItem.Name = "gravureToolStripMenuItem";
            this.gravureToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.gravureToolStripMenuItem.Text = "Gravure";
            this.gravureToolStripMenuItem.Click += new System.EventHandler(this.gravureToolStripMenuItem_Click);
            // 
            // repoussageToolStripMenuItem
            // 
            this.repoussageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.faibleToolStripMenuItem,
            this.fortToolStripMenuItem});
            this.repoussageToolStripMenuItem.Name = "repoussageToolStripMenuItem";
            this.repoussageToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.repoussageToolStripMenuItem.Text = "Repoussage";
            // 
            // faibleToolStripMenuItem
            // 
            this.faibleToolStripMenuItem.Name = "faibleToolStripMenuItem";
            this.faibleToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
            this.faibleToolStripMenuItem.Text = "Faible";
            this.faibleToolStripMenuItem.Click += new System.EventHandler(this.faibleToolStripMenuItem_Click);
            // 
            // fortToolStripMenuItem
            // 
            this.fortToolStripMenuItem.Name = "fortToolStripMenuItem";
            this.fortToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
            this.fortToolStripMenuItem.Text = "Fort";
            this.fortToolStripMenuItem.Click += new System.EventHandler(this.fortToolStripMenuItem_Click);
            // 
            // aiguisageToolStripMenuItem
            // 
            this.aiguisageToolStripMenuItem.Name = "aiguisageToolStripMenuItem";
            this.aiguisageToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.aiguisageToolStripMenuItem.Text = "Aiguisage";
            this.aiguisageToolStripMenuItem.Click += new System.EventHandler(this.aiguisageToolStripMenuItem_Click);
            // 
            // contrasteToolStripMenuItem
            // 
            this.contrasteToolStripMenuItem.Name = "contrasteToolStripMenuItem";
            this.contrasteToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
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
            this.flouToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.flouToolStripMenuItem.Text = "Flou";
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.normalToolStripMenuItem.Text = "Normal";
            this.normalToolStripMenuItem.Click += new System.EventHandler(this.normalToolStripMenuItem_Click);
            // 
            // mouvementToolStripMenuItem
            // 
            this.mouvementToolStripMenuItem.Name = "mouvementToolStripMenuItem";
            this.mouvementToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.mouvementToolStripMenuItem.Text = "Mouvement";
            this.mouvementToolStripMenuItem.Click += new System.EventHandler(this.mouvementToolStripMenuItem_Click);
            // 
            // gaussienToolStripMenuItem
            // 
            this.gaussienToolStripMenuItem.Name = "gaussienToolStripMenuItem";
            this.gaussienToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.gaussienToolStripMenuItem.Text = "Gaussien";
            this.gaussienToolStripMenuItem.Click += new System.EventHandler(this.gaussienToolStripMenuItem_Click);
            // 
            // lissageToolStripMenuItem
            // 
            this.lissageToolStripMenuItem.Name = "lissageToolStripMenuItem";
            this.lissageToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.lissageToolStripMenuItem.Text = "Lissage";
            this.lissageToolStripMenuItem.Click += new System.EventHandler(this.lissageToolStripMenuItem_Click);
            // 
            // couleurToolStripMenuItem
            // 
            this.couleurToolStripMenuItem.Name = "couleurToolStripMenuItem";
            this.couleurToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.couleurToolStripMenuItem.Text = "Couleur";
            this.couleurToolStripMenuItem.Click += new System.EventHandler(this.couleurToolStripMenuItem_Click);
            // 
            // test15x15ToolStripMenuItem
            // 
            this.test15x15ToolStripMenuItem.Name = "test15x15ToolStripMenuItem";
            this.test15x15ToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.test15x15ToolStripMenuItem.Text = "Test (15x15)";
            this.test15x15ToolStripMenuItem.Click += new System.EventHandler(this.Test15x15ToolStripMenuItem_Click);
            // 
            // fractalesToolStripMenuItem
            // 
            this.fractalesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fractalesToolStripMenuItem.Image")));
            this.fractalesToolStripMenuItem.Name = "fractalesToolStripMenuItem";
            this.fractalesToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            this.fractalesToolStripMenuItem.Text = "Fractales ";
            this.fractalesToolStripMenuItem.Click += new System.EventHandler(this.fractalesToolStripMenuItem_Click);
            // 
            // barcodesToolStripMenuItem
            // 
            this.barcodesToolStripMenuItem.Image = global::Photoshop3000.Properties.Resources.newTab;
            this.barcodesToolStripMenuItem.Name = "barcodesToolStripMenuItem";
            this.barcodesToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.barcodesToolStripMenuItem.Text = "Barcodes";
            this.barcodesToolStripMenuItem.Click += new System.EventHandler(this.barcodesToolStripMenuItem_Click);
            // 
            // créationToolStripMenuItem
            // 
            this.créationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copieImageToolStripMenuItem,
            this.dessinsToolStripMenuItem,
            this.effetMiroirToolStripMenuItem1,
            this.effetPeintureToolStripMenuItem,
            this.bruitNumériqueToolStripMenuItem1,
            this.pixélisationToolStripMenuItem1,
            this.décallageToolStripMenuItem,
            this.triDesCouleursToolStripMenuItem});
            this.créationToolStripMenuItem.Name = "créationToolStripMenuItem";
            this.créationToolStripMenuItem.Size = new System.Drawing.Size(85, 24);
            this.créationToolStripMenuItem.Text = "Création   ";
            // 
            // copieImageToolStripMenuItem
            // 
            this.copieImageToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copieImageToolStripMenuItem.Image")));
            this.copieImageToolStripMenuItem.Name = "copieImageToolStripMenuItem";
            this.copieImageToolStripMenuItem.Size = new System.Drawing.Size(295, 26);
            this.copieImageToolStripMenuItem.Text = "Copier une image dans une image ";
            this.copieImageToolStripMenuItem.Click += new System.EventHandler(this.copieImageToolStripMenuItem_Click);
            // 
            // dessinsToolStripMenuItem
            // 
            this.dessinsToolStripMenuItem.Image = global::Photoshop3000.Properties.Resources.newTab;
            this.dessinsToolStripMenuItem.Name = "dessinsToolStripMenuItem";
            this.dessinsToolStripMenuItem.Size = new System.Drawing.Size(295, 26);
            this.dessinsToolStripMenuItem.Text = "Dessins ";
            this.dessinsToolStripMenuItem.Click += new System.EventHandler(this.dessinsToolStripMenuItem_Click);
            // 
            // effetMiroirToolStripMenuItem1
            // 
            this.effetMiroirToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simpleLargeurToolStripMenuItem,
            this.simpleToolStripMenuItem1,
            this.doubleToolStripMenuItem1,
            this.doubleHauteurToolStripMenuItem,
            this.quadrupleToolStripMenuItem1,
            this.moitiéHauteurToolStripMenuItem,
            this.moitiéLargeurToolStripMenuItem});
            this.effetMiroirToolStripMenuItem1.Name = "effetMiroirToolStripMenuItem1";
            this.effetMiroirToolStripMenuItem1.Size = new System.Drawing.Size(295, 26);
            this.effetMiroirToolStripMenuItem1.Text = "Effet miroir";
            // 
            // simpleLargeurToolStripMenuItem
            // 
            this.simpleLargeurToolStripMenuItem.Name = "simpleLargeurToolStripMenuItem";
            this.simpleLargeurToolStripMenuItem.Size = new System.Drawing.Size(174, 24);
            this.simpleLargeurToolStripMenuItem.Text = "Simple largeur";
            this.simpleLargeurToolStripMenuItem.Click += new System.EventHandler(this.SimpleLargeurToolStripMenuItem_Click);
            // 
            // simpleToolStripMenuItem1
            // 
            this.simpleToolStripMenuItem1.Name = "simpleToolStripMenuItem1";
            this.simpleToolStripMenuItem1.Size = new System.Drawing.Size(174, 24);
            this.simpleToolStripMenuItem1.Text = "Simple hauteur";
            this.simpleToolStripMenuItem1.Click += new System.EventHandler(this.simpleToolStripMenuItem_Click);
            // 
            // doubleToolStripMenuItem1
            // 
            this.doubleToolStripMenuItem1.Name = "doubleToolStripMenuItem1";
            this.doubleToolStripMenuItem1.Size = new System.Drawing.Size(174, 24);
            this.doubleToolStripMenuItem1.Text = "Double largeur";
            this.doubleToolStripMenuItem1.Click += new System.EventHandler(this.doubleToolStripMenuItem_Click);
            // 
            // doubleHauteurToolStripMenuItem
            // 
            this.doubleHauteurToolStripMenuItem.Name = "doubleHauteurToolStripMenuItem";
            this.doubleHauteurToolStripMenuItem.Size = new System.Drawing.Size(174, 24);
            this.doubleHauteurToolStripMenuItem.Text = "Double hauteur";
            this.doubleHauteurToolStripMenuItem.Click += new System.EventHandler(this.doubleHauteurToolStripMenuItem_Click);
            // 
            // quadrupleToolStripMenuItem1
            // 
            this.quadrupleToolStripMenuItem1.Name = "quadrupleToolStripMenuItem1";
            this.quadrupleToolStripMenuItem1.Size = new System.Drawing.Size(174, 24);
            this.quadrupleToolStripMenuItem1.Text = "Quadruple";
            this.quadrupleToolStripMenuItem1.Click += new System.EventHandler(this.quadrupleToolStripMenuItem_Click);
            // 
            // moitiéHauteurToolStripMenuItem
            // 
            this.moitiéHauteurToolStripMenuItem.Name = "moitiéHauteurToolStripMenuItem";
            this.moitiéHauteurToolStripMenuItem.Size = new System.Drawing.Size(174, 24);
            this.moitiéHauteurToolStripMenuItem.Text = "Moitié Hauteur";
            this.moitiéHauteurToolStripMenuItem.Click += new System.EventHandler(this.MoitiéHauteurToolStripMenuItem_Click);
            // 
            // moitiéLargeurToolStripMenuItem
            // 
            this.moitiéLargeurToolStripMenuItem.Name = "moitiéLargeurToolStripMenuItem";
            this.moitiéLargeurToolStripMenuItem.Size = new System.Drawing.Size(174, 24);
            this.moitiéLargeurToolStripMenuItem.Text = "Moitié Largeur";
            this.moitiéLargeurToolStripMenuItem.Click += new System.EventHandler(this.MoitiéLargeurToolStripMenuItem_Click);
            // 
            // effetPeintureToolStripMenuItem
            // 
            this.effetPeintureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.méthodeNormaleToolStripMenuItem,
            this.méthodeAiguisageToolStripMenuItem,
            this.méthodeContrasteToolStripMenuItem,
            this.effetPeintureSurHuileToolStripMenuItem,
            this.effetPeintureÀLhuileContrasteToolStripMenuItem});
            this.effetPeintureToolStripMenuItem.Name = "effetPeintureToolStripMenuItem";
            this.effetPeintureToolStripMenuItem.Size = new System.Drawing.Size(295, 26);
            this.effetPeintureToolStripMenuItem.Text = "Effet peinture";
            // 
            // méthodeNormaleToolStripMenuItem
            // 
            this.méthodeNormaleToolStripMenuItem.Name = "méthodeNormaleToolStripMenuItem";
            this.méthodeNormaleToolStripMenuItem.Size = new System.Drawing.Size(274, 24);
            this.méthodeNormaleToolStripMenuItem.Text = "Méthode normale";
            this.méthodeNormaleToolStripMenuItem.Click += new System.EventHandler(this.MéthodeNormaleToolStripMenuItem_Click);
            // 
            // méthodeAiguisageToolStripMenuItem
            // 
            this.méthodeAiguisageToolStripMenuItem.Name = "méthodeAiguisageToolStripMenuItem";
            this.méthodeAiguisageToolStripMenuItem.Size = new System.Drawing.Size(274, 24);
            this.méthodeAiguisageToolStripMenuItem.Text = "Méthode aiguisage";
            this.méthodeAiguisageToolStripMenuItem.Click += new System.EventHandler(this.MéthodeAiguisageToolStripMenuItem_Click);
            // 
            // méthodeContrasteToolStripMenuItem
            // 
            this.méthodeContrasteToolStripMenuItem.Name = "méthodeContrasteToolStripMenuItem";
            this.méthodeContrasteToolStripMenuItem.Size = new System.Drawing.Size(274, 24);
            this.méthodeContrasteToolStripMenuItem.Text = "Méthode contraste";
            this.méthodeContrasteToolStripMenuItem.Click += new System.EventHandler(this.MéthodeContrasteToolStripMenuItem_Click);
            // 
            // effetPeintureSurHuileToolStripMenuItem
            // 
            this.effetPeintureSurHuileToolStripMenuItem.Name = "effetPeintureSurHuileToolStripMenuItem";
            this.effetPeintureSurHuileToolStripMenuItem.Size = new System.Drawing.Size(274, 24);
            this.effetPeintureSurHuileToolStripMenuItem.Text = "Effet peinture à l\'huile";
            this.effetPeintureSurHuileToolStripMenuItem.Click += new System.EventHandler(this.EffetPeintureSurHuileToolStripMenuItem_Click);
            // 
            // effetPeintureÀLhuileContrasteToolStripMenuItem
            // 
            this.effetPeintureÀLhuileContrasteToolStripMenuItem.Name = "effetPeintureÀLhuileContrasteToolStripMenuItem";
            this.effetPeintureÀLhuileContrasteToolStripMenuItem.Size = new System.Drawing.Size(274, 24);
            this.effetPeintureÀLhuileContrasteToolStripMenuItem.Text = "Effet peinture à l\'huile Contraste";
            this.effetPeintureÀLhuileContrasteToolStripMenuItem.Click += new System.EventHandler(this.EffetPeintureÀLhuileContrasteToolStripMenuItem_Click);
            // 
            // bruitNumériqueToolStripMenuItem1
            // 
            this.bruitNumériqueToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.coloréToolStripMenuItem1,
            this.selEtPoivreToolStripMenuItem1,
            this.grenailleToolStripMenuItem1});
            this.bruitNumériqueToolStripMenuItem1.Name = "bruitNumériqueToolStripMenuItem1";
            this.bruitNumériqueToolStripMenuItem1.Size = new System.Drawing.Size(295, 26);
            this.bruitNumériqueToolStripMenuItem1.Text = "Bruit numérique";
            // 
            // coloréToolStripMenuItem1
            // 
            this.coloréToolStripMenuItem1.Name = "coloréToolStripMenuItem1";
            this.coloréToolStripMenuItem1.Size = new System.Drawing.Size(153, 24);
            this.coloréToolStripMenuItem1.Text = "Coloré";
            this.coloréToolStripMenuItem1.Click += new System.EventHandler(this.coloréToolStripMenuItem_Click);
            // 
            // selEtPoivreToolStripMenuItem1
            // 
            this.selEtPoivreToolStripMenuItem1.Name = "selEtPoivreToolStripMenuItem1";
            this.selEtPoivreToolStripMenuItem1.Size = new System.Drawing.Size(153, 24);
            this.selEtPoivreToolStripMenuItem1.Text = "Sel et poivre";
            this.selEtPoivreToolStripMenuItem1.Click += new System.EventHandler(this.selEtPoivreToolStripMenuItem_Click);
            // 
            // grenailleToolStripMenuItem1
            // 
            this.grenailleToolStripMenuItem1.Name = "grenailleToolStripMenuItem1";
            this.grenailleToolStripMenuItem1.Size = new System.Drawing.Size(153, 24);
            this.grenailleToolStripMenuItem1.Text = "Grenaille";
            this.grenailleToolStripMenuItem1.Click += new System.EventHandler(this.grenailleToolStripMenuItem_Click);
            // 
            // pixélisationToolStripMenuItem1
            // 
            this.pixélisationToolStripMenuItem1.Image = global::Photoshop3000.Properties.Resources.newTab;
            this.pixélisationToolStripMenuItem1.Name = "pixélisationToolStripMenuItem1";
            this.pixélisationToolStripMenuItem1.Size = new System.Drawing.Size(295, 26);
            this.pixélisationToolStripMenuItem1.Text = "Pixellisation ";
            this.pixélisationToolStripMenuItem1.Click += new System.EventHandler(this.pixélisationToolStripMenuItem_Click);
            // 
            // décallageToolStripMenuItem
            // 
            this.décallageToolStripMenuItem.Image = global::Photoshop3000.Properties.Resources.newTab;
            this.décallageToolStripMenuItem.Name = "décallageToolStripMenuItem";
            this.décallageToolStripMenuItem.Size = new System.Drawing.Size(295, 26);
            this.décallageToolStripMenuItem.Text = "Décalage ";
            this.décallageToolStripMenuItem.Click += new System.EventHandler(this.DécalageToolStripMenuItem_Click);
            // 
            // triDesCouleursToolStripMenuItem
            // 
            this.triDesCouleursToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parCôtéToolStripMenuItem,
            this.parCoinToolStripMenuItem});
            this.triDesCouleursToolStripMenuItem.Name = "triDesCouleursToolStripMenuItem";
            this.triDesCouleursToolStripMenuItem.Size = new System.Drawing.Size(295, 26);
            this.triDesCouleursToolStripMenuItem.Text = "Tri des couleurs";
            // 
            // parCôtéToolStripMenuItem
            // 
            this.parCôtéToolStripMenuItem.Name = "parCôtéToolStripMenuItem";
            this.parCôtéToolStripMenuItem.Size = new System.Drawing.Size(128, 24);
            this.parCôtéToolStripMenuItem.Text = "Par côté";
            this.parCôtéToolStripMenuItem.Click += new System.EventHandler(this.parCôtéToolStripMenuItem_Click);
            // 
            // parCoinToolStripMenuItem
            // 
            this.parCoinToolStripMenuItem.Name = "parCoinToolStripMenuItem";
            this.parCoinToolStripMenuItem.Size = new System.Drawing.Size(128, 24);
            this.parCoinToolStripMenuItem.Text = "Par coin";
            this.parCoinToolStripMenuItem.Click += new System.EventHandler(this.parCoinToolStripMenuItem_Click);
            // 
            // stéganographieToolStripMenuItem
            // 
            this.stéganographieToolStripMenuItem.Image = global::Photoshop3000.Properties.Resources.newTab;
            this.stéganographieToolStripMenuItem.Name = "stéganographieToolStripMenuItem";
            this.stéganographieToolStripMenuItem.Size = new System.Drawing.Size(145, 24);
            this.stéganographieToolStripMenuItem.Text = "Stéganographie  ";
            this.stéganographieToolStripMenuItem.Click += new System.EventHandler(this.stéganographieToolStripMenuItem_Click);
            // 
            // infosToolStripMenuItem
            // 
            this.infosToolStripMenuItem.Image = global::Photoshop3000.Properties.Resources.newTab;
            this.infosToolStripMenuItem.Name = "infosToolStripMenuItem";
            this.infosToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.infosToolStripMenuItem.Text = "Histogrammes  ";
            this.infosToolStripMenuItem.Click += new System.EventHandler(this.infosToolStripMenuItem_Click);
            // 
            // paramètresToolStripMenuItem
            // 
            this.paramètresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.param,
            this.settingsToolStripMenuItem,
            this.aideToolStripMenuItem});
            this.paramètresToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramètresToolStripMenuItem.Name = "paramètresToolStripMenuItem";
            this.paramètresToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.paramètresToolStripMenuItem.Text = "Outils";
            // 
            // param
            // 
            this.param.Image = global::Photoshop3000.Properties.Resources.newTab;
            this.param.Name = "param";
            this.param.Size = new System.Drawing.Size(265, 24);
            this.param.Text = "Paramètres de modification ";
            this.param.Click += new System.EventHandler(this.paramImageToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = global::Photoshop3000.Properties.Resources.newTab;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(265, 24);
            this.settingsToolStripMenuItem.Text = "Informations sur l\'image ";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.infoImageToolStripMenuItem_Click);
            // 
            // aideToolStripMenuItem
            // 
            this.aideToolStripMenuItem.Image = global::Photoshop3000.Properties.Resources.newTab;
            this.aideToolStripMenuItem.Name = "aideToolStripMenuItem";
            this.aideToolStripMenuItem.Size = new System.Drawing.Size(265, 24);
            this.aideToolStripMenuItem.Text = "Aide ";
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
            this.Clear.Location = new System.Drawing.Point(687, 467);
            this.Clear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(94, 81);
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
            this.Settings.Location = new System.Drawing.Point(687, 27);
            this.Settings.Margin = new System.Windows.Forms.Padding(0);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(94, 52);
            this.Settings.TabIndex = 3;
            this.Settings.Text = "Settings";
            this.Settings.UseVisualStyleBackColor = false;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // CheckLBSettings
            // 
            this.CheckLBSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckLBSettings.BackColor = System.Drawing.Color.DimGray;
            this.CheckLBSettings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CheckLBSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckLBSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CheckLBSettings.FormattingEnabled = true;
            this.CheckLBSettings.Items.AddRange(new object[] {
            "Mode Nuit",
            "Taille réelle"});
            this.CheckLBSettings.Location = new System.Drawing.Point(687, 79);
            this.CheckLBSettings.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CheckLBSettings.Name = "CheckLBSettings";
            this.CheckLBSettings.Size = new System.Drawing.Size(94, 28);
            this.CheckLBSettings.TabIndex = 9;
            this.CheckLBSettings.ThreeDCheckBoxes = true;
            this.toolTip1.SetToolTip(this.CheckLBSettings, resources.GetString("CheckLBSettings.ToolTip"));
            this.CheckLBSettings.Visible = false;
            this.CheckLBSettings.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckLBSettings_CheckChanged);
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
            this.SaveButton.BackColor = System.Drawing.Color.LimeGreen;
            this.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SaveButton.Location = new System.Drawing.Point(687, 82);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(94, 383);
            this.SaveButton.TabIndex = 10;
            this.SaveButton.Text = "Save";
            this.toolTip1.SetToolTip(this.SaveButton, "Sauvegarde l\'image sélectionnée\r\n(pour sélectionner l\'image, se mettre\r\ndans l\'on" +
        "glet de l\'image à sauvegarder)");
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Tabs
            // 
            this.Tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.Tabs.Location = new System.Drawing.Point(9, 25);
            this.Tabs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(670, 523);
            this.Tabs.TabIndex = 1;
            this.Tabs.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            this.Tabs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(790, 558);
            this.Controls.Add(this.CheckLBSettings);
            this.Controls.Add(this.Settings);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.SaveButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(806, 596);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Photoshop Evolution";
            this.ResizeBegin += new System.EventHandler(this.Photoshop3000_sizebegin);
            this.Resize += new System.EventHandler(this.Photoshop3000_SizeChange);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem stéganographieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paramètresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button Settings;
        private System.Windows.Forms.CheckedListBox CheckLBSettings;
        private ToolTip toolTip1;
        private ToolStripMenuItem param;
        private Button SaveButton;
        private ToolStripMenuItem aideToolStripMenuItem;
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
        private ToolStripMenuItem doubleHauteurToolStripMenuItem;
        private ToolStripMenuItem bruitNumériqueToolStripMenuItem1;
        private ToolStripMenuItem coloréToolStripMenuItem1;
        private ToolStripMenuItem selEtPoivreToolStripMenuItem1;
        private ToolStripMenuItem grenailleToolStripMenuItem1;
        private ToolStripMenuItem pixélisationToolStripMenuItem1;
        private ToolStripMenuItem décallageToolStripMenuItem;
        private ToolStripMenuItem test15x15ToolStripMenuItem;
        private ToolStripMenuItem méthodeNormaleToolStripMenuItem;
        private ToolStripMenuItem méthodeAiguisageToolStripMenuItem;
        private ToolStripMenuItem méthodeContrasteToolStripMenuItem;
        private ToolStripMenuItem simpleLargeurToolStripMenuItem;
        private ToolStripMenuItem moitiéHauteurToolStripMenuItem;
        private ToolStripMenuItem moitiéLargeurToolStripMenuItem;
        private ToolStripMenuItem effetPeintureSurHuileToolStripMenuItem;
        private ToolStripMenuItem effetPeintureÀLhuileContrasteToolStripMenuItem;
        private ToolStripMenuItem barcodesToolStripMenuItem;
        private ToolStripMenuItem couleurToolStripMenuItem;
        private ColorDialog colorDialog1;
        private ToolStripMenuItem triDesCouleursToolStripMenuItem;
        private ToolStripMenuItem parCôtéToolStripMenuItem;
        private ToolStripMenuItem parCoinToolStripMenuItem;

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

            public override Color ToolStripContentPanelGradientBegin
                => Color.AntiqueWhite;
            public override Color ToolStripContentPanelGradientEnd
                => Color.AntiqueWhite;

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
                    return Color.FromArgb(100, 100, 100);
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

    }
}

