namespace TomasosPizzeriaAPI.DTOs
{
    public class CreateDishDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Ingredients { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}
