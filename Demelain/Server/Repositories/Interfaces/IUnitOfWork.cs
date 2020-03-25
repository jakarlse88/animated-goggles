using System.Threading.Tasks;

namespace Demelain.Server.Repositories
{
    public interface IUnitOfWork
    {
        IPersonalDetailsRepository PersonalDetailsRepository { get; }
        Task CommitAsync();
        Task RollbackAsync();
    }
}