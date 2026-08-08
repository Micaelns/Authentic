namespace AuthenticApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RolePermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permissions", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SoftwareId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Softwares", t => t.SoftwareId, cascadeDelete: true)
                .Index(t => t.SoftwareId);
            
            CreateTable(
                "dbo.Softwares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NickName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        PasswordHash = c.String(),
                        IsBlocked = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.RolePermissions", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Roles", "SoftwareId", "dbo.Softwares");
            DropForeignKey("dbo.RolePermissions", "PermissionId", "dbo.Permissions");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", new[] { "SoftwareId" });
            DropIndex("dbo.RolePermissions", new[] { "PermissionId" });
            DropIndex("dbo.RolePermissions", new[] { "RoleId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Softwares");
            DropTable("dbo.Roles");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.Permissions");
        }
    }
}
