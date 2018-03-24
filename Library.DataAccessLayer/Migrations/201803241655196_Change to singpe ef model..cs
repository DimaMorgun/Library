namespace Library.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changetosingpeefmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookAuthor1",
                c => new
                    {
                        Book_BookId = c.Int(nullable: false),
                        Author_AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_BookId, t.Author_AuthorId })
                .ForeignKey("dbo.Books", t => t.Book_BookId, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.Author_AuthorId, cascadeDelete: true)
                .Index(t => t.Book_BookId)
                .Index(t => t.Author_AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookAuthor1", "Author_AuthorId", "dbo.Authors");
            DropForeignKey("dbo.BookAuthor1", "Book_BookId", "dbo.Books");
            DropIndex("dbo.BookAuthor1", new[] { "Author_AuthorId" });
            DropIndex("dbo.BookAuthor1", new[] { "Book_BookId" });
            DropTable("dbo.BookAuthor1");
        }
    }
}
