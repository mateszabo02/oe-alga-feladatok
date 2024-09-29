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
        int n = 0;
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
            if(n > E.Length)
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
        int n = 0;
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
                return E[0];
            }
            else { throw new NincsElemKivetel(); }
        }

        public void Sorba(T ertek)
        {
           if(n > E.Length)
            {
                throw new NincsHelyKivetel();
            }
            else
            {
                E[n] = ertek;
                n++;
            }
        }

        public T Sorbol()
        {
            if (!Ures)
            {
                for(int i=1; i<n; i++)
                {
                    E[i-1] = E[i];
                }
                return E[--n];
            }
            else throw new NincsElemKivetel();
        }
    }
    public class TombLista<T> : IEnumerable<T> where T : Lista<T>
    {
        T[] E;
        int n = 0;
        public int Elemszam
        {
            get { return n; }
        }
        public TombLista(int meret)
        {
            E = new T[meret];
        }
        public TombLista(int meret, T[] tomb)
        {
            E = new T[meret];
            for (int i = 0; i < tomb.Length; i++)
            {
                E[i] = tomb[i];
            }
        }
        public T Kiolvas(int index)
        {
            if (index > E.Length || index < 0)
            {
                throw new HibasIndexKivetel();
            }
            else { return E[index]; }
        }

        public void Modosit(int index, T ertek)
        {
            if(index > E.Length || index < 0)
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
            if (n > E.Length)
            {
                T[] Temp = E;
                E = new T[n * 2];
                for(int i=0; i < n; i++)
                {
                    E[i] = Temp[i];
                }
            }
            else
            {
                E[n] = ertek;
                n++;
            }
        }

        public void Beszur(int index, T ertek)
        {
            if(index > E.Length || index < 0)
            {
                throw new HibasIndexKivetel();
            }
            else if (n<E.Length)
            {
                for(int i=n-1; i<=index; i--)
                {
                    E[i] = E[i - 1];
                }
                E[index] = ertek;
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
            }
        }

        public void Torol(T ertek)
        {
            if (n != 0)
            {
                int i = 0;
                while (i < n)
                {
                    if (E[i].Equals(ertek))
                    {
                        for(int j=i; j<n-1; j++)
                        {
                            E[j] = E[j+1];
                        }
                    }
                    n--;
                }
            }
            else { throw new NincsElemKivetel(); }
        }

        public void Bejar(Action<T> muvelet)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            TombListaBejaro<T> bejaro = new TombListaBejaro<T>(E, n);
            return bejaro;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    class TombListaBejaro<T> : IEnumerator<T>
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
            aktualisIndex = -1;
        }
    }
}
