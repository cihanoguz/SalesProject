using System;
using Mobiliva.Core.Entity;

namespace Mobiliva.DAL.Entities.Products
{
    public class Product : BaseEntity
    {
        public string Description { get; set; }
        public string Category { get; set; }
        public float Unit { get; set; }
        public float UnitPrice { get; set; }
    }
}

