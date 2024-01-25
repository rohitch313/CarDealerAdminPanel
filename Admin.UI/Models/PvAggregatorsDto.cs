namespace Admin.UI.Models
{
    public class PvAggregatorsDto
    {
        public int Id { get; set; }

        public string PurchaseAmount { get; set; } = null!;

        public int MakeId { get; set; }

        public int ModelId { get; set; }

        public int YearOfRegistration { get; set; }

        public int VariantId { get; set; }

        public string PriceBreak { get; set; } = null!;

        public string StockIn { get; set; } = null!;

        public string Rcavailable { get; set; } = null!;

        public int UserInfoId { get; set; }
    }
}
