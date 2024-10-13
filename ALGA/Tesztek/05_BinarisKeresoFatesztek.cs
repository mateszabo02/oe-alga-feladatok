
using NUnit.Framework;
using OE.ALGA.Adatszerkezetek;

namespace OE.ALGA.Tesztek
{
    [TestFixture]
    public class FaHalmazTesztek
    {
        [Test]
        public void Beszuras() //F4.
        {
            Halmaz<int> v = new FaHalmaz<int>();
            v.Beszur(1);
            v.Beszur(3);
            v.Beszur(2);
            Assert.That(v.Eleme(1), Is.True);
            Assert.That(v.Eleme(2), Is.True);
            Assert.That(v.Eleme(3), Is.True);
            Assert.That(v.Eleme(4), Is.False);
        }

        [Test]
        public void Torles() //F5.
        {
            Halmaz<int> v = new FaHalmaz<int>();
            v.Beszur(1);
            v.Beszur(3);
            v.Beszur(2);
            v.Torol(2);
            
            Assert.That(v.Eleme(1), Is.True);
            Assert.That(v.Eleme(2), Is.False);
            Assert.That(v.Eleme(3), Is.True);
            Assert.That(v.Eleme(4), Is.False);
        }

        [Test]
        public void DuplaBeszuras() //F5.
        {
            Halmaz<int> v = new FaHalmaz<int>();
            v.Beszur(1);
            v.Beszur(2);
            v.Beszur(3);
            v.Beszur(2);
            v.Torol(2);
            Assert.That(v.Eleme(1), Is.True);
            Assert.That(v.Eleme(2), Is.False);
            Assert.That(v.Eleme(3), Is.True);
            Assert.That(v.Eleme(4), Is.False);
        }

        [Test]
        public void PreorderBejaras() //F6.
        {
            Halmaz<int> v = new FaHalmaz<int>();
            v.Beszur(5);
            v.Beszur(3);
            v.Beszur(1);
            v.Beszur(8);
            v.Beszur(4);
            v.Beszur(9);
            v.Beszur(7);
            string osszefuzo = "";
            v.Bejar(x => osszefuzo += x);
            Assert.That("5314879", Is.EqualTo(osszefuzo));
        }

    }
}
