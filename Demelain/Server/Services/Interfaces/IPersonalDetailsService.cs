using System.Threading.Tasks;
using Demelain.Server.Models.Entities;

// ReSharper disable once CheckNamespace
namespace Demelain.Server.Services
{
    public interface IPersonalDetailsService
    {
        Task<PersonalDetails> GetByIdAsync(int id);
    }
}