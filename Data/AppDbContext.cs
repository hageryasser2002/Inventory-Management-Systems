using InventorySystem.Models;
using InventorySystem.Models.Reports;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }


        public DbSet<Product> products { get; set; }
        public DbSet<Inventory> inventory { get; set; }
        public DbSet<Warehouse> warehouses { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<ApplicationUser> users { get; set; }
        public DbSet<TransactionHistoryReport> transactionHistoryReports { get; set; }
        public DbSet<LowStockReport> lowStockReports { get; set; }
        public DbSet<ArchieveTransaction>  archieveTransactions { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
