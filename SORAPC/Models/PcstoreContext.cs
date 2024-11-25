using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SORAPC.Models;

public partial class PcstoreContext : DbContext
{
    public PcstoreContext()
    {
    }

    public PcstoreContext(DbContextOptions<PcstoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Catalog> Catalogs { get; set; }

    public virtual DbSet<Category> Categorys { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<PosOrder> PosOrders { get; set; }

    public virtual DbSet<Statuss> Statusses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ZESHALONDRAG\\SQLEXPRESS;Initial Catalog=PCstore;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Catalog>(entity =>
        {
            entity.HasKey(e => e.IdCatalog).HasName("PK__Catalogs__38D620C5DFA35328");

            entity.Property(e => e.IdCatalog).HasColumnName("ID_Catalog");
            entity.Property(e => e.CategoryId).HasColumnName("Category_ID");
            entity.Property(e => e.Descriptions).HasColumnType("text");
            entity.Property(e => e.ImageUrl)
                .HasColumnType("text")
                .HasColumnName("Image_url");
            entity.Property(e => e.Names)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Catalogs)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Catalogs__Catego__4F7CD00D");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__Category__6DB3A68A1A0A5A63");

            entity.Property(e => e.IdCategory).HasColumnName("ID_Category");
            entity.Property(e => e.Names)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__Orders__EC9FA95520E26AB5");

            entity.Property(e => e.IdOrder).HasColumnName("ID_Order");
            entity.Property(e => e.Dates)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.TotalSum).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UsersId).HasColumnName("Users_ID");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Orders__Status_I__5535A963");

            entity.HasOne(d => d.Users).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__Orders__Users_ID__5441852A");
        });

        modelBuilder.Entity<PosOrder>(entity =>
        {
            entity.HasKey(e => e.IdPosOrder).HasName("PK__PosOrder__D77BC1CB33C4963F");

            entity.Property(e => e.IdPosOrder).HasColumnName("ID_PosOrder");
            entity.Property(e => e.CatalogsId).HasColumnName("Catalogs_ID");
            entity.Property(e => e.OrdersId).HasColumnName("Orders_ID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Catalogs).WithMany(p => p.PosOrders)
                .HasForeignKey(d => d.CatalogsId)
                .HasConstraintName("FK__PosOrders__Catal__59063A47");

            entity.HasOne(d => d.Orders).WithMany(p => p.PosOrders)
                .HasForeignKey(d => d.OrdersId)
                .HasConstraintName("FK__PosOrders__Order__5812160E");
        });

        modelBuilder.Entity<Statuss>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PK__Statuss__5AC2A73415D37535");

            entity.ToTable("Statuss");

            entity.Property(e => e.IdStatus).HasColumnName("ID_Status");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__ED4DE44286BC8BB4");

            entity.HasIndex(e => e.Phone, "UQ__Users__5C7E359ED7E1FDD4").IsUnique();

            entity.HasIndex(e => e.Logins, "UQ__Users__D00D06329A3F9546").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.Logins)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Passwords)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.UserMiddleName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserSurname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
