using Microsoft.AspNetCore.Http;
using Siemens.Models;
using Siemens.Services;
using Microsoft.AspNetCore.Mvc;

namespace Siemens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetById(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (product.Price < 0 || product.StockQuantity < 0)
                return BadRequest("Price and Stock Quantity cannot be negative.");

            var created = _service.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            if (product.Price < 0 || product.StockQuantity < 0)
                return BadRequest("Cannot allow negative values.");

            // Return 404 if the product doesn't exist.
            if (!_service.Update(id, product))
                return NotFound();

            return NoContent();
        }
    }
}
