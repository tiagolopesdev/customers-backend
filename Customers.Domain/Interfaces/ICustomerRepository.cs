
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.SeedWork;

namespace Customers.Domain.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<List<Customer>> GetByName(string name);
    }
}
