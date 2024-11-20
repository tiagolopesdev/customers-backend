

namespace Customers.Domain.SeedWork
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public string? UpdatedBy { get; set; }

        public void SetDateCreated()
        {
            DateCreated = DateTime.Now;
        }
        public void SetDateUpdated()
        {
            DateUpdated = DateTime.Now;
        }
        public void SetDateDeleted()
        {
            DateDeleted = DateTime.Now;
        }
    }
}
