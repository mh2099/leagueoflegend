﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace lolLib.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class lolEntities : DbContext
    {
        public lolEntities()
            : base(DBConnection.ShowConnectionString())
        {
    		Configuration.ProxyCreationEnabled = false;
    
    		((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 200;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ban> ban { get; set; }
        public DbSet<frame> frame { get; set; }
        public DbSet<game> game { get; set; }
        public DbSet<gameframe> gameframe { get; set; }
        public DbSet<mastery> mastery { get; set; }
        public DbSet<participant> participant { get; set; }
        public DbSet<participantframe> participantframe { get; set; }
        public DbSet<participantframes> participantframes { get; set; }
        public DbSet<participantidentities> participantidentities { get; set; }
        public DbSet<player> player { get; set; }
        public DbSet<rune> rune { get; set; }
        public DbSet<stat> stat { get; set; }
        public DbSet<team> team { get; set; }
        public DbSet<timeline> timeline { get; set; }
    }
}
