namespace Cinema_Ticketing_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Screenings", "DateAndTime", c => c.DateTime(nullable: false, precision: 0));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Screenings", "DateAndTime", c => c.DateTime(nullable: false));
        }
    }
}
