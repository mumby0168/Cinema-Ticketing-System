namespace Cinema_Ticketing_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedScreeningLinkToTicketModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "ScreeningId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "ScreeningId");
            AddForeignKey("dbo.Tickets", "ScreeningId", "dbo.Screenings", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "ScreeningId", "dbo.Screenings");
            DropIndex("dbo.Tickets", new[] { "ScreeningId" });
            DropColumn("dbo.Tickets", "ScreeningId");
        }
    }
}
