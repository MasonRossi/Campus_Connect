using CampusConnect.Models;
using System.Threading.Tasks;

namespace CampusConnect.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByDisplayNameAsync(string displayName);
        Task<bool> ExistsByDisplayNameAsync(string displayName);
        Task<User> GetByIdWithRSVPsAndCreatedEventsAsync(string userId);
    }
}
