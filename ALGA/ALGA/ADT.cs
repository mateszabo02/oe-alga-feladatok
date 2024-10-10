using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public interface Lista<T>
    {
        public int Elemszam { get; }
        public T Kiolvas(int index);
        public void Modosit(int index, T ertek);
        public void Hozzafuz(T ertek);
        public void Beszur(int index, T ertek);
        public void Torol(T ertek);
        public void Bejar(Action<T> muvelet);
    }
    public interface Halmaz<T>
    {
        public void Beszur(T ertek);
        public bool Eleme(T ertek);
        public void Torol(T ertek);
        public void Bejar(Action<T> muvelet);
    }
    public interface Verem<T>
    {
        bool Ures { get; }
        void Verembe(T ertek);
        T Verembol();
        T Felso();
    }
    public interface Sor<T>
    {
        bool Ures { get;}
        void Sorba(T ertek);
        T Sorbol();
        T Elso();
    }
    public class NincsElemKivetel : Exception { }
    public class NincsHelyKivetel: Exception { }
    public class HibasIndexKivetel : Exception { }
}
