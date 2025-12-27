using Microsoft.AspNetCore.Mvc;
using Moq;
using Siemens.Controllers;
using Siemens.Models;
using Siemens.Services;
using Xunit;

namespace InventoryApi.Tests
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _mockService;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductsController(_mockService.Object);
        }

        /// Verifies that GetAll returns an OK response with a list of products.
        [Fact]
        public void GetAll_ReturnsOk_WithList()
        {
            _mockService.Setup(s => s.GetAll())
                .Returns(new List<Product> { new Product { Name = "Test", Price = 10, StockQuantity = 5 } });

            var result = _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Single(list);
        }

        /// Verifies that Create returns BadRequest when a product has a negative price.
        [Fact]
        public void Create_ReturnsBadRequest_ForNegativePrice()
        {
            
            var result = _controller.Create(new Product { Name = "Bad", Price = -10, StockQuantity = 10 });

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}