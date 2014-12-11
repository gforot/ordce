//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrdiniCatCe.Gui.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class RichiesteOrdine
    {
        public RichiesteOrdine()
        {
            this.PezziInOrdine = new HashSet<PezziInOrdine>();
        }
    
        public string Indirizzo { get; set; }
        public string Telefono { get; set; }
        public string EMail { get; set; }
        public string Cellulare { get; set; }
        public bool Avvisato { get; set; }
        public Nullable<System.DateTime> DataAvvisato { get; set; }
        public Nullable<System.DateTime> DataOrdinato { get; set; }
        public Nullable<System.DateTime> DataArrivato { get; set; }
        public Nullable<ModalitaDiAvviso> ModalitàAvviso { get; set; }
        public Nullable<System.DateTime> DataRitirato { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Localita { get; set; }
        public string NumeroCivico { get; set; }
        public bool Ritirato { get; set; }
        public Nullable<System.DateTime> DataRichiesta { get; set; }
        public bool Ordinato { get; set; }
        public bool Arrivato { get; set; }
        public bool RicevutaCaparra { get; set; }
        public Nullable<decimal> Caparra { get; set; }
        public Nullable<System.DateTime> DataCaparra { get; set; }
    
        public virtual ICollection<PezziInOrdine> PezziInOrdine { get; set; }
    }
}
