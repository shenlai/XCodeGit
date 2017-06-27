using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XCode.Application;
using XCode.DTO;

namespace XCode.WebApi.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {

        private readonly IProductService productService;

        public ProductController(IProductService productservice)
        {
            this.productService = productservice;
        }
        
        [HttpPost]
        [Route("productlist")]
        public Response<List<ProductDto>> GetProducts()
        {
            var res = new Response<List<ProductDto>>() { Head = new ResponseHead() { IsSuccess = true } };
            var list = productService.GetProducts();
            res.Content = list;
            return res;
        }

    }
}
