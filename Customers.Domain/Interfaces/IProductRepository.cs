﻿using Customers.Domain.AggregatesModel.Products;
using Customers.Domain.SeedWork;

namespace Customers.Domain.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<List<Product>> GetByName(string name);
    }
}
