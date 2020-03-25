using System.Threading.Tasks;
using Demelain.Server.Models;
using Demelain.Server.Repositories;

namespace Demelain.Server.Services
{
    public class PersonalDetailsService : IPersonalDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonalDetailsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PersonalDetails> GetByIdAsync(int id)
        {
            var result =
                await _unitOfWork
                    .PersonalDetailsRepository
                    .FindById(id);

            return result;
        }
    }
}