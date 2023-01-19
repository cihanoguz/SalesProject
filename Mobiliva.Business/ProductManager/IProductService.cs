using System;
using Mobiliva.Model.Dto;
using Mobiliva.Model.Request;
using Mobiliva.Model.Response;
using Mobiliva.Model;

namespace Mobiliva.Business.ProductManager
{
	public interface IProductService
	{
        BaseResponse<List<ProductDto>> GetProductList(ProductSearchRequest request);

    }
}

