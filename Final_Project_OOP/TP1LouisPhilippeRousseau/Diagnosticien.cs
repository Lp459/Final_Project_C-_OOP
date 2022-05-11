using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1LouisPhilippeRousseau
{
    public class Diagnosticien : IObservateurComportements
    {
        private int nombreDeCollision = 0;
        private bool choixEstQuitter = false;
        private bool tropDeCollision = false;
        private bool personnageAGagner = false;
        public void CollisionObservée(Protagoniste personnage)
        {
            Console.Beep();
            nombreDeCollision++;
        }
        public bool Poursuivre(Carte carte, Protagoniste personnage) => !personnageAGagner && !choixEstQuitter && !tropDeCollision;

        public string ExpliquerArrêt()
        {
            if (choixEstQuitter)
            {
                return " il s'agit d'un départ volontaire (vous avez cliquer sur Q pour quitter)";
            }
            else if (personnageAGagner)
            {
                return "le personnage est sortie de la pièce , il a gagner !";
            }
            else if (tropDeCollision)
            {
                return "trop de collision , risque de santé pour le participant";
            }

            return "";
            
        }
        public Choix Analyser(Choix choix , Protagoniste personnage , Carte carte)
        {
            if(nombreDeCollision > 5)
            {
                carte.innitialiser.Join();
                   tropDeCollision = true;
                carte.fin = false;
            }
            if(choix == Choix.Quitter)
            {
                choixEstQuitter = true;
                carte.fin = false;
            }
            if(carte.EstDisponible(Algos.CréerNouveauPoint(choix , personnage)))
            {
                if (carte.EstSurFrontière(Algos.CréerNouveauPoint(choix, personnage)))
                {
                    personnageAGagner = true;
                    carte.fin = false;

                }
            }
            return choix;
        }
    }
}
