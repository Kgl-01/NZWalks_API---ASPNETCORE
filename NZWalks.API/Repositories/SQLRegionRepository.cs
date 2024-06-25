using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System.Linq.Expressions;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly ApplicationDbContext _db;

        public SQLRegionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _db.Regions.ToListAsync();
        }

        public async Task<Region> GetByIdAsync(Expression<Func<Region, bool>> condition)
        {
            return await _db.Regions.FirstOrDefaultAsync(condition);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _db.Regions.AddAsync(region);
            await _db.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region regionModel)
        {
            var existingRegion = await _db.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Name = regionModel.Name;
            existingRegion.Code = regionModel.Code;
            existingRegion.RegionImageUrl = regionModel.RegionImageUrl;

            await _db.SaveChangesAsync();

            return existingRegion;
        }

        public async Task<Region> DeleteAsync(Expression<Func<Region, bool>> condition)
        {

            var deleteItem = await _db.Regions.FirstOrDefaultAsync(condition);

            if (deleteItem == null)
            {
                return null;
            }

            _db.Regions.Remove(deleteItem);
            await _db.SaveChangesAsync();

            return deleteItem;

        }

    }
}
