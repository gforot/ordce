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
    
    public partial class Fornitori
    {
        public Fornitori()
        {
            this.RichiesteOrdine = new HashSet<RichiesteOrdine>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<RichiesteOrdine> RichiesteOrdine { get; set; }
    }
}
