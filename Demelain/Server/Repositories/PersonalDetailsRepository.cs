using Demelain.Server.Data;
using Demelain.Server.Models.Entities;

namespace Demelain.Server.Repositories
{
    /// <summary>
    /// Type wrapper that exposes repository functionality for the
    /// PersonalDetails entity.
    /// </summary>
    public class PersonalDetailsRepository : RepositoryBase<PersonalDetails>, IPersonalDetailsRepository
    {
        public PersonalDetailsRepository(NexusContext context) : base(context)
        {
        }
    }
}