using CampusConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusConnect.Data.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Event>> GetAllWithLocationAsync()
        {
            return await _context.Events.Include(e => e.Location).ToListAsync();
        }

        public async Task<Event> GetByIdWithLocationAsync(string eventId)
        {
            return await _context.Events.Include(e => e.Location)
                                        .FirstOrDefaultAsync(e => e.EventId == eventId);
        }

        public async Task<Event> GetByIdWithAllRelationsAsync(string eventId)
        {
            return await _context.Events
                .Include(e => e.Location)
                .Include(e => e.CreatedBy)
                .Include(e => e.RSVPs)
                .FirstOrDefaultAsync(e => e.EventId == eventId);
        }

        public async Task<Event> GetByIdWithRSVPsAsync(string eventId)
        {
            return await _context.Events
                .Include(e => e.RSVPs)
                .FirstOrDefaultAsync(e => e.EventId == eventId);
        }
    }
}
