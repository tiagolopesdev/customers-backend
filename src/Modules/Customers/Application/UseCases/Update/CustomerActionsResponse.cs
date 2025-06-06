
namespace Application.UseCases.Update;

public abstract class CustomerActionsResponse
{
  public Guid? Id { get; set; }
  public bool IsEnable { get; set; }
  public string? UpdatedBy { get; set; }
}
