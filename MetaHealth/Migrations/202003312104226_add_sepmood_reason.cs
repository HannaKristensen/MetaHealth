namespace MetaHealth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_sepmood_reason : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SepMoods", "Reason", c => c.String(maxLength: 130));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SepMoods", "Reason");
        }
    }
}
