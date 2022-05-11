using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObstacleurService;

namespace TP1LouisPhilippeRousseau
{
    public class Obstacle : Protagoniste 
    {
        static public char SYMBOLE;
        
        override protected char Symbole { get; init; }

        IDécideur décideur;

        public Obstacle(char symboleEnvoyé , IDirecteur dir, Point point) : base(point, dir)
        {
            Symbole = symboleEnvoyé;
            SYMBOLE = Symbole;
        }
        public override char GetSymbole()
        {
            return Symbole;
        }

    }
}
