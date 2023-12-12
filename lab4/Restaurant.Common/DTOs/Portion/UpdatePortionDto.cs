namespace Restaurant.Common.DTOs.Portion
{
    public class UpdatePortionDto
    {
        public decimal Amount { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
