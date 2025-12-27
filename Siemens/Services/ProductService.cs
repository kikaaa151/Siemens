using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using Siemens.Models;

namespace Siemens.Services
{
    /// Service implementation for managing products using a JSON file as storage.
    public class ProductService : IProductService
    {
        // File path used to persist product data.
        private readonly string _filePath = "inventory.json";

        /// Ensures the backing JSON file exists when the service is created.
        public ProductService()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        /// Reads product list from the JSON file.
        private List<Product> ReadData()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        /// Serializes and writes product list to the JSON file.
        private void SaveData(List<Product> products)
        {
            var json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        /// Returns all products from storage.
        public IEnumerable<Product> GetAll() => ReadData();

        /// Finds a product by id or returns null when not found.
        public Product? GetById(int id)
        {
            return ReadData().FirstOrDefault(p => p.Id == id);
        }

        /// Adds a product, assigns an auto-incremented id, persists and returns it.
        public Product Add(Product product)
        {
            var products = ReadData();
            product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);
            SaveData(products);
            return product;
        }

        /// Updates an existing product; returns false when not found.
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
