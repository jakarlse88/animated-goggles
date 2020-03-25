using System.Threading.Tasks;
using Demelain.Server.Models;

namespace Demelain.Server.Services
{
    public interface IPersonalDetailService
    {
        Task<PersonalDetails> GetByIdAsync(int id);
    }
}