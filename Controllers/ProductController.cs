using InventorySystem.Data;
using InventorySystem.DTOs;
using InventorySystem.Models;
using InventorySystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost("Add")]
        public IActionResult AddProduct(ProductDTO product)
        {
            _productRepository.Add(product);
            _productRepository.Save();
            return Ok("Product added successfully");
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateProduct(int id, ProductDTO updatedProduct)
        {
            var existing = _productRepository.GetById(id);
            if (existing == null)
                return NotFound("Product not found");

            existing.Name = updatedProduct.Name;
            existing.Description = updatedProduct.Description;
            existing.Quantity = updatedProduct.Quantity;
            existing.Price = updatedProduct.Price;
            existing.LowStockThreshold = updatedProduct.LowStockThreshold;

            _productRepository.Update(existing);
            _productRepository.Save();

            return Ok("Product updated successfully");
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var existing = _productRepository.GetById(id);
            if (existing == null)
                return NotFound("Product not found");

            _productRepository.Delete(id);
            _productRepository.Save();

            return Ok("Product deleted successfully");
        }

        [HttpGet("Details/{id}")]
        public ActionResult<Product> GetProductDetails(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }

        [HttpGet("All")]
        public ActionResult<List<Product>> GetAllProducts()
        {
            return Ok(_productRepository.GetAll());
        }

    }
}
