using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Optimalizalas
{
    public class HatizsakProblema
    {
        public int n { get; }
        public int Wmax {  get; }
        public int[] w { get; }
        public float[] p { get; }
        public HatizsakProblema(int n, int wmax, int[] w, float[] p)
        {
            this.n = n;
            Wmax = wmax;
            this.w = w;
            this.p = p;
        }
        public int OsszSuly(bool[] pakolas)
        {
            int osszw = 0;
            for(int i=0; i<pakolas.Length; i++)
            {
                if (pakolas[i])
                {
                    osszw += w[i];
                }
            }
            return osszw;
        }
        public float OsszErtek(bool[] pakolas)
        {
            float osszp = 0;
            for(int i=0; i<pakolas.Length; i++)
            {
                if (pakolas[i])
                {
                    osszp += p[i];
                }
            }
            return osszp;
        }
        public bool Ervenyes(bool[] pakolas)
        {
            return OsszSuly(pakolas)<=Wmax;
        }
    }
    public class NyersEro<T>
    {
        int m;
        Func<int, T> generator;
        Func<T, float> josag;
        public NyersEro(int m, Func<int, T> generator, Func<T, float> josag)
        {
            this.m = m;
            this.generator = generator;
            this.josag = josag;
        }
        public int LepesSzam { get; set; }
        public T OptimalisMegoldas()
        {
            T o = generator(1);
            for(int i=2; i<=m; i++)
            {
                T x = generator(i);
                LepesSzam++;
                if (josag(x) > josag(o))
                {
                    o = x;
                }
            }
            return o;
        }
    }
    public class NyersEroHatizsakPakolas
    {
        public int LepesSzam { get; private set; }
        HatizsakProblema problema;
        public NyersEroHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
        }
        public bool[] Generator(int i)
        {
            int szam = i - 1;
            bool[] K = new bool[problema.n];
            for(int j= 0; j<problema.n; j++)
            {
                K[j]= Convert.ToBoolean(i& 1);
                i >>= 1;
            }
            return K;
        }
        public float Josag(bool[] pakolas)
        {
            if (problema.Ervenyes(pakolas))
            {
                return problema.OsszErtek(pakolas);
            }
            return -1;
        }
        public bool[] OptimalisMegoldas()
        {
            int lehetseges = 1 << problema.n;
            NyersEro<bool[]> nyersEro = new NyersEro<bool[]>(lehetseges, this.Generator, this.Josag);
            bool[] optimalis = nyersEro.OptimalisMegoldas();
            this.LepesSzam = nyersEro.LepesSzam;
            return optimalis;
        }
        public float OptimalisErtek()
        {
            return problema.OsszErtek(OptimalisMegoldas());
        }
    }
}
