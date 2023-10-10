using Inventory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // InventoryItems.
        public DbSet<InventoryItem> Inventory { get; set; }
        public DbSet<LogEntry> Log { get; set; }

        public DbSet<IdentityUser> AspNetUsers { get; set; }
    }
}
