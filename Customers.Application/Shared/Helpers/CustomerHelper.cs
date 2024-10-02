
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.SeedWork;

namespace Customers.Application.Shared.Helpers
{
    public static class CustomerHelper
    {
        public static Customer PrecisionDecimalValues(Customer customer)
        {
            if (customer.Buys != null && customer.Buys.Count > 0)
            {
                customer.Buys.ForEach(item =>
                {
                    item.Price = CalculatePrecision(item.Price); // Math.Round(item.Price, 2, MidpointRounding.AwayFromZero);
                    item.Total = CalculatePrecision(item.Total); // Math.Round(item.Total, 2, MidpointRounding.AwayFromZero);
                });
            }

            if (customer.Payments != null && customer.Payments.Count > 0)
            {
                customer.Payments.ForEach(item =>
                {
                    item.Value = CalculatePrecision(item.Value); // Math.Round(item.Value, 2, MidpointRounding.AwayFromZero);
                });
            }

            return customer;
        }

        public static List<Customer> FilterPropertyListNotDeleted(List<Customer> customer)
        {
            List<Customer> customerList = new();

            foreach (var customerItem in customer)
            {
                customerList.Add(FilterPropertyNotDeleted(customerItem));
            }
            return customerList;
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

        public static double CalculatePrecision(double value, int? precision = 2)
        {
            return Math.Round(value, (int)precision, MidpointRounding.AwayFromZero);
        }
    }
}
