using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class SulyozottEgeszGrafEl : EgeszGrafEl, SulyozottGrafEl<int>
    {
        public SulyozottEgeszGrafEl(int honnan, int hova, float suly) : base(honnan, hova)
        {
            this.Suly = suly;
        }

        public float Suly { get; }
    }
    public class CsucsmatrixSulyozottEgeszGraf : SulyozottGraf<int, SulyozottEgeszGrafEl>
    {
        int n;
        float[,] M;
        public CsucsmatrixSulyozottEgeszGraf(int n)
        {
            this.n = n;
            M = new float[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    M[i, j] = float.NaN;
                }
            }
        }
        public int CsucsokSzama{ get { return n; } }

        public int ElekSzama
        {
            get
            {
                int cnt = 0; ;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (M[i, j] != float.NaN)
                        {
                            cnt++;
                        }
                    }
                }
                return cnt;
            }
        }

        public Halmaz<int> Csucsok
        {
            get
            {
                Halmaz<int> v = new FaHalmaz<int>();
                for(int i = 0;i < n; i++)
                {
                    v.Beszur(i);
                }
                return v;
            }
        }

        public Halmaz<SulyozottEgeszGrafEl> Elek
        {
            get
            {
                Halmaz<SulyozottEgeszGrafEl> e = new FaHalmaz<SulyozottEgeszGrafEl> ();
                for(int i = 0; i<n; i++)
                {
                    for(int  j = 0; j < n; j++)
                    {
                        if (!M[i, j].Equals(float.NaN))
                        {
                            e.Beszur(new SulyozottEgeszGrafEl(i, j, Suly(i,j)));
                        }
                    }
                }
                return e;
            }
        }

        public float Suly(int honnan, int hova)
        {
            if(VezetEl(honnan, hova))
            {
                return M[honnan, hova];
            }
            else
            {
                throw new NincsElKivetel();
            }
        }

        public Halmaz<int> Szomszedai(int csucs)
        {
            Halmaz<int> v = new FaHalmaz<int> ();
            for(int i = 0;i<n;i++)
            {
                if(VezetEl(csucs, i))
                {
                    v.Beszur (i);
                }
            }
            return v;
        }

        public void UjEl(int honnan, int hova, float suly)
        {
            M[honnan, hova] = suly;
        }

        public bool VezetEl(int honnan, int hova)
        {
            return !float.IsNaN(M[honnan, hova]);
        }
    }
    /*public class Utkereses
    {
        public static Szotar<V,float> Dijkstra<V,E>(SulyozottGraf<V,E> g, V start)
        {
            Szotar<V, float> L = new HasitoSzotarTulcsordulasiTerulettel<V, float>(g.CsucsokSzama);
            Szotar<V, V> P = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.CsucsokSzama);
            KupacPrioritasosSor<V> S = new KupacPrioritasosSor<V>(g.CsucsokSzama, (ez, ennel) => L.Kiolvas(ez) < L.Kiolvas(ennel));
            g.Csucsok.Bejar(x =>
            {
                L.Beir(x, float.MaxValue);
                S.Sorba(x);
            });
            L.Beir(start, 0);
            S.Frissit(start);
            while (S != null)
            {
                V u = S.Elso();


            }
            
        }
    }*/
    /*public class FeszitFaKereses
    {
        public static Halmaz<E> Kruskal<V,E>(SulyozottGraf<V,E> g) where E : SulyozottGrafEl<V>, IComparable
        {
            Halmaz<E> A = new FaHalmaz<E>();
            Szotar<V, int> vhalmaz = new HasitoSzotarTulcsordulasiTerulettel<V, int>(g.CsucsokSzama);
            int i = 0;
            g.Csucsok.Bejar(x => { vhalmaz.Beir(x, i++); });

        }
    }*/
}
