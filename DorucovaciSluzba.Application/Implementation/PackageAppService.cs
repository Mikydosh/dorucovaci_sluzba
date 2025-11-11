using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DorucovaciSluzba.Application.Abstraction;
using DorucovaciSluzba.Domain.Entities;
using DorucovaciSluzba.Infrastructure.Database;

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
            return _appDbContext.Zasilky.ToList();
        }
    }
}
