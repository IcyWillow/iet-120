namespace iet_120.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateWordTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Words",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Words", "UserId", "dbo.Users");
            DropIndex("dbo.Words", new[] { "UserId" });
            DropTable("dbo.Words");
        }
    }
}
