using DorucovaciSluzba.Application.Abstraction;
using DorucovaciSluzba.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DorucovaciSluzba.Application.Implementation
{
    public class PackageHistoryAppService : IPackageHistoryAppService
    {
        private readonly DbContext _dbContext;

        public PackageHistoryAppService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(int zasilkaId, int stavId)
        {
            var historyItem = new PackageHistoryItem
            {
                ZasilkaId = zasilkaId,
                StavId = stavId,
                DatumZmeny = DateTime.Now
            };

            _dbContext.Set<PackageHistoryItem>().Add(historyItem);
            _dbContext.SaveChanges();
        }

        public IList<PackageHistoryItem> GetHistoryForPackage(int zasilkaId)
        {
            return _dbContext.Set<PackageHistoryItem>()
                .Include(h => h.Stav)
                .Where(h => h.ZasilkaId == zasilkaId)
                .OrderBy(h => h.DatumZmeny)
                .ToList();
        }
    }
}
