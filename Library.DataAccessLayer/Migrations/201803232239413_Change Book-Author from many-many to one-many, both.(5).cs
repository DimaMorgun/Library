namespace Library.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBookAuthorfrommanymanytoonemanyboth5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "PublicationHouse_PublicationHouseId", "dbo.PublicationHouses");
            DropIndex("dbo.BookAuthors", new[] { "BookId" });
            DropIndex("dbo.BookAuthors", new[] { "AuthorId" });
            DropIndex("dbo.Books", new[] { "PublicationHouse_PublicationHouseId" });
            DropColumn("dbo.Books", "PublicationHouse_PublicationHouseId");
            DropTable("dbo.BookAuthors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        BookAuthorId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookAuthorId);
            
            AddColumn("dbo.Books", "PublicationHouse_PublicationHouseId", c => c.Int());
            CreateIndex("dbo.Books", "PublicationHouse_PublicationHouseId");
            CreateIndex("dbo.BookAuthors", "AuthorId");
            CreateIndex("dbo.BookAuthors", "BookId");
            AddForeignKey("dbo.Books", "PublicationHouse_PublicationHouseId", "dbo.PublicationHouses", "PublicationHouseId");
            AddForeignKey("dbo.BookAuthors", "BookId", "dbo.Books", "BookId", cascadeDelete: true);
            AddForeignKey("dbo.BookAuthors", "AuthorId", "dbo.Authors", "AuthorId", cascadeDelete: true);
        }
    }
}
