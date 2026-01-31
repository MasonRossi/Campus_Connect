using CampusConnect.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusConnect.Data.Repositories
{
    public interface IRSVPRepository : IRepository<RSVP>
    {
        void RemoveRange(IEnumerable<RSVP> rsvps);
    }
}
