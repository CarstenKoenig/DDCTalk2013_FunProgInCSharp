using System;
using DDC.Libs;

namespace UpnRechner
{
    class Program
    {
        static void Main()
        {
            Action<string> verarbeiten = 
                eingabe =>
                eingabe.Split(new[] {' ', ',', ';'})
                        .Apply(Upn.BerechneErgebnis)
                        .Box().Do(Console.WriteLine, "Ergebnis: {0}");

            verarbeiten.Do(Execute);
        }

        static void Execute(Action<string> aktion)
        {
            while(true)
            {
                Console.Write("UPN Term eingaben (leer um zu beenden): ");
                var eingabe = Console.ReadLine();
                if (string.IsNullOrEmpty(eingabe)) return;
                eingabe.Do(aktion);
            }
        }

    }
}
