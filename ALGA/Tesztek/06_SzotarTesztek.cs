/*
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OE.ALGA.Adatszerkezetek;
 
 namespace OE.ALGA.Tesztek
 {
    [TestFixture]
     public class SzotarTesztek
     {
         public static int TesztHasitoFuggveny(string kulcs) //F2.(f)
         {
             if (string.IsNullOrEmpty(kulcs))
                 return 0;
             int sum = 0;
             foreach (char c in kulcs.ToCharArray())
                 sum += ((byte)c);
             return (sum * sum); // a modulo osztást a szótárnak kell végeznie, mert ő tudja csak a belső tömb méretet
         }
 
         [Test]
         public void AlapMukodes() //F2.(f)
         {
             Szotar<string, int> sz = new HasitoSzotarTulcsordulasiTerulettel<string, int>(10, TesztHasitoFuggveny);
             sz.Beir("Bela", 5);
             sz.Beir("Lajos", 2);
             Assert.Equals(5, sz.Kiolvas("Bela"));
             Assert.That(2, sz.Kiolvas("Lajos"));
         }

        [Test]
        public void AlapertelmezettHasitoFuggvennyel() //F2.(f)
         {
             Szotar<string, int> sz = new HasitoSzotarTulcsordulasiTerulettel<string, int>(10);
             sz.Beir("Bela", 5);
             sz.Beir("Lajos", 2);
             Assert.That(5, Is.EqualTo(sz.Kiolvas("Bela")));
             Assert.That(2, Is.EqualTo(sz.Kiolvas("Lajos")));
         }
        
         [Test]
         public void Kulcsutkozes() //F2.(f)
         {
             Szotar<string, int> sz = new HasitoSzotarTulcsordulasiTerulettel<string, int>(10, TesztHasitoFuggveny);
             sz.Beir("Bela", 5);
             sz.Beir("Bale", 15);
             sz.Beir("Lajos", 2);
             sz.Beir("Lasoj", 12);
            Assert.That(5, Is.EqualTo(sz.Kiolvas("Bela")));
            Assert.That(2, Is.EqualTo(sz.Kiolvas("Lajos")));
            Assert.That(15, Is.EqualTo(sz.Kiolvas("Bale")));
            Assert.That(12, Is.EqualTo(sz.Kiolvas("Lasoj")));
         }
        
         [Test]
         [ExpectedException(typeof(HibasKulcsKivetel))]
         public void NincsElem() //F2.(f)
         {
             Szotar<string, int> sz = new HasitoSzotarTulcsordulasiTerulettel<string, int>(5, TesztHasitoFuggveny);
             sz.Beir("Bela", 5);
             sz.Beir("Lajos", 2);
             sz.Kiolvas("Ferenc");
            //Assert.Throws<Exception>(() => { throw new HibasKulcsKivetel(); });
        }
 
         [Test]
         public void TorlesMarad() //F2.(g)
         {
             Szotar<string, int> sz = new HasitoSzotarTulcsordulasiTerulettel<string, int>(5, TesztHasitoFuggveny);
             sz.Beir("Bela", 5);
             sz.Beir("Lajos", 2);
             sz.Torol("Bela");
             Assert.That(2, Is.EqualTo(sz.Kiolvas("Lajos")));
         }

        [Test]
        [ExpectedException(typeof(HibasKulcsKivetel))]
         public void TorlesEltunt() //F2.(g)
         {
             Szotar<string, int> sz = new HasitoSzotarTulcsordulasiTerulettel<string, int>(5, TesztHasitoFuggveny);
             sz.Beir("Bela", 5);
             sz.Beir("Lajos", 2);
             sz.Torol("Bela");
             sz.Kiolvas("Bela");
            //Assert.Throws<Exception>(() => { throw new HibasKulcsKivetel(); });
         }
     }
 
 }
*/