using DorucovaciSluzba.Domain.Entities;
using DorucovaciSluzba.Infrastructure.Database.Seeding;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Zasilka>()
                .HasOne(z => z.Odesilatel)
                .WithMany(u => u.OdeslaneZasilky)
                .HasForeignKey(z => z.OdesilatelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Zasilka>()
                .HasOne(z => z.Prijemce)
                .WithMany(u => u.PrijateZasilky)
                .HasForeignKey(z => z.PrijemceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Zasilka>()
                .HasOne(z => z.Kuryr)
                .WithMany()
                .HasForeignKey(z => z.KuryrId)
                .OnDelete(DeleteBehavior.Restrict);

            var userTypeInit = new UserTypeInit();
            modelBuilder.Entity<TypUzivatel>().HasData(userTypeInit.GetUserTypes());

            var userInit = new UserInit();
            modelBuilder.Entity<Uzivatel>().HasData(userInit.GetUsers());
        }
    }
}
