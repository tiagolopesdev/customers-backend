
using Customers.Domain.AggregatesModel.CustomerAggregate;

namespace Customers.Application.Shared.Helpers
{
    public static class CustomerHelper
    {
        public static List<Customer> ApplyingFilters(List<Customer> data, bool owing, string? usersSales)
        {
            List<Customer> dataToReturn = new List<Customer>();

            if (!string.IsNullOrEmpty(usersSales) && owing)
            {
                data.ForEach(element =>
                {
                    var existsBuys = element.Buys.Exists(filter => filter.UpdatedBy == usersSales);

                    if (existsBuys && element.AmountToPay > 0) dataToReturn.Add(element);
                });
            }
            else if (!string.IsNullOrEmpty(usersSales))
            {
                data.ForEach(element =>
                {
                    var existsBuys = element.Buys.Exists(filter => filter.UpdatedBy == usersSales);

                    if (existsBuys) dataToReturn.Add(element);
                });
            }
            else if (owing)
            {
                data.ForEach(element =>
                {
                    if (element.AmountToPay > 0) dataToReturn.Add(element);
                });
            }
            else
            {
                dataToReturn.AddRange(data);
            }

            return dataToReturn;
        }

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
