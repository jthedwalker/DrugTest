namespace DrugTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id, unique: true)
                .Index(t => t.UserName, unique: true);
            
            CreateTable(
                "dbo.TestResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DriverId = c.Int(),
                        TestBatchId = c.Int(),
                        Status = c.Int(nullable: false),
                        Alcohol = c.Int(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drivers", t => t.DriverId)
                .ForeignKey("dbo.TestBatches", t => t.TestBatchId)
                .Index(t => t.DriverId)
                .Index(t => t.TestBatchId);
            
            CreateTable(
                "dbo.TestBatches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        Criteria = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ErrorDesc = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestResults", "TestBatchId", "dbo.TestBatches");
            DropForeignKey("dbo.TestResults", "DriverId", "dbo.Drivers");
            DropIndex("dbo.Errors", new[] { "Id" });
            DropIndex("dbo.TestResults", new[] { "TestBatchId" });
            DropIndex("dbo.TestResults", new[] { "DriverId" });
            DropIndex("dbo.Drivers", new[] { "UserName" });
            DropIndex("dbo.Drivers", new[] { "Id" });
            DropTable("dbo.Errors");
            DropTable("dbo.TestBatches");
            DropTable("dbo.TestResults");
            DropTable("dbo.Drivers");
        }
    }
}
