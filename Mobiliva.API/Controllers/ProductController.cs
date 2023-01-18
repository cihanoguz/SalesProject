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
        public ActionResult<BaseResponse<List<ProductDto>>> GetProduct([FromBody] ProductSearchRequest request)
        {
            ProductSearchRequest request1 = new ProductSearchRequest();
            var response = _productService.GetProduct(request1);

            return response;
        }

        [HttpPost]
        public ActionResult<BaseResponse<bool>> CreateOrder(CreateOrderRequest request)
        {
            var response = new BaseResponse<bool>();

            try
            {
                //Product dto = new Product();
                //dto.Category = "test";
                //dto.Description = "test";
                //dto.Unit = 1;
                //dto.UnitPrice = 14;


                //_productRepository.Add(dto);

                queue mail = new queue();
                /*    mail.Connect();
                    mail.DeclareExchange();
                    mail.DeclareQueue();
                    mail.BindQueue();
                    mail.Publish(); */
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
               /* Product dto = new Product();
                dto.Category = "test";
                dto.Description = "test";
                dto.Unit = 1;
                dto.UnitPrice = 14;
               */


               // _productRepository.Add(dto);

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }


            return response;
        }
    }
}

