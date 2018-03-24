namespace Library.DataAccessLayer.Migrations
{
    using System;
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
            //AddForeignKey("ChildTableName", "ParentId", "ParentTableName", "Id", cascadeDelete: true);
        }
    }
}
