using Microsoft.AspNetCore.Identity;

namespace DorucovaciSluzba.Infrastructure.Database.Seeding
{
    internal class UserRolesInit
    {
        public List<IdentityUserRole<int>> GetAllUserRoles()
        {
            return new List<IdentityUserRole<int>>
            {
                // Admin
                new IdentityUserRole<int> { UserId = 1, RoleId = 1 },
                new IdentityUserRole<int> { UserId = 2, RoleId = 1 },

                // Podpora
                new IdentityUserRole<int> { UserId = 3, RoleId = 2 },
                
                // Kurýři
                new IdentityUserRole<int> { UserId = 4, RoleId = 3 },
                new IdentityUserRole<int> { UserId = 5, RoleId = 3 },
                new IdentityUserRole<int> { UserId = 6, RoleId = 3 },

                
                // Uživatelé
                new IdentityUserRole<int> { UserId = 7, RoleId = 4 },
                new IdentityUserRole<int> { UserId = 8, RoleId = 4 },
                new IdentityUserRole<int> { UserId = 9, RoleId = 4 },
                new IdentityUserRole<int> { UserId = 10, RoleId = 4 },
                new IdentityUserRole<int> { UserId = 11, RoleId = 4 },

            };
        }
    }
}