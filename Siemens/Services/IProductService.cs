using Siemens.Models;

namespace Siemens.Services
{
    /// Service interface for managing product data operations.
    public interface IProductService
    {
        /// Retrieves all products from inventory.
        IEnumerable<Product> GetAll();

        /// Retrieves a product by its unique identifier.
        Product? GetById(int id);

        /// Adds a new product to the inventory.
        Product Add(Product product);

        /// Updates an existing product by its identifier.
        bool Update(int id, Product product);
    }
}
