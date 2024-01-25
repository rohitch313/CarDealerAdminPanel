namespace Admin.UI.Models
{
    public class CustomerSupportDto
    {
        public int Id { get; set; }

        public string Call { get; set; } = null!;

        public string WhatsApp { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
