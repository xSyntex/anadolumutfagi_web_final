using Microsoft.EntityFrameworkCore;
using Web_Programlama_Proje.Models;

namespace Web_Programlama_Proje.Data
{
    // Veritabanı işlemlerini yöneten ana sınıf
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Veritabanındaki MenuItems tablosunu temsil eder
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
