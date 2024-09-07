
namespace Customers.Domain.SeedWork
{
    public interface IBaseRepository<T> where T : Entity
    {
        void Create(T entity);
        Task<Guid> Update(T entity);
        Task<Guid> Delete(T entity);
        Task<T> GetById(Guid id);
        Task<List<T>> GetAll();
    }
}
