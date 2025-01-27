
using Customers.Domain.AggregatesModel.Products;
using Customers.Domain.Interfaces;
using MediatR;
using OfficeOpenXml;

namespace Customers.Application.UseCases.ProductUseCases.CreateProductsByExcel
{
    public class CreateProductsByExcelHandler : IRequestHandler<CreateProductsByExcelRequest, string>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductsByExcelHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<string> Handle(CreateProductsByExcelRequest request, CancellationToken cancellationToken)
        {

            using var stream = new MemoryStream();

            await request.ExcelFile.CopyToAsync(stream);
            stream.Position = 0;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(stream);

            var worksheet = package.Workbook.Worksheets[0];

            int length = Convert.ToInt32(worksheet.LastValueCell.Address.Replace("F", ""));

            for (int row = 2; row <= length; row++)
            {

                Product product = Product.NewEntity(
                    worksheet.Cells[row, 1].Text,
                    worksheet.Cells[row, 2].Text,
                    (double)worksheet.Cells[row, 6].Value,
                    (double)worksheet.Cells[row, 5].Value,
                    Convert.ToInt32(worksheet.Cells[row, 4].Value)
                    );

                List<Product> existsProduct = await _productRepository.GetByName(product.Name);

                if (existsProduct.Count > 0)
                {
                    await _productRepository.Update(product);
                }
                else
                {
                    _productRepository.Create(product);
                }
            }


            throw new NotImplementedException();
        }
    }
}
