namespace ProductsRepository.DataModels
{
    public class Product
    { 
            public int Id { get; set; }
            public int CategoryId { get; set; }
            public Category? Category { get; set; }
            public string Name { get; set; } = string.Empty;
            public string ProductCode { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int StockQuantity { get; set; }
            public DateTime DateAdded { get; set; }
    }
}
