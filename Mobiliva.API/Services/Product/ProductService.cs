using System;
using Mobiliva.API.Controllers;
using Mobiliva.Model.Dto;
using Mobiliva.Model.Request;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mobiliva.Model;
using Mobiliva.Model.Response;
using Mobiliva.Repository.Products.Interface;

namespace Mobiliva.API.Services.Product
{
	public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,
            ILogger<ProductController> logger,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public BaseResponse<List<ProductDto>> GetProduct(ProductSearchRequest request)
        {

            var response = new BaseResponse<List<ProductDto>>();
            response.Data = new List<ProductDto>();
            if (request.PageDataCount <= 1)
            {
                request.PageDataCount = 1;
            }
            try
            {
                var query = _productRepository.GetBy(x => x.RecordStatus == Mobiliva.Core.Enums.Enums.RecordStatus.Active);

                if (request.Skip < 1)
                {
                    request.Skip = 1;
                }

                if (!string.IsNullOrWhiteSpace(request.Description))
                {
                    query = query.Where(x => x.Description == request.Description);
                }
                if (!string.IsNullOrWhiteSpace(request.Category))
                {
                    query = query.Where(x => x.Category == request.Category);
                }
                if (request.MaxUnitPrice > 0 || request.MinUnitPrice > 0)
                {
                    //query = query.Where(x => (request.MaxUnitPrice > 0 ? x.UnitPrice < request.MaxUnitPrice));
                }
                var q2 = query.ToList();
                int count = query.Count() % request.PageDataCount == 0 ? (query.Count() / request.PageDataCount) : (query.Count() / request.PageDataCount) + 1;
                response.Data = query
                    .Skip((request.Skip - 1) * 2)
                    .Take(2)
                    .Select(x => _mapper.Map<ProductDto>(x))
                    .ToList();
                //new ProductDto
                //{
                //    Id = x.Id,
                //    Description = x.Description,
                //    Category = x.Category,
                //    Unit = x.Unit,
                //    ///...

                //}).ToList();



                _logger.LogInformation("Seri köz getir");
                // _logger.Log(LogLevel.Information)

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                response.SetMessage(ex.Message);
            }

            return response;
        }
    }
}

