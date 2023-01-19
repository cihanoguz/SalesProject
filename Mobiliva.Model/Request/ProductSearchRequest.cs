using System;
namespace Mobiliva.Model.Request
{
    public class ProductSearchRequest
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        public float MinUnitPrice { get; set; }

        public float MaxUnitPrice { get; set; }

        public int Skip { get; set; }

        public int PageDataCount { get; set; }

    }
}

