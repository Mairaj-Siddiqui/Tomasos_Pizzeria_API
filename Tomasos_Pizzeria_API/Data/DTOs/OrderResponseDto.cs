namespace TomasosPizzeriaAPI.DTOs
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<string> Dishes { get; set; } = new();
    }
}
