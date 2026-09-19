using Authentic_Api.Models.Entities;
using AuthenticApi.App_Data;
using System.Linq;

namespace AuthenticApi.Migrations.Seeds
{
    public static class RolePermissionSeed
    {
        public static void Execute(AuthenticContext context)
        {
            ExecuteAdminRole(context);
            ExecuteDriveRole(context);
            ExecuteDefaultRole(context);
        }

        private static void ExecuteDefaultRole(AuthenticContext context)
        {
            var defaultRole = context.Roles
                    .First(x => x.Name == "Default");

            var createDestination = context.Permissions
                .First(x => x.Code == "create.Destination");

            var createVehicleType = context.Permissions
                .First(x => x.Code == "create.VehicleType");

            var updateVehicleType = context.Permissions
                .First(x => x.Code == "update.VehicleType");

            var deleteVehicleType = context.Permissions
                .First(x => x.Code == "delete.VehicleType");

            var createVehicle = context.Permissions
                .First(x => x.Code == "create.vehicle"); 

            ExecuteNewPermission(context, defaultRole.Id, createDestination.Id);
            ExecuteNewPermission(context, defaultRole.Id, createVehicleType.Id);
            ExecuteNewPermission(context, defaultRole.Id, updateVehicleType.Id);
            ExecuteNewPermission(context, defaultRole.Id, deleteVehicleType.Id);
            ExecuteNewPermission(context, defaultRole.Id, createVehicle.Id);
        }

        private static void ExecuteDriveRole(AuthenticContext context)
        {
            var driveRole = context.Roles
                    .First(x => x.Name == "Motorista");

            var createDestination = context.Permissions
                .First(x => x.Code == "create.Destination");

            var startVehicleTravel = context.Permissions
                .First(x => x.Code == "start.vehicleTravel");

            var endsVehicleTravel = context.Permissions
                .First(x => x.Code == "ends.vehicleTravel");

            ExecuteNewPermission(context, driveRole.Id, createDestination.Id);
            ExecuteNewPermission(context, driveRole.Id, startVehicleTravel.Id);
            ExecuteNewPermission(context, driveRole.Id, endsVehicleTravel.Id);
        }

        private static void ExecuteAdminRole(AuthenticContext context)
        {
            var adminRole = context.Roles
                    .First(x => x.Name == "Admin");

            var createDestination = context.Permissions
                .First(x => x.Code == "create.Destination");

            var listDestination = context.Permissions
                .First(x => x.Code == "list.destination");

            var createVehicleType = context.Permissions
                .First(x => x.Code == "create.VehicleType");

            var findVehicleType = context.Permissions
                .First(x => x.Code == "find.vehicleType");

            var listVehicleType = context.Permissions
                .First(x => x.Code == "list.vehicleType");

            var updateVehicleType = context.Permissions
                .First(x => x.Code == "update.VehicleType");

            var deleteVehicleType = context.Permissions
                .First(x => x.Code == "delete.VehicleType");

            var createVehicle = context.Permissions
                .First(x => x.Code == "create.vehicle");

            var listVehicle = context.Permissions
                .First(x => x.Code == "list.vehicle");

            var startVehicleTravel = context.Permissions
                .First(x => x.Code == "start.vehicleTravel");

            var endsVehicleTravel = context.Permissions
                .First(x => x.Code == "ends.vehicleTravel");

            var findVehicleTravel = context.Permissions
                .First(x => x.Code == "find.vehicleType");

            var hanckinEconomyVehicleTravel = context.Permissions
                .First(x => x.Code == "hankingEconomy.vehicleTravel");

            var hankingMilageVehicleTravel = context.Permissions
                .First(x => x.Code == "hankingMilage.vehicleTravel");

            var reportsVehicleTravel = context.Permissions
                .First(x => x.Code == "reports.vehicleTravel");

            ExecuteNewPermission(context, adminRole.Id, createDestination.Id);
            ExecuteNewPermission(context, adminRole.Id, listDestination.Id);
            ExecuteNewPermission(context, adminRole.Id, createVehicleType.Id);
            ExecuteNewPermission(context, adminRole.Id, updateVehicleType.Id);
            ExecuteNewPermission(context, adminRole.Id, deleteVehicleType.Id);
            ExecuteNewPermission(context, adminRole.Id, createVehicle.Id);
            ExecuteNewPermission(context, adminRole.Id, startVehicleTravel.Id);
            ExecuteNewPermission(context, adminRole.Id, endsVehicleTravel.Id);
            ExecuteNewPermission(context, adminRole.Id, listVehicle.Id);
            ExecuteNewPermission(context, adminRole.Id, listVehicleType.Id);
            ExecuteNewPermission(context, adminRole.Id, findVehicleType.Id);
            ExecuteNewPermission(context, adminRole.Id, findVehicleTravel.Id);
            ExecuteNewPermission(context, adminRole.Id, hanckinEconomyVehicleTravel.Id);
            ExecuteNewPermission(context, adminRole.Id, hankingMilageVehicleTravel.Id);
            ExecuteNewPermission(context, adminRole.Id, reportsVehicleTravel.Id);
        }

        private static void ExecuteNewPermission(AuthenticContext context, int roleId, int permissionId)
        {
            if (!context.RolePermissions.Any(x =>
                                                x.RoleId == roleId &&
                                                x.PermissionId == permissionId))
            {
                context.RolePermissions.Add(new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                });
            }
        }
    }
}