using CampusConnect.Models;

namespace CampusConnect.Data.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(AppDbContext context) : base(context) { }
    }
}
