using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public interface Halmaz<T>
    {
        public void Beszur(T ertek);
        public void Eleme(T ertek);
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
}
