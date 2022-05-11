using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1LouisPhilippeRousseau
{
    public enum Choix { Droite, Haut, Gauche, Bas, Quitter , Rien}
    abstract public  class Protagoniste
    {
        public Point Position { get; internal set; }
        protected abstract  char Symbole{ get;init; }
        private List<IObservateurComportements> listeAbonner = new List<IObservateurComportements>();
        protected IDirecteur directeur;

        public Protagoniste(Point point, IDirecteur dir)
        {
            Position = point;
            directeur = dir;
        }
        public Protagoniste(Point point)
        {
            Position = point;
            directeur = new DirecteurHumain();
        }
        
        public void Abonner(IObservateurComportements observateur)
        {
            listeAbonner.Add(observateur);
        }
        public void Collision()
        {
            foreach(IObservateurComportements abonner in listeAbonner)
            {
                abonner.CollisionObservée(this);
            }
        }
        public Choix Agir(Carte carteDeJeu)
        {
            return directeur.Agir(this, carteDeJeu);
        }
        public void Deplacer(Point position)
        {
            Position = new Point(position.X, position.Y);
        }
        abstract public char GetSymbole();
       
        

    }
}
