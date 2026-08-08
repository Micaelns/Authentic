using Authentic_Api.Models.Entities;
using AuthenticApi.App_Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace AuthenticApi.Migrations.Seeds
{
    public static class RoleSeed
    {
        public static void Execute(AuthenticContext context)
        {
            context.Roles.AddOrUpdate(
                x => x.Name,

                new Role
                {
                    Name = "Admin",
                    SoftwareId = 1
                },
                new Role
                {
                    Name = "Motorista",
                    SoftwareId = 1
                },
                new Role
                {
                    Name = "Default",
                    SoftwareId = 1
                }
            );
        }
    }
}