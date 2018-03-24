namespace Library.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBookAuthorfrommanymanytoonemanyboth : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BookAuthor", newName: "BookAuthors");
            DropPrimaryKey("dbo.BookAuthors");
            AddColumn("dbo.BookAuthors", "BookAuthorId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.BookAuthors", "BookAuthorId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.BookAuthors");
            DropColumn("dbo.BookAuthors", "BookAuthorId");
            AddPrimaryKey("dbo.BookAuthors", new[] { "BookId", "AuthorId" });
            RenameTable(name: "dbo.BookAuthors", newName: "BookAuthor");
        }
    }
}
