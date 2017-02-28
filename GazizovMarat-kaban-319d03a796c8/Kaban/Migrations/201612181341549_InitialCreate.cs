namespace Kaban.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        WorkTime = c.Time(nullable: false, precision: 7),
                        Category = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cards");
        }
    }
}
