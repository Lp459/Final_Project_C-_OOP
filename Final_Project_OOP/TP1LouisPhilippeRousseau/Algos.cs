using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TP1LouisPhilippeRousseau
{
    public enum SorteDirecteur { Brownien, Humain, Inconnue }
    class Algos
    {
        static public Random random = new Random();
        static public Object MutexObsatcle = new object();
        static public Object Mutexaffichage = new object();
        static public Point PointSwap(Point m)
        {
            return new Point(m.Y, m.X);
        }
        static public void Permuter<T>(ref T a, ref T b){

            T temp = a;
            a = b;
            b = temp;
        }
        public static Point CréerNouveauPoint(Choix choix, Protagoniste joueur)
        {
            switch (choix)
            {
                case Choix.Gauche:
                    return new Point(joueur.Position.X, joueur.Position.Y - 1);
                case Choix.Droite:
                    return new Point(joueur.Position.X, joueur.Position.Y + 1);
                case Choix.Haut:
                    return new Point(joueur.Position.X - 1, joueur.Position.Y);
                case Choix.Bas:
                    return new Point(joueur.Position.X + 1, joueur.Position.Y);
                default:
                    return joueur.Position;
            }
        }
        static public void StartThreads(List<Thread> listeThread)
        {
            foreach(Thread t in listeThread)
            {
                t.Start();
            }
        }
        static public void StopThreads(List<Thread> listeThread)
        {
            foreach (Thread t in listeThread)
            {
                t.Join();
            }
        }
        static public (char , int , int) [] ConvertirListeTripletObstacle(List<Obstacle> lstObstacle)
        {
            int longueur = lstObstacle.Count;
            (char, int, int)[] tabTriplet = new (char, int, int)[longueur];
            int compteur = 0;
            foreach(Obstacle o in lstObstacle)
            {
                tabTriplet[compteur] = (o.GetSymbole(), o.Position.X, o.Position.Y);
                compteur++;
            }
            return tabTriplet;
        }
        static public bool EntreDeuxBorne(int x, int borneMin, int borneMax) => x > borneMin && x < borneMax;
       
    }
}
