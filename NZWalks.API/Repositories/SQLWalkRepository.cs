using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System.Linq.Expressions;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly ApplicationDbContext _db;

        public SQLWalkRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _db.Walks.AddAsync(walk);
            await _db.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int limit = 5)
        {
            IQueryable<Walk> walks = _db.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(w => w.Name.Contains(filterQuery));

                }

            }

            //Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(w => w.Name) : walks.OrderByDescending(w => w.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(w => w.LengthInKm) : walks.OrderByDescending(w => w.LengthInKm);
                }

            }

            //Pagination
            var skipResults = (pageNumber - 1) * limit;


            return await walks.Skip(skipResults).Take(limit).ToListAsync();

            //return await _db.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Expression<Func<Walk, bool>> expression)
        {
            IQueryable<Walk> query = _db.Set<Walk>();

            return await query.Include("Difficulty").Include("Region").FirstOrDefaultAsync(expression);

        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {

            var existingWalk = await _db.Walks.FirstOrDefaultAsync(x => x.Id == id);


            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;

            await _db.SaveChangesAsync();
            return existingWalk;

        }

        public async Task<Walk?> DeleteAsync(Expression<Func<Walk, bool>> expression)
        {
            var existingWalk = await _db.Walks.FirstOrDefaultAsync(expression);

            if (existingWalk == null)
            {
                return null;
            }

            _db.Walks.Remove(existingWalk);
            await _db.SaveChangesAsync();

            return existingWalk;
        }
    }
}
