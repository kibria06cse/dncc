namespace BillBoardDNCC.Migrations
{
    using BillBoardDNCC.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BillBoardDNCC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BillBoardDNCC.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(t => t.UserName == "admin"))
            {
                var user = new ApplicationUser { UserName = "admin", Email = "admin@dncc.com" };
                userManager.Create(user, "admin@123");

                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "admin" });
                context.SaveChanges();

                userManager.AddToRole(user.Id, "admin");
            }

            if (!context.Roles.Any(t => t.Name == "PowerUser"))
            {
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "PowerUser" });
                context.SaveChanges();
            }

            if (!context.Users.Any(t => t.UserName == "powerUser"))
            {
                var user = new ApplicationUser { UserName = "powerUser", Email = "powerUser@dncc.com" };
                userManager.Create(user, "powerUser@123");

                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "PowerUser" });
                context.SaveChanges();

                userManager.AddToRole(user.Id, "PowerUser");
            }

            if (!context.Users.Any(t => t.UserName == "mech_admin"))
            {
                var user = new ApplicationUser { UserName = "mech_admin", Email = "mech_admin@dncc.com" };
                userManager.Create(user, "mech_admin@123");

                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "mech_admin" });
                context.SaveChanges();

                userManager.AddToRole(user.Id, "mech_admin");
            }

            if (!context.Users.Any(t => t.UserName == "aminbazar_admin"))
            {
                var user = new ApplicationUser { UserName = "aminbazar_admin", Email = "aminbazar_admin@dncc.com" };
                userManager.Create(user, "aminbazar_admin@123");

                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "aminbazar_admin" });
                context.SaveChanges();

                userManager.AddToRole(user.Id, "aminbazar_admin");
            }


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
