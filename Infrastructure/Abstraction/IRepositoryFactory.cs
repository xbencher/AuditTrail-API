
using Infrastructure.Domain;

namespace Infrastructure.Abstraction
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    }
}
