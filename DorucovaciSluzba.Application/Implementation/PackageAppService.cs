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
    public class PackageAppService : IPackageAppService
    {
        AppDbContext _appDbContext;

        public PackageAppService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IList<Zasilka> Select()
        {
            return _appDbContext.Zasilky
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

            _appDbContext.Zasilky.Add(zasilka);
            _appDbContext.SaveChanges();
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
                if (!_appDbContext.Zasilky.Any(z => z.Cislo == cislo))
                {
                    return cislo; // Unikátní číslo nalezeno
                }
            }

            throw new Exception($"Nepodařilo se vygenerovat unikátní číslo zásilky po {maxPokusu} pokusech.");
        }
    }
}
