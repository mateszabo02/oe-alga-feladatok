using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Paradigmak
{
    public interface IVegrehajthato
    {
        void Vegrehajtas();
    }
    public class TaroloMegteltKivetel() : Exception
    {

    }
    public class FeladatTarolo<T> : IEnumerable<T> where T : IVegrehajthato
    {

        protected T[] tarolo;
        protected int  n = 0;
        public FeladatTarolo(int meret)
        {
            tarolo = new T[meret];
        }

        public void Felvesz(T elem)
        {
            if (tarolo.Length > n)
            {
                tarolo[n] = elem;
                n++;
            }
            else throw new TaroloMegteltKivetel();
        }

        public virtual void MindentVegrehajt()
        {
            for(int i = 0; i < tarolo.Length; i++)
            {
                tarolo[i].Vegrehajtas();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            FeladatTaroloBejaro<T> bejaro = new FeladatTaroloBejaro<T> (tarolo, n);
            return bejaro;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    public interface IFuggo
    {
        public bool FuggosegTeljesul { get; }
    }
    public class FuggoFeladatTarolo<T> : FeladatTarolo<T> where T : IVegrehajthato, IFuggo
    {
        public FuggoFeladatTarolo(int meret) : base(meret)
        {
        }
        public override void MindentVegrehajt()
        {
            for(int i = 0; i<tarolo.Length; i++)
            {
                if (tarolo[i].FuggosegTeljesul)
                {
                    tarolo[i].Vegrehajtas();
                }
            }
        }
    }

    class FeladatTaroloBejaro<T> : IEnumerator<T>
    {
        T[] tarolo;
        int n;
        int aktualisIndex = -1;

        public FeladatTaroloBejaro(T[] tarolo, int n)
        {
            this.n = n;
            this.tarolo = tarolo;
        }

        public T Current
        {
            get { return tarolo[aktualisIndex]; }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if (aktualisIndex < n)
            {
                aktualisIndex++;
                return true;
            }
            return false;

        }

        public void Reset()
        {
            aktualisIndex=-1;
        }
    }
}
