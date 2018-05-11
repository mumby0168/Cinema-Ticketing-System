namespace Cinema_Ticketing_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "RowNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "ColumnNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "ColumnNumber");
            DropColumn("dbo.Tickets", "RowNumber");
        }
    }
}
