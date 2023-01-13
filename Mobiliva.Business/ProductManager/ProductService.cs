using System;
using Mobiliva.Model.Dto;
using Mobiliva.Model.Request;
using AutoMapper;
using Mobiliva.Model;
using Mobiliva.Model.Response;
using Mobiliva.Repository.Products.Interface;
using Mobiliva.Core.Entity;
using Mobiliva.DAL.Entities.Orders;
using Mobiliva.DAL.Entities.Products;
using Mobiliva.Business.Cache;
using Mobiliva.Repository.Customers.Repository;
using Microsoft.Extensions.Logging;

namespace Mobiliva.Business.ProductManager
{
	public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly ICacheManager _cacheService;

        public ProductService(IProductRepository productRepository,
            ILogger<ProductService> logger,
            IMapper mapper ,
            ICacheManager cacheService
           )
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public BaseResponse<List<ProductDto>> GetProduct(ProductSearchRequest request)
        {

            var response = new BaseResponse<List<ProductDto>>();
            response.Data = new List<ProductDto>();
          
            try
            {
                var query = GetProductsFromCache();
                var query1 = _productRepository.GetBy(x => x.RecordStatus == Mobiliva.Core.Enums.Enums.RecordStatus.Active);

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

                var data = PagedList<Object>.ToPagedList(query, request.Skip, request.PageDataCount);
                response.Data = _mapper.Map<List<ProductDto>>(data);

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

        private IQueryable<Mobiliva.DAL.Entities.Products.Product>? GetProductsFromCache()
        {
            if (!_cacheService.IsAdd("product"))
            {
                _cacheService.Add("product", _productRepository.GetBy(x => x.RecordStatus == Mobiliva.Core.Enums.Enums.RecordStatus.Active), 1);
            }

            IQueryable<Mobiliva.DAL.Entities.Products.Product>? retval = (IQueryable<DAL.Entities.Products.Product>?)_cacheService.Get("product");
            return retval;

        }

     


        public class PagedList<T> : List<T>
        {
            public int CurrentPage { get; private set; }
            public int TotalPages { get; private set; }
            public int PageSize { get; private set; }
            public int TotalCount { get; private set; }
          
            public PagedList(List<T> items, int count, int pageNumber, int pageSize)
            {
                TotalCount = count;
                PageSize = pageSize;
                CurrentPage = pageNumber;
                TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                AddRange(items);
            }
            public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
            {
                pageSize = pageSize <= 1 ? 1 : pageSize;
                pageNumber = pageNumber < 1 ? 1 : pageNumber;

                var count = source.Count();
                var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return new PagedList<T>(items, count, pageNumber, pageSize);
            }
        }
    }
}

