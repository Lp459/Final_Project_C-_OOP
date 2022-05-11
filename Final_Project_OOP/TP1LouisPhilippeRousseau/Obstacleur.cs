using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ObstacleurService;

namespace TP1LouisPhilippeRousseau
{
    public class Obstacleur
    {

        List<Obstacle> lstObstacle = new List<Obstacle>();
        public List<Thread> lstThread = new List<Thread>();
        bool peutAgir = true;
        Carte laCarte;

        public void Populer(Carte carte, int nbObstacles)
        {
            List<Point> listePositionVide = new List<Point>();
            int indexPositionRandom;
            List<char> symboleChars = new List<char> { 'B', 'G', 'H' , 'J' , 'Q' , 'I' };
            FabriqueObstacleur f = new FabriqueObstacleur();
            laCarte = carte;
            foreach (Point p in carte.Trouver(' '))
            {
                listePositionVide.Add(p);
            }


            if (listePositionVide.Count < 1)
            {
                Console.WriteLine("Carte pleine , impossible de mettre un capteur");
            }

           
            for (int i = 0; i < nbObstacles; i++)
            {
                indexPositionRandom = Algos.random.Next(0, listePositionVide.Count - 1);
                
                lstObstacle.Add(new Obstacle(symboleChars[i],new DirecteurBrownien(), listePositionVide[indexPositionRandom]));

            }

            CancellationToken jeton = new CancellationToken();

            IContrôleurObstacleur e = f.Créer(ValiderDeplacement , Algos.ConvertirListeTripletObstacle(lstObstacle));
            carte.listeProtagonistes.AddRange(lstObstacle);



            lstThread.Add(new Thread(() =>
            {
                e.Démarrer();

                while (peutAgir)
                {
                    Task<(char, (int, int))>[] tabTasks = new Task<(char, (int, int))>[nbObstacles];
                    Point[] tabPoints = new Point[nbObstacles];
                    int compteur = 0;
                    foreach (Obstacle o in lstObstacle)
                    {
                        IDécideur d = e.ObtenirDécideur(o.GetSymbole());
                        bool valide = true;
                        while (valide && !jeton.IsCancellationRequested)
                        {
                            try
                            {
                                tabTasks[compteur] = d.RecevoirDéplacement((o.Position.X, o.Position.Y), jeton);
                                compteur++;
                                valide = false;
                            }
                            catch (AucuneOptionException e)
                            { 
                                valide = false;
                            }
                            catch (AggregateException e)
                            {
                                valide = false;
                            }
                            

                        }

                    }
                    Task.WaitAll(tabTasks);
                    
                    for (int i = 0; i < tabTasks.Length; i++)
                    {
                        tabPoints[i] = new Point(tabTasks[i].Result.Item2.Item1, tabTasks[i].Result.Item2.Item2);
                        
                        //j'utilise le même mutex que celui de déplacersymbole dans la carte , empechant qu'un obstacle et le participant bouge en même temps
                        lock (carte.mutext)
                        {
                            if (ValiderDeplacement((lstObstacle[i].Position.X, lstObstacle[i].Position.Y), (tabPoints[i].X, tabPoints[i].Y)))
                            {
                                lstObstacle[i].Deplacer(tabPoints[i]);
                            }
                        }
                       
                    }

                }
                e.Arrêter();
            }));
            Algos.StartThreads(lstThread);
        }
        public void Terminer()
        {
            peutAgir = false;
            Algos.StopThreads(lstThread);
        }
        public  bool ValiderDeplacement((int , int) p1 , (int , int) p2)
        {
            return laCarte.ValiderPosition(new Point(p1.Item1, p1.Item2), new Point(p2.Item1, p2.Item2));
        }


    }
}
