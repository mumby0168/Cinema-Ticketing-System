namespace Cinema_Ticketing_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Hmmm : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tickets", "SeatNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "SeatNumber", c => c.String());
        }
    }
}
