using Authentic_Api.Models.Entities;
using AuthenticApi.App_Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace AuthenticApi.Migrations.Seeds
{
    public static class PermissionSeed
    {
        public static void Execute(AuthenticContext context)
        {
            context.Permissions.AddOrUpdate(
                x => x.Code,

                new Permission
                {
                    Code = "create.Destination",
                    Description = "Criar Destino"
                },
                new Permission
                {
                    Code = "create.VehicleType",
                    Description = "Criar Tipo de Veículo"
                },
                new Permission
                {
                    Code = "update.VehicleType",
                    Description = "Atualizar Tipo de Veículo"
                },
                new Permission
                {
                    Code = "deletar.VehicleType",
                    Description = "deletar Tipo de Veículo"
                },
                new Permission
                {
                    Code = "create.vehicle",
                    Description = "Criar Veículo"
                },
                new Permission
                {
                    Code = "start.vehicle.Travel",
                    Description = "Iniciar viagem do Veículo"
                },
                new Permission
                {
                    Code = "ends.vehicle.Travel",
                    Description = "Finalizar viagem do Veículo"
                }
            );
        }
    }
}