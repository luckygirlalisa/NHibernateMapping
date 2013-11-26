namespace Domain.Entities
{
    class Order
    {
        public int OrderId { get; set; }
        public int Version { get; set; }
        public string OrderDate { get; set; }
        public int CustomerId { get; set; }
    }
}
    