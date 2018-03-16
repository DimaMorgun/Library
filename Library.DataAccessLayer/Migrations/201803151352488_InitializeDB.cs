namespace Library.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Birthday = c.Int(),
                        Deathday = c.Int(),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        YearOfPublishing = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.Magazines",
                c => new
                    {
                        MagazineId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Number = c.Int(),
                        YearOfPublishing = c.Int(),
                    })
                .PrimaryKey(t => t.MagazineId);
            
            CreateTable(
                "dbo.BookAuthor",
                c => new
                    {
                        AuthorId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AuthorId, t.BookId })
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookAuthor", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookAuthor", "AuthorId", "dbo.Authors");
            DropIndex("dbo.BookAuthor", new[] { "BookId" });
            DropIndex("dbo.BookAuthor", new[] { "AuthorId" });
            DropTable("dbo.BookAuthor");
            DropTable("dbo.Magazines");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
