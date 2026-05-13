
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.SeedWork;

namespace Customers.Domain.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Guid> UpdateCustomer(Customer entity);
        Task<Pagination<Customer>> GetByName(string name, int pageIndex, int pageSize, bool owing);
        Task<Pagination<Customer>> GetAll(int pageIndex, int pageSize, bool owing);
    }
}
