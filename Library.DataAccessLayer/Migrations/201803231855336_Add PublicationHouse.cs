namespace Library.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPublicationHouse : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.BookAuthor");
            CreateTable(
                "dbo.PublicationHouses",
                c => new
                    {
                        PublicationHouseId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Adress = c.String(),
                    })
                .PrimaryKey(t => t.PublicationHouseId);
            
            CreateTable(
                "dbo.BookPublicationHouse",
                c => new
                    {
                        BookId = c.Int(nullable: false),
                        PublicationHouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookId, t.PublicationHouseId })
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.PublicationHouses", t => t.PublicationHouseId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.PublicationHouseId);
            
            AddPrimaryKey("dbo.BookAuthor", new[] { "BookId", "AuthorId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookPublicationHouse", "PublicationHouseId", "dbo.PublicationHouses");
            DropForeignKey("dbo.BookPublicationHouse", "BookId", "dbo.Books");
            DropIndex("dbo.BookPublicationHouse", new[] { "PublicationHouseId" });
            DropIndex("dbo.BookPublicationHouse", new[] { "BookId" });
            DropPrimaryKey("dbo.BookAuthor");
            DropTable("dbo.BookPublicationHouse");
            DropTable("dbo.PublicationHouses");
            AddPrimaryKey("dbo.BookAuthor", new[] { "AuthorId", "BookId" });
        }
    }
}
