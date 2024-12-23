using MediatR;

namespace Customers.Application.UseCases.ProductUseCases.UpdateProduct
{
    public sealed record class UpdateProductRequest(
        Guid Id, string Name, string Description, 
        double Value, double BasePrice, int Quantity, 
        bool IsEnable, string UpdatedBy) : IRequest<Guid>
    {
    }
}
