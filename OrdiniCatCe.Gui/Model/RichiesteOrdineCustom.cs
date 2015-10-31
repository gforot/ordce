using System;
using System.Globalization;
using System.Linq;
using System.Threading;


namespace OrdiniCatCe.Gui.Model
{
    public partial class RichiesteOrdine
    {
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(Nome)) return Cognome;
                if (string.IsNullOrEmpty(Cognome)) return Nome;
                return string.Format("{0} {1}",Cognome, Nome);
            }
        }

        /// <summary>
        /// Torna true se esiste almeno un pezzo mancante.
        /// </summary>
        public bool ContainsPezzoSprovvisto
        {
            get
            {
                return PezziInOrdine != null && PezziInOrdine.Any(p => p.Sprovvisto);
            }
        }

        public int NumberOfPezzi
        {
            get
            {
                if (PezziInOrdine == null) return 0;
                int sum = 0;
                foreach (PezziInOrdine p in PezziInOrdine)
                {
                    if (p.Quantita.HasValue)
                    { sum += p.Quantita.Value; ; }
                }

                return sum;
            }
        }

        public bool ContainsPezzoArrivatoButNotAvvisato
        {
            get
            {
                return PezziInOrdine != null && PezziInOrdine.Any(p => p.IsArrivatoButNotAvvisato);
            }
        }

        public bool ContainsPezzoOrdinatoButNotArrivato
        {
            get
            {
                return PezziInOrdine != null && PezziInOrdine.Any(p => p.IsOrdinatoButNotArrivato);
            }
        }

        public bool ContainsPezzoNotOrdinato
        {
            get
            {
                return PezziInOrdine != null && PezziInOrdine.Any(p => !p.Ordinato);
            }
        }

        public bool ContainsPezzoOfFornitore(string fornitore)
        {
            return PezziInOrdine != null && PezziInOrdine.Any(p => ((p.Fornitori != null) && (Thread.CurrentThread.CurrentCulture.CompareInfo.IndexOf(p.Fornitori.Name, fornitore, CompareOptions.IgnoreCase)>=0)));
        }

        public bool ContainsPezzoOfFornitoreById(int idFornitore)
        {
            return PezziInOrdine != null && PezziInOrdine.Any(p => p.IdFornitore == idFornitore);
        }

        public int GetNumberOf(Func<PezziInOrdine, bool> f)
        {
            int? rval = this.PezziInOrdine.Where(f).Sum(s => s.Quantita);
            return rval.HasValue ? rval.Value : 0;
        }
    }
}
