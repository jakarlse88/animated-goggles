using System;
using System.Linq;
using System.Linq.Expressions;
using Demelain.Server.Data;
using Demelain.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Demelain.Server.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> FindAll() =>
            _context
                .Set<T>()
                .AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _context
                .Set<T>()
                .Where(expression)
                .AsNoTracking();

        public void Insert(T entity) =>
            _context
                .Set<T>()
                .Add(entity);

        public void Update(T entity) =>
            _context
                .Set<T>()
                .Update(entity);

        public void Delete(T entity) =>
            _context
                .Set<T>()
                .Remove(entity);

        public bool Exists(Expression<Func<T, bool>> expression) =>
            _context
                .Set<T>()
                .Where(expression)
                .Any();
    }
}