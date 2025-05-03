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
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IInventoryTransactionRepository _transactionRepository;

        public InventoryController(IInventoryRepository inventoryRepository, IInventoryTransactionRepository transactionRepository)
        {
            _inventoryRepository = inventoryRepository;
            _transactionRepository = transactionRepository;
        }

        [HttpPost("Add")]
        public IActionResult AddStock(int productId, int warehouseId, int quantity)
        {
            if (quantity <= 0)
                return BadRequest("Quantity must be positive");

            _inventoryRepository.AddOrUpdateInventory(productId, warehouseId, quantity);

            var transaction = new InventoryTransaction
            {
                ProductId = productId,
                FromWarehouseId = warehouseId,
                TransactionType = TransactionType.AddStock,
                Quantity = quantity,
                UserName = "admin"
            };
            _transactionRepository.Add(transaction);
            _transactionRepository.Save();

            return Ok("Stock added successfully");
        }

        [HttpPost("Remove")]
        public IActionResult RemoveStock(int productId, int warehouseId, int quantity)
        {
            if (quantity <= 0)
                return BadRequest("Quantity must be positive");

            var inventory = _inventoryRepository.GetByProductAndWarehouse(productId, warehouseId);
            if (inventory == null || inventory.Quantity < quantity)
                return BadRequest("Not enough stock to remove");

            _inventoryRepository.AddOrUpdateInventory(productId, warehouseId, -quantity);

            if (inventory.Quantity - quantity <= 0)
            {
                _inventoryRepository.Delete(inventory.Id);  
            }

            var transaction = new InventoryTransaction
            {
                ProductId = productId,
                FromWarehouseId = warehouseId,
                TransactionType = TransactionType.RemoveStock,
                Quantity = quantity,
                UserName = "admin"
            };
            _transactionRepository.Add(transaction);
            _transactionRepository.Save();

            return Ok("Stock removed successfully");
        }

        [HttpPost("Transfer")]
        public IActionResult TransferStock(int productId, int fromWarehouseId, int toWarehouseId, int quantity)
        {
            if (quantity <= 0)
                return BadRequest("Quantity must be positive");

            var inventory = _inventoryRepository.GetByProductAndWarehouse(productId, fromWarehouseId);
            if (inventory == null || inventory.Quantity < quantity)
                return BadRequest("Not enough stock to transfer");

           
            _inventoryRepository.AddOrUpdateInventory(productId, fromWarehouseId, -quantity);
      
            _inventoryRepository.AddOrUpdateInventory(productId, toWarehouseId, quantity);

            var transaction = new InventoryTransaction
            {
                ProductId = productId,
                FromWarehouseId = fromWarehouseId,
                ToWarehouseId = toWarehouseId,
                TransactionType = TransactionType.TransferStock,
                Quantity = quantity,
                UserName = "admin"
            };
            _transactionRepository.Add(transaction);
            _transactionRepository.Save();

            return Ok("Stock transferred successfully");
        }

    }
}
