using System;
using System.Linq;
using DDC.Libs;

namespace Demo1
{
    class Program
    {
        static void Main()
        {
            DemoKomposition();
            DemoCurry();
            DemoPartialApp();
            DemoPartialApp2();
        }

        static void WarteAufReturnUndLöscheBildschirm()
        {
            Console.WriteLine("Return zum Fortfahren");
            Console.ReadLine();
            Console.Clear();
        }

        #region Komposition

        static int F(int x)
        {
            return x*x;
        }

        static void DemoKomposition()
        {
            Console.WriteLine("f(x) = x*x");
            Console.WriteLine("g(x) = x/2.0");

            Func<int, double> g = x => x/2.0;

            var gf = g.Komposition<int, int, double>(F);
            Console.WriteLine("g nach f von 5 = {0}", gf(5));

            var ff = HigherOrder.Komposition<int, int, int>(F, F);
            // F.Komposition(F) geht leider nicht
            Console.WriteLine("f nach f von 2 = {0}", ff(2));

            WarteAufReturnUndLöscheBildschirm();
        }

        #endregion

        #region Curry

        static int Add(int a, int b)
        {
            return a + b;
        }

        static void DemoCurry()
        {
            Console.WriteLine("teste plus=Curry(Add)...");

            var plus = HigherOrder.Curry<int, int, int>(Add);
            Console.WriteLine("plus(4)(5) = {0}", plus(4)(5));

            WarteAufReturnUndLöscheBildschirm();
        }

        #endregion

        #region partial Application

        private static void DemoPartialApp()
        {
            Console.WriteLine("teste partial Application von plus=Curry(Add)...");

            var plus = HigherOrder.Curry<int, int, int>(Add);
            var plus10 = plus(10);

            Console.WriteLine("plus10(2) = {0}", plus10(2));

            WarteAufReturnUndLöscheBildschirm();
        }

        private static void DemoPartialApp2()
        {
            Console.WriteLine("teste Applikation in Form von teilweiser Anwendung von Argumenten");

            var zahlen = Enumerable.Range(1, 5);
            zahlen.Apply(String.Join, "; ")
                  .Do(Console.WriteLine, "Zahlen: {0}");

            WarteAufReturnUndLöscheBildschirm();
        }

        #endregion    
    }
}
