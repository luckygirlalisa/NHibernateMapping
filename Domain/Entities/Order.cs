namespace Domain.Entities
{
    public class Order
    {
        public virtual int OrderId { get; set; }
        public virtual int Version { get; set; }
        public virtual string OrderDate { get; set; }
        public virtual int CustomerId { get; set; }

//        public virtual Customer Customer { get; set; }
    }
}
    