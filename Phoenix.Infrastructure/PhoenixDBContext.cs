using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Phoenix.Domain;

namespace Phoenix.Infrastructure
{
    public class PhoenixDBContext: DbContext
    {
        public PhoenixDBContext(DbContextOptions<PhoenixDBContext> options) : base(options)
        {
        }

        
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<CustomerAddress> CustomerAddresses { get; set; } = null!;
        public DbSet<CustomerItemReference> CustomerItemReferences { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<ItemWarehouse> ItemWarehouses { get; set; } = null!;
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<NumberSeries> NumberSeries { get; set; } = null!;
        public DbSet<PurchaseParameters> PurchaseParameters { get; set; } = null!;
        public DbSet<SalesDelivery> SalesDeliveries { get; set; } = null!;
        public DbSet<SalesDeliveryLine> SalesDeliveryLines { get; set; } = null!;
        public DbSet<SalesInvoice> SalesInvoices { get; set; } = null!;
        public DbSet<SalesInvoiceLine> SalesInvoiceLines { get; set; } = null!;
        public DbSet<SalesOrder> SalesOrders { get; set; } = null!;
        public DbSet<SalesOrderLine> SalesOrderLines { get; set; } = null!;
        public DbSet<SalesParameters> SalesParameters { get; set; } = null!;
        public DbSet<Supplier> Suppliers { get; set; } = null!;
        public DbSet<Warehouse> Warehouses { get; set; } = null!;

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                                       e.State == EntityState.Added
                                                              || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).ModifiedOn = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedOn = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(e => e.CustomerAddresses)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.CustomerId)
                .HasPrincipalKey(e => e.Id);
            modelBuilder.Entity<Warehouse>()
                .HasOne(e => e.Country)
                .WithMany(e => e.Warehouses)
                .HasForeignKey(e => e.CountryId)
                .HasPrincipalKey(e => e.Id);
        }
    }
}
