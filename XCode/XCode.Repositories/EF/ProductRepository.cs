using System;

using XCode.Domain;
using XCode.Domain.IRepositories;

namespace XCode.Repositories.EF
{
    public class ProductRepository : EntityFrameworkRepository<Product>, IProductRepository
    {
        public ProductRepository(IRepositoryContext context)
            : base(context)
        {

        }
    }
}
