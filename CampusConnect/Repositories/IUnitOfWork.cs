using System;
using System.Threading.Tasks;

namespace CampusConnect.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IEventRepository Events { get; }
        ILocationRepository Locations { get; }
        IRSVPRepository RSVPs { get; }

        Task<int> CompleteAsync();
    }
}
