using DorucovaciSluzba.Domain.Entities;
using DorucovaciSluzba.Infrastructure.Database.Seeding;
using DorucovaciSluzba.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DorucovaciSluzba.Infrastructure.Database
{
    public class AppDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Zasilka> Zasilky { get; set; }
        public DbSet<StavZasilka> StavyZasilek { get; set; }
        public DbSet<PackageHistoryItem> PackageHistoryItems { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Zasilka>()
                .Property(z => z.DatumOdeslani)
                .ValueGeneratedOnAdd()  // Fix: Datum se vygeneruje se jen při Add, ne při Update
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            modelBuilder.Entity<Zasilka>()
                .HasOne<User>()
                .WithMany(u => u.OdeslaneZasilky)
                .HasForeignKey(z => z.OdesilatelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Zasilka>()
                .HasOne<User>()
                .WithMany(u => u.PrijateZasilky)
                .HasForeignKey(z => z.PrijemceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Zasilka>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(z => z.KuryrId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seeding pro 4 stavy zásilky
            var stateInit = new StateInit();
            modelBuilder.Entity<StavZasilka>().HasData(stateInit.GetStates());

            // Identity seeding
            var rolesInit = new RolesInit();
            modelBuilder.Entity<Role>().HasData(rolesInit.GetRoles());

            // Seeding pro 4 role (typy uživatelů)
            var identityUserInit = new IdentityUserInit();
            modelBuilder.Entity<User>().HasData(identityUserInit.GetAllUsers());

            // Seeding pro uživatele s různými rolemi
            var userRolesInit = new UserRolesInit();
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(userRolesInit.GetAllUserRoles());

            // Seeding pro zásilky s různými uživateli
            var packageInit = new PackageInit();
            modelBuilder.Entity<Zasilka>().HasData(packageInit.GetPackages());
        }
    }
}
