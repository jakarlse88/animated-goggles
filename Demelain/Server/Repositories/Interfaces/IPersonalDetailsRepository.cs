using System.Threading.Tasks;
using Demelain.Server.Models;

namespace Demelain.Server.Repositories
{
    public interface IPersonalDetailsRepository : IRepositoryBase<PersonalDetails>
    {
        Task<PersonalDetails> FindById(int id);
    }
}