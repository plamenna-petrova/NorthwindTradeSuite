﻿using NorthwindTradeSuite.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Domain.Entities
{
    public class OrderDetails : BaseEntity
    {
        public string OrderID { get; set; }

        public virtual Order Order { get; set; }

        public string ProductID { get; set; }

        public virtual Product Product { get; set; }

        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }
    }
}
