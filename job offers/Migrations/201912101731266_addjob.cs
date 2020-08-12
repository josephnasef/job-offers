namespace job_offers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addjob : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.jobs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        JobContent = c.String(),
                        JobImage = c.String(),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JobsCategories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.jobs", "CategoryID", "dbo.JobsCategories");
            DropIndex("dbo.jobs", new[] { "CategoryID" });
            DropTable("dbo.jobs");
        }
    }
}
