using InventorySystem.DTOs;
using InventorySystem.Models;
using InventorySystem.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public WarehouseController(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        [HttpPost("Add")]
        public IActionResult AddWarehouse(WarehouseDTO warehouse)
        {
            _warehouseRepository.Add(warehouse);
            _warehouseRepository.Save();
            return Ok("Warehouse added successfully");
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateWarehouse(int id, WarehouseDTO updatedWarehouse)
        {
            var existing = _warehouseRepository.GetById(id);
            if (existing == null)
                return NotFound("Warehouse not found");

            existing.Name = updatedWarehouse.Name;
            existing.Location = updatedWarehouse.Location;
            existing.Manager = updatedWarehouse.Manager;

            _warehouseRepository.Update(existing);
            _warehouseRepository.Save();

            return Ok("Warehouse updated successfully");
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteWarehouse(int id)
        {
            var existing = _warehouseRepository.GetById(id);
            if (existing == null)
                return NotFound("Warehouse not found");

            _warehouseRepository.Delete(id);
            _warehouseRepository.Save();

            return Ok("Warehouse deleted successfully");
        }

        [HttpGet("Details/{id}")]
        public ActionResult<Product> GetWarehouseDetails(int id)
        {
            var warehouse = _warehouseRepository.GetById(id);
            if (warehouse == null)
                return NotFound("Warehouuse not found");

            return Ok(warehouse);
        }

        [HttpGet("All")]
        public ActionResult<List<Warehouse>> GetAllWarehouses()
        {
            return Ok(_warehouseRepository.GetAll());
        }
    }
}
