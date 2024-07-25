using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cafe.Models;

public partial class Prn212CoffeeContext : DbContext
{
    public Prn212CoffeeContext()
    {
    }

    public Prn212CoffeeContext(DbContextOptions<Prn212CoffeeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Coffee> Coffees { get; set; }

    public virtual DbSet<CoffeeOrder> CoffeeOrders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<TableOrder> TableOrders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Console.WriteLine(Directory.GetCurrentDirectory());
        IConfiguration config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .Build();
        var strConn = config["ConnectionStrings:MyDatabase"];
        optionsBuilder.UseSqlServer(strConn);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coffee>(entity =>
        {
            entity.HasKey(e => e.Cid);

            entity.HasIndex(e => e.CoffeeName, "UQ_Coffee_name").IsUnique();

            entity.Property(e => e.Cid).HasColumnName("cid");
            entity.Property(e => e.CoffeeName)
                .HasMaxLength(50)
                .HasColumnName("coffee_name");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<CoffeeOrder>(entity =>
        {
            entity.HasKey(e => e.Coid);

            entity.Property(e => e.Coid).HasColumnName("coid");
            entity.Property(e => e.SellerId).HasColumnName("seller_id");
            entity.Property(e => e.TableOrderId).HasColumnName("table_order_id");
            entity.Property(e => e.TotalPrice).HasColumnName("total_price");

            entity.HasOne(d => d.Seller).WithMany(p => p.CoffeeOrders)
                .HasForeignKey(d => d.SellerId)
                .HasConstraintName("FK_CoffeeOrders_Users");

            entity.HasOne(d => d.TableOrder).WithMany(p => p.CoffeeOrders)
                .HasForeignKey(d => d.TableOrderId)
                .HasConstraintName("FK_CoffeeOrders_TableOrders");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.CoffeeOrderId, e.CoffeeId }).HasName("PK_CoffeeItems");

            entity.Property(e => e.CoffeeOrderId).HasColumnName("coffee_order_id");
            entity.Property(e => e.CoffeeId).HasColumnName("coffee_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Coffee).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.CoffeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItems_Coffees");

            entity.HasOne(d => d.CoffeeOrder).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.CoffeeOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItems_CoffeeOrders");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Rid);

            entity.HasIndex(e => e.RoleName, "UQ_Roles_name").IsUnique();

            entity.Property(e => e.Rid).HasColumnName("rid");
            entity.Property(e => e.RoleName)
                .HasMaxLength(10)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Tid);

            entity.Property(e => e.Tid).HasColumnName("tid");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("free")
                .HasColumnName("status");
        });

        modelBuilder.Entity<TableOrder>(entity =>
        {
            entity.HasKey(e => e.Toid);

            entity.Property(e => e.Toid).HasColumnName("toid");
            entity.Property(e => e.BookTime)
                .HasColumnType("datetime")
                .HasColumnName("book_time");
            entity.Property(e => e.BookerId).HasColumnName("booker_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");

            entity.HasOne(d => d.Booker).WithMany(p => p.TableOrders)
                .HasForeignKey(d => d.BookerId)
                .HasConstraintName("FK_TableOrders_Users");

            entity.HasOne(d => d.Table).WithMany(p => p.TableOrders)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableOrders_Tables");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.HasIndex(e => e.Username, "UQ_Users_username").IsUnique();

            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.Password)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("phone_number");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
