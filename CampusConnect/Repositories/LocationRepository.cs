using CampusConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusConnect.Data.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(AppDbContext context) : base(context) { }
        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _context.Locations.ToListAsync();
        }
    }
}
