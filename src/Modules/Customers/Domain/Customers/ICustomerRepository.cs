using BlockDomain.SeedWork;

namespace Domain.Customers;

public interface ICustomerRepository : IBaseRepository<Customer>
{
  Task<Guid> UpdateCustomer(Customer entity);
  Task<List<Customer>> GetByName(string name);
}
