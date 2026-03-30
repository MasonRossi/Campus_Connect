using CampusConnect.Models;
using Microsoft.EntityFrameworkCore;

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
            .ThenInclude(r => r.Event)
                .ThenInclude(e => e.Location)
        .Include(u => u.CreatedEvents)
            .ThenInclude(e => e.Location)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}
