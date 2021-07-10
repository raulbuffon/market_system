using Market_system.Models;
using Market_system.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Market_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ProductService _productService;

        public ProductsController(ProductService productService, ILogger<ProductsController> logger)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            _logger.LogInformation("Call controller - Get all products");
            return _productService.GetAll();
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public ActionResult<Product> GetById(string id)
        {
            _logger.LogInformation("Call controller - Get product by Id");
            var product = _productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            _logger.LogInformation("Call controller - Post new product");
            _productService.Create(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id.ToString() }, product);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Product productIn)
        {
            _logger.LogInformation("Call controller - Put to update a product");
            var product = _productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.Update(id, productIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            _logger.LogInformation("Call controller - Delete product by Id");
            var product = _productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.RemoveById(product.Id);

            return NoContent();
        }
    }
}