using DorucovaciSluzba.Application.Abstraction;
using DorucovaciSluzba.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DorucovaciSluzba.Application.Implementation
{
    public class UserAppService : IUserAppService
    {
        private readonly DbContext _dbContext;

        public UserAppService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Uzivatel> Select()
        {
            return _dbContext.Set<Uzivatel>()
                   .Include(u => u.Typ)
                   .ToList();
        }

        public Uzivatel? FindByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            return _dbContext.Set<Uzivatel>()
                .FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        }

        public Uzivatel Create(Uzivatel uzivatel)
        {
            _dbContext.Set<Uzivatel>().Add(uzivatel);
            _dbContext.SaveChanges();
            return uzivatel;
        }

        public void Update(Uzivatel uzivatel)
        {
            _dbContext.Set<Uzivatel>().Update(uzivatel);
            _dbContext.SaveChanges();
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
            var uzivatel = _dbContext.Set<Uzivatel>().Find(userId);
            if (uzivatel == null)
            {
                return false; // uživatel neexistuje
            }

            try
            {
                _dbContext.Set <Uzivatel>().Remove(uzivatel);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Uzivatel? GetById(int id)
        {
            return _dbContext.Set<Uzivatel>()
                .Include(u => u.Typ) // Načti i typ/roli uživatele
                .FirstOrDefault(u => u.Id == id);
        }

        public IList<TypUzivatel> GetAllUserTypes()
        {
            return _dbContext.Set<TypUzivatel>()
                .OrderBy(t => t.Typ) // Seřaď podle názvu
                .ToList();
        }
    }
}
