using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Server.Constants
{
    public class RoleConstants
    {        
        public const string Admin=nameof(Admin);
        public const string Basic=nameof(Basic);

        public static IReadOnlyList<string> DefaultRoles {get;} =  new ReadOnlyCollection<string>(
            [
                Admin,
                Basic
            ]
        );

        public static bool IsDefault(string roleName)=> DefaultRoles.Any(role=>role==roleName);
    }
}