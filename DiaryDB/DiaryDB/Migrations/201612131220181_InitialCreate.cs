namespace DiaryDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diary",
                c => new
                    {
                        Date = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Event = c.String(),
                        Duration = c.DateTime(nullable: false),
                        Place = c.String(),
                    })
                .PrimaryKey(t => t.Date);
            
            CreateTable(
                "dbo.DiaryEvents",
                c => new
                    {
                        Event = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Event);
            
            CreateTable(
                "dbo.DiaryPlaces",
                c => new
                    {
                        Event = c.String(nullable: false, maxLength: 128),
                        Place = c.String(),
                    })
                .PrimaryKey(t => t.Event);
            
            CreateTable(
                "dbo.DiaryEventsDiaries",
                c => new
                    {
                        DiaryEvents_Event = c.String(nullable: false, maxLength: 128),
                        Diary_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.DiaryEvents_Event, t.Diary_Date })
                .ForeignKey("dbo.DiaryEvents", t => t.DiaryEvents_Event, cascadeDelete: true)
                .ForeignKey("dbo.Diary", t => t.Diary_Date, cascadeDelete: true)
                .Index(t => t.DiaryEvents_Event)
                .Index(t => t.Diary_Date);
            
            CreateTable(
                "dbo.DiaryPlacesDiaries",
                c => new
                    {
                        DiaryPlaces_Event = c.String(nullable: false, maxLength: 128),
                        Diary_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.DiaryPlaces_Event, t.Diary_Date })
                .ForeignKey("dbo.DiaryPlaces", t => t.DiaryPlaces_Event, cascadeDelete: true)
                .ForeignKey("dbo.Diary", t => t.Diary_Date, cascadeDelete: true)
                .Index(t => t.DiaryPlaces_Event)
                .Index(t => t.Diary_Date);
            
            CreateTable(
                "dbo.DiaryPlacesDiaryEvents",
                c => new
                    {
                        DiaryPlaces_Event = c.String(nullable: false, maxLength: 128),
                        DiaryEvents_Event = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.DiaryPlaces_Event, t.DiaryEvents_Event })
                .ForeignKey("dbo.DiaryPlaces", t => t.DiaryPlaces_Event, cascadeDelete: true)
                .ForeignKey("dbo.DiaryEvents", t => t.DiaryEvents_Event, cascadeDelete: true)
                .Index(t => t.DiaryPlaces_Event)
                .Index(t => t.DiaryEvents_Event);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DiaryPlacesDiaryEvents", "DiaryEvents_Event", "dbo.DiaryEvents");
            DropForeignKey("dbo.DiaryPlacesDiaryEvents", "DiaryPlaces_Event", "dbo.DiaryPlaces");
            DropForeignKey("dbo.DiaryPlacesDiaries", "Diary_Date", "dbo.Diary");
            DropForeignKey("dbo.DiaryPlacesDiaries", "DiaryPlaces_Event", "dbo.DiaryPlaces");
            DropForeignKey("dbo.DiaryEventsDiaries", "Diary_Date", "dbo.Diary");
            DropForeignKey("dbo.DiaryEventsDiaries", "DiaryEvents_Event", "dbo.DiaryEvents");
            DropIndex("dbo.DiaryPlacesDiaryEvents", new[] { "DiaryEvents_Event" });
            DropIndex("dbo.DiaryPlacesDiaryEvents", new[] { "DiaryPlaces_Event" });
            DropIndex("dbo.DiaryPlacesDiaries", new[] { "Diary_Date" });
            DropIndex("dbo.DiaryPlacesDiaries", new[] { "DiaryPlaces_Event" });
            DropIndex("dbo.DiaryEventsDiaries", new[] { "Diary_Date" });
            DropIndex("dbo.DiaryEventsDiaries", new[] { "DiaryEvents_Event" });
            DropTable("dbo.DiaryPlacesDiaryEvents");
            DropTable("dbo.DiaryPlacesDiaries");
            DropTable("dbo.DiaryEventsDiaries");
            DropTable("dbo.DiaryPlaces");
            DropTable("dbo.DiaryEvents");
            DropTable("dbo.Diary");
        }
    }
}
