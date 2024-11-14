using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;

namespace Identity.Server.Constants.Tenancy
{
     public class ServiceTenantInfo: ITenantInfo
    { 
        public string Id { get ; set ; }
        public string Identifier { get ; set ; }
        public string Name { get ; set ; }
        public string ConnectionString { get ; set ; }
        public string AdminEmail { get ; set ; }
        public DateTime ValidUpTo { get ; set ; }
        public bool IsActive { get; set; }   

    }
}