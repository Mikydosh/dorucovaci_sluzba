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
                    .Include(z => z.Stav)
                    .OrderBy(z => z.Id)
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
                 .Include(z => z.Stav)
                 .FirstOrDefault(z => z.Cislo == cislo);
        }

        public Zasilka? GetById(int id)
        {
            return _dbContext.Set<Zasilka>()
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
            
            if (zasilka.KuryrId.HasValue)
            {
                existujiciZasilka.KuryrId = zasilka.KuryrId;
            }

            _dbContext.SaveChanges();
        }

        public IList<StavZasilka> GetAllStates()
        {
            return _dbContext.Set<StavZasilka>()
                .OrderBy(s => s.Id)
                .ToList();
        }

        public IList<Zasilka> SelectForUser(int userId)
        {
            return _dbContext.Set<Zasilka>()
                .Include(z => z.Stav)
                .Where(z => z.OdesilatelId == userId || z.PrijemceId == userId)
                .OrderByDescending(z => z.DatumOdeslani)
                .ToList();
        }

        public IList<Zasilka> SelectForKuryr(int kuryrId)
        {
            return _dbContext.Set<Zasilka>()
                .Include(z => z.Stav)
                .Where(z => z.KuryrId == kuryrId)
                .OrderByDescending(z => z.DatumOdeslani)
                .ToList();
        }

        // filtry
        public IList<Zasilka> Select(string? sortBy = null, string? sortOrder = "asc", string? search = null)
        {
            var query = _dbContext.Set<Zasilka>()
                .Include(z => z.Stav)
                .AsQueryable();

            // Textové vyhledávání
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                query = query.Where(z =>
                    z.Cislo.ToLower().Contains(search) ||
                    z.DestinaceUlice.ToLower().Contains(search) ||
                    z.DestinaceMesto.ToLower().Contains(search) ||
                    z.DestinacePsc.Contains(search)
                );
            }

            // Výchozí řazení podle Id
            if (string.IsNullOrEmpty(sortBy))
            {
                sortBy = "Id";
            }

            // Aplikuj řazení podle sloupce
            query = sortBy.ToLower() switch
            {
                "id" => sortOrder == "desc"
                    ? query.OrderByDescending(z => z.Id)
                    : query.OrderBy(z => z.Id),

                "cislo" => sortOrder == "desc"
                    ? query.OrderByDescending(z => z.Cislo)
                    : query.OrderBy(z => z.Cislo),

                "datum" => sortOrder == "desc"
                    ? query.OrderByDescending(z => z.DatumOdeslani)
                    : query.OrderBy(z => z.DatumOdeslani),

                "kuryr" => sortOrder == "desc"
                    ? query.OrderByDescending(z => z.KuryrId)
                    : query.OrderBy(z => z.KuryrId),

                "stav" => sortOrder == "desc"
                    ? query.OrderByDescending(z => z.Stav!.Stav)
                    : query.OrderBy(z => z.Stav!.Stav),

                _ => query.OrderBy(z => z.Id) // Výchozí
            };

            return query.ToList();
        }
    }
}
