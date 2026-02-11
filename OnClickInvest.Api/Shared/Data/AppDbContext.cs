using Microsoft.EntityFrameworkCore;
using OnClickInvest.Api.Modules.Users.Models;
using OnClickInvest.Api.Modules.Tenancy.Models;
using OnClickInvest.Api.Modules.Auth.Models;
using OnClickInvest.Api.Modules.Plans.Models;
using OnClickInvest.Api.Modules.Subscriptions.Models;
using OnClickInvest.Api.Modules.Portfolios.Models;
using OnClickInvest.Api.Modules.Investors.Models;
using OnClickInvest.Api.Modules.Reports.Models;

namespace OnClickInvest.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<Plan> Plans => Set<Plan>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();
        public DbSet<Portfolio> Portfolios => Set<Portfolio>();
        public DbSet<Investor> Investors => Set<Investor>();
        public DbSet<Investment> Investments => Set<Investment>();

        // 🔥 REPORTS
        public DbSet<Projection> Projections => Set<Projection>();
        public DbSet<ProjectionScenario> ProjectionScenarios => Set<ProjectionScenario>();
        public DbSet<ProjectionSnapshot> ProjectionSnapshots => Set<ProjectionSnapshot>();




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // Tenant
            // =========================
            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.ToTable("tenants");

                entity.HasKey(t => t.Id);

                entity.Property(t => t.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(t => t.IsActive)
                    .HasDefaultValue(true);

                entity.Property(t => t.CreatedAt)
                    .IsRequired();

                entity.Property(t => t.UpdatedAt)
                    .IsRequired();
            });

            // =========================
            // User
            // =========================
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasIndex(u => new { u.TenantId, u.Email })
                    .IsUnique();

                entity.Property(u => u.PasswordHash)
                    .IsRequired();

                entity.Property(u => u.Role)
                    .IsRequired();

                entity.Property(u => u.IsActive)
                    .HasDefaultValue(true);

                entity.Property(u => u.CreatedAt)
                    .IsRequired();

                entity.Property(u => u.TenantId)
                    .IsRequired(false);

                entity.HasOne(u => u.Tenant)
                    .WithMany(t => t.Users)
                    .HasForeignKey(u => u.TenantId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // =========================
            // RefreshToken
            // =========================
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("refresh_tokens");

                entity.HasKey(r => r.Id);

                entity.Property(r => r.Token)
                    .IsRequired();

                entity.Property(r => r.ExpiresAt)
                    .IsRequired();

                entity.HasOne(r => r.User)
                    .WithMany(u => u.RefreshTokens)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // =========================
            // Plan
            // =========================
            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("plans");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Description)
                    .HasMaxLength(500);

                entity.Property(p => p.Price)
                    .HasPrecision(10, 2)
                    .IsRequired();

                entity.Property(p => p.MaxUsers)
                    .IsRequired();

                entity.Property(p => p.IsActive)
                    .HasDefaultValue(true);

                entity.Property(p => p.CreatedAt)
                    .IsRequired();

                entity.Property(p => p.UpdatedAt)
                    .IsRequired(false);
            });

            // =========================
            // Subscription
            // =========================
            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("subscriptions");

                entity.HasKey(s => s.Id);

                entity.Property(s => s.StartAt)
                    .IsRequired();

                entity.Property(s => s.CreatedAt)
                    .IsRequired();

                entity.Property(s => s.IsActive)
                    .HasDefaultValue(true);

                entity.HasOne(s => s.Tenant)
                    .WithMany()
                    .HasForeignKey(s => s.TenantId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Plan)
                    .WithMany()
                    .HasForeignKey(s => s.PlanId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // =========================
            // Investor (CLIENTE FINAL)
            // =========================
            modelBuilder.Entity<Investor>(entity =>
            {
                entity.ToTable("investors");
                entity.HasKey(i => i.Id);

                entity.Property(i => i.Name).IsRequired().HasMaxLength(150);
                entity.Property(i => i.Email).IsRequired().HasMaxLength(150);
                entity.HasIndex(i => new { i.TenantId, i.Email }).IsUnique();

                entity.Property(i => i.Document).HasMaxLength(30);
                entity.Property(i => i.IsActive).HasDefaultValue(true);
                entity.Property(i => i.CreatedAt).IsRequired();
                entity.Property(i => i.UpdatedAt).IsRequired();
            });

            modelBuilder.Entity<Portfolio>(entity =>
           {
               entity.ToTable("portfolios");
               entity.HasKey(p => p.Id);

               entity.Property(p => p.Name).IsRequired().HasMaxLength(150);
               entity.Property(p => p.IsActive).HasDefaultValue(true);
               entity.Property(p => p.CreatedAt).IsRequired();
               entity.Property(p => p.UpdatedAt).IsRequired();

               entity.HasMany(p => p.Investments)
                   .WithOne(i => i.Portfolio)
                   .HasForeignKey(i => i.PortfolioId)
                   .OnDelete(DeleteBehavior.Cascade);
           });

            modelBuilder.Entity<Investment>(entity =>
            {
                entity.ToTable("investments");
                entity.HasKey(i => i.Id);

                entity.Property(i => i.AssetName).IsRequired().HasMaxLength(150);
                entity.Property(i => i.AssetType).IsRequired().HasMaxLength(50);
                entity.Property(i => i.Quantity).HasPrecision(18, 4);
                entity.Property(i => i.AveragePrice).HasPrecision(18, 2);
                entity.Property(i => i.TotalInvested).HasPrecision(18, 2);
                entity.Property(i => i.CreatedAt).IsRequired();
            });

            // =====================================================
            // REPORTS - PROJECTIONS
            // =====================================================

            modelBuilder.Entity<Projection>(entity =>
            {
                entity.ToTable("projections");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.InitialCapital)
                      .HasPrecision(18, 2);

                entity.Property(p => p.MonthlyContribution)
                      .HasPrecision(18, 2);

                entity.Property(p => p.Years)
                      .IsRequired();

                entity.Property(p => p.CreatedAt)
                      .IsRequired();

                entity.HasOne(p => p.Investor)
                      .WithMany()
                      .HasForeignKey(p => p.InvestorId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Tenant)
                      .WithMany()
                      .HasForeignKey(p => p.TenantId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProjectionScenario>(entity =>
            {
                entity.ToTable("projection_scenarios");

                entity.HasKey(s => s.Id);

                entity.Property(s => s.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(s => s.AnnualRate)
                      .HasPrecision(5, 2);

                entity.HasOne(s => s.Projection)
                      .WithMany(p => p.Scenarios)
                      .HasForeignKey(s => s.ProjectionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProjectionSnapshot>(entity =>
            {
                entity.ToTable("projection_snapshots");

                entity.HasKey(s => s.Id);

                entity.Property(s => s.TotalInvested)
                      .HasPrecision(18, 2);

                entity.Property(s => s.TotalAmount)
                      .HasPrecision(18, 2);

                entity.Property(s => s.Date)
                      .IsRequired();

                entity.HasOne(s => s.Scenario)
                      .WithMany(sc => sc.Snapshots)
                      .HasForeignKey(s => s.ScenarioId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


        }
    }
}
