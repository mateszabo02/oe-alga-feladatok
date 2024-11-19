using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class LancElem<T>
    {
        public T tart;
        public LancElem<T>? kov;
        public LancElem(T tart, LancElem<T>? kov)
        {
            this.tart = tart;
            this.kov = kov;
        }
    }
    public class LancoltVerem<T> : Verem<T>
    {
        LancElem<T>? fej;
        public LancoltVerem()
        {
            fej = null;
        }
        public bool Ures
        {
            get
            {
                return fej == null;
            }
        }
        
        public T Felso()
        {
            if(fej != null)
            {
                return fej.tart;
            }
            else { throw new NincsElemKivetel(); }
        }

        public void Verembe(T ertek)
        {
            LancElem<T> uj = new LancElem<T>(ertek, fej);
            fej = uj;
        }

        public T Verembol()
        {
            if( fej == null )
            {
                throw new NincsElemKivetel();
            }
            else
            {
                T ertek = fej.tart;
                fej = fej.kov;
                return ertek;
            }
        }
    }
    public class LancoltSor<T> : Sor<T>
    {
        LancElem<T>? fej = null;
        LancElem<T>? vege = null;
        public bool Ures
        {
            get { return fej == null && vege == null; }
        }

        public T Elso()
        {
            if(fej == null)
            {
                throw new NincsElemKivetel();
            }
            else { return fej.tart; }
        }

        public void Sorba(T ertek)
        {
            LancElem<T> uj = new LancElem<T>(ertek, null);
            if(fej == null)
            {
                fej = uj;
            }
            else
            {
                vege.kov = uj;
            }
            vege = uj;
        }

        public T Sorbol()
        {
            if (fej == null)
            {
                throw new NincsElemKivetel();

            }
            if(fej == vege)
            {
                vege = null;
            }
            T ertek = fej.tart;
            fej = fej.kov;
            return ertek;
        }
    }
    public class LancoltLista<T> : Lista<T>, IEnumerable<T>
    {
        LancElem<T>? fej = null;
        int elemszam = 0;
        public int Elemszam
        {
            get {   return elemszam; }
        }

        public void Bejar(Action<T> muvelet)
        {
            LancElem<T> temp = fej;
            while(temp != null )
            {
                muvelet(temp.tart);
                temp = temp.kov;
            }
        }

        public void Beszur(int index, T ertek)
        {
            if (fej == null || index == 0)
            {
                LancElem<T> uj = new LancElem<T>(ertek, fej);
                fej = uj;
                elemszam++;
            }
            else
            {
                LancElem<T> temp = fej;
                int i = 1;
                while (temp.kov != null && i < index)
                {
                    temp = temp.kov;
                    i++;
                }
                if (i <= index)
                {
                    LancElem<T> uj = new LancElem<T>(ertek, temp.kov);
                    temp.kov = uj;
                }
                else { throw new HibasIndexKivetel(); }
            }
        }

        public void Hozzafuz(T ertek)
        {
            Beszur(elemszam, ertek);
        }

        public T Kiolvas(int index)
        {
            if(index<0 || index >= elemszam)
            {
                throw new HibasIndexKivetel() ;
            }
            LancElem<T> temp = fej;
            for(int i = 0; i < index; i++)
            {
                temp = temp.kov;
            }
            return temp.tart;
        }

        public void Modosit(int index, T ertek)
        {
            if(index<0 || index >= elemszam)
            {
                throw new HibasIndexKivetel();
            }
            LancElem<T> temp = fej;
            for(int i=0; i < index; i++)
            {
                temp = temp.kov;
            }
            temp.tart = ertek;
        }

        public void Torol(T ertek)
        {
            LancElem<T>? temp = fej;
            LancElem<T>? e = null;
            while (temp.kov != null)
            {
                if (temp.tart.Equals(ertek))
                {
                    if(temp == fej)
                    {
                        fej = fej.kov;
                    }
                    else
                    {
                        e.kov = temp.kov;
                    }
                    elemszam--;
                }
                e = temp;
                temp = temp.kov;
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new LancoltListaBejaro<T>(fej);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
    public class LancoltListaBejaro<T> : IEnumerator<T>
    {
        LancElem<T>? fej;
        LancElem<T>? aktualisElem;
        bool elott;
        public T Current
        {
            get { return aktualisElem.tart; }
        }
        public LancoltListaBejaro(LancElem<T>? fej)
        {
            this.fej = fej;
            this.aktualisElem = null;
            elott = true;
        }

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if(aktualisElem == null)
            {
                if (elott)
                {
                    elott = false;
                    aktualisElem = fej;
                }
            }
            else
            {
                aktualisElem = aktualisElem.kov;
            }
            return aktualisElem != null;
        }

        public void Reset()
        {
            aktualisElem = null;
            elott = false;
        }
    }
}
