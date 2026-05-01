
using Customers.Domain.SeedWork;

namespace Customers.Application.Shared.DTO
{
    public class PaginationDto<T> where T : Entity
    {
        public bool HasMore { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItens { get; set; }
        public List<T> Data { get; set; }
    }
}
