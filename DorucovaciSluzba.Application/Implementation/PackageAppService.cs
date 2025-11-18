using DorucovaciSluzba.Application.Abstraction;
using DorucovaciSluzba.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DorucovaciSluzba.Application.Implementation
{
    public class PackageAppService : IPackageAppService
    {
        private readonly DbContext _dbContext;

        public PackageAppService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Zasilka> Select()
        {
            return _dbContext.Set<Zasilka>()
                    .Include(z => z.Odesilatel)
                    .Include(z => z.Prijemce)
                    .Include(z => z.Kuryr)
                    .Include(z => z.Stav)
                    .ToList();
        }

        public void Create(Zasilka zasilka)
        {
            // Vygeneruj unikátní číslo zásilky
            zasilka.Cislo = GenerujCisloZasilky();
            zasilka.DatumOdeslani = DateTime.Now;
            zasilka.StavId = 1;

            _dbContext.Set<Zasilka>().Add(zasilka);
            _dbContext.SaveChanges();
        }

        private string GenerujCisloZasilky()
        {
            const int maxPokusu = 500;
            var random = new Random();

            for (int pokus = 0; pokus < maxPokusu; pokus++)
            {
                // Generuje číslo ve formátu xxx-xx-xx
                string cislo = $"{random.Next(0, 1000):D3}-{random.Next(0, 100):D2}-{random.Next(0, 100):D2}";

                // Kontrola, jestli číslo neexistuje v databázi
                if (!_dbContext.Set<Zasilka>().Any(z => z.Cislo == cislo))
                {
                    return cislo;
                }
            }

            throw new Exception($"Nepodařilo se vygenerovat unikátní číslo zásilky po {maxPokusu} pokusech.");
        }
        public bool Delete(int zasilkaId)
        {
            var zasilka = _dbContext.Set<Zasilka>().Find(zasilkaId);
            if (zasilka == null)
            {
                return false; // Zásilka neexistuje
            }
            _dbContext.Set<Zasilka>().Remove(zasilka);
            _dbContext.SaveChanges();
            return true;
        }

        public Zasilka? FindByCisloAndEmail(string cislo, string email)
        {
            return _dbContext.Set<Zasilka>()
                .Include(z => z.Odesilatel)
                .Include(z => z.Prijemce)
                .Include(z => z.Kuryr)
                .Include(z => z.Stav)
                .FirstOrDefault(z =>
                    z.Cislo == cislo &&
                    (z.Odesilatel!.Email.ToLower() == email.ToLower() ||
                     z.Prijemce!.Email.ToLower() == email.ToLower())
                );
        }

        public Zasilka? GetById(int id)
        {
            return _dbContext.Set<Zasilka>()
                .Include(z => z.Odesilatel)
                .Include(z => z.Prijemce)
                .Include(z => z.Kuryr)
                .Include(z => z.Stav)
                .FirstOrDefault(z => z.Id == id);
        }

        public void Update(Zasilka zasilka)
        {
            var existujiciZasilka = _dbContext.Set<Zasilka>().Find(zasilka.Id);

            if (existujiciZasilka == null)
            {
                throw new Exception("Zásilka nebyla nalezena.");
            }

            existujiciZasilka.StavId = zasilka.StavId;
            existujiciZasilka.KuryrId = zasilka.KuryrId;

            _dbContext.SaveChanges();
        }

        public IList<Uzivatel> GetAllCouriers()
        {
            return _dbContext.Set<Uzivatel>()
                .Where(u => u.TypId == 3) // TypId 3 = Kurýr
                .OrderBy(u => u.Prijmeni)
                .ThenBy(u => u.Jmeno)
                .ToList();
        }

        public IList<StavZasilka> GetAllStates()
        {
            return _dbContext.Set<StavZasilka>()
                .OrderBy(s => s.Id)
                .ToList();
        }
    }
}
