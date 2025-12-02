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

        Zasilka? FindByCisloAndEmail(string cislo, string email);
        Zasilka? GetById(int id);
        void Update(Zasilka zasilka);

        IList<StavZasilka> GetAllStates();

        IList<Zasilka> SelectForUser(int userId);
        IList<Zasilka> SelectForKuryr(int kuryrId);
    }
}
