using MediatR;
using Microsoft.AspNetCore.Http;

namespace Customers.Application.UseCases.ProductUseCases.CreateProductsByExcel
{
    public class CreateProductsByExcelRequest : IRequest<string>
    {
        public IFormFile ExcelFile { get; set; }
    }
}
