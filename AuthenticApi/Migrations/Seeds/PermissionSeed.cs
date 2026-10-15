using Authentic_Api.Models.Entities;
using AuthenticApi.App_Data;
using System.Data.Entity.Migrations;

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
                    Code = "delete.VehicleType",
                    Description = "deletar Tipo de Veículo"
                },
                new Permission
                {
                    Code = "create.vehicle",
                    Description = "Criar Veículo"
                },
                new Permission
                {
                    Code = "start.vehicleTravel",
                    Description = "Iniciar viagem do Veículo"
                },
                new Permission
                {
                    Code = "ends.vehicleTravel",
                    Description = "Finalizar viagem do Veículo"
                },
                new Permission
                {
                    Code = "list.destination",
                    Description = "Listar Destino"
                },
                new Permission
                {
                    Code = "list.vehicle",
                    Description = "Listar Veículo"
                },
                new Permission
                {
                    Code = "list.vehicleType",
                    Description = "Listar Tipo de Veículo"
                },
                new Permission
                {
                    Code = "find.vehicleType",
                    Description = "Buscar Tipo de Veículo"
                },
                new Permission
                {
                    Code = "find.vehicleTravel",
                    Description = "Buscar Viagem do Veículo"
                },
                new Permission
                {
                    Code = "list.vehicleTravel",
                    Description = "Listar Viagens do Veículo"
                },
                new Permission
                {
                    Code = "rankingEconomy.vehicleTravel",
                    Description = "Listar Ranking Econômico do Veículo"
                },
                new Permission
                {
                    Code = "rankingMilage.vehicleTravel",
                    Description = "Listar Ranking quilometragem do Veículo"
                },
                new Permission
                {
                    Code = "reports.vehicleTravel",
                    Description = "Relatório do Viagem do Veículo"
                },
                new Permission
                {
                    Code = "create.driver",
                    Description = "Criar Motorista"
                },
                new Permission
                {
                    Code = "list.driver",
                    Description = "Listar Motorista"
                },
                new Permission
                {
                    Code = "find.driver",
                    Description = "Buscar Motorista"
                },
                new Permission
                {
                    Code = "ranking.travelDriver",
                    Description = "Listar Ranking de Viagens do Motorista"
                }
            );
            context.SaveChanges();
        }
    }
}