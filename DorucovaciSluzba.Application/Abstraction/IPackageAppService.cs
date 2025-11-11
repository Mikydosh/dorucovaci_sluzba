using DorucovaciSluzba.Domain.Entities;

namespace DorucovaciSluzba.Application.Abstraction
{
    public interface IPackageAppService
    {
        IList<Zasilka> Select();
    }
}
