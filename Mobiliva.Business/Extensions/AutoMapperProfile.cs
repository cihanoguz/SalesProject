using System;
using AutoMapper;
using Mobiliva.DAL.Entities.Products;
using Mobiliva.Model.Dto;

namespace Mobiliva.Business.Extensions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}

