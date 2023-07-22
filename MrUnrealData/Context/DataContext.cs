
using MrUnrealData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MrUnrealData.Context
{
    public class DataContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


        public DataContext()
        {
            Database.Connection.ConnectionString = "server=DESKTOP-43MTSU1;database=MrUnreal;trusted_connection=true";
        }

        public DbSet<Creation> Creations { get; set; }
        public DbSet<User> Users { get; set; }
    }
}