using Electro.IdentityServer.Configurations;
using Electro.IdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Electro.IdentityServer.Database.Initailize
{
    public class DbInitializer : IDbInitializer
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ElectroUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ElectroUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            if (_roleManager.FindByNameAsync(IdentityServerConfigs.Admin).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(IdentityServerConfigs.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(IdentityServerConfigs.Customer)).GetAwaiter().GetResult();
            }
            else { return; }

            ElectroUser adminUser = new()
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "0967823093",
                FullName = "Hoang Thai Admin",
                Address = "06 Tran Van On, P. Phu Hoa, TDM, BD"
            };

            _userManager.CreateAsync(adminUser, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser, IdentityServerConfigs.Admin).GetAwaiter().GetResult();

            var _ = _userManager.AddClaimsAsync(adminUser, new Claim[] {
                new Claim(JwtClaimTypes.Name,adminUser.FullName),
                new Claim(JwtClaimTypes.Address,adminUser.Address),
                new Claim(JwtClaimTypes.Role,IdentityServerConfigs.Admin)
            }).Result;



            ElectroUser customerUser = new()
            {
                UserName = "customer@gmail.com",
                Email = "customer@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "0387038740",
                FullName = "Tram Le Customer",
                Address = "07 Tran Van On, P. Phu Hoa, TDM, BD"
            };

            _userManager.CreateAsync(customerUser, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(customerUser, IdentityServerConfigs.Customer).GetAwaiter().GetResult();

            _ = _userManager.AddClaimsAsync(customerUser, new Claim[] {
                 new Claim(JwtClaimTypes.Name,adminUser.FullName),
                new Claim(JwtClaimTypes.Address,adminUser.Address),
                new Claim(JwtClaimTypes.Role,IdentityServerConfigs.Admin)
            }).Result;
        }
    }
}
