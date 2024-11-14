using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Server.Constants
{
    public class PermissionConstants
    {
        public static class ServiceAction
        {
        public const string View= nameof(View);
        public const string Create= nameof(Create);
        public const string Update= nameof(Update);
        public const string Delete= nameof(Delete);
        public const string UpgradeSubscription= nameof(UpgradeSubscription);
        }

        public static class ServiceFeature
        {
            public const string Tenants =nameof(Tenants);
            public const string Users =nameof(Users);
            public const string UserRoles =nameof(UserRoles);
            public const string Roles =nameof(Roles);
            public const string RoleClaims =nameof(RoleClaims);
            public const string Services =nameof(Services);
        }

        public record ServicePermission(string Description, string Action,string Feature, bool IsBasic=false, bool IsRoot=false)
        {
            public string Name => NameFor(Action, Feature);

            public static string NameFor(string action, string feature)
            {
                return $"Permission.{feature}.{action}";
            }
        }

        public static class ServicePermissions
        {
            private static readonly ServicePermission[] _allPermissions = 
            [
                new ServicePermission("View Role", ServiceAction.View,ServiceFeature.Users),
                new ServicePermission("Create Role", ServiceAction.Create,ServiceFeature.Users),
                new ServicePermission("Update Users", ServiceAction.Update,ServiceFeature.Users),
                new ServicePermission("Delete Users", ServiceAction.Delete,ServiceFeature.Users),

                
                new ServicePermission("View User Roles", ServiceAction.View,ServiceFeature.UserRoles),
                new ServicePermission("Update User Roles", ServiceAction.Update,ServiceFeature.UserRoles),


                
                new ServicePermission("View Roles", ServiceAction.View,ServiceFeature.Roles),
                new ServicePermission("Create Roles", ServiceAction.Create,ServiceFeature.Roles),
                new ServicePermission("Update Roles", ServiceAction.Update,ServiceFeature.Roles),
                new ServicePermission("Delete Roles", ServiceAction.Delete,ServiceFeature.Roles),

                new ServicePermission("View Role Claimes/Permissions", ServiceAction.View,ServiceFeature.RoleClaims),
                new ServicePermission("Update Role Claimes/Permissions", ServiceAction.Update,ServiceFeature.RoleClaims),


                new ServicePermission("View Services", ServiceAction.View,ServiceFeature.Services, IsBasic: true),
                new ServicePermission("Create Services", ServiceAction.Create,ServiceFeature.Services),
                new ServicePermission("Update Services", ServiceAction.Update,ServiceFeature.Services),
                new ServicePermission("Delete Services", ServiceAction.Delete,ServiceFeature.Services),

                new ServicePermission("View Tenants", ServiceAction.View,ServiceFeature.Tenants, IsRoot: true),
                new ServicePermission("Create Tenants", ServiceAction.Create,ServiceFeature.Tenants, IsRoot: true),
                new ServicePermission("Update Tenants", ServiceAction.Update,ServiceFeature.Tenants, IsRoot: true),
                new ServicePermission("Upgrade Tenants Subscription", ServiceAction.UpgradeSubscription,ServiceFeature.Tenants, IsRoot: true)

            ];
            public static IReadOnlyList<ServicePermission> All {get;} =  new ReadOnlyCollection<ServicePermission>(_allPermissions);
            public static IReadOnlyList<ServicePermission> Root {get;} =  new ReadOnlyCollection<ServicePermission>(_allPermissions.Where(p=>p.IsRoot).ToArray());
            public static IReadOnlyList<ServicePermission> Admin {get;} =  new ReadOnlyCollection<ServicePermission>(_allPermissions.Where(p=>!p.IsRoot).ToArray());
            public static IReadOnlyList<ServicePermission> Basic {get;} =  new ReadOnlyCollection<ServicePermission>(_allPermissions.Where(p=>p.IsBasic).ToArray());
        }
    
    }
}