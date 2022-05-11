using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1LouisPhilippeRousseau
{
    public class UpletCharColor
    {
        public char Valeur { get;set; }

        public ConsoleColor Color { get; set; }
        public UpletCharColor(char cararctere, ConsoleColor couleur)
        {
            Valeur = cararctere;
            Color = couleur;
        }
        public ConsoleColor GetColor()
        {
            return Color;
        }
        public char GetValeur()
        {
            return Valeur;
        }

    }
}
