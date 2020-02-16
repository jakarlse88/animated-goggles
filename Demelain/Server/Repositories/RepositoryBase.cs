using System.Linq;
using System.Threading.Tasks;
using Demelain.Server.Data;
using Demelain.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demelain.Server.Repositories
{
    /// <summary>
    /// Base repository class from which the separate repository classes
    /// derive functionality.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly DemelainContext _context;

        protected RepositoryBase(DemelainContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously retrieves an entity by id. If no entity
        /// matches the id, the result will be null. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id)
        {
            var result =
                await _context
                    .Set<T>()
                    .Where(t => 
                        t.Id == id)
                    .FirstOrDefaultAsync();

            return result;
        }
    }
}