using DorucovaciSluzba.Application.Abstraction;
using DorucovaciSluzba.Domain.Entities;
using DorucovaciSluzba.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DorucovaciSluzba.Application.Implementation
{
    public class UserAppService : IUserAppService
    {
        AppDbContext _appDbContext;

        public UserAppService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IList<Uzivatel> Select()
        {
            return _appDbContext.Uzivatele
                   .Include(u => u.Typ)
                   .ToList();
        }

        public Uzivatel? FindByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            return _appDbContext.Uzivatele
                .FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        }

        public Uzivatel Create(Uzivatel uzivatel)
        {
            _appDbContext.Uzivatele.Add(uzivatel);
            _appDbContext.SaveChanges();
            return uzivatel;
        }

        public void Update(Uzivatel uzivatel)
        {
            _appDbContext.Uzivatele.Update(uzivatel);
            _appDbContext.SaveChanges();
        }

        public Uzivatel GetOrCreate(string jmeno, string prijmeni, string email,
            string ulice, string cp, string mesto, string psc)
        {
            // Zkus najít podle emailu
            var existujici = FindByEmail(email);
            if (existujici != null)
            {
                // Uživatel existuje - aktualizuj adresu, pokud se změnila
                bool zmeneno = false;

                if (existujici.Ulice != ulice) { existujici.Ulice = ulice; zmeneno = true; }
                if (existujici.CP != cp) { existujici.CP = cp; zmeneno = true; }
                if (existujici.Mesto != mesto) { existujici.Mesto = mesto; zmeneno = true; }
                if (existujici.Psc != psc) { existujici.Psc = psc; zmeneno = true; }

                if (zmeneno)
                {
                    Update(existujici);
                }

                return existujici;
            }

            // Neexistuje → vytvoř nového neregistrovaného uživatele (bez hesla)
            var novy = new Uzivatel
            {
                Jmeno = jmeno,
                Prijmeni = prijmeni,
                Email = email,
                Heslo = null, // Žádné heslo = neregistrovaný
                Ulice = ulice,
                CP = cp,
                Mesto = mesto,
                Psc = psc,
                TypId = 2 // Běžný uživatel
            };

            return Create(novy);
        }

        public bool Delete(int userId)
        {
            var uzivatel = _appDbContext.Uzivatele.Find(userId);
            if (uzivatel == null)
            {
                return false; // uživatel neexistuje
            }
            _appDbContext.Uzivatele.Remove(uzivatel);
            _appDbContext.SaveChanges();
            return true;
        }
    }
}
