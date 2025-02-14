using System;
using System.Text.RegularExpressions;

namespace CalcoloImposte
{
    public static class ControlloInput
    {
        public static string RichiediStringa(string messaggio)
        {
            string input;
            while (true)
            {
                Console.Write(messaggio);
                input = Console.ReadLine().Trim();

                if (Regex.IsMatch(input, @"^[A-Z][a-z]+$"))
                    return input;

                Console.WriteLine("Formato non Valido! Controlla che la prima lettera sia maiuscola e di star utilizzando i caratteri corretti.");
            }
        }

        public static DateTime RichiediData(string messaggio)
        {
            DateTime data;
            while (true)
            {
                Console.Write(messaggio);
                if (DateTime.TryParse(Console.ReadLine(), out data))
                    return data;
                Console.WriteLine("Formato data non valido! Controlla di star utilizzando '/' come divisorio e che la data sia corretta.");
            }
        }

        public static char RichiediSesso(string messaggio)
        {
            char sesso;
            while (true)
            {
                Console.Write(messaggio);
                if (char.TryParse(Console.ReadLine().ToUpper(), out sesso) && (sesso == 'M' || sesso == 'F'))
                    return sesso;
                Console.WriteLine("Inserisci M o F.");
            }
        }
    }
}