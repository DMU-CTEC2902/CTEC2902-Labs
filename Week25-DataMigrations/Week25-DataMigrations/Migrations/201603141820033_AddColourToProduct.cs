namespace Week25_DataMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColourToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Colour", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Colour");
        }
    }
}
