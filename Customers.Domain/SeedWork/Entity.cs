

namespace Customers.Domain.SeedWork
{
    public abstract class   Entity
    {
        public Guid Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public string? UpdatedBy { get; set; }

        public void SetDateCreated()
        {
            Id = Guid.NewGuid();
            DateCreated = SouthAmericaZone();
        }
        public void SetDateUpdated()
        {
            DateUpdated = SouthAmericaZone();
        }
        public void SetDateDeleted()
        {
            DateDeleted = SouthAmericaZone();
        }

        public static DateTime SouthAmericaZone()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(
                DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")
                );
        }
    }
}
