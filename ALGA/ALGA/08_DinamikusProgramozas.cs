using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Optimalizalas
{
    public class DinamikusHatizsakPakolas
    {
        HatizsakProblema problema;
        public DinamikusHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
        }
        public int LepesSzam { get; private set; }
        int[,] TablazatFeltoltes()
        {
            int[,] F = new int[problema.n+1, problema.Wmax+1];
            for (int t = 0; t <= problema.n; t++)
            {
                F[t, 0] = 0;
            }
            for (int h = 0; h <= problema.Wmax; h++)
            {
                F[0,h] = 0;
            }
            for(int t=1; t <= problema.n; t++)
            {
                for(int h = 1;h <= problema.Wmax; h++)
                {
                    int nem = F[t - 1, h];
                    if(h>= problema.w[t-1])
                    {
                        int igen = F[t - 1, h - problema.w[t - 1]] + (int)Math.Round(problema.p[t - 1]);
                        F[t,h] = Math.Max(igen, nem);
                    }
                    else
                    {
                        F[t, h] = nem;
                    }
                }
            }
            return F;
        }
        public float OptimalisErtek()
        {
            return TablazatFeltoltes()[problema.n,problema.Wmax];
        }
        public bool[] OptimalisMegoldas()
        {
            bool[] O = new bool[problema.n];
            for(int i = 0; i < O.Length; i++)
            {
                O[i] = false;
            }
            int[,] tablazat = TablazatFeltoltes();
            int t = problema.n;
            int h= problema.Wmax;
            while(t>0 && h>0)
            {
                LepesSzam++;
                if (tablazat[t,h] != tablazat[t - 1, h])
                {
                    O[t-1] = true;
                    h -= problema.w[t - 1];
                }
                t--;
            }
            return O;
        }
    }
}
