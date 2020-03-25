using System.Threading.Tasks;
using Demelain.Server.Data;

namespace Demelain.Server.Repositories
{
    /// <summary>
    /// Represents a single transaction involving one or more insert/update/delete operations on one or more entities.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IPersonalDetailsRepository _personalDetailsRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// If an <see cref="IPersonalDetailsRepository"/> has previously been instantiated on the
        /// <see cref="IUnitOfWork"/> instance, returns that instance. Otherwise, creates and returns a new instance. 
        /// </summary>
        /// <returns>An <see cref="IPersonalDetailsRepository"/> object on the UnitofWork instance.</returns>
        public IPersonalDetailsRepository PersonalDetailsRepository =>
            _personalDetailsRepository ??= new PersonalDetailsRepository(_context);

        /// <summary>
        /// Commits any changes tracked by the <see cref="ApplicationDbContext"/>.
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync() =>
            await _context.SaveChangesAsync();

        /// <summary>
        /// Rolls back any changes tracked by the <see cref="ApplicationDbContext"/>.
        /// </summary>
        /// <returns></returns>
        public async Task RollbackAsync() =>
            await _context.DisposeAsync();
    }
}