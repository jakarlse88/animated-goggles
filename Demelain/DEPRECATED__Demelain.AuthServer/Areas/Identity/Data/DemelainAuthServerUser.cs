using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Demelain.AuthServer.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the DemelainAuthServerUser class
    public class DemelainAuthServerUser : IdentityUser
    {
        [PersonalData] public string FirstName { get; set; }
        [PersonalData] public string MiddleName { get; set; }
        [PersonalData] public string LastName { get; set; }
    }
}
