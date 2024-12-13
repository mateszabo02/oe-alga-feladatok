using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    public class Utkereses
    {
        public static Szotar<V, float> Dijkstra<V, E>(SulyozottGraf<V, E> g, V start)
        {
            Szotar<V, float> L = new HasitoSzotarTulcsordulasiTerulettel<V, float>(g.CsucsokSzama);
            Szotar<V, V> P = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.CsucsokSzama);
            KupacPrioritasosSor<V> S = new KupacPrioritasosSor<V>(g.CsucsokSzama, (ez, ennel) => L.Kiolvas(ez).CompareTo(L.Kiolvas(ennel))<0);
            g.Csucsok.Bejar(x =>
            {
                L.Beir(x, float.MaxValue);
                S.Sorba(x);
            });
            L.Beir(start, 0);
            S.Frissit(start);
            while (!S.Ures)
            {
                V u = S.Sorbol();
                g.Szomszedai(u).Bejar(x =>
                {
                    if (L.Kiolvas(u) + g.Suly(u, x) < L.Kiolvas(x))
                    {
                        L.Beir(x, (L.Kiolvas(u) + g.Suly(u, x)));
                        P.Beir(x, u);
                        S.Frissit(x);
                    }
                });

            }
            return L;
        }
    }
    public class FeszitofaKereses
    {
        public static Szotar<V, V> Prim<V, E>(SulyozottGraf<V, E> g, V start) where V : IComparable
        {
            Szotar<V, float> K = new HasitoSzotarTulcsordulasiTerulettel<V, float>(g.CsucsokSzama);
            Szotar<V, V> P = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.CsucsokSzama);
            KupacPrioritasosSor<V> S = new KupacPrioritasosSor<V>(g.CsucsokSzama, (ez, ennel) => K.Kiolvas(ez).CompareTo(K.Kiolvas(ennel))<0);
            Halmaz<V> W = new FaHalmaz<V>();
            g.Csucsok.Bejar(x =>
            {
                K.Beir(x, float.MaxValue);
                S.Sorba(x);
                W.Beszur(x);
            });
            K.Beir(start, 0);
            S.Frissit(start);

            while (!S.Ures)
            {
                V u = S.Sorbol();
                W.Torol(u);
                g.Szomszedai(u).Bejar(x =>
                {
                    if (W.Eleme(x) && g.Suly(u, x) < K.Kiolvas(x))
                    {
                        K.Beir(x, g.Suly(u, x));
                        P.Beir(x, u);
                        S.Frissit(x);
                    }
                });
            }

            return P;
        }


        public static Halmaz<E> Kruskal<V, E>(SulyozottGraf<V, E> g) where E : SulyozottGrafEl<V>, IComparable
        {
            PrioritasosSor<E> S = new KupacPrioritasosSor<E>(g.ElekSzama, (ez, ennel) => ez.Suly < ennel.Suly);
            Halmaz<E> A = new FaHalmaz<E>();
            Szotar<V, int> vhalmaz = new HasitoSzotarTulcsordulasiTerulettel<V, int>(g.CsucsokSzama);
            int i = 0;
            g.Csucsok.Bejar(x => { vhalmaz.Beir(x, i++); });

            g.Elek.Bejar(x =>
            {
                S.Sorba(x);
            });
            ;
            while (!S.Ures)
            {
                E e = S.Sorbol();
                if (!vhalmaz.Kiolvas(e.Honnan).Equals(vhalmaz.Kiolvas(e.Hova)))
                {
                    if (!A.Eleme(e)) A.Beszur(e);

                    int q1 = vhalmaz.Kiolvas(e.Honnan);
                    int q2 = vhalmaz.Kiolvas(e.Hova);

                    g.Csucsok.Bejar(x =>
                    {
                        if (vhalmaz.Kiolvas(x).Equals(q1))
                        {
                            vhalmaz.Beir(x, q2);
                        }

                    });
                    ;
                }

            }
            return A;
        }
    }
}
