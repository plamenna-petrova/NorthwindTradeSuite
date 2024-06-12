namespace NorthwindTradeSuite.Domain.Entities
{
    public class OrderDetails
    {
        public string OrderId { get; set; }

        public virtual Order Order { get; set; }

        public string ProductId { get; set; }

        public virtual Product Product { get; set; }

        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }
    }
}
