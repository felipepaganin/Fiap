using Fiap.Data.Context;
using Fiap.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fiap.Data
{
    public class ApplicationRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : class
    {
        private readonly FiapDbContext _repositoryContext;
        private readonly DbSet<T> _dbSet;

        public ApplicationRepository(FiapDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext ?? throw new ArgumentNullException(nameof(repositoryContext));
            _dbSet = _repositoryContext.Set<T>();
        }

        public IQueryable<T> FindAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task<T> AddAsync(T entity)
        {
            var result = await _dbSet.AddAsync(entity, new CancellationToken());

            return result.Entity;
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Any(expression);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            var obj = await _dbSet.FindAsync(entity);
            _dbSet.Remove(obj);
            await Task.CompletedTask;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}

