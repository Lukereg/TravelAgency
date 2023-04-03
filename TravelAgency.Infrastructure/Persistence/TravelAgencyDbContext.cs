using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Infrastructure.Persistence
{
    public class TravelAgencyDbContext : DbContext
    {

        public TravelAgencyDbContext()
        {

        }

        public TravelAgencyDbContext(DbContextOptions<TravelAgencyDbContext> options) : base(options)
        {

        }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=TravelAgencyDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Login, "login_UNIQUE")
                .IsUnique();

                entity.Property(u => u.Login)
                .HasMaxLength(50)
                .IsRequired();

                entity.Property(u => u.Password)
                .HasMaxLength(255)
                .IsRequired();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.TourName)
                .HasMaxLength(70)
                .IsRequired();

                entity.Property(o => o.Description)
                .IsRequired();

                entity.Property(o => o.Price)
                .IsRequired();

                entity.Property(o => o.StartDate)
                .IsRequired();

                entity.Property(o => o.EndDate)
                .IsRequired();

                entity.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

                entity.HasOne(o => o.Creator)
                .WithMany(c => c.CreatedOrders)
                .HasForeignKey(o => o.CreatorId);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                entity.Property(c => c.LastName)
                .HasMaxLength(50)
                .IsRequired();

                entity.Property(c => c.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();

                entity.Property(c => c.Address)
                .HasMaxLength(255)
                .IsRequired();
            });
        }
    }
}
