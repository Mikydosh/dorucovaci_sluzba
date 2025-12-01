using DorucovaciSluzba.Infrastructure.Identity;

namespace DorucovaciSluzba.Infrastructure.Database.Seeding
{
    internal class RolesInit
    {
        public List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();

            Role roleAdmin = new Role()
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "9cf14c2c-19e7-40d6-b744-8917505c3106"
            };

            Role rolePodpora = new Role()
            {
                Id = 2,
                Name = "Podpora",
                NormalizedName = "PODPORA",
                ConcurrencyStamp = "3ea1f7bc-4de1-5fg9-d4e8-5890e8cdb4ff"
            };

            Role roleKuryr = new Role()
            {
                Id = 3,
                Name = "Kuryr",
                NormalizedName = "KURYR",
                ConcurrencyStamp = "be0efcde-9d0a-461d-8eb6-444b043d6660"
            };

            Role roleUzivatel = new Role()
            {
                Id = 4,
                Name = "Uzivatel",
                NormalizedName = "UZIVATEL",
                ConcurrencyStamp = "29dafca7-cd20-4cd9-a3dd-4779d7bac3ee"
            };

            roles.Add(roleAdmin);
            roles.Add(rolePodpora);
            roles.Add(roleKuryr);
            roles.Add(roleUzivatel);

            return roles;
        }
    }
}