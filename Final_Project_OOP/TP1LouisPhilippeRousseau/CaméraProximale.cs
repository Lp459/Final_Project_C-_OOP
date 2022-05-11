using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1LouisPhilippeRousseau
{
    
    class CaméraProximale : Capteur
    {
        private static double distanceCouleur = 3.5;
        static public char SYMBOLE = 'x';
        override public char Symbole { get; protected set; } = SYMBOLE;
        protected Protagoniste joueurASuivre;
        static PanneauAffichage Panneau { get; set; }


        private static List<Point> listePoint = new List<Point>();
        public CaméraProximale(Protagoniste joueur, Point position, PanneauAffichage panneau) : base(position)
        {
            joueurASuivre = joueur;
            Panneau = panneau;
        }
        override public void MouvementObservé(Carte carteDeJeu)
        {
            AffichezCarteProximale(carteDeJeu, joueurASuivre);

        }
        static private void AffichezCarteProximale(Carte carte, Protagoniste joueurASuivre)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;

            foreach (Point a in listePoint)
            {
                Panneau.AjouterModif(Algos.PointSwap(a), carte.GetChar(a), ConsoleColor.White);
            }
            listePoint.Clear();

            

            List<Point> listeObstacle = new List<Point>();
            

            Caméra.AjouterObstacleListe(carte, listeObstacle);


            for (int ligne = 0; ligne < carte.Hauteur; ligne++)
            {
                for (int colonne = 0; colonne < carte.Largeur; colonne++)
                {
                    if (Point.Distance(new Point(ligne, colonne), joueurASuivre.Position) <= distanceCouleur)
                    {

                        if (Point.Distance(new Point(ligne, colonne), joueurASuivre.Position) == 0)
                        {
                            Panneau.AjouterModif(new Point(colonne, ligne), 'R', ConsoleColor.Red);
                        }

                        else if (listeObstacle.Contains(new Point(ligne, colonne)))
                        {
                            foreach (Protagoniste pro in carte.listeProtagonistes)
                            {
                                if(new Point(ligne,colonne) == pro.Position)
                                {
                                    Panneau.AjouterModif(new Point(colonne, ligne), pro.GetSymbole(), ConsoleColor.Red);
                                }
                               
                            }
                            
                        }
                        else
                        {
                            Panneau.AjouterModif(new Point(colonne, ligne), carte.GetChar(ligne, colonne), ConsoleColor.Red);
                            listePoint.Add(new Point(ligne, colonne));
                        }


                    }


                }

            }


        }
    }
}
