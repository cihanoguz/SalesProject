using System;
using Mobiliva.Business.ProductManager;
using Mobiliva.Model.Dto;
using Mobiliva.Model.Request;
using Mobiliva.API.Services.Rabbitmq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mobiliva.Model.Response;
using Mobiliva.API.Code;

namespace Mobiliva.API.Controllers
{
    public class ProductController : BaseController<ProductController>
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger,
            IProductService productService) 
        {
            _logger = logger;
            _productService = productService;
        }


        [HttpPost]
        public ActionResult<BaseResponse<List<ProductDto>>> GetProductList([FromBody] ProductSearchRequest request)
        {
            ProductSearchRequest request1 = new ProductSearchRequest();
            var response = _productService.GetProductList(request1);

            return response;
        }

        [HttpPost]
        public ActionResult<BaseResponse<bool>> CreateOrder(CreateOrderRequest request)
        {
            var response = new BaseResponse<bool>();

            try
            {
                Queue mail = new Queue();
            
                mail.TestRabbitmq();

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }


            return response;
        }
        [HttpGet]
        public ActionResult<BaseResponse<bool>> AddProduct() {
            var response = new BaseResponse<bool>();

            try
            {
             

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }


            return response;
        }
    }
}

