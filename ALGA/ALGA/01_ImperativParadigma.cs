using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Paradigmak
{
    public interface IVegrehajto
    {
        void Vegrehajtas();
    }
    public class FeladatTarolo<T> where T : IVegrehajto
    {

        T[] tarolo;
        int n = 0;
        public FeladatTarolo(int meret)
        {
            tarolo = new T[meret];
        }

        void Felvesz(T elem)
        {
            if (tarolo.Length > n)
            {
                tarolo[n] = elem;
                n++;
            }
            else throw new TaroloMegteltKivetel();
        }

        void MindentVegrehajt()
        {
            for(int i = 0; i < tarolo.Length; i++)
            {
                tarolo[i].Vegrehajtas();
            }
        }

    }
    public interface IFuggo
    {
        protected bool FuggosegTeljesul();
    }
    class FuggoFeladatTarolo : FeladatTarolo<T> where T : IFuggo, IVegrehajto
    {
        public FuggoFeladatTarolo(int meret) : base(meret)
        {
        }
    }

    class TaroloMegteltKivetel() : Exception
    {

    }
}
