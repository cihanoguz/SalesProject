using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using Mobiliva.Model.Dto;
using Mobiliva.Model.Request;
using Mobiliva.Model.Response;
using AutoMapper;
using Microsoft.VisualBasic;
using Mobiliva.Model;
using Newtonsoft.Json;
using Mobiliva.Repository.Products.Interface;

namespace Extensions.Cache
{
    public class CacheHelper
    {
        ICache cache;
   //     private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CacheHelper(ICache cache,// IProductRepository productRepository,
            IMapper mapper)
        {
            this.cache = cache;
      //      _productRepository = productRepository;
            _mapper = mapper;
        }

        private string Products_CacheKey = "Products_CacheKey";
        public bool ProductsClear() { return Clear(Products_CacheKey); }
        public List<ProductDto> Products
        {
            get
            {
                var fromCache = Get<List<ProductDto>>(Products_CacheKey);
                if (fromCache == null)
                {
                    var datas = GetProducts().Result;//new List<ProductDto>();//= _productRepository.GetAll();
                    if (datas != null  && datas.Count() > 0)
                    {
                        Set(Products_CacheKey, datas);
                        fromCache = datas;
                    }
                }

                return fromCache;
            }
        }


        public bool Clear(string name)
        {
            cache.Remove(name);
            return true;
        }

        public T Get<T>(string cacheKey) where T : class
        {
            object cookies;

            if (!cache.TryGetValue(cacheKey, out cookies))
                return null;

            return cookies as T;
        }

        public void Set(string cacheKey, object value)
        {
            cache.Set(cacheKey, value, 180);
        }
        private async Task<List<ProductDto>> GetProducts()
        {
            HttpClient client = new HttpClient();

            ProductSearchRequest request = new ProductSearchRequest();
            var uri = new Uri("https://localhost:15165/api/Product/GetProduct");
            string json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var data = new BaseResponse<List<ProductDto>>();

            try
            {
                HttpResponseMessage response =  await client.PostAsync(uri, content);
                HttpStatusCode code = response.StatusCode;
                if (code == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<BaseResponse< List<ProductDto>>> (result);
                  
                    if (data.HasError)
                    {
                        
                    }
                   
                }
            }
            catch (Exception ex)
            {
               
            }
            return data.Data;
        }
    }
}

