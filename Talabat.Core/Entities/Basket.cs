namespace Talabat.Core.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        // Constructor
        public CustomerBasket()
        {
            Id = Guid.NewGuid().ToString();
            CreatedOn = DateTime.UtcNow;
        }
        public CustomerBasket(string id)
        {
            Id = id;
            CreatedOn = DateTime.UtcNow;
        }
    }
}