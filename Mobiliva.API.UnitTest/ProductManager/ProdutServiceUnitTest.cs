using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Mobiliva.API.Controllers;
using Mobiliva.Business.Cache;
using Mobiliva.Business.ProductManager;
using Mobiliva.Model.Request;
using Mobiliva.Repository.Customers.Repository;
using Moq;
using Mobiliva.Repository.Products.Interface;
using Microsoft.AspNetCore.Http;

namespace Mobiliva.API.UnitTest.ProductManager
{

    public class ProdutServiceUnitTest
    {
        private Mock<IProductService> productService;
        private Mock<ILogger<ProductController>> logger ;


        public ProdutServiceUnitTest()
        {
            this.productService = new Mock<IProductService>();
            this.logger = new Mock<ILogger<ProductController>>();
        }


        //[SetUp]
        //public void Setup()
        //{
        //}

        [Test]
        public void GetProduct_ExpectedResult_Conditation1()
        {
            // arrange
            ProductController service = new ProductController(logger.Object, productService.Object );
            ProductSearchRequest request = new ProductSearchRequest();
            

            //act
            var response = service.GetProduct(request);

            //Assert
            Assert.AreEqual(response.Value.HasError, false);
        }
    }
}

