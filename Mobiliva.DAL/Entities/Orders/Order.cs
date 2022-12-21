using System;
using Mobiliva.Core.Entity;

namespace Mobiliva.DAL.Entities.Orders
{
    public class Order : BaseEntity
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerGSM { get; set; }
        public float TotalAmount { get; set; }

        //relations
        public virtual IList<OrderDetail> OrderDetails { get; set; }

    }
}

