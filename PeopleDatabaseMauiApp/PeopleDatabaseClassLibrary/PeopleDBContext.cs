using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PeopleDatabaseClassLibrary.Models;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace PeopleDatabaseClassLibrary;

public partial class PeopleDBContext : DbContext
{
    public PeopleDBContext()
    {
    }

    public PeopleDBContext(DbContextOptions<PeopleDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Person> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=people4c;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("addresses");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.City).HasMaxLength(80);
            entity.Property(e => e.HouseNumber).HasMaxLength(20);
            entity.Property(e => e.Street).HasMaxLength(100);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("people");

            entity.HasIndex(e => e.AddressId, "FK_People_Addresses");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.AddressId)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(11)")
                .HasColumnName("addressId");
            entity.Property(e => e.Age).HasColumnType("int(11)");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .UseCollation("utf8mb4_polish_ci");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .UseCollation("utf8mb4_polish_ci");

            entity.HasOne(d => d.Address).WithMany(p => p.People)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_People_Addresses");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
