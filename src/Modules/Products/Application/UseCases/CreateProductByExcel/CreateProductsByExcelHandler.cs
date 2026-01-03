using BlockApplication.Contracts.CommandQuery;
using OfficeOpenXml;
using Product.Domain.Product;

namespace Product.Application.UseCases.CreateProductByExcel;

public class CreateProductsByExcelHandler : IHandler<CreateProductsByExcelCommand, Guid>
{
    private readonly IProductRepository _productRepository;

    public CreateProductsByExcelHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Guid> Handle(CreateProductsByExcelCommand request, CancellationToken cancellationToken)
    {
        using var stream = new MemoryStream();

        await request.ExcelFile.CopyToAsync(stream);
        stream.Position = 0;

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage(stream);

        var worksheet = package.Workbook.Worksheets[0];


        for (int row = 2; row <= worksheet.Cells.Last().End.Row; row++)
        {

            ProductAggregateRoot product = ProductAggregateRoot.NewEntity(
                worksheet.Cells[row, 1].Text,
                worksheet.Cells[row, 2].Text,
                (double)worksheet.Cells[row, 6].Value,
                (double)worksheet.Cells[row, 5].Value,
                Convert.ToInt32(worksheet.Cells[row, 4].Value)
                );

            List<ProductAggregateRoot> existsProduct = await _productRepository.GetByName(product.Name);

            if (existsProduct.Count > 0)
            {
                await _productRepository.Update(product);
            }
            else
            {
                _productRepository.Create(product);
            }
        }

        return Guid.NewGuid();
    }
}
