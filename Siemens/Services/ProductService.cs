using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using Siemens.Models;

namespace Siemens.Services
{
    public class ProductService : IProductService
    {
        private readonly string _filePath = "inventory.json";

        public ProductService()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        private List<Product> ReadData()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        private void SaveData(List<Product> products)
        {
            var json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public IEnumerable<Product> GetAll() => ReadData();

        public Product? GetById(int id)
        {
            return ReadData().FirstOrDefault(p => p.Id == id);
        }

        public Product Add(Product product)
        {
            var products = ReadData();
            product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);
            SaveData(products);
            return product;
        }

        public bool Update(int id, Product updatedProduct)
        {
            var products = ReadData();
            var existing = products.FirstOrDefault(p => p.Id == id);
            if (existing == null) return false;

            // Copy updated values onto the existing product object.
            existing.Name = updatedProduct.Name;
            existing.Price = updatedProduct.Price;
            existing.StockQuantity = updatedProduct.StockQuantity;

            SaveData(products);
            return true;
        }
    }
}
