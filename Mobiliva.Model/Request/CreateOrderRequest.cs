using System;
namespace Mobiliva.Model.Request
{
	public class CreateOrderRequest
	{
        public CreateOrderRequest()
        {
            ProductDetails = new List<ProductDetail>();
        }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerGSM { get; set; }

        public List<ProductDetail> ProductDetails { get; set; }
    }

    public class ProductDetail
    {
        public long ProductId { get; set; }

        public float Unit { get; set; }

        public float UnitPrice { get; set; }

    }
}

