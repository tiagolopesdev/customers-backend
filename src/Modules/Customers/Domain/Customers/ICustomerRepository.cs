using BlockDomain.SeedWork;

namespace Domain.Customers;

public interface ICustomerRepository : IBaseRepository<CustomerAggregateRoot>
{
    Task<Guid> UpdateCustomer(CustomerAggregateRoot entity);
    Task<List<CustomerAggregateRoot>> GetByName(string name);
}
