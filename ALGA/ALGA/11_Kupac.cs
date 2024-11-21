using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class Kupac<T>
    {
        protected T[] E;
        protected int n;
        protected Func<T, T, bool> nagyobbPrioritas;
        public Kupac(T[] E, int n, Func<T,T,bool> nagyobbPrioritas)
        {
            this.E = E;
            this.n = n;
            this.nagyobbPrioritas = nagyobbPrioritas;
            KupacotEpit();
        }
        public static int Bal(int i)
        {
            return 2 * i;
        }
        public static int Jobb(int i)
        {
            return 2 * i + 1;
        }
        public static int Szulo(int i)
        {
            return i / 2;
        }
        public void Kupacol(int i)
        {
            int b = Bal(i);
            int j = Jobb(i);
            int max;
            if(b<n && nagyobbPrioritas(E[b], E[i]))
            {
                max = b;
            }
            else
            {
                max = i;
            }
            if(j<n && nagyobbPrioritas(E[j], E[max]))
            {
                max = j;
            }
            if(max != i)
            {
                T temp = E[i];
                E[i] = E[max];
                E[max] = temp;
                Kupacol(max);
            }
        }
        public void KupacotEpit()
        {
            for(int i = n/2; i>=0; i--)
            {
                Kupacol(i);
            }
        }
    }
    public class KupacRendezes<T> : Kupac<T> where T : IComparable
    {
        public KupacRendezes(T[] E) : base(E, E.Length, (x,y) => x.CompareTo(y) >0)
        {
        }
        public void Rendezes()
        {
            KupacotEpit();
            for(int i=n-1; i>=1; i--)
            {
                T temp = E[0];
                E[0] = E[i];
                E[i] = temp;
                n--;
                Kupacol(0);
            }
        }
    }
    public class KupacPrioritasosSor<T> : Kupac<T>, PrioritasosSor<T>
    {
        public KupacPrioritasosSor(int meret, Func<T,T,bool> nagyobbPrioritas) : base(new T[meret], 0, nagyobbPrioritas)
        {

        }
        public void KulcsotFelvisz(int i)
        {
            int sz = Szulo(i);
            if (sz >= 0 && nagyobbPrioritas(E[i], E[sz]))
            {
                T temp = E[sz];
                E[sz] = E[i];
                E[i] = temp;
                KulcsotFelvisz(sz);
            }
        }
        public bool Ures => (n == 0);

        public T Elso()
        {
            if (Ures)
            {
                throw new NincsElemKivetel();
            }
            return E[0];
        }

        public void Frissit(T elem)
        {
            int i = 0;
            while(i<n && !E[i].Equals(elem))
            {
                i++;
            }
            if (i < n)
            {
                KulcsotFelvisz(i);
                Kupacol(i);
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }


        public void Sorba(T ertek)
        {
            if(n<E.Length)
            {
                n++;
                E[n-1] = ertek;
                KulcsotFelvisz(n-1);
            }
            else
            {
                throw new NincsHelyKivetel();
            }
        }

        public T Sorbol()
        {
            if (!Ures)
            {
                T max = E[0];
                E[0] = E[n-1];
                n--;
                Kupacol(0);
                return max;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }
    }
}
