namespace ATH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFirstAndLastNameColumnsInUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FName", c => c.String(nullable: false, maxLength: 45));
            AddColumn("dbo.AspNetUsers", "LName", c => c.String(nullable: false, maxLength: 45));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LName");
            DropColumn("dbo.AspNetUsers", "FName");
        }
    }
}
