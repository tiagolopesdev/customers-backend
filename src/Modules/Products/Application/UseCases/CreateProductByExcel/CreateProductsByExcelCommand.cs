using Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.CreateProductByExcel;

public sealed record class CreateProductsByExcelCommand (IFormFile ExcelFile) : ICommand<Guid>
{
  public Guid Id { get; set; }
}
