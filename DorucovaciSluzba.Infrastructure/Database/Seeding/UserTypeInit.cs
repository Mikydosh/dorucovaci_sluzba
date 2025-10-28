using DorucovaciSluzba.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DorucovaciSluzba.Infrastructure.Database.Seeding
{
    internal class UserTypeInit
    {
        public IList<TypUzivatel> GetUserTypes()
        {
            IList<TypUzivatel> types = new List<TypUzivatel>();

            types.Add(new TypUzivatel()
            {
                Id = 1,
                Typ = "admin"
            });

            types.Add(new TypUzivatel()
            {
                Id = 2,
                Typ = "bezny uzivatel"
            });

            types.Add(new TypUzivatel()
            {
                Id = 3,
                Typ = "kuryr"
            });

            types.Add(new TypUzivatel()
            {
                Id = 4,
                Typ = "podpora"
            });

            return types;
        }
    }
}
