using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1LouisPhilippeRousseau
{
    public class Ardoise : PanneauAffichage
    {

        private List<List<(string message, ConsoleColor couleur)>> listeMessages =
            new List<List<(string message, ConsoleColor couleur)>>();

        private Dictionary<string, int> Compteur = new Dictionary<string, int>();

        private object mutex = new object();

        private Dictionary<string, int> dictionnaireCapteur = new Dictionary<string, int>();

        private int indexCapteur = 0;

        public Ardoise(Point xy, int lignex, int ligney) : base(xy, lignex, ligney) { }
        public void Ajouter(string nom, string message, ConsoleColor couleur)
        {
            if (!dictionnaireCapteur.ContainsKey(nom))
            {
                AjouterCapteur(nom, indexCapteur);
                Compteur.Add(nom, 0);
                indexCapteur++;
            }

            int i = dictionnaireCapteur[nom];
            listeMessages[i].Add((Compteur[nom] + " "+ nom + " " + message, couleur));
            Compteur[nom]++;

        }
        private void AjouterCapteur(string nom, int index)
        {
            dictionnaireCapteur.Add(nom, index);
            listeMessages.Add(new List<(string message, ConsoleColor couleur)>());
        }

        public void Write()
        {

            int PositionC = 15;
            int compteurList = 0;
            foreach (List<(string, ConsoleColor)> lst in listeMessages)
            {
                int compteur = 0;
                lock (Algos.MutexObsatcle)
                {
                    for(int i = lst.Count -1;i>0;--i)
                    {
                        if(compteurList * PositionC < Largeur)
                        {
                            Console.SetCursorPosition(PositionHautGauche.X + (compteurList * PositionC), compteur + PositionHautGauche.Y);
                            Console.ForegroundColor = lst[i].Item2;
                            Console.Write(lst[i].Item1);

                           
                        }
                        compteur++;
                    }
                    compteurList++;
                }
            }
            foreach (List<(string, ConsoleColor)> lst in listeMessages)
            {
                if (lst.Count == Hauteur + 1)
                {
                    lst.RemoveAt(0);
                }
            }
        }
    }
}