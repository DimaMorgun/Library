namespace Library.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4thtaskwithpublicationhouses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookPublicationHouses",
                c => new
                    {
                        BookPublicationHouseId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        PublicationHouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookPublicationHouseId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.PublicationHouses", t => t.PublicationHouseId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.PublicationHouseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookPublicationHouses", "PublicationHouseId", "dbo.PublicationHouses");
            DropForeignKey("dbo.BookPublicationHouses", "BookId", "dbo.Books");
            DropIndex("dbo.BookPublicationHouses", new[] { "PublicationHouseId" });
            DropIndex("dbo.BookPublicationHouses", new[] { "BookId" });
            DropTable("dbo.BookPublicationHouses");
        }
    }
}
