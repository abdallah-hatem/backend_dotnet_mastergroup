namespace backend_dotnet.Models
{
    public class CountryCategoryPrice
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }

        // Navigation properties
        public Country? Country { get; set; }
        public Category? Category { get; set; }
    }
} 