using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OE.ALGA.Adatszerkezetek
{
    public class EgeszGrafEl : GrafEl<int>, IComparable
    {
        public int Honnan {  get; }

        public int Hova { get; }
        public EgeszGrafEl(int honnan, int hova)
        {
            Honnan = honnan;
            Hova = hova;
        }

        public virtual int CompareTo(object? obj)
        {
            if (obj != null && obj is EgeszGrafEl b)
            {
                if(Honnan  != b.Honnan)
                {
                    return Honnan.CompareTo(b.Honnan);
                }
                else return Hova.CompareTo(b.Hova);
            }
            else throw new InvalidOperationException();
        }
    }
    public class CsucsmatrixSulyozatlanEgeszGraf : SulyozatlanGraf<int, EgeszGrafEl>
    {
        int n;
        bool[,] M;
        public CsucsmatrixSulyozatlanEgeszGraf(int n)
        {
            this.n = n;
            this.M = new bool[n, n];
        }
        public int CsucsokSzama { get { return n; } }

        public int ElekSzama
        {
            get
            {
                int cnt = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (M[i, j] == true)
                        {
                            cnt++;
                        }
                    }
                }
                /*for (int i = 0; i < M.GetLength(0); i++)
                {
                    for (int j = 0; j < M.GetLength(1); j++)
                    {
                        cnt += Convert.ToInt32(M[i, j]);
                    }
                }*/
                return cnt;
            }
        }

        public Halmaz<int> Csucsok
        {
            get
            {
                FaHalmaz<int> csucsok = new FaHalmaz<int>();
                for (int i = 0; i < n; i++)
                {
                    csucsok.Beszur(i);
                }
                return csucsok;
            }
        }


        public Halmaz<EgeszGrafEl> Elek
        {
            get
            {
                FaHalmaz<EgeszGrafEl> el = new FaHalmaz<EgeszGrafEl>();
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (M[i, j] == true)
                        {
                            el.Beszur(new EgeszGrafEl(i, j));
                        }
                    }
                }
                return el;
            }
        }

        public Halmaz<int> Szomszedai(int csucs)
        {
            FaHalmaz<int> csucsok = new FaHalmaz<int>();
            for (int i = 0; i < n; i++)
            {
                if (M[csucs, i])
                {
                    csucsok.Beszur(i);
                }
            }
            return csucsok;
        }

        public void UjEl(int honnan, int hova)
        {
            M[honnan, hova] = true;
        }

        public bool VezetEl(int honnan, int hova)
        {
            return M[honnan, hova];
        }
    }
    public class GrafBejarasok
    {
        public static Halmaz<V> SzelessegiBejaras<V, E>(Graf<V, E> g, V start, Action<V> muvelet) where V : IComparable
        {
            Sor<V> S = new LancoltSor<V>();
            S.Sorba(start);
            Halmaz<V> F = new FaHalmaz<V>();
            F.Beszur(start);
            while (!S.Ures)
            {
                V k = S.Sorbol();
                muvelet(k);
                g.Szomszedai(k).Bejar(x =>
                {
                    if (!F.Eleme(x))
                    {
                        F.Beszur(x);
                        S.Sorba(x);
                    }
                });
            }
            return F;
        }
        public static FaHalmaz<V> MelysegiBejaras<V,E>(Graf<V,E> g, V start, Action<V> muvelet) where V : IComparable
        {
            FaHalmaz<V> F = new FaHalmaz<V>();
            MelysegiBejarasRekurzio(g, start, F, muvelet);
            return F;
        }
        public static void MelysegiBejarasRekurzio<V,E>(Graf<V,E> g, V k, FaHalmaz<V> F, Action<V> muvelet) where V : IComparable
        {
            F.Beszur(k);
            muvelet(k);
            g.Szomszedai(k).Bejar((V x) =>
            {
                if (!F.Eleme(x))
                {
                    MelysegiBejarasRekurzio(g,x,F,muvelet);
                }
            });
        }
    }
}
