using Microsoft.EntityFrameworkCore;


namespace Order.Repository.Context
{
    public class OrderDBContext  : DbContext
    {
        public OrderDBContext(DbContextOptions<OrderDBContext> options) : base(options) { }

        public DbSet<Order.Model.Entities.Order> Orders { get; set; }
        public DbSet<Order.Model.Entities.OrderItem> OrderItems { get; set; }
        public DbSet<Order.Model.Entities.Customer> Customers { get; set; }
        public DbSet<Order.Model.Entities.Product> Products { get; set; }
        public DbSet<Order.Model.Entities.ProductPricing> ProductPricings { get; set; }
        
        public DbSet<Order.Model.Entities.Invoice> Invoices { get; set; }
        public DbSet<Order.Model.Entities.OrderStatus> orderStatuses { get; set; }
        public DbSet<Order.Model.Entities.ProductCategory> ProductCategories { get; set; }
        public DbSet<Order.Model.Entities.RefreshLog> RefreshLogs { get; set; }

    }
}
