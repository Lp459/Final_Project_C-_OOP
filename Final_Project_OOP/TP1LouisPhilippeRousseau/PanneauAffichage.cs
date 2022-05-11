using System;
using System.Collections.Generic;

namespace TP1LouisPhilippeRousseau
{
    public class PanneauAffichage
    {

        List<UpletCharColor[]> charararray = new List<UpletCharColor[]>();
        List<(Point, UpletCharColor)> upletCharColor = new List<(Point, UpletCharColor)>();
        public Point PositionHautGauche { get; set; }
        public int Largeur { get; set; }
        public int Hauteur { get; set; }

        object mutex = new object();

        public PanneauAffichage(Point positionTopLeft, int lignex, int ligney)
        {
            Largeur = ligney;
            Hauteur = lignex;
            PositionHautGauche = positionTopLeft;
        }

        public void Affichez()
        {
            int ligneCourante = 0;

            if (charararray.Count != 0)
            {
                FaireModif();
                lock (Algos.Mutexaffichage)
                {

                    foreach (UpletCharColor[] tabCharColor in charararray)
                    {
                        if (ligneCourante < Hauteur)
                        {
                            Console.SetCursorPosition(this.PositionHautGauche.X, ligneCourante + this.PositionHautGauche.Y);
                            for (int x = 0; x < tabCharColor.Length; x++)
                            {
                                if (tabCharColor[x] != null)
                                {

                                    Console.SetCursorPosition(x + this.PositionHautGauche.X, ligneCourante + this.PositionHautGauche.Y);
                                    Console.ForegroundColor = tabCharColor[x].GetColor();
                                    Console.Write(tabCharColor[x].GetValeur());

                                }

                            }
                            ligneCourante++;
                        }


                    }
                    charararray.Clear();
                }
            }
        }
        public void FaireModif()
        {

            foreach ((Point, UpletCharColor) pointÀChanger in upletCharColor.ToArray())
            {
                lock (Algos.Mutexaffichage)
                {
                    charararray[pointÀChanger.Item1.Y][(pointÀChanger.Item1.X)] = pointÀChanger.Item2;
                }

            }
            upletCharColor.Clear();
        }

        public void AjouterModif(Point a, char n, ConsoleColor b)
        {
            lock (mutex)
            {
                upletCharColor.Add((a, new UpletCharColor(n, b)));
            }
        }


        public void Write(string texte, ConsoleColor color)
        {
            int compteur = texte.Length;
            int m = 0;
            int n = 0;
            UpletCharColor[] tabCharColor = new UpletCharColor[Largeur];
            for (int x = 0; x < compteur; x++)
            {
                if (m < Largeur)
                {
                    lock (mutex)
                    {
                        tabCharColor[m] = new UpletCharColor(texte[x], color);
                    }
                    m++;
                }
                else
                {
                    m = 0;
                    n++;
                    lock (mutex)
                    {
                        charararray.Add(tabCharColor);
                        tabCharColor[m] = new UpletCharColor(texte[x], color);
                    }
                }

            }
            lock (mutex)
            {
                charararray.Add(tabCharColor);
            }
        }

        public void Write(List<(char, ConsoleColor)> charColor)
        {
            int compteur = charColor.Count;
            int m = 0;
            int n = 0;
            UpletCharColor[] tabCharColor = new UpletCharColor[Largeur];
            for (int x = 0; x < compteur; x++)
            {
                if (m < Largeur)
                {
                    lock (mutex)
                    {
                        tabCharColor[m] = new UpletCharColor(charColor[x].Item1, charColor[x].Item2);
                    }
                    m++;
                }
                else
                {
                    m = 0;
                    n++;
                    lock (mutex)
                    {
                        charararray.Add(tabCharColor);
                        tabCharColor[m] = new UpletCharColor(charColor[x].Item1, charColor[x].Item2);
                    }
                }

            }
            lock (mutex)
            {
                charararray.Add(tabCharColor);
            }
        }



    }
}
