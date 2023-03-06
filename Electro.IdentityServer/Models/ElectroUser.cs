using Microsoft.AspNetCore.Identity;

namespace Electro.IdentityServer.Models
{
    public class ElectroUser : IdentityUser
    {
        public string Address { get; set; }

        public string FullName { get; set; }
    }
}
