using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Paradigmak
{
    class FeltetelesFeladatTarolo<T> : FeladatTarolo<T>, IEnumerable<T> where T : IVegrehajthato
    {
        public FeltetelesFeladatTarolo(int meret) : base(meret)
        {
        }
        public void FeltetelesVegrehajtas(Func<T, bool> feltetel)
        {
            for (int i = 0; i < n; i++)
            {
                if (feltetel(tarolo[i]))
                {
                    tarolo[i].Vegrehajtas();
                }
            }
        }
        public Func<T,bool> BejaroFeltetel 
        {
            get;
            set;
        }
        public IEnumerator<T> GetEnumerator()
        {
            if (BejaroFeltetel == null)
            {
                Func<T, bool> func = t => true;
                FeltetelesFeladatTaroloBejaro<T> bejaro = new FeltetelesFeladatTaroloBejaro<T>(tarolo, n, func);
                return bejaro;
            }
            else {
                FeltetelesFeladatTaroloBejaro<T> bejaro = new FeltetelesFeladatTaroloBejaro<T>(tarolo, n, BejaroFeltetel);
                return bejaro;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
    class FeltetelesFeladatTaroloBejaro<T> : IEnumerator<T>
    {
        T[] tarolo;
        int n;
        int aktualisIndex = -1;
        Func<T, bool> feltetel;
        public FeltetelesFeladatTaroloBejaro(T[] tarolo, int n, Func<T, bool> feltetel)
        {
            this.tarolo = tarolo;
            this.n = n;
            this.feltetel = feltetel;
        }

        public T Current
        {
            get
            {
                return tarolo[aktualisIndex];
            }
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
                if (feltetel(tarolo[aktualisIndex]))
                {
                    return true;
                }
                aktualisIndex++;
            }
            return false;
        }

        public void Reset()
        {
            aktualisIndex = -1;
        }
    }
}
