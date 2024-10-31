using System;
using System.Collections.Generic;
using System.Linq;
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
        float[,] TablazatFeltoltes()
        {
            float[,] F = new float[problema.n, problema.Wmax];
            for (int t = 0; t < problema.n; t++)
            {
                F[t, 0] = 0;
            }
            for (int h = 0; h < problema.Wmax; h++)
            {
                F[0,h] = 0;
            }
            for(int t=0; t < problema.n; t++)
            {
                for(int h = 0;h < problema.Wmax; h++)
                {
                    if(h>= problema.w[t])
                    {
                        F[t,h] = Math.Max(F[t-1,h], F[t - 1, h - problema.w[t] + (int)Math.Round(problema.p[t])]);
                    }
                    else
                    {
                        F[t, h] = F[t-1, h];
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
            float[,] tablazat = TablazatFeltoltes();
            int t = problema.n;
            int h= problema.Wmax;
            while(t>0 && h>0)
            {
                if (tablazat[t,h] != tablazat[t - 1, h])
                {
                    O[t] = true;
                    h = h - problema.Wmax;
                }
                t--;
            }
            return O;
        }
    }
}
