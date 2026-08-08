using Authentic_Api.Models.Entities;
using AuthenticApi.App_Data;
using System;
using System.Data.Entity.Migrations;

namespace AuthenticApi.Migrations.Seeds
{
    public static class SoftwareSeed
    {
        public static void Execute(AuthenticContext context)
        {
            context.Softwares.AddOrUpdate(
                x => x.Name,

                new Software
                {
                    Name = "Zelo-Frota-Api",
                    Description = "Sistema de Frota de Caminhões - Logística"
                }
            );
        }
    }
}