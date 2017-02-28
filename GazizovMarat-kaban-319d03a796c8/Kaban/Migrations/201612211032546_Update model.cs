namespace Kaban.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatemodel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Card", newName: "Cards");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Cards", newName: "Card");
        }
    }
}
