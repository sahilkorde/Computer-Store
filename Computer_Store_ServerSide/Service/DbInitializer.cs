using Computer_Store_Common;
using Computer_Store_DataAccess.Data;
using Computer_Store_ServerSide.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Computer_Store_ServerSide.Service
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        public DbInitializer(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
                if (!_roleManager.RoleExistsAsync(SD.Role_admin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_admin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                }
                else
                {
                    return;
                }

                IdentityUser user = new()
                {
                    UserName = "Sahil@gmail.com",
                    Email = "Sahil@gmail.com",
                    EmailConfirmed = true
                };

                _userManager.CreateAsync(user, "Admin123*").GetAwaiter().GetResult();

                _userManager.AddToRoleAsync(user, SD.Role_admin).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {

            }
        }
    }
}
