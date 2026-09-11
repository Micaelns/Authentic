namespace AuthenticApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TableRefreshToken : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefreshTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TokenHash = c.String(),
                        DeviceId = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ExpiresAt = c.DateTime(nullable: false),
                        RevokedAt = c.DateTime(),
                        ReplacedByTokenHash = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefreshTokens", "User_Id", "dbo.Users");
            DropIndex("dbo.RefreshTokens", new[] { "User_Id" });
            DropTable("dbo.RefreshTokens");
        }
    }
}
