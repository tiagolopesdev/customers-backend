using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.CreateProductByExcel;

public sealed record class CreateProductsByExcelCommand (IFormFile ExcelFile) : IRequest<Guid>
{
  public Guid Id { get; set; }
}
