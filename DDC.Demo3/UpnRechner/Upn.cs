using System;
using System.Collections.Generic;
using System.Linq;
using DDC.Libs;
using Stack = DDC.Libs.Liste<float>;

namespace UpnRechner
{
    static class Upn
    {
        public static float BerechneErgebnis(IEnumerable<string> eingaben)
        {
            var initialStack = Listen.Empty<float>();
            return eingaben
                .Aggregate(initialStack, EingabeVerarbeiten)
                .Head();
        }

        static Stack EingabeVerarbeiten(Stack stack, string eingabe)
        {
            switch (eingabe)
            {
                case "+":
                    return OperationDurchführen(stack, (a, b) => a + b);
                case "-":
                    return OperationDurchführen(stack, (a, b) => a - b);
                case "*":
                    return OperationDurchführen(stack, (a, b) => a * b);
                case "/":
                    return OperationDurchführen(stack, (a, b) => a / b);
                default:
                    return ZahlAufStack(stack, float.Parse(eingabe));
            }
        }

        static Stack OperationDurchführen(Stack stack, Func<float, float, float> op)
        {
            var t1 = ZahlVomStack(stack);
            var t2 = ZahlVomStack(t1.Item1);
            // ACHTUNG: Zahlen liegen verkehrt herum auf dem Stack!
            var ergebnis = op(t2.Item2, t1.Item2);
            return ergebnis.Cons(t2.Item1);
        }

        static Tuple<Stack, float> ZahlVomStack(Stack stack)
        {
            return Tuple.Create(stack.Tail(), stack.Head());
        }

        static Stack ZahlAufStack(Stack stack, float zahl)
        {
            return zahl.Cons(stack);
        }
    }
}
