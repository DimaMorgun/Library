namespace Library.DataAccessLayer.Migrations
{
    using Library.DataAccessLayer.UnitOfWork;
    using Library.EntityLayer.Identity;
    using Library.ViewModelLayer.Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Library.DataAccessLayer.Context.LibraryDataAccessContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Library.DataAccessLayer.Context.LibraryDataAccessContext context)
        {
            //if (!context.Roles.Any(r => r.Name == "admin"))
            //{
            //    var store = new RoleStore<IdentityRole>(context);
            //    var manager = new RoleManager<IdentityRole>(store);
            //    var role = new IdentityRole { Name = "admin" };

            //    manager.Create(role);
            //}

            //if (!context.Users.Any(u => u.UserName == "Dmytro"))
            //{
            //    var store = new UserStore<ApplicationUser>(context);
            //    var manager = new UserManager<ApplicationUser>(store);
            //    var user = new ApplicationUser { UserName = "Dmytro", Email = "DimaMorgun97@gmail.com" };

            //    manager.Create(user, "Ghjcnj gfhjkm1");
            //    manager.AddToRole(user.Id, "admin");
            //}
        }
    }
}
