namespace Library.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangetooldrelationsinBooktoAuthors : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookAuthors", "Author_AuthorId", "dbo.Authors");
            DropForeignKey("dbo.BookAuthors", "Book_BookId", "dbo.Books");
            DropIndex("dbo.BookAuthors", new[] { "Author_AuthorId" });
            DropIndex("dbo.BookAuthors", new[] { "Book_BookId" });
            DropPrimaryKey("dbo.BookAuthors");
            AlterColumn("dbo.BookAuthors", "Author_AuthorId", c => c.Int(nullable: false));
            AlterColumn("dbo.BookAuthors", "Book_BookId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.BookAuthors", new[] { "Book_BookId", "Author_AuthorId" });
            CreateIndex("dbo.BookAuthors", "Book_BookId");
            CreateIndex("dbo.BookAuthors", "Author_AuthorId");
            AddForeignKey("dbo.BookAuthors", "Author_AuthorId", "dbo.Authors", "AuthorId", cascadeDelete: true);
            AddForeignKey("dbo.BookAuthors", "Book_BookId", "dbo.Books", "BookId", cascadeDelete: true);
            DropColumn("dbo.BookAuthors", "BookAuthorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookAuthors", "BookAuthorId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.BookAuthors", "Book_BookId", "dbo.Books");
            DropForeignKey("dbo.BookAuthors", "Author_AuthorId", "dbo.Authors");
            DropIndex("dbo.BookAuthors", new[] { "Author_AuthorId" });
            DropIndex("dbo.BookAuthors", new[] { "Book_BookId" });
            DropPrimaryKey("dbo.BookAuthors");
            AlterColumn("dbo.BookAuthors", "Book_BookId", c => c.Int());
            AlterColumn("dbo.BookAuthors", "Author_AuthorId", c => c.Int());
            AddPrimaryKey("dbo.BookAuthors", "BookAuthorId");
            CreateIndex("dbo.BookAuthors", "Book_BookId");
            CreateIndex("dbo.BookAuthors", "Author_AuthorId");
            AddForeignKey("dbo.BookAuthors", "Book_BookId", "dbo.Books", "BookId");
            AddForeignKey("dbo.BookAuthors", "Author_AuthorId", "dbo.Authors", "AuthorId");
        }
    }
}
