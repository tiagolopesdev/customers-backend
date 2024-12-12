
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.AggregatesModel.Payment;

namespace Customers.Application.Shared.Helpers
{
    public static class CustomerHelper
    {
        public static List<Customer> ApplyingFilters(List<Customer> data, bool owing, string? usersSales, DateTime? date)
        {
            List<Customer> dataToReturn = new List<Customer>();

            if (!string.IsNullOrEmpty(usersSales) && !string.IsNullOrEmpty(date.ToString()))
            {
                data.ForEach(element =>
                {

                    bool existsBuys = element.Buys.Exists(filter => filter.UpdatedBy == usersSales);
                    bool existsPayment = element.Payments.Exists(filter => filter.UpdatedBy == usersSales);

                    if (existsBuys || existsPayment)
                    {
                        List<PaymentEntity> payments = new();

                        element.Payments.ForEach(buy =>
                        {
                            if (buy.DateCreated.Value.Date.CompareTo(date) == 0 || (buy.DateUpdated != null && buy.DateUpdated.Value.Date.CompareTo(date) == 0))
                            {
                                payments.Add(buy);
                            }
                        });

                        if (payments.Count > 0)
                        {
                            element.Payments = payments;
                            dataToReturn.Add(element);
                        }
                    }
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
