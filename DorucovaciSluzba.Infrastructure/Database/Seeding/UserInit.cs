using DorucovaciSluzba.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DorucovaciSluzba.Infrastructure.Database.Seeding
{
    internal class UserInit
    {
        public IList<Uzivatel> GetUsers()
        {
            IList<Uzivatel> users = new List<Uzivatel>();

            users.Add(new Uzivatel()
            {
                Id = 1,
                Jmeno = "Web",
                Prijmeni = "Admin",
                Email = "admin@web.cz",
                Heslo = "admin",
                TypId = 1
            });

            return users;
        }
    }
}
