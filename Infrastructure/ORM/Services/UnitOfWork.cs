using Infrastructure.Abstraction;
using Infrastructure.Domain;
using Infrastructure.ORM.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Infrastructure.ORM.Services
{
    public class UnitOfWork<TContext> : IUnitOfWork, IRepositoryFactory where TContext : DbContext
    {
        private readonly TContext _dbContext;
        private bool _disposed = false;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        IRepository<TEntity> IRepositoryFactory.Repository<TEntity>()
        {
            if (_repositories == null)
                _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type))
                _repositories[type] = new Repository<TEntity>(_dbContext);

            return (IRepository<TEntity>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                   
                    _dbContext.Dispose();
                }

                _disposed = true;
            }
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
                _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type))
                _repositories[type] = new Repository<TEntity>(_dbContext);

            return (IRepository<TEntity>)_repositories[type];
        }
    }
}
