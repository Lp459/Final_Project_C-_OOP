using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using GenerateurId;

namespace TP1LouisPhilippeRousseau
{
    abstract public class Capteur : IObservateurMouvement
    {
        public Point Position { get; private set; }
        public abstract char Symbole { get; protected set; }
        public abstract void MouvementObservé(Carte carte);

        public Capteur(Point position)
        {
            Position = position;
        }
    }
}
