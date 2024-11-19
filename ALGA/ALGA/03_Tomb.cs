using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class TombVerem<T> : Verem<T>
    {
        int n;
        T[] E;
        public TombVerem(int meret) 
        {
            E = new T[meret];
        }

        public bool Ures
        {
            get { return n == 0; }
        }

        public T Felso()
        {
            if (!Ures)
            {
                return E[n - 1];
            }
            else { throw new NincsElemKivetel(); }

        }

        public void Verembe(T ertek)
        {
            if(n >= E.Length)
            {
                throw new NincsHelyKivetel();
            }
            else
            {
                E[n] = ertek;
                n++;
            }
        }

        public T Verembol()
        {
            if (!Ures)
            {
                return E[--n];
            }
            else 
            {
                throw new NincsElemKivetel();
            }
        }
    }
    public class TombSor<T> : Sor<T>
    {
        T[] E;
        int e;
        int u;
        int n;
        public TombSor(int meret)
        {
            E = new T[meret];
        }
        public bool Ures
        {
            get { return n == 0; }
        }

        public T Elso()
        {
            if (!Ures)
            {
                return E[e];
            }
            else { throw new NincsElemKivetel(); }
        }

        public void Sorba(T ertek)
        {
           if(n >= E.Length)
            {
                throw new NincsHelyKivetel();
            }
            else
            {
                E[u] = ertek;
                u = (u+1) % E.Length;
                n++;
            }
        }

        public T Sorbol()
        {
            if (!Ures)
            {
                T temp = E[e];
                e = (e+1) % E.Length;
                n--;
                return temp;
            }
            else throw new NincsElemKivetel();
        }
    }
    public class TombLista<T> : Lista<T>, IEnumerable<T>
    {
        T[] E;
        int n;
        public int Elemszam
        {
            get { return n; }
        }
        public TombLista(int meret = 5)
        {
            E = new T[meret];
            n = 0;
        }
        public T Kiolvas(int index)
        {
            if (index >= E.Length || index < 0)
            {
                throw new HibasIndexKivetel();
            }
            else { return E[index]; }
        }

        public void Modosit(int index, T ertek)
        {
            if(index >= E.Length || index < 0)
            {
                throw new HibasIndexKivetel();
            }
            else
            {
                E[index] = ertek;
            }
        }

        public void Hozzafuz(T ertek)
        {
            Beszur(n, ertek);
        }

        public void Beszur(int index, T ertek)
        {
            if(index > n || index < 0)
            {
                throw new HibasIndexKivetel();
            }
            else if (n<E.Length)
            {
                for(int i=n; i>index; i--)
                {
                    E[i] = E[i - 1];
                }
                E[index] = ertek;
                n++;
            }
            else
            {
                T[] Temp = E;
                E = new T[n * 2];
                for (int i = 0; i < n; i++)
                {
                    E[i] = Temp[i];
                }
                E[index] = ertek;
                n++;
            }
        }

        public void Torol(T ertek)
        {
            int cnt = 0;
            for(int i=0; i<n; i++)
            {
                if (E[i].Equals(ertek))
                {
                    cnt++;
                }
                else
                {
                    E[i - cnt] = E[i];
                }
            }
            n -= cnt;
        }

        public void Bejar(Action<T> muvelet)
        {
            for(int i=0; i<n; i++)
            {
                muvelet(E[i]);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            TombListaBejaro<T> bejaro = new TombListaBejaro<T>(E, n);
            return bejaro;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
    public class TombListaBejaro<T> : IEnumerator<T>
    {
        T[] E;
        int n;
        int aktualisIndex = -1;
        public T Current
        {
            get { return E[aktualisIndex]; }
        }
        public TombListaBejaro(T[] E, int n)
        {
            this.E = E;
            this.n = n;
        }

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            aktualisIndex++;
            return aktualisIndex < n;
        }

        public void Reset()
        {
            aktualisIndex = -1;
        }
    }
}
