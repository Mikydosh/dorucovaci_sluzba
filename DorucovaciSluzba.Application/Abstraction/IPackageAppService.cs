using DorucovaciSluzba.Domain.Entities;

namespace DorucovaciSluzba.Application.Abstraction
{
    public interface IPackageAppService
    {
        IList<Zasilka> Select();
        // filtry
        IList<Zasilka> Select(string? sortBy = null, string? sortOrder = "asc", string? search = null);
        void Create(Zasilka zasilka);
        bool Delete(int zasilkaId);

        Zasilka? FindByCislo(string cislo);
        Zasilka? GetById(int id);
        void Update(Zasilka zasilka);

        void UpdateAddress(int zasilkaId, string ulice, string cp, string mesto, string psc);

        IList<StavZasilka> GetAllStates();

        IList<Zasilka> SelectForUser(int userId);
        IList<Zasilka> SelectForKuryr(int kuryrId);
    }
}
