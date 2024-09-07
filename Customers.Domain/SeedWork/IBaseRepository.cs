
namespace Customers.Domain.SeedWork
{
    public interface IBaseRepository<T> where T : Entity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> GetById(Guid id);
        Task<List<T>> GetAll();
    }
}
