using System;
using System.Linq;
using InvoicingAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoicingAPI.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<UserCompanyMap> UserCompanyMaps => Set<UserCompanyMap>();
        public DbSet<Customer> Customers => Set<Customer>();
        // public DbSet<Product> Products => Set<Product>();
        public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();
        public DbSet<MenuItem> MenuItems => Set<MenuItem>();
        public DbSet<InvoiceDocument> InvoiceDocuments => Set<InvoiceDocument>();
        public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
        public DbSet<InvoiceNumberSettings> InvoiceNumberSettings => Set<InvoiceNumberSettings>();
        public DbSet<RoleMaster> RoleMasters => Set<RoleMaster>();   // ✅ NEW

        public DbSet<Material> Materials => Set<Material>();
        public DbSet<Product> Products => Set<Product>();          // already exists, just keep it
        public DbSet<ProductBOM> ProductBoms => Set<ProductBOM>();
        public DbSet<MaterialPurchase> MaterialPurchases => Set<MaterialPurchase>();
        public DbSet<StockTransaction> StockTransactions => Set<StockTransaction>();
        public DbSet<UnitConversion> UnitConversions => Set<UnitConversion>();
        public DbSet<DeliveryChallan> DeliveryChallan => Set<DeliveryChallan>();
        public DbSet<DeliveryChallanItems> DeliveryChallanItems => Set<DeliveryChallanItems>();
        public DbSet<DeliveryChallanCounters> DeliveryChallanCounters => Set<DeliveryChallanCounters>();

        public DbSet<RejectionNote> RejectionNote => Set<RejectionNote>();
        public DbSet<RejectionNoteItems> RejectionNoteItems => Set<RejectionNoteItems>();
        public DbSet<RejectionNoteCounters> RejectionNoteCounters => Set<RejectionNoteCounters>();




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply IEntityTypeConfiguration classes
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Disable cascade delete globally
            foreach (var fk in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            // -----------------------------
            // USER-COMPANY-MAP RELATIONS
            //-----------------------------
            modelBuilder.Entity<UserCompanyMap>(entity =>
            {
                entity.HasOne(m => m.User)
                      .WithMany()
                      .HasForeignKey(m => m.UserId);

                entity.HasOne(m => m.Company)
                      .WithMany()
                      .HasForeignKey(m => m.CompanyId);

                entity.HasOne(m => m.Role)
                      .WithMany()
                      .HasForeignKey(m => m.RoleId);
            });

            // -----------------------------
            // SEED ROLES (NO CHANGE)
            // -----------------------------
            modelBuilder.Entity<RoleMaster>().HasData(
                new RoleMaster
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    RoleName = "Owner",
                    Description = "Full access to the company",
                    IsSystemRole = true,
                    CreatedAt = DateTime.UtcNow
                },
                new RoleMaster
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    RoleName = "Admin",
                    Description = "Manage all modules except company deletion",
                    IsSystemRole = true,
                    CreatedAt = DateTime.UtcNow
                },
                new RoleMaster
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    RoleName = "Accountant",
                    Description = "Finance, GST, invoices, expenses",
                    IsSystemRole = false,
                    CreatedAt = DateTime.UtcNow
                },
                new RoleMaster
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    RoleName = "Sales Manager",
                    Description = "Create invoices, manage customers",
                    IsSystemRole = false,
                    CreatedAt = DateTime.UtcNow
                },
                new RoleMaster
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    RoleName = "Viewer",
                    Description = "Read-only access",
                    IsSystemRole = false,
                    CreatedAt = DateTime.UtcNow
                }
            );

            // ----------------------------------------------------
            // NEW MENU STRUCTURE WITH FIXED GUIDs + PARENT SUPPORT
            // ----------------------------------------------------
            var dashboardId = Guid.Parse("10000000-0000-0000-0000-000000000001");
            var companiesId = Guid.Parse("10000000-0000-0000-0000-000000000002");
            var customersId = Guid.Parse("10000000-0000-0000-0000-000000000003");
            var productsId = Guid.Parse("10000000-0000-0000-0000-000000000004");

            var salesId = Guid.Parse("10000000-0000-0000-0000-000000000005");
            var settingsId = Guid.Parse("10000000-0000-0000-0000-000000000006");

            var invoicesId = Guid.Parse("20000000-0000-0000-0000-000000000001");
            var challansId = Guid.Parse("20000000-0000-0000-0000-000000000002");
            var rejectionNotesId = Guid.Parse("20000000-0000-0000-0000-000000000003");

            var invoiceFormatId = Guid.Parse("30000000-0000-0000-0000-000000000001");
            var pdfTemplateId = Guid.Parse("30000000-0000-0000-0000-000000000002");
            var bankDetailsId = Guid.Parse("30000000-0000-0000-0000-000000000003");
            var paymentQrId = Guid.Parse("30000000-0000-0000-0000-000000000004");

            modelBuilder.Entity<MenuItem>().HasData(

                // -------------------------
                // MAIN MENU
                // -------------------------
                new MenuItem { Id = dashboardId, Title = "Dashboard", Route = "/dashboard", Icon = "dashboard", Order = 1, IsActive = true, ParentId = null },
                new MenuItem { Id = companiesId, Title = "Companies", Route = "/companies", Icon = "business", Order = 2, IsActive = true, ParentId = null },
                new MenuItem { Id = customersId, Title = "Customers", Route = "/customers", Icon = "users", Order = 3, IsActive = true, ParentId = null },
                new MenuItem { Id = productsId, Title = "Products", Route = "/products", Icon = "box", Order = 4, IsActive = true, ParentId = null },

                // SALES (PARENT)
                new MenuItem { Id = salesId, Title = "Sales", Route = "/sales", Icon = "sales", Order = 5, IsActive = true, ParentId = null },

                // SETTINGS (PARENT)
                new MenuItem { Id = settingsId, Title = "Settings", Route = "/settings", Icon = "settings", Order = 6, IsActive = true, ParentId = null },


                // -------------------------
               // SALES (CHILDREN)
                //-------------------------
                new MenuItem { Id = invoicesId, Title = "Invoices", Route = "/sales/invoices", Icon = "invoice", Order = 1, ParentId = salesId, IsActive = true },
                new MenuItem { Id = challansId, Title = "Delivery Challans", Route = "/sales/challans", Icon = "truck", Order = 2, ParentId = salesId, IsActive = true },
                new MenuItem { Id = rejectionNotesId, Title = "Rejection Notes", Route = "/sales/rejection-notes", Icon = "cancel", Order = 3, ParentId = salesId, IsActive = true },


                // -------------------------
                // SETTINGS (CHILDREN)
               // -------------------------
                new MenuItem { Id = invoiceFormatId, Title = "Invoice Number Format", Route = "/settings/invoice-number", Icon = "hash", Order = 1, ParentId = settingsId, IsActive = true },
                new MenuItem { Id = pdfTemplateId, Title = "PDF Template", Route = "/settings/invoice-pdf-template", Icon = "file", Order = 2, ParentId = settingsId, IsActive = true },
                new MenuItem { Id = bankDetailsId, Title = "Bank Details", Route = "/settings/bank-details", Icon = "bank", Order = 3, ParentId = settingsId, IsActive = true },
                new MenuItem { Id = paymentQrId, Title = "Payment QR", Route = "/settings/payment-qr", Icon = "qrcode", Order = 4, ParentId = settingsId, IsActive = true }
            );

            // -----------------------------
            // ENTITY RELATIONSHIPS
            // -----------------------------
            modelBuilder.Entity<Material>()
                .HasOne(m => m.Company)
                .WithMany(c => c.Materials)
                .HasForeignKey(m => m.CompanyId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Company)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CompanyId);

            modelBuilder.Entity<ProductBOM>()
                .HasOne(b => b.Product)
                .WithMany()
                .HasForeignKey(b => b.ProductId);

            modelBuilder.Entity<ProductBOM>()
                .HasOne(b => b.Material)
                .WithMany()
                .HasForeignKey(b => b.MaterialId);

            modelBuilder.Entity<MaterialPurchase>()
                .HasOne(mp => mp.Material)
                .WithMany()
                .HasForeignKey(mp => mp.MaterialId);

            modelBuilder.Entity<StockTransaction>()
                .HasOne(st => st.Material)
                .WithMany()
                .HasForeignKey(st => st.MaterialId);
        }

    }
}

