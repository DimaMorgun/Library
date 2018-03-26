namespace Library.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentinBookAuthormodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookAuthors", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.BookAuthors", "BookId", "dbo.Books");
            DropIndex("dbo.BookAuthors", new[] { "BookId" });
            DropIndex("dbo.BookAuthors", new[] { "AuthorId" });
            RenameColumn(table: "dbo.BookAuthors", name: "AuthorId", newName: "Author_AuthorId");
            RenameColumn(table: "dbo.BookAuthors", name: "BookId", newName: "Book_BookId");
            AlterColumn("dbo.BookAuthors", "Book_BookId", c => c.Int());
            AlterColumn("dbo.BookAuthors", "Author_AuthorId", c => c.Int());
            CreateIndex("dbo.BookAuthors", "Author_AuthorId");
            CreateIndex("dbo.BookAuthors", "Book_BookId");
            AddForeignKey("dbo.BookAuthors", "Author_AuthorId", "dbo.Authors", "AuthorId");
            AddForeignKey("dbo.BookAuthors", "Book_BookId", "dbo.Books", "BookId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookAuthors", "Book_BookId", "dbo.Books");
            DropForeignKey("dbo.BookAuthors", "Author_AuthorId", "dbo.Authors");
            DropIndex("dbo.BookAuthors", new[] { "Book_BookId" });
            DropIndex("dbo.BookAuthors", new[] { "Author_AuthorId" });
            AlterColumn("dbo.BookAuthors", "Author_AuthorId", c => c.Int(nullable: false));
            AlterColumn("dbo.BookAuthors", "Book_BookId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.BookAuthors", name: "Book_BookId", newName: "BookId");
            RenameColumn(table: "dbo.BookAuthors", name: "Author_AuthorId", newName: "AuthorId");
            CreateIndex("dbo.BookAuthors", "AuthorId");
            CreateIndex("dbo.BookAuthors", "BookId");
            AddForeignKey("dbo.BookAuthors", "BookId", "dbo.Books", "BookId", cascadeDelete: true);
            AddForeignKey("dbo.BookAuthors", "AuthorId", "dbo.Authors", "AuthorId", cascadeDelete: true);
        }
    }
}
