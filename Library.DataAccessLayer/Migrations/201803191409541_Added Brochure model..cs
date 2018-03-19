namespace Library.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBrochuremodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brochures",
                c => new
                    {
                        BrochureId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TypeOfCover = c.String(),
                        NumberOfPages = c.Int(),
                    })
                .PrimaryKey(t => t.BrochureId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Brochures");
        }
    }
}
