namespace NorthwindTradeSuite.Domain.Entities
{
    public class OrderDetails
    {
        public string OrderId { get; set; } = null!;

        public virtual Order Order { get; set; } = null!;

        public string ProductId { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;

        public decimal UnitPrice { get; set; } 

        public short Quantity { get; set; }

        public float Discount { get; set; }
    }
}
