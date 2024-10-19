using Customers.Domain.SeedWork;

namespace Customers.Application.Shared.Helpers
{
    public static class Utilities
    {
        public static List<T>? FilterNotDeleteEntity<T>(List<T> entity) where T : Entity
        {
            return entity.Where(filter => filter.DateDeleted == null).ToList();
        }

        public static double CalculatePrecision(double value, int? precision = 2)
        {
            return Math.Round(value, (int)precision, MidpointRounding.AwayFromZero);
        }
    }
}
