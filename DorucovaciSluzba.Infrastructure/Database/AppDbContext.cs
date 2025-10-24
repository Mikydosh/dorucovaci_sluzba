using DorucovaciSluzba.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DorucovaciSluzba.Infrastructure.Database
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Zasilka> Zasilky { get; set; }
        public DbSet<Uzivatel> Uzivatele { get; set; }
        public DbSet<TypUzivatel> TypyUzivatelu { get; set; }
        public DbSet<StavZasilka> StavyZasilek { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
