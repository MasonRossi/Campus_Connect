using CampusConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CampusConnect.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User> GetByDisplayNameAsync(string displayName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.DisplayName == displayName);
        }

        public async Task<bool> ExistsByDisplayNameAsync(string displayName)
        {
            return await _context.Users.AnyAsync(u => u.DisplayName == displayName);
        }

        public async Task<User> GetByIdWithRSVPsAndCreatedEventsAsync(string userId)
        {
            return await _context.Users
                .Include(u => u.RSVPs)
                .Include(u => u.CreatedEvents)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}
