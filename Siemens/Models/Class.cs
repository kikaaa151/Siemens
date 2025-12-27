namespace Siemens.Models
{
    /// Represents a product in the inventory system.
    public class Product
    {
        /// Unique identifier for the product.
        public int Id { get; set; }

        /// The name of the product .
        public required string Name { get; set; }

        /// The price of the product.
        public decimal Price { get; set; }

        /// The current quantity of the product in stock.
        public int StockQuantity { get; set; }
    }
}
