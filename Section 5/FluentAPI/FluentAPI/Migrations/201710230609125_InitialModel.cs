namespace FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_Course",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 2000),
                        Level = c.Int(nullable: false),
                        FullPrice = c.Single(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AuthorId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Covers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Course", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseTags",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseId, t.TagId })
                .ForeignKey("dbo.tbl_Course", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.CourseTags", "CourseId", "dbo.tbl_Course");
            DropForeignKey("dbo.Covers", "Id", "dbo.tbl_Course");
            DropForeignKey("dbo.tbl_Course", "AuthorId", "dbo.Authors");
            DropIndex("dbo.CourseTags", new[] { "TagId" });
            DropIndex("dbo.CourseTags", new[] { "CourseId" });
            DropIndex("dbo.Covers", new[] { "Id" });
            DropIndex("dbo.tbl_Course", new[] { "AuthorId" });
            DropTable("dbo.CourseTags");
            DropTable("dbo.Tags");
            DropTable("dbo.Covers");
            DropTable("dbo.tbl_Course");
            DropTable("dbo.Authors");
        }
    }
}
