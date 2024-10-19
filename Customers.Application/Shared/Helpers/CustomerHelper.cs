
using Customers.Domain.AggregatesModel.CustomerAggregate;

namespace Customers.Application.Shared.Helpers
{
    public static class CustomerHelper
    {
        public static Customer AssignAmountToPay(Customer customer)
        {
            if (customer.Payments != null && customer.Payments.Count > 0)
            {
                customer.AmountToPay -= customer.Payments.Sum(payment => payment.Value);
            }

            return customer;
        }
        public static List<Customer> AssignAmountToPayList(List<Customer> customers)
        {
            customers.ForEach(item =>
            {
                item = AssignAmountToPay(item);
            });

            return customers;
        }
        public static Customer PrecisionDecimalValues(Customer customer)
        {
            if (customer.Buys != null && customer.Buys.Count > 0)
            {
                customer.Buys.ForEach(item =>
                {
                    item.Price = Utilities.CalculatePrecision(item.Price); 
                    item.Total = Utilities.CalculatePrecision(item.Total);
                });
            }

            if (customer.Payments != null && customer.Payments.Count > 0)
            {
                customer.Payments.ForEach(item =>
                {
                    item.Value = Utilities.CalculatePrecision(item.Value);
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
                customer.Payments = Utilities.FilterNotDeleteEntity(customer.Payments);
            }
            if (customer.Buys != null && customer.Buys.Count > 0)
            {
                customer.Buys = Utilities.FilterNotDeleteEntity(customer.Buys);
            }
            return customer;
        }
    }
}
