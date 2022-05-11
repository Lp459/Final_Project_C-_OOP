using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1LouisPhilippeRousseau
{
    public class Centrale
    {
        List<Capteur> listeCapteur = new List<Capteur>();
        public void Populer(Carte carte, Protagoniste joueur, PanneauAffichage menu, PanneauAffichage aff, Ardoise ardoise)
        {
            List<Point> listePositionVide = carte.Trouver(' ');
            if (listePositionVide.Count < 1)
            {
                Console.WriteLine("Carte pleine , impossible de mettre un capteur");
            }


            carte.listeProtagonistes.Add(joueur);
            int indexPositionRandom = Algos.random.Next(0, listePositionVide.Count - 1);
            listeCapteur.Add(new CaméraProximale(joueur, listePositionVide[indexPositionRandom], aff));
            listeCapteur.Add(new Caméra(new Point(1,1), aff));
            listeCapteur.Add(new DétecteurMouvement(new Point(5,5), ardoise));
            listeCapteur.Add(new DétecteurMouvement(new Point(3, 6), ardoise));
            carte.Installer(listeCapteur);
        }
    }
}
