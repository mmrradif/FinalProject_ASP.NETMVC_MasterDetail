namespace EmployeeManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 80),
                        BirthDate = c.DateTime(nullable: false),
                        Age = c.Int(nullable: false),
                        Picture = c.String(),
                        MaritalStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeeMasterTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.SkillId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.SkillId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeMasterTables", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.EmployeeMasterTables", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.EmployeeMasterTables", new[] { "SkillId" });
            DropIndex("dbo.EmployeeMasterTables", new[] { "EmployeeId" });
            DropTable("dbo.Skills");
            DropTable("dbo.EmployeeMasterTables");
            DropTable("dbo.Employees");
        }
    }
}
