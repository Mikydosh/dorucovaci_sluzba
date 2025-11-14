using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DorucovaciSluzba.Domain.Entities;

namespace DorucovaciSluzba.Application.Abstraction
{
    public interface IUserAppService
    {
        IList<Uzivatel> Select();

        Uzivatel? FindByEmail(string email);
        Uzivatel Create(Uzivatel uzivatel);
        void Update(Uzivatel uzivatel);
        Uzivatel GetOrCreate(string jmeno, string prijmeni, string email,
            string ulice, string cp, string mesto, string psc);
    }
}
