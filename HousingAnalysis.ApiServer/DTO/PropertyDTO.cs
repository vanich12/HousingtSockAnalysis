namespace HousingAnalysis.ApiServer.DTO
{
    public class PropertyDTO
    {
        public string PropertyId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string? ZipCode { get; set; }
        public string? Description { get; set; }
        public decimal? PricePerMonth { get; set; }
        public int? YearBuilt { get; set; }
    }

}
