using DorucovaciSluzba.Domain.Entities;

namespace DorucovaciSluzba.Application.Abstraction
{
    public interface IPackageHistoryAppService
    {
        // Přidá nový záznam o změně stavu zásilky
        void Create(int zasilkaId, int stavId);

        // Načte všechny změny stavu pro konkrétní zásilku (seřazené od nejstarší)
        IList<PackageHistoryItem> GetHistoryForPackage(int zasilkaId);
    }
}
