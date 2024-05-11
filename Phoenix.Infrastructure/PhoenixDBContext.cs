using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Phoenix.Domain;
using System.Security.Cryptography.X509Certificates;

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
        public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public DbSet<PaymentTerm> PaymentTerms { get; set; } = null!;
        public DbSet<VATPostingGroup> VATPostingGroups { get; set; } = null!;
        public DbSet<CustomerPostingGroup> CustomerPostingGroups { get; set; } = null!;
        public DbSet<CustomerPriceGroup> CustomerPriceGroups { get; set; } = null!;

        public DbSet<SampleHeader> SampleHeader { get; set; }
        public DbSet<SampleDetail> SampleDetail { get; set; }


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
            modelBuilder.Entity<CustomerPostingGroup>()
                .HasMany(e => e.Customers)
                .WithOne(e => e.CustomerPostingGroup)
                .HasForeignKey(e => e.CustomerPostingGroupID)
                .HasPrincipalKey(e => e.Id);
            modelBuilder.Entity<CustomerPriceGroup>()
                .HasMany(e => e.Customers)
                .WithOne(e => e.CustomerPriceGroup)
                .HasForeignKey(e => e.CustomerPriceGroupID)
                .HasPrincipalKey(e => e.Id);
            modelBuilder.Entity<PaymentMethod>()
                .HasMany(e => e.Customers)
                .WithOne(e => e.PaymentMethod)
                .HasForeignKey(e => e.PaymentMethodID)
                .HasPrincipalKey(e => e.Id);
            modelBuilder.Entity<VATPostingGroup>()
                .HasMany(e => e.Customers)
                .WithOne(e => e.VATPostingGroup)
                .HasForeignKey(e => e.VATPostingGroupID)
                .HasPrincipalKey(e => e.Id);
            modelBuilder.Entity<Warehouse>()
                .HasOne(e => e.Country)
                .WithMany(e => e.Warehouses)
                .HasForeignKey(e => e.CountryId)
                .HasPrincipalKey(e => e.Id);
            modelBuilder.Entity<SampleHeader>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<SampleDetail>()
                .HasOne(x => x.SampleHeader)
                .WithMany(x => x.SampleDetails)
                .HasForeignKey(x => x.SampleHeaderId)
                .HasPrincipalKey(x => x.Id);            
        }
    }
}
