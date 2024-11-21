using OE.ALGA.Adatszerkezetek;

namespace ALGA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FaHalmaz<int> fa = new FaHalmaz<int>();
            fa.Beszur(10);
            fa.Beszur(5);
            fa.Beszur(15);
            fa.Beszur(3);
            fa.Beszur(9);
            /*int[] A = [5, 8, 7, 0, 9, 6, 4, 1, 3, 2];
            KupacRendezes<int> k = new KupacRendezes<int>(A);
            k.Rendezes();
            for (int i = 0; i < A.Length; i++) 
            {
                Console.WriteLine(A[i]);
            }*/
        }
    }
}
