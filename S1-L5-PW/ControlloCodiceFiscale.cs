using System;
using System.Text.RegularExpressions;

namespace CalcoloImposte
{
    public static class ControlloCodiceFiscale
    {
        public static bool ValidazioneCodiceFiscale(string codiceFiscale, string nome, string cognome, DateTime dataNascita, char sesso)
        {
            if (codiceFiscale.Length != 16 || !Regex.IsMatch(codiceFiscale, "^[A-Z0-9]+$"))
                return false;

            string cognomeEstratto = codiceFiscale.Substring(0, 3);
            string cognomeGenerato = GeneraCodiceCognome(cognome);
            if (cognomeEstratto != cognomeGenerato)
                return false;

            string nomeEstratto = codiceFiscale.Substring(3, 3);
            string nomeGenerato = GeneraCodiceNome(nome);
            if (nomeEstratto != nomeGenerato)
                return false;

            string annoEstratto = codiceFiscale.Substring(6, 2);
            string annoGenerato = dataNascita.Year.ToString().Substring(2, 2);
            if (annoEstratto != annoGenerato)
                return false;

            string meseEstratto = codiceFiscale.Substring(8, 1);
            string meseGenerato = GeneraCodiceMese(dataNascita.Month);
            if (meseEstratto != meseGenerato)
                return false;

            int giornoEstratto = int.Parse(codiceFiscale.Substring(9, 2));
            int giornoAtteso = sesso == 'M' ? dataNascita.Day : dataNascita.Day + 40;
            if (giornoEstratto != giornoAtteso)
                return false;

            return true;
        }

        private static string GeneraCodiceCognome(string cognome)
        {
            return GeneraCodiceLettere(cognome);
        }

        private static string GeneraCodiceNome(string nome)
        {
            return GeneraCodiceLettere(nome, true);
        }

        private static string GeneraCodiceLettere(string testo, bool isNome = false)
        {
            string consonanti = "";
            string vocali = "";

            foreach (char c in testo.ToUpper())
            {
                if ("BCDFGHJKLMNPQRSTVWXYZ".Contains(c))
                    consonanti += c;
                else if ("AEIOU".Contains(c))
                    vocali += c;
            }

            if (isNome && consonanti.Length >= 4)
                return $"{consonanti[0]}{consonanti[2]}{consonanti[3]}";

            string risultato = consonanti + vocali + "XXX";
            return risultato.Substring(0, 3);
        }

        private static string GeneraCodiceMese(int mese)
        {
            string[] codiciMese = { "A", "B", "C", "D", "E", "H", "L", "M", "P", "R", "S", "T" };
            return codiciMese[mese - 1];
        }
    }
}