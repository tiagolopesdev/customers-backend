
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.SeedWork;

namespace Customers.Application.Shared.Helpers
{
    public static class CustomerHelper
    {
        public static List<Customer> FilterPropertyListNotDeleted(List<Customer> customer)
        {
            foreach (var customerItem in customer)
            {

                if (customerItem.Payments != null && customerItem.Payments.Count > 0)
                {
                    customerItem.Payments = FilterEntity(customerItem.Payments);
                }
                if (customerItem.Buys != null && customerItem.Buys.Count > 0)
                {
                    customerItem.Buys = FilterEntity(customerItem.Buys);
                }
            }
            return customer;
        }

        public static Customer FilterPropertyNotDeleted(Customer customer)
        {
            if (customer.Payments != null && customer.Payments.Count > 0)
            {
                customer.Payments = FilterEntity(customer.Payments);
            }
            if (customer.Buys != null && customer.Buys.Count > 0)
            {
                customer.Buys = FilterEntity(customer.Buys);
            }
            return customer;
        }
        public static List<T>? FilterEntity<T>(List<T> entity) where T : Entity
        {
            return entity.Where(filter => filter.DateDeleted == null).ToList();
        }
    }
}
