namespace Week26_CleanCodeRefactoring.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeDateDispatchedNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "DateDispatched", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "DateDispatched", c => c.DateTime(nullable: false));
        }
    }
}
