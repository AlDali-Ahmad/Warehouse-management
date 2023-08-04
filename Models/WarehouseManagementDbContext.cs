using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Warehouse_Management_2.Models;

public partial class WarehouseManagementDbContext : DbContext
{
    public WarehouseManagementDbContext()
    {
    }

    public WarehouseManagementDbContext(DbContextOptions<WarehouseManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<BuyOrder> BuyOrders { get; set; }

    public virtual DbSet<BuyOrderDetail> BuyOrderDetails { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<SellOrder> SellOrders { get; set; }

    public virtual DbSet<SellOrderDetail> SellOrderDetails { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-VHS4OHL\\SQLEXPRESS;Initial Catalog=Warehouse_management_db;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<BuyOrder>(entity =>
        {
            entity.HasIndex(e => e.Customerid, "IX_BuyOrders_Customerid");

            entity.HasIndex(e => e.Employeeid, "IX_BuyOrders_Employeeid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderDate).HasColumnType("date");

            entity.HasOne(d => d.Customer).WithMany(p => p.BuyOrders)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("FK_BuyOrders_Customers");

            entity.HasOne(d => d.Employee).WithMany(p => p.BuyOrders)
                .HasForeignKey(d => d.Employeeid)
                .HasConstraintName("FK_BuyOrders_Employees");
        });

        modelBuilder.Entity<BuyOrderDetail>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Quentity).HasColumnType("text");

            entity.HasOne(d => d.Order).WithMany(p => p.BuyOrderDetails)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("FK_BuyOrderDetails_BuyOrders");

            entity.HasOne(d => d.Product).WithMany(p => p.BuyOrderDetails)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("FK_BuyOrderDetails_Products");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CityName).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasIndex(e => e.Cityid, "IX_Customers_Cityid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.NumperPhone)
                .HasColumnType("text")
                .HasColumnName("numper_phone");

            entity.HasOne(d => d.City).WithMany(p => p.Customers)
                .HasForeignKey(d => d.Cityid)
                .HasConstraintName("FK_Customers_Cities");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Father).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.NumberPhone)
                .HasColumnType("text")
                .HasColumnName("number_phone");

            entity.HasOne(d => d.City).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Cityid)
                .HasConstraintName("FK_Employees_Cities");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountInStock).HasColumnName("Count_in_Stock");
            entity.Property(e => e.Expir).HasColumnType("date");
            entity.Property(e => e.ProductName).HasMaxLength(50);
        });

        modelBuilder.Entity<SellOrder>(entity =>
        {
            entity.HasIndex(e => e.Supplierid, "IX_SellOrders_Supplierid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DiscountPercentage).HasColumnType("text");
            entity.Property(e => e.OrderDate).HasColumnType("date");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SellOrders)
                .HasForeignKey(d => d.Supplierid)
                .HasConstraintName("FK_SellOrders_Suppliers");
        });

        modelBuilder.Entity<SellOrderDetail>(entity =>
        {
            entity.HasIndex(e => e.Orderid, "IX_SellOrderDetails_Orderid");

            entity.HasIndex(e => e.Productid, "IX_SellOrderDetails_Productid");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.HasOne(d => d.Order).WithMany(p => p.SellOrderDetails)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("FK_SellOrderDetails_SellOrders");

            entity.HasOne(d => d.Product).WithMany(p => p.SellOrderDetails)
                .HasForeignKey(d => d.Productid)
                .HasConstraintName("FK_SellOrderDetails_Products");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasIndex(e => e.Cityid, "IX_Suppliers_Cityid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.NumberPhone)
                .HasColumnType("text")
                .HasColumnName("number_phone");

            entity.HasOne(d => d.City).WithMany(p => p.Suppliers)
                .HasForeignKey(d => d.Cityid)
                .HasConstraintName("FK_Suppliers_Cities");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
