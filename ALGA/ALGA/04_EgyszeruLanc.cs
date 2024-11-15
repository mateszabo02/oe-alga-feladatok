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
        public LancElem(T tart, LancElem<T> kov)
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
        LancElem<T>? fej;
        LancElem<T>? vege;
        public LancoltSor()
        {
            fej = null;
        }
        public bool Ures
        {
            get { return fej == null; }
        }

        public T Elso()
        {
            if(fej == null)
            {
                return fej.tart;
            }
            else { throw new NincsElemKivetel(); }
        }

        public void Sorba(T ertek)
        {
            LancElem<T> uj = new LancElem<T>(ertek, fej);
            if(vege == null)
            {
                vege.kov = uj;
            }
            else
            {
                fej = uj;
            }
            vege = uj;
        }

        public T Sorbol()
        {
            if( fej != null)
            {
                T ertek = fej.tart;
                fej = fej.kov;
                if(fej == null)
                {
                    vege = null;
                }
                return ertek;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }
    }
    public class LancoltLista<T> : Lista<T>
    {
        LancElem<T>? fej;
        int elemszam = 0;
        public int Elemszam
        {
            get {   return elemszam; }
        }
        public LancoltLista()
        {
            fej = null;
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
            LancElem<T> uj = new LancElem<T>(ertek, fej);
            if( fej == null)
            {
                fej = uj;
            }
            else
            {
                LancElem<T> temp = fej;
                while(temp.kov != null)
                {
                    temp = temp.kov;
                }
                temp.kov = uj;
            }
            elemszam++;
        }

        public T Kiolvas(int index)
        {
            LancElem<T> temp = fej;
            int i = 0;
            while(temp != null && i < index)
            {
                temp = temp.kov;
                i++;
            }
            if (temp == null)
            {
                return temp.tart;
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Modosit(int index, T ertek)
        {
            LancElem<T> temp = fej;
            int i = 0;
            while(temp != null && i < index)
            {
                temp = temp.kov;
                i++;
            }
            if(temp != null)
            {
                temp.tart = ertek;
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Torol(T ertek)
        {
            LancElem<T> temp = fej;
            LancElem<T> e = null;
            do
            {
                while (temp != null && !temp.tart.Equals(ertek))
                {
                    e = temp;
                    temp = temp.kov;
                }
                if (temp != null)
                {
                    LancElem<T> q = temp.kov;
                    if (e == null)
                    {
                        fej = q;
                    }
                    else
                    {
                        e.kov = q;
                    }
                    temp = q;
                    elemszam--;
                }
            } while (temp != null);
        }
    }
    public class LancoltListaBejaro<T> : IEnumerator<T>
    {
        LancElem<T>? fej;
        LancElem<T>? aktualisElem;
        public T Current
        {
            get { return aktualisElem.tart; }
        }
        public LancoltListaBejaro(LancElem<T>? fej)
        {
            this.fej = fej;
        }

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if(aktualisElem == null)
            {
                aktualisElem = fej;
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
        }
    }
}
