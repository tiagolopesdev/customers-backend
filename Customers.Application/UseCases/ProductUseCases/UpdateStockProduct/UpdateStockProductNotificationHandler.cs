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
            for (int i = 0; i < notification.Name.Count; i++)
            {
                var result = await _productRepository.GetByName(notification.Name[i]);

                if (result[0] == null) continue;

                result[0].Quantity -= notification.Quantity[i];

                await _productRepository.Update(result[0]);
            }

            return;
        }
    }
}
