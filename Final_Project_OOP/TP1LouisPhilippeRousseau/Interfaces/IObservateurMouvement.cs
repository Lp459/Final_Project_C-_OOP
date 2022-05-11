using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1LouisPhilippeRousseau
{
    public interface IObservateurMouvement
    {
        void MouvementObservé(Carte carte);
    }
}
