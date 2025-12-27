using Microsoft.AspNetCore.Http;
using Siemens.Models;
using Siemens.Services;
using Microsoft.AspNetCore.Mvc;

namespace Siemens.Controllers
{
    /// API controller for managing product operations.
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        /// Initializes a new instance of the ProductsController with the provided service.
        public ProductsController(IProductService service)
        {
            _service = service;
        }

        /// Retrieves all products.
        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        /// Retrieves a specific product by id.
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetById(id);
            return product == null ? NotFound() : Ok(product);
        }

        /// Creates a new product after basic validation.
        [HttpPost]
        public IActionResult Create(Product product)
        {
            // Validate that price and stock quantity are not negative.
            if (product.Price < 0 || product.StockQuantity < 0)
                return BadRequest("Price and Stock Quantity cannot be negative.");

            var created = _service.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// Updates an existing product after validation.
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            // Validate that price and stock quantity are not negative.
            if (product.Price < 0 || product.StockQuantity < 0)
                return BadRequest("Cannot allow negative values.");

            // Return 404 if the product doesn't exist.
            if (!_service.Update(id, product))
                return NotFound();

            return NoContent();
        }
    }
}
