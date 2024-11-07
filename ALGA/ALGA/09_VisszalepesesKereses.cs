using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Optimalizalas
{
    class VisszalepesesOptimalizacio<T>
    {
        int n; //szint
        int[] M; //lehetséges részmegoldások száma
        T[,] R; //lehetséges részmegoldások
        Func<int, T, bool> ft;
        Func<int, T, T[], bool> fk;
        Func<T[], float> josag;
        public VisszalepesesOptimalizacio(int n, int[] m, T[,] r, Func<int, T, bool> ft, Func<int, T, T[], bool> fk, Func<T[], float> josag)
        {
            this.n = n;
            M = m;
            R = r;
            this.ft = ft;
            this.fk = fk;
            this.josag = josag;
        }
        public int LepesSzam {  get; private set; }
        public void Backtrack(int szint, ref T[] E, ref bool van, ref T[] O)
        {
            int i = 0;
            while (i < M[i])
            {
                
                if(ft(szint, R[szint, i]))
                {
                    if(fk(szint, R[szint, i], E))
                    {
                        E[szint] = R[szint, i];
                        if(szint == n)
                        {
                            if(!van || josag(E) > josag(E))
                            {
                                O = E;
                            }
                            van = true;
                        }
                        else
                        {
                            Backtrack(szint+1, ref E, ref van, ref O);
                        }
                    }
                }
                i++;
                LepesSzam++;
            }
        }
        public T[] OptimalisMegoldas()
        {
            T[] O = new T[n];
            T[] E = new T[n];
            bool van = false;
            Backtrack(0, ref E, ref van, ref O);
            return O;
        }
    }
    public class VisszalepesesHatizsakPakolas
    {
        HatizsakProblema problema;
        public VisszalepesesHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
        }
        public int LepesSzam { get; private set; }
        bool[] OptimalisMegoldas()
        {
            int[] M = new int[problema.n];
            for(int i = 0; i < M.Length; i++)
            {
                M[i] = 2;
            }
            bool[,] R = new bool[problema.n, 2];
            for(int i = 0;i < R.Length; i++)
            {
                R[i, 0] = true;
            }
            VisszalepesesOptimalizacio<bool> visszalepeses = new VisszalepesesOptimalizacio<bool>(problema.n, M, R, (int szint, bool r) => true,
                (int szint, bool r, bool[] E) => { E[szint] = r; return problema.OsszSuly(E) <= problema.Wmax; },
                (E) => (int)Math.Round(problema.OsszErtek(E)));
            return visszalepeses.OptimalisMegoldas();
        }
        public float OptimalisErtek()
        {
            bool[] optimalis = OptimalisMegoldas();
            return problema.OsszErtek(optimalis);
        }
    }
    
}
