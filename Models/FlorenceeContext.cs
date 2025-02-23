using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RPm_Gackan1.Models;

public partial class FlorenceeContext : DbContext
{
    public FlorenceeContext()
    {
    }

    public FlorenceeContext(DbContextOptions<FlorenceeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Cataloge> Cataloges { get; set; }

    public virtual DbSet<Magazine> Magazines { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<PosOrder> PosOrders { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-410VRMN\\SQLEXPRESS;Initial Catalog=Florencee;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.IdCart).HasName("PK__Cart__701794905350F0E1");

            entity.ToTable("Cart");

            entity.Property(e => e.IdCart).HasColumnName("ID_cart");
            entity.Property(e => e.CatalogeId).HasColumnName("Cataloge_ID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UsersId)
                .HasMaxLength(200)
                .IsFixedLength()
                .HasColumnName("Users_ID");

            entity.HasOne(d => d.Cataloge).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CatalogeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__Cataloge_I__4AB81AF0");
        });

        modelBuilder.Entity<Cataloge>(entity =>
        {
            entity.HasKey(e => e.IdCataloge).HasName("PK__Cataloge__E129E1D3314E3902");

            entity.ToTable("Cataloge");

            entity.Property(e => e.IdCataloge).HasColumnName("ID_Cataloge");
            entity.Property(e => e.Images)
                .HasMaxLength(600)
                .IsFixedLength();
            entity.Property(e => e.MagazinesId).HasColumnName("Magazines_ID");
            entity.Property(e => e.ProductCategoriesId).HasColumnName("ProductCategories_ID");
            entity.Property(e => e.ProductDescription)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.ProductName)
                .HasMaxLength(25)
                .IsFixedLength();
            entity.Property(e => e.ProductPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Magazines).WithMany(p => p.Cataloges)
                .HasForeignKey(d => d.MagazinesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cataloge__Magazi__403A8C7D");

            entity.HasOne(d => d.ProductCategories).WithMany(p => p.Cataloges)
                .HasForeignKey(d => d.ProductCategoriesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cataloge__Produc__3F466844");
        });

        modelBuilder.Entity<Magazine>(entity =>
        {
            entity.HasKey(e => e.IdMagazines).HasName("PK__Magazine__B3D15F2522475D51");

            entity.Property(e => e.IdMagazines).HasColumnName("ID_Magazines");
            entity.Property(e => e.AddressMagazin)
                .HasMaxLength(60)
                .IsFixedLength();
            entity.Property(e => e.NameMagazin)
                .HasMaxLength(25)
                .IsFixedLength();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrders).HasName("PK__Orders__20F81C1D1807E8B9");

            entity.HasIndex(e => e.OrderNumber, "UQ__Orders__CAC5E7434C5E2EAC").IsUnique();

            entity.Property(e => e.IdOrders).HasColumnName("ID_Orders");
            entity.Property(e => e.DateOrder).HasColumnType("datetime");
            entity.Property(e => e.SumBill).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UsersId).HasColumnName("Users_ID");

            entity.HasOne(d => d.Users).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Users_ID__440B1D61");
        });

        modelBuilder.Entity<PosOrder>(entity =>
        {
            entity.HasKey(e => e.IdPosOrder).HasName("PK__PosOrder__D77BC1CBF0452DDD");

            entity.ToTable("PosOrder");

            entity.Property(e => e.IdPosOrder).HasColumnName("ID_PosOrder");
            entity.Property(e => e.CatalogeId).HasColumnName("Cataloge_ID");
            entity.Property(e => e.OrdersId).HasColumnName("Orders_ID");

            entity.HasOne(d => d.Cataloge).WithMany(p => p.PosOrders)
                .HasForeignKey(d => d.CatalogeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PosOrder__Catalo__46E78A0C");

            entity.HasOne(d => d.Orders).WithMany(p => p.PosOrders)
                .HasForeignKey(d => d.OrdersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PosOrder__Orders__47DBAE45");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.IdProductCategory).HasName("PK__ProductC__8FAD631ECE9BC5F6");

            entity.Property(e => e.IdProductCategory).HasColumnName("ID_ProductCategory");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(25)
                .IsFixedLength();
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.IdReview).HasName("PK__Review__E39E9647909E662F");

            entity.ToTable("Review");

            entity.Property(e => e.IdReview).HasColumnName("ID_Review");
            entity.Property(e => e.CatalogeId).HasColumnName("Cataloge_ID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ReviewText).HasMaxLength(500);
            entity.Property(e => e.UsersId).HasColumnName("Users_ID");

            entity.HasOne(d => d.Cataloge).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.CatalogeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Review__Cataloge__5DCAEF64");

            entity.HasOne(d => d.Users).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Review__Users_ID__5CD6CB2B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUsers).HasName("PK__Users__B97FFDA1263DB808");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Users__85FB4E383B672630").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105348413DB9F").IsUnique();

            entity.Property(e => e.IdUsers).HasColumnName("ID_Users");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsFixedLength();
            entity.Property(e => e.UserLogin)
                .HasMaxLength(25)
                .IsFixedLength();
            entity.Property(e => e.UserPassword)
                .HasMaxLength(350)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
