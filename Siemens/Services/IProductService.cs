using Siemens.Models;

namespace Siemens.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();

        Product? GetById(int id);

        Product Add(Product product);

        bool Update(int id, Product product);
    }
}
