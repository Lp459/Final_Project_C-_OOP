using System;
using System.Threading;
using ObstacleurService;
namespace TP1LouisPhilippeRousseau
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            
            var carte = FabriqueCarte.Créer(args.Length == 0 ?
            "../../../CarteTest.txt" : args[0]);
            // Création des panneaux
            var aff = new PanneauAffichage(new Point(), carte.Hauteur, carte.Largeur);
            var menu = new PanneauAffichage
            (
            new Point(0, aff.Hauteur), 5, ConsoleClassique.NB_COLONNES
            );
            var ardoise = new Ardoise
            (
            new Point(carte.Largeur, 0), carte.Hauteur ,
            ConsoleClassique.NB_COLONNES - carte.Largeur
            );
            var écran = new Écran();
            écran.Ajouter(aff, menu);
            carte.Abonner(écran);
            // Mises en correspondance
            var raymond = new Participant(carte.Trouver(Participant.SYMBOLE)[0]);
            SorteDirecteur sorte = args.Length >= 2 ?
            FabriqueDirecteur.TraduireSorte(args[1]) :
            SorteDirecteur.Humain;
            raymond.Associer(new FabriqueDirecteur().Créer(sorte, menu));
            var centrale = new Centrale();
            centrale.Populer(carte, raymond, menu, aff, ardoise);
            var diag = new Diagnosticien();
            raymond.Abonner(diag);
            
            var obst = new Obstacleur();
            obst.Populer(carte, 3);
           

            écran.Démarrer(); // démarrage de l'affichage
                              // Simulation...
            while (diag.Poursuivre(carte, raymond))
            {
                var choix = diag.Analyser(raymond.Agir(carte), raymond, carte);
                carte.Appliquer(raymond, choix);
            }
            obst.Terminer();
            menu.Write(diag.ExpliquerArrêt(), ConsoleColor.Yellow);
            écran.Arrêter(); // arrêt de l'affichage
           
        }
    }
}
