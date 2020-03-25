using System.Linq;
using System.Threading.Tasks;
using Demelain.Server.Data;
using Demelain.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Demelain.Server.Repositories
{
    public class PersonalDetailsRepository : RepositoryBase<PersonalDetails>, IPersonalDetailsRepository
    {
        public PersonalDetailsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PersonalDetails> FindById(int id) =>
            await base.FindByCondition(entity => 
                    entity.Id == id)
                .FirstOrDefaultAsync();
    }
}