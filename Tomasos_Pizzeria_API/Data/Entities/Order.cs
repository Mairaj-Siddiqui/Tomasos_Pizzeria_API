namespace TomasosPizzeriaAPI.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public List<OrderDish> OrderDishes { get; set; } = new();
    }
}
