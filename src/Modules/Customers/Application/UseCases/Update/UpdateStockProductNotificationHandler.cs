
using BlockApplication.Contracts.Notification;
using MediatR;

namespace Application.UseCases.Update
{
    public class UpdateStockProductNotificationHandler : INotificationEventHandler<UpdateStockProductNotification>
    {

        public Task Handle(UpdateStockProductNotification notification, CancellationToken cancellationToken)
        {

            IEnumerable<UpdateBuyRequest> buyWithProducts = notification.Buy.Where(filter => filter.ProductId != Guid.Empty);

            foreach (UpdateBuyRequest request in buyWithProducts)
            {
                // Publica evento no service bus
            }

            

            //var test = new UpdateStockProductNotification(
            //products.Select(element => new UpdateStockItemModel(
            //                element.ProductId,
            //                element.Quantity
            //                )
            //            )
            //            .ToList()
            //);

            //foreach (var product in notification.Products)
            //{
            //    var result = await _productRepository.GetById(product.ProductId);

            //    if (result == null) continue;

            //    result.Quantity -= product.Quantity;
            //    result.QuantitySold += product.Quantity;

            //    await _productRepository.Update(result);
            //}
            //return;

            /*
             TODO: 
                - Salvar no banco de dados
                - Enviar mensagem para o service bus *            
             */

            throw new NotImplementedException();
        }
    }
}
