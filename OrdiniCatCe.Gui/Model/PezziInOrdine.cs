//------------------------------------------------------------------------------
// <auto-generated>
//    Codice generato da un modello.
//
//    Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//    Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrdiniCatCe.Gui.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PezziInOrdine
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Arrivato { get; set; }
        public int IdRichiestaOrdine { get; set; }
        public string Codice { get; set; }
        public Nullable<decimal> PrezzoAcquisto { get; set; }
        public Nullable<decimal> PrezzoVendita { get; set; }
        public bool Ordinato { get; set; }
        public bool Ritirato { get; set; }
        public Nullable<int> IdMarca { get; set; }
        public Nullable<int> IdFornitore { get; set; }
        public bool Avvisato { get; set; }
        public Nullable<System.DateTime> DataAvvisato { get; set; }
        public Nullable<System.DateTime> DataOrdinato { get; set; }
        public Nullable<System.DateTime> DataArrivato { get; set; }
        public Nullable<System.DateTime> DataRitirato { get; set; }
        public bool FuoriStock { get; set; }
        public bool Sprovvisto { get; set; }
        public Nullable<int> Quantita { get; set; }
    
        public virtual Fornitori Fornitori { get; set; }
        public virtual Marche Marche { get; set; }
        public virtual RichiesteOrdine RichiesteOrdine { get; set; }
    }
}
