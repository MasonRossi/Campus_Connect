using CampusConnect.Models;
using System.Collections.Generic;

namespace CampusConnect.Data.Repositories
{
    public class RSVPRepository : Repository<RSVP>, IRSVPRepository
    {
        public RSVPRepository(AppDbContext context) : base(context) { }

        public void RemoveRange(IEnumerable<RSVP> rsvps)
        {
            _context.RSVPs.RemoveRange(rsvps);
        }
    }
}
