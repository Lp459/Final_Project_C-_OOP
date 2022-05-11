using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1LouisPhilippeRousseau;

namespace TP1LouisPhilippeRousseau
{

    public class Carte
    {
        private char[,] CarteDeJeu = new char[8, 16];
        public int Hauteur { get; init; }
        public int Largeur { get; init; }

        public object mutext = new object();

        public Point[] mouvment = new Point[2];

        public Thread innitialiser;

        public bool fin = true;

         public List<Protagoniste> listeProtagonistes { get; set; } = new List<Protagoniste>();

        public const char charGagné = '!';

        List<IObservateurMouvement> listeAbonner = new List<IObservateurMouvement>();
        public char this[Point position]
        {
            get => CarteDeJeu[position.X, position.Y];
            private set => CarteDeJeu[position.X, position.Y] = value;
        }
        public char this[int x, int y]
        {
            get => CarteDeJeu[x, y];
            private set => CarteDeJeu[x, y] = value;
        }
        internal Carte(char[,] carteDeJeuFichier)
        {
            Largeur = CarteDeJeu.GetLength(1);
            Hauteur = CarteDeJeu.GetLength(0);
            for (int ligne = 0; ligne < Hauteur; ligne++)
            {
                for (int colonne = 0; colonne < Largeur; colonne++)
                {
                    this[ligne, colonne] = carteDeJeuFichier[ligne, colonne];
                }
            }

        }
        public char GetChar(Point pointDonner)
        {
            return CarteDeJeu[pointDonner.X, pointDonner.Y];
        }
        public char GetChar(int x, int y)
        {
            return CarteDeJeu[x, y];
        }


        public void Installer(List<Capteur> listeCapteur)
        {
            foreach(Capteur c in listeCapteur)
            {
                if(c.Symbole != 'c')
                {
                    if (!EstDisponible(c.Position)) throw new PositionIllégaleException();
                    CarteDeJeu[c.Position.X, c.Position.Y] = c.Symbole;
                    c.MouvementObservé(this);
                }
                
            }
            innitialiser = new Thread(() =>
           {
               while (fin)
               {
                   foreach (Capteur capteur in listeCapteur)
                   {
                       Thread.Sleep(100);
                       lock (Algos.Mutexaffichage)
                       {
                           capteur.MouvementObservé(this);
                       }
                   }
               }
           });
            innitialiser.Start();
        }
        public void Installer(List<Obstacle> listeDeObstacle)
        {
            foreach (Obstacle o in listeDeObstacle)
            {
                
                 if (!EstDisponible(o.Position)) throw new PositionIllégaleException();
                 CarteDeJeu[o.Position.X, o.Position.Y] = o.GetSymbole();
            } 
        }
        public void Abonner(IObservateurMouvement observateur)
        {
            listeAbonner.Add(observateur);
            
        }
        public void Appliquer(Protagoniste joueur, Choix direction)
        {

            Point newPoint = Algos.CréerNouveauPoint(direction, joueur);
            
            if (ValiderPosition(newPoint, joueur))
            {
                mouvment[0] = newPoint;
                mouvment[1] = joueur.Position;
                DéplacerSymbole(newPoint, joueur);
                joueur.Deplacer(newPoint);
                if (EstSurFrontière(joueur.Position))
                {
                    this[joueur.Position] = charGagné;

                }
                SignalerAbonnerMouvement();

            }
            else
            {

                joueur.Collision();

            }




        }
        public List<Point> Trouver(char charATrouver)
        {
            List<Point> listePointCharATrouver = new List<Point>();
            for (int ligne = 0; ligne < Hauteur; ligne++)
            {
                for (int colonne = 0; colonne < Largeur; colonne++)
                {
                    if (CarteDeJeu[ligne, colonne] == charATrouver)
                    {
                        listePointCharATrouver.Add(new Point(ligne, colonne));
                    }
                }
            }
            return listePointCharATrouver;
        }
        public void DéplacerSymbole(Point newPoint, Protagoniste joueur)
        {
            lock (mutext)
            {
                char symboleJoueur = this[joueur.Position];
                this[newPoint] = symboleJoueur;
                this[joueur.Position.X, joueur.Position.Y] = ' ';
            }
           
        }

        public void SignalerAbonnerMouvement()
        {
            foreach (IObservateurMouvement abonner in listeAbonner)
            {
                abonner.MouvementObservé(this);
            }

        }
        public char[,] Snapshot()
        {
            char[,] snap = new char[Hauteur, Largeur];
            for (int i = 0; i < Hauteur; i++)
            {
                for (int j = 0; j < Largeur; j++)
                {
                    lock (mutext)
                    {
                        snap[i, j] = this[i, j];
                    }
                }
            }
            return snap;
        }
        public bool EstDisponible(Point point) => this[point] == ' ';
        public bool EstSurFrontière(Point point) => point.X == 0 || point.Y == 0 || point.X == this.Hauteur - 1 || point.Y == this.Largeur - 1;

        public bool EstDans(Point point) => Algos.EntreDeuxBorne(point.X, -1, Hauteur) && Algos.EntreDeuxBorne(point.Y, -1, Largeur);

        public bool ValiderPosition(Point point, Protagoniste joueur)
            {
            
            
            if (point.X> Hauteur || point.Y > Largeur || point.X <= 0 || point.Y <=0){ return false; }

            if (GetChar((point))==' '){ return true; }
            return false;
            }
        public bool ValiderPosition(Point point, Point point2)
        {

            if(Point.Distance(point , point2) != 1) { return false; }

            if (point2.X > Hauteur || point2.Y > Largeur || point2.X <= 0 || point2.Y <= 0) { return false; }

            if (GetChar((point2)) != ' ') { return false; }

            foreach(Protagoniste p in listeProtagonistes)
            {
                if(point2.X == p.Position.X || point2.Y == p.Position.Y)
                {
                    return false;
                }
            }

            return true;
        }
        public bool EstAUneCase(Point point , Protagoniste joueur) => (point.X == joueur.Position.X - 1|| point.X == joueur.Position.X + 1 || point.X == joueur.Position.X) && (point.Y == joueur.Position.Y - 1 || point.Y == joueur.Position.Y + 1 || point.Y == joueur.Position.Y);


        public (bool, char) AppliquerTests(Point pos, params Func<char, bool>[] tests)
        {
            int longueur = tests.Length;
            for(int i = 0; i < longueur; i++)
            {
                if (!tests[i](this[pos])){
                    return (tests[i](this[pos]),this[pos]) ;
                }
            }
            return default;
        }

    }
}
