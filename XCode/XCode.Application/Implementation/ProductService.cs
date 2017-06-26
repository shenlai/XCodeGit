using EmitMapper;
using System.Collections.Generic;
using XCode.Domain;
using XCode.Domain.IRepositories;
using XCode.DTO;

namespace XCode.Application.Implementation
{
    
    public class ProductService:IProductService
    {
        private readonly IProductRepository productRepository;


        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public List<ProductDto> GetProducts()
        {
            var entities = productRepository.GetEntityList(null);

            var list = entities.PageData;

            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<List<Product>, List<ProductDto>>();

            return mapper.Map(list);

        }
    }
}
