using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Server.Constants.Tenancy
{
    public class TenancyConstants
    {
        public static class Root
        {
            public const string Id="root";
            public const string Name="root";
            public const string Email="admin.root@school.com";

        }

        public const string DefaultPassword ="root";
        public const string TenantIdName ="tenant";
        public const string FirstName="Admnistrator";
        public const string LastName="Master";
    }

}