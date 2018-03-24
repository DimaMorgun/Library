namespace Library.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Returnedmodelstate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookAuthor1", "Book_BookId", "dbo.Books");
            DropForeignKey("dbo.BookAuthor1", "Author_AuthorId", "dbo.Authors");
            DropIndex("dbo.BookAuthor1", new[] { "Book_BookId" });
            DropIndex("dbo.BookAuthor1", new[] { "Author_AuthorId" });
            DropTable("dbo.BookAuthor1");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookAuthor1",
                c => new
                    {
                        Book_BookId = c.Int(nullable: false),
                        Author_AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_BookId, t.Author_AuthorId });
            
            CreateIndex("dbo.BookAuthor1", "Author_AuthorId");
            CreateIndex("dbo.BookAuthor1", "Book_BookId");
            AddForeignKey("dbo.BookAuthor1", "Author_AuthorId", "dbo.Authors", "AuthorId", cascadeDelete: true);
            AddForeignKey("dbo.BookAuthor1", "Book_BookId", "dbo.Books", "BookId", cascadeDelete: true);
        }
    }
}
