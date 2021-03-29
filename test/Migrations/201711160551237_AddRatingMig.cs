namespace test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRatingMig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.helloworlds", "Rating", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.helloworlds", "Rating");
        }
    }
}
