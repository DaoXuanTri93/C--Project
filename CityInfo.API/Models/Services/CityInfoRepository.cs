using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Models.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;
        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // lấy tất cả dữ liệu của thành phố
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointOfInterest)
        {
            if (includePointOfInterest)
            {
                return await _context.Cities.Include(c => c.PointOfInterests)
                    .Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }
            return await _context.Cities
                    .Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }
        public async Task<IEnumerable<PointOfInterest>> GetPointOfInterestForCityAsync(int cityId)
        {
            return await _context.PointOfInterests.Where(c => c.Id == cityId).ToListAsync();
        }

        public async Task<IEnumerable<PointOfInterest?>> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointOfInterests
                  .Where(p => p.CityId == cityId && p.Id == pointOfInterestId).ToListAsync();
        }

        public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);
            if (city != null)
            {
                city.PointOfInterests.Add(pointOfInterest);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
