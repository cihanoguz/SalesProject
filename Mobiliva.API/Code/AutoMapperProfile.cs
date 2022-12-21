using System;
using Mobiliva.Model.Dto;
using AutoMapper;
using Mobiliva.DAL.Entities;

namespace Mobiliva.API.Code
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

