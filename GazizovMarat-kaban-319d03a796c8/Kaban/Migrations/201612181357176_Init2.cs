namespace Kaban.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Cards", newName: "Card");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Card", newName: "Cards");
        }
    }
}
