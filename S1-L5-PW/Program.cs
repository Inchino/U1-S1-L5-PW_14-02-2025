using System;

namespace CalcoloImposte
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Inserisci i dati del contribuente:");

            string nome = ControlloInput.RichiediStringa("Nome: ");
            string cognome = ControlloInput.RichiediStringa("Cognome: ");
            DateTime dataNascita = ControlloInput.RichiediData("Data di nascita (gg/mm/aaaa): ");
            char sesso = ControlloInput.RichiediSesso("Sesso (M/F): ");
            string comuneResidenza = ControlloInput.RichiediStringa("Comune di residenza: ");

            string codiceFiscale;
            do
            {
                Console.Write("Codice Fiscale: ");
                codiceFiscale = Console.ReadLine().ToUpper();
            } while (!ControlloCodiceFiscale.ValidazioneCodiceFiscale(codiceFiscale, nome, cognome, dataNascita, sesso));

            Console.WriteLine("Codice Fiscale valido!");

            decimal reddito;
            while (true)
            {
                Console.Write("Reddito annuale: ");
                if (decimal.TryParse(Console.ReadLine(), out reddito) && reddito >= 0)
                    break;
                Console.WriteLine("Inserisci un valore numerico positivo!");
            }

            Contribuente contribuente = new Contribuente(nome, cognome, dataNascita, codiceFiscale, sesso, comuneResidenza, reddito);
            decimal impostaDaVersare = contribuente.CalcolaImposta();

            Console.WriteLine("\n=======================================");
            Console.WriteLine("CALCOLO DELL'IMPOSTA DA VERSARE:\n");
            Console.WriteLine($"Contribuente: {contribuente.Nome} {contribuente.Cognome}");
            Console.WriteLine($"Nato il {contribuente.DataNascita:dd/MM/yyyy} ({contribuente.Sesso})");
            Console.WriteLine($"Residente in {contribuente.ComuneResidenza}");
            Console.WriteLine($"Codice Fiscale: {contribuente.CodiceFiscale}");
            Console.WriteLine($"Reddito dichiarato: {contribuente.RedditoAnnuale:F2} euro");
            Console.WriteLine($"IMPOSTA DA VERSARE: {impostaDaVersare:F2} euro");
            Console.WriteLine("=======================================\n");

            Console.WriteLine("\nPremi un tasto per uscire...");
            Console.ReadKey();
        }
    }
}

