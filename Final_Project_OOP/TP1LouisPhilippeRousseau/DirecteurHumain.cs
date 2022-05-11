using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1LouisPhilippeRousseau
{
   public class DirecteurHumain : IDirecteur
    {
        public DirecteurHumain(PanneauAffichage panneau)
        {
            panneau.Write("Déplacer à la gauche : flèche gauche", ConsoleColor.Green);
            panneau.Write("Déplacer à la droite : flèche droite", ConsoleColor.Green);
            panneau.Write("Déplacer vers le bas : flèche bad", ConsoleColor.Green);
            panneau.Write("Déplacer vers le haut : flèche haut", ConsoleColor.Green);
            panneau.Write("Pour Quitter : Q", ConsoleColor.Red);
        }
        public DirecteurHumain() { }
        public Choix Agir(Protagoniste personnage, Carte carteDeJeu)
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.RightArrow:
                    return Choix.Droite;
                case ConsoleKey.LeftArrow:
                    return Choix.Gauche;
                case ConsoleKey.UpArrow:
                    return Choix.Haut;
                case ConsoleKey.DownArrow:
                    return Choix.Bas;
                case ConsoleKey.Q:
                    return Choix.Quitter;
                default:
                    return Choix.Rien;
            }
        }
    }
}
