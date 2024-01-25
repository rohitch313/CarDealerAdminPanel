namespace Admin.UI.Models
{
    public class CarDto
    {
        public int CarId { get; set; }

        public string CarName { get; set; } = null!;

        public string Variant { get; set; } = null!;

        public string? Image { get; set; }

        public int? UserId { get; set; }

    }
}
