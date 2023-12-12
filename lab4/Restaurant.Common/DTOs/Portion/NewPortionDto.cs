namespace Restaurant.Common.DTOs.Portion
{
    public class NewPortionDto
    {
        public int DishId { get; set; }

        public int UnitId { get; set; }

        public decimal Amount { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
