using Microsoft.EntityFrameworkCore;
using OnClickInvest.Api.Modules.Users.Models;
using OnClickInvest.Api.Modules.Tenancy.Models;
using OnClickInvest.Api.Modules.Auth.Models;
using OnClickInvest.Api.Modules.Plans.Models;

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
            // Plan (NOVO)
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
        }
    }
}
