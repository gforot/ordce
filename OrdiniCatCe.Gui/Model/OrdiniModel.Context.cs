﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OrdiniEntities : DbContext
    {
        public OrdiniEntities()
            : base("name=OrdiniEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Marche> Marche { get; set; }
        public DbSet<RichiesteOrdine> RichiesteOrdine { get; set; }
        public DbSet<Fornitori> Fornitori { get; set; }
        public DbSet<PezziInOrdine> PezziInOrdine { get; set; }
    }
}
