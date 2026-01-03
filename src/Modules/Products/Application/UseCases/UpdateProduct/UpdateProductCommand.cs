using BlockApplication.Contracts.CommandQuery;

namespace Product.Application.UseCases.UpdateProduct;

public sealed record class UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    double Value,
    double BasePrice,
    int Quantity,
    bool IsEnable,
    string UpdatedBy
    ) : ICommand<Guid>
{
}
