using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Identity.Server.Models
{
    public class AppUser :  IdentityUser
    {
        public string FirstName { get; set; }  
        public string LastName { get; set; }
        public string UserLock {get; set;}
        public string RefreshToken { get; set; }    
        public DateTime RefreshTokenExpiryTime { get; set; }
        public bool IsActive {get; set;}
    }
}