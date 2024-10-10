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
        }
    }
}
