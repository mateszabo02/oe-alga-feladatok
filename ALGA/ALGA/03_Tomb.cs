using System;
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
                return E[n - 1];
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
                return E[--n];
            }
            else throw new NincsElemKivetel();
        }
    }
    public class TombLista<T>
    {
        T[] E;
        int n = 0;
        public int Elemszam
        {
            get { return n; }
        }
    }

}
