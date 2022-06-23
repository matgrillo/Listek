namespace Listek.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Listek.Models;


    internal sealed class Configuration : DbMigrationsConfiguration<Listek.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Listek.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.


            //seeding - ko entity framework ustvari bazo seeda uporabnika
            AddUsers(context);
        }

        void AddUsers(Listek.Models.ApplicationDbContext context)
        {
            var user = new ApplicationUser { UserName = "uporabnik1@gmail.com" }; //nov uporabnik uporabnik1
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            um.Create(user, "password"); //ustvari uporabnika z geslom password
        }
    }
}
