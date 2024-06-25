using NZWalks.API.Models.Domain;
using System.Linq.Expressions;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);

        Task<IEnumerable<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int limit = 5);

        Task<Walk?> GetByIdAsync(Expression<Func<Walk, bool>> expression);

        Task<Walk?> UpdateAsync(Guid id, Walk walk);

        Task<Walk?> DeleteAsync(Expression<Func<Walk, bool>> expression);

    }
}
