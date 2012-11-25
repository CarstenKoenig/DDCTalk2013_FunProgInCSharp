using System;
using System.Linq;
using System.Numerics;
using DDC.Libs;

namespace Demo2
{
    class Program
    {
        static void Main()
        {
            DemoMap();
            DemoGrosseListeMitFoldL();
            // DemoGrosseListeMitFoldR(); // <- leider SO
            DemoGrosseListeMitFSharpTailrecursiveFoldRVersion();
            // DemoGrosseListeMitCSharpTailrecursiveFoldRVersion(); // <- leider SO
        }

        static void WarteAufReturnUndLöscheBildschirm()
        {
            Console.WriteLine("Return zum Fortfahren");
            Console.ReadLine();
            Console.Clear();
        }

        static void DemoMap()
        {
            Console.Write("DEMO Map: quadriere der Liste ");

            var liste = Enumerable.Range(1, 5).ToListe();
            Console.WriteLine(liste);

            var quadrate = liste.Map(n => n * n);
            Console.WriteLine(quadrate);

            WarteAufReturnUndLöscheBildschirm();
        }

        static void DemoGrosseListeMitFoldL()
        {
            Console.WriteLine("FoldL: Summe aller Elemente für eine sehr große Liste");

            var gross = Enumerable.Range(1, 1000000).ToListe();
            var gross2 = gross.FoldL((s, a) => s + new BigInteger(a), BigInteger.Zero); // kein Thema
            Console.WriteLine(gross2);

            WarteAufReturnUndLöscheBildschirm();
        }

        static void DemoGrosseListeMitFoldR()
        {
            Console.WriteLine("FoldR: Summe aller Elemente für eine sehr große Liste");

            var gross = Enumerable.Range(1, 1000000).ToListe();
            var gross2 = gross.FoldR((a, s) => s + new BigInteger(a), BigInteger.Zero); // stack overflow
            Console.WriteLine(gross2);

            WarteAufReturnUndLöscheBildschirm();
        }

        static void DemoGrosseListeMitCSharpTailrecursiveFoldRVersion()
        {
            Console.WriteLine("CSharp-Version von FoldR (tail recursive): Summe aller Elemente für eine sehr große Liste");

            var gross = Enumerable.Range(1, 1000000).ToListe();
            var gross2 = FoldRtr(gross, (a, s) => s + new BigInteger(a), BigInteger.Zero, i => i);
            Console.WriteLine(gross2);

            WarteAufReturnUndLöscheBildschirm();
        }

        static void DemoGrosseListeMitFSharpTailrecursiveFoldRVersion()
        {
            Console.WriteLine("FSharp-Version von FoldR (tail recursive): Summe aller Elemente für eine sehr große Liste");

            var gross = Enumerable.Range(1, 1000000).ToListe();
            var gross2 = ListenMitFSharp.FoldR(gross, (a, s) => s + new BigInteger(a), BigInteger.Zero);
            Console.WriteLine(gross2);

            WarteAufReturnUndLöscheBildschirm();
        }

        #region Sollte eigentlich keinen Stack-Overflow auslösen, macht es aber leider doch

        static S FoldRtr<A, S>(
            Liste<A> liste, 
            Func<A, S, S> fold, 
            S akku,
            Func<S, S> continuation)
        {
            if (liste.IsEmpty()) 
                return continuation(akku);

            return FoldRtr(
                liste.Tail(),
                fold, akku,
                r => continuation(fold(liste.Head(), r)));
        }

        #endregion

    }
}
