using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1LouisPhilippeRousseau
{
    public class Participant : Protagoniste
    {
        static public char SYMBOLE = 'R';
        override protected char Symbole { get; init; } = SYMBOLE;
       
        public Participant(Point point, IDirecteur directeur) : base (point , directeur) { }
        
        public Participant(Point point) : base(point) { }
        
        override public char GetSymbole()
        {
            return SYMBOLE;
        }
        public void Associer(IDirecteur dir)
        {
            directeur = dir;
        }

    }
}
