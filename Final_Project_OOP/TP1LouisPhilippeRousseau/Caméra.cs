using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1LouisPhilippeRousseau
{
    class Caméra : Capteur
    {
        static public char SYMBOLE = 'c';
        override public char Symbole { get; protected set; } = SYMBOLE;
        public List<Point> listePoint = new List<Point>();
        
        
        
        public PanneauAffichage panneauRecu { get; init; }

        public Caméra(Point pointRecu, PanneauAffichage bob) : base(pointRecu)
        {
            panneauRecu = bob;
        }
        override public void MouvementObservé(Carte carteObserver)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            List<Point> listeObstacle = new List<Point>();
            AjouterObstacleListe(carteObserver, listeObstacle);

            for (int comptHauteur = 0; comptHauteur < carteObserver.Hauteur; comptHauteur++)  //compteur qui passe tous les elements du tableau--largeur  et les affiches
            {
                string ligneDuTableau = "";
                for (int comptLargeur = 0; comptLargeur < carteObserver.Largeur; comptLargeur++)
                {
                    if (listeObstacle.Contains(new Point(comptHauteur, comptLargeur)))
                    {
                        foreach (Protagoniste pro in carteObserver.listeProtagonistes)
                        {
                            if (pro.Position == new Point(comptHauteur , comptLargeur))
                            {
                                ligneDuTableau += pro.GetSymbole();

                            }
                        }
                    }
                    else
                    {
                        ligneDuTableau += carteObserver.GetChar(comptHauteur, comptLargeur);
                    }
                }
                panneauRecu.Write(ligneDuTableau, ConsoleColor.White);
            }
        }
        public static void AjouterObstacleListe(Carte carteObserver, List<Point> listeObstacle)
        {
            foreach (Protagoniste pro in carteObserver.listeProtagonistes)
            {
                listeObstacle.Add(pro.Position);
            }
        }
    }
}
