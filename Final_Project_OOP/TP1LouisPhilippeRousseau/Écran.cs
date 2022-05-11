using System;
using System.Collections.Generic;
using System.Threading;

namespace TP1LouisPhilippeRousseau
{
    public class Écran : IObservateurMouvement
    {
        List<Thread> listeDeThread = new List<Thread>();

        List<PanneauAffichage> listePanneau = new List<PanneauAffichage>();

        object mutex = new object();
        bool peutContinuer = true;
        public void MouvementObservé(Carte carteObserver)
        {
            int hauteur = carteObserver.Hauteur;
            int largeur = carteObserver.Largeur;
            foreach (PanneauAffichage a in listePanneau)
            {
                if (hauteur == a.Largeur && largeur == a.Hauteur)
                {
                    for (int i = 0; i < a.Hauteur; i++)
                    {
                        string ligne = "";
                        for (int n = 0; n < a.Hauteur; n++)
                        {
                            ligne += carteObserver.GetChar(hauteur, largeur);
                        }
                        a.Write(ligne, ConsoleColor.White);

                    }
                }
            }
        }
        
        public void Ajouter(params PanneauAffichage[] bob)
        {
            foreach (PanneauAffichage a in bob)
                lock (mutex)
                {
                    listePanneau.Add(a);
                }
        }
        public void Affichez()
        {
            foreach (PanneauAffichage panneau in listePanneau)
            {
                panneau.Affichez();

            }
        }

        // arrêt de l'affichage
        public void Arrêter()
        {
            peutContinuer = false;
            Algos.StopThreads(listeDeThread);
        }
        public void Démarrer()
        {
            int compteur = 1;

            Thread t = new Thread(() =>
            {
                while (peutContinuer)
                {
                    lock (Algos.Mutexaffichage)
                    {
                        Thread.Sleep(100);
                        listePanneau[0].Affichez();
                    }

                    lock (Algos.Mutexaffichage)
                    {
                        Thread.Sleep(100);
                        listePanneau[1].Affichez();
                    }
                }
            });

            listeDeThread.Add(t);
            compteur += 10;

            Algos.StartThreads(listeDeThread);
        }
    }
}
