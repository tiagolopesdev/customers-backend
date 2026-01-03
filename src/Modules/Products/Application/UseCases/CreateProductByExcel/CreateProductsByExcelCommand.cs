using BlockApplication.Contracts.CommandQuery;
using Microsoft.AspNetCore.Http;

namespace Product.Application.UseCases.CreateProductByExcel;

public sealed record class CreateProductsByExcelCommand (IFormFile ExcelFile) : ICommand<Guid>
{
  public Guid Id { get; set; }
}
