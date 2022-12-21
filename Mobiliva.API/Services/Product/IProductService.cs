using System;
using Mobiliva.Model.Dto;
using Mobiliva.Model.Request;
using Mobiliva.Model.Response;
using Mobiliva.Model;

namespace Mobiliva.API.Services.Product
{
	public interface IProductService
	{
        BaseResponse<List<ProductDto>> GetProduct(ProductSearchRequest request);

    }
}

