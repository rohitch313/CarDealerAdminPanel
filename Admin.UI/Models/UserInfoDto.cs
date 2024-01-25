namespace Admin.UI.Models
{
    public class UserInfoDto
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? UserEmail { get; set; }

        public int? Sid { get; set; }

        public string Phone { get; set; } = null!;

        public bool Active { get; set; }
        public bool Rejected { get; set; }

    }
}
