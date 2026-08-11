using Authentic_Api.Models.Entities;
using AuthenticApi.App_Data;
using System.Data.Entity.Migrations;

namespace AuthenticApi.Migrations.Seeds
{
    public static class UserSeed
    {
        public static void Execute(AuthenticContext context)
        {
            context.Users.AddOrUpdate(
                x => x.Email,

                new User
                {
                    Name = "Admin",
                    NickName = "Adm",
                    Email = "adm@authentic.com",
                    PhoneNumber = ""
                }
            );

        }
    }
}