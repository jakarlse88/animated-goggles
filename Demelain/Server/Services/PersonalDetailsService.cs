using System.Threading.Tasks;
using Demelain.Server.Models.Entities;
using Demelain.Server.Repositories;

namespace Demelain.Server.Services
{
    public class PersonalDetailsService : IPersonalDetailsService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public PersonalDetailsService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<PersonalDetails> GetByIdAsync(int id)
        {
            var result =
                await _repositoryWrapper
                    .PersonalDetails
                    .GetByIdAsync(id);

            return result;
        }
    }
}