namespace Library.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBookAuthorfrommanymanytoonemanyboth3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookPublicationHouse", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookPublicationHouse", "PublicationHouseId", "dbo.PublicationHouses");
            DropIndex("dbo.BookPublicationHouse", new[] { "BookId" });
            DropIndex("dbo.BookPublicationHouse", new[] { "PublicationHouseId" });
            AddColumn("dbo.Books", "PublicationHouse_PublicationHouseId", c => c.Int());
            CreateIndex("dbo.Books", "PublicationHouse_PublicationHouseId");
            AddForeignKey("dbo.Books", "PublicationHouse_PublicationHouseId", "dbo.PublicationHouses", "PublicationHouseId");
            DropTable("dbo.BookPublicationHouse");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookPublicationHouse",
                c => new
                    {
                        BookId = c.Int(nullable: false),
                        PublicationHouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookId, t.PublicationHouseId });
            
            DropForeignKey("dbo.Books", "PublicationHouse_PublicationHouseId", "dbo.PublicationHouses");
            DropIndex("dbo.Books", new[] { "PublicationHouse_PublicationHouseId" });
            DropColumn("dbo.Books", "PublicationHouse_PublicationHouseId");
            CreateIndex("dbo.BookPublicationHouse", "PublicationHouseId");
            CreateIndex("dbo.BookPublicationHouse", "BookId");
            AddForeignKey("dbo.BookPublicationHouse", "PublicationHouseId", "dbo.PublicationHouses", "PublicationHouseId", cascadeDelete: true);
            AddForeignKey("dbo.BookPublicationHouse", "BookId", "dbo.Books", "BookId", cascadeDelete: true);
        }
    }
}
