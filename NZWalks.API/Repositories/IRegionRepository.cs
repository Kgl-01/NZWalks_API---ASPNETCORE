using NZWalks.API.Models.Domain;
using System.Linq.Expressions;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
        Task<Region> GetByIdAsync(Expression<Func<Region, bool>> condition);
        Task<Region> CreateAsync(Region regionDomainModel);

        Task<Region> UpdateAsync(Guid id, Region regionModel);

        Task<Region> DeleteAsync(Expression<Func<Region, bool>> condition);
    }
}
