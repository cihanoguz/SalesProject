using System;
namespace Mobiliva.Model.Dto
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public float Unit { get; set; }
        public float UnitPrice { get; set; }
    }
}

