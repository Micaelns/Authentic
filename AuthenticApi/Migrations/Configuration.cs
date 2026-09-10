namespace AuthenticApi.Migrations
{
    using AuthenticApi.Migrations.Seeds;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AuthenticApi.App_Data.AuthenticContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AuthenticApi.App_Data.AuthenticContext context)
        {
            SoftwareSeed.Execute(context);
            PermissionSeed.Execute(context);
            RoleSeed.Execute(context);
            RolePermissionSeed.Execute(context);
            UserSeed.Execute(context);
        }
    }
}
