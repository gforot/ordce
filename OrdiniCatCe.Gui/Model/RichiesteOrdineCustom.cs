﻿using System;
using System.Globalization;
using System.Linq;
using System.Threading;


namespace OrdiniCatCe.Gui.Model
{
    public partial class RichiesteOrdine
    {
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
                return PezziInOrdine == null ? 0 : PezziInOrdine.Count;
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
    }
}
