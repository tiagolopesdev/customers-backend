
namespace Customers.Domain.SeedWork
{
    public class Pagination<T> where T : Entity
    {
        public bool HasMore { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItens { get; set; }
        public required List<T> Data {  get; set; }

        public static Pagination<T> NewPagination(double totalItens, List<T> data, double pageIndex, int pageSize)
        {
            var dataToReturn = new Pagination<T>()
            {
                TotalItens = (int)totalItens,
                Data = data,
                TotalPages = (int)Math.Ceiling(totalItens / pageSize),
                PageIndex = (int)pageIndex,
                PageSize = pageSize
            };

            dataToReturn.HasMore = dataToReturn.PageIndex < dataToReturn.TotalPages;

            return dataToReturn;
        }
    }
}
