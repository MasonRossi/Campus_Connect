using System.Threading.Tasks;

namespace CampusConnect.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUserRepository Users { get; private set; }
        public IEventRepository Events { get; private set; }
        public ILocationRepository Locations { get; private set; }
        public IRSVPRepository RSVPs { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Users = new UserRepository(_context);
            Events = new EventRepository(_context);
            Locations = new LocationRepository(_context);
            RSVPs = new RSVPRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
