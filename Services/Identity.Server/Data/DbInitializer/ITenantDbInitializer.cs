using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Server.Data.DbInitializer
{
    public interface ITenantDbInitializer
    {
        Task InitializeDatabaseAsync(CancellationToken cancellationToken); 
    }
}