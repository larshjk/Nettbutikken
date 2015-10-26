namespace Nettbutikken.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Collections.Generic;

    public partial class Dbkontekst : DbContext
    {
        public Dbkontekst()
            : base("name=Nettbutikk")
        {
            Database.CreateIfNotExists();
        }
        public DbSet<Vare> Varer { get; set; }
        public DbSet<Kategori> Kategorier { get; set; }
        public DbSet<Vogn> Vogner { get; set; }
        public DbSet<Ordre> Ordrer { get; set; }
        public DbSet<OrdreDetaljer> OrdreDetaljers { get; set; }
        public DbSet<Konto.dbBruker> Bruker { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
