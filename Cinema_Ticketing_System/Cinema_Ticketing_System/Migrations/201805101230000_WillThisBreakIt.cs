namespace Cinema_Ticketing_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WillThisBreakIt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "SeatNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "SeatNumber");
        }
    }
}
