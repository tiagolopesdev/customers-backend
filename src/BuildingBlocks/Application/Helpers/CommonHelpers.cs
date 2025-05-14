
using BlockDomain.SeedWork;

namespace BlockApplication.Helpers;

public class CommonHelpers
{
  public static DateTime DateTimeForBrazil()
  {
    return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Brazilian Standard Time"));
  }
  public static List<T>? FilterNotDeleteEntity<T>(List<T> entity) where T : Entity
  {
    return entity.Where(filter => filter.DateDeleted == null).ToList();
  }

  public static double CalculatePrecision(double value, int? precision = 2)
  {
    return Math.Round(value, (int)precision, MidpointRounding.AwayFromZero);
  }
}
