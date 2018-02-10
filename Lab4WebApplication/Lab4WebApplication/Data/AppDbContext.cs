using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using Lab4WebApplication.Data.Entities;

namespace Lab4WebApplication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new AppDbInitializer());
        }

        public virtual DbSet<User> Users {get; set;}
        public virtual DbSet<Pet> Pets {get; set;}

        public class AppDbInitializer: DropCreateDatabaseIfModelChanges<AppDbContext>
        {

        }
    }
}