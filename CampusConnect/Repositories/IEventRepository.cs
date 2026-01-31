using CampusConnect.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusConnect.Data.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<IEnumerable<Event>> GetAllWithLocationAsync();
        Task<Event> GetByIdWithLocationAsync(string eventId);
        Task<Event> GetByIdWithAllRelationsAsync(string eventId);
        Task<Event> GetByIdWithRSVPsAsync(string eventId);
    }
}
