
namespace BlockInfrastructure.EventBus
{
    public class IntegrationEvent
    {
        public Guid Id { get; }
        public DateTime OccurrendOn { get; }

        protected IntegrationEvent()
        {
            Id = Guid.NewGuid();
            OccurrendOn = DateTime.Now;
        }
    }
}
