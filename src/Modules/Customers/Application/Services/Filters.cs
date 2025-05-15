
using Domain.Customers;

namespace Application.Services;

public static class Filters
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
          List<Payment> payments = new();

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
}
