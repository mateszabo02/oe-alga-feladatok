using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    class LancElem<T>
    {
        public T tart;
        public LancElem<T>? kov;
        public LancElem(T tart, LancElem<T> kov)
        {
            this.tart = tart;
            this.kov = kov;
        }
    }
    class LancoltVerem<T> : Verem<T>
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
    class LancoltSor<T> : Sor<T>
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
    class LancoltLista<T> : Lista<T>
    {
        LancElem<T>? fej;
        public int Elemszam
        {
            get;
        }
        public LancoltLista()
        {
            fej = null;
        }

        public void Bejar(Action<T> muvelet)
        {
            throw new NotImplementedException();
        }

        public void Beszur(int index, T ertek)
        {
            if (fej == null || index == 0)
            {
                LancElem<T> uj = new LancElem<T>(ertek, fej);
                fej = uj;
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
        }

        public T Kiolvas(int index)
        {
            throw new NotImplementedException();
        }

        public void Modosit(int index, T ertek)
        {
            throw new NotImplementedException();
        }

        public void Torol(T ertek)
        {
            throw new NotImplementedException();
        }
    }
}
