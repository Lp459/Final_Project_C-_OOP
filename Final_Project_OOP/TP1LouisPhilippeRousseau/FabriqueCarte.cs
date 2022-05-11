using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1LouisPhilippeRousseau
{
    public class FabriqueCarte
    {
        static protected char[] charValide = new char[]
        {
           Carte.charGagné , Participant.SYMBOLE , ' ' , Caméra.SYMBOLE , CaméraProximale.SYMBOLE , '#'
        };
        public static Carte Créer(string nomFichier)
        {
            using (var reader = new System.IO.StreamReader(nomFichier))
            {
                var lst = new List<string>();
                for (string s = reader.ReadLine(); s != null; s = reader.ReadLine())
                    lst.Add(s);
                return Valider(new Carte(ToData(Valider(lst))));
            }
        }
        public static List<string> Valider(List<string> listeLignesFichier)
        {
            int longeur = listeLignesFichier[0].Length;
            if(listeLignesFichier.Count == 0)
            {
                throw new listeDeLigneVideException();
            }
            foreach(string ligne in listeLignesFichier)
            {

                if (ligne == null && !(ligne.Length == longeur))
                {
                    throw new ligneVideException();
                }
                

            }
            return listeLignesFichier;
        }
        public static Carte Valider(Carte carte)
        {


            for (int ligne = 0; ligne < carte.Hauteur; ++ligne)
            {

                for (int colonne = 0; colonne < carte.Largeur; ++colonne)
                {
                    if (!CharValide(carte[ligne, colonne])){
                        throw new charInvalideException();
                    }
                }
            }
            return carte;
        }
        protected static bool CharValide(char charAVerifier) => charValide.Contains(charAVerifier);
        public static char[,] ToData(List<string> listeLigneFichier)
        {
            int nbColonne = listeLigneFichier[0].Length;
            int nbLigne = listeLigneFichier.Count;
            char[,] tab2d = new char[nbLigne, nbColonne];
            char[] tabligne;
            for(int ligne = 0; ligne < nbLigne; ++ligne)
            {
                tabligne = listeLigneFichier[ligne].ToCharArray();
                for(int colonne = 0; colonne < nbColonne; ++colonne)
                {
                    tab2d[ligne, colonne] = tabligne[colonne];
                }
            }
            return tab2d;
        }

    }
}
