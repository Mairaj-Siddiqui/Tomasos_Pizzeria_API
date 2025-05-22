namespace TomasosPizzeriaAPI.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation
        public List<Dish> Dishes { get; set; } = new();
    }
}
