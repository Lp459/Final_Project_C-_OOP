using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1LouisPhilippeRousseau
{
    class DirecteurBrownien : IDirecteur
    {
        public Choix Agir(Protagoniste protag , Carte carte)
        {
            List<Choix> choixRestant = new List<Choix>() { Choix.Bas, Choix.Droite, Choix.Gauche, Choix.Haut };
            List<Choix> choixValide = TrouverPositionValide(protag, carte, choixRestant);
            int dir = Algos.random.Next(choixValide.Count);
            if (choixValide.Count == 0)
            {
                return Choix.Rien;
            }
            
            return choixValide[dir];
        }
        public List<Choix> TrouverPositionValide(Protagoniste protag , Carte carte , List<Choix> lstChoix)
        {
            List<Choix> lstChoixValide = new List<Choix>();

            foreach(Choix choix in lstChoix)
            {
                if(carte.ValiderPosition(Algos.CréerNouveauPoint(choix , protag) , protag))
                {
                    lstChoixValide.Add(choix);
                }
            }
            return lstChoixValide;
        }
    }
}
