using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1LouisPhilippeRousseau
{
    class FabriqueDirecteur
    {
        public static SorteDirecteur TraduireSorte(string sorteDirecteur)
        {
            if (sorteDirecteur == "brownien")
            {
                return SorteDirecteur.Brownien;
            }
            return sorteDirecteur == "humain" ? SorteDirecteur.Humain : SorteDirecteur.Inconnue;
        }
        public IDirecteur Créer(SorteDirecteur dir, PanneauAffichage panneau)
        {
            if (dir == SorteDirecteur.Brownien)
            {
                return new DirecteurBrownien();
            }
            return dir == SorteDirecteur.Humain ? new DirecteurHumain(panneau) : default;
        }

    }
}
