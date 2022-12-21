using System;
using Mobiliva.Core.Entity;

namespace Mobiliva.DAL.Entities.Orders
{
    public class OrderDetail : BaseEntity
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public float UnitPrice { get; set; }


        //relations
        public virtual Order Order { get; set; }
    }
}

