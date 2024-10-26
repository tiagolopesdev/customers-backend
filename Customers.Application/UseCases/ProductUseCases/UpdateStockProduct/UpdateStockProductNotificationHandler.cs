using Customers.Domain.Interfaces;
using MediatR;

namespace Customers.Application.UseCases.ProductUseCases.UpdateStockProduct
{
    public class UpdateStockProductNotificationHandler : INotificationHandler<UpdateStockProductNotification>
    {
        public readonly IProductRepository _productRepository;

        public UpdateStockProductNotificationHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(UpdateStockProductNotification notification, CancellationToken cancellationToken)
        {
            foreach (var product in notification.Products)
            {
                var result = await _productRepository.GetById(product.ProductId);

                if (result == null) continue;

                result.Quantity -= product.Quantity;

                await _productRepository.Update(result);
            }
            return;
        }
    }
}
