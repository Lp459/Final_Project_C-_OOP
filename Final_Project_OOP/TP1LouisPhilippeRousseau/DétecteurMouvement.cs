using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1LouisPhilippeRousseau
{
    class DétecteurMouvement : Capteur
    {
        static private double DistanceRouge = 3.5;
        static private double DistanceJaune = 5;
        override public char Symbole { get; protected set; } = 'm';

        private Ardoise ardoise; 
        public DétecteurMouvement(  Point position, Ardoise ARDDOISE  )  : base(position)
        {
            ardoise = ARDDOISE;
        }
        
        override public void MouvementObservé(Carte carteDeJeu)
        {
            if (carteDeJeu.listeProtagonistes != null)
            {
                foreach (Protagoniste protag in carteDeJeu.listeProtagonistes)
                {
                    double distance = Math.Round(Point.Distance(Position, protag.Position),2);
                    if (distance <= DistanceJaune)
                    {
                        if (distance <= DistanceRouge)
                        {
                            ardoise.Ajouter(protag.GetSymbole().ToString(), " : " + distance , ConsoleColor.Red);
                            ardoise.Write();
                            
                        }
                        else
                        { 
                            ardoise.Ajouter(protag.GetSymbole().ToString(), " : " + distance, ConsoleColor.Yellow);
                            ardoise.Write();

                        }
                    }
                    
                }
                ardoise.Affichez();
            }
        }


    }
}
