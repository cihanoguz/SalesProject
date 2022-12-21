using System;
using Mobiliva.API.Code;
using Mobiliva.Model.Dto;
using Mobiliva.Model.Request;
using Mobiliva.API.Services.Product;
using Mobiliva.API.Services.Rabbitmq;
using AutoMapper;
using Mobiliva.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mobiliva.Model;
using Mobiliva.Model.Request;
using Mobiliva.Model.Response;
using Mobiliva.Repository.Products.Interface;

namespace Mobiliva.API.Controllers
{
    public class ProductController : BaseController<ProductController>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(IProductRepository productRepository,
            ILogger<ProductController> logger,
            IMapper mapper,
            IProductService productService) 
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
            _productService = productService;
        }


        [HttpPost]
        public ActionResult<BaseResponse<List<ProductDto>>> GetProduct([FromBody] ProductSearchRequest request)
        {
            var response = _productService.GetProduct(request);

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

                que mail = new que();
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
                Product dto = new Product();
                dto.Category = "test";
                dto.Description = "test";
                dto.Unit = 1;
                dto.UnitPrice = 14;


                _productRepository.Add(dto);

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }


            return response;
        }
    }
}

