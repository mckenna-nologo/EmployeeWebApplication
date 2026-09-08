//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;

//namespace EmployeeWebApplication.Models;

//public partial class MyDbContext : DbContext
//{
//    public MyDbContext()
//    {
//    }

//    public MyDbContext(DbContextOptions<MyDbContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<Employee> Employees { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=LKPC;Database=Employees;Trusted_Connection=True;TrustServerCertificate=True");

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Employee>(entity =>
//        {
//            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF19A9E6D7C");

//            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
//            entity.Property(e => e.DateCreated)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");
//            entity.Property(e => e.Department).HasMaxLength(100);
//            entity.Property(e => e.Email).HasMaxLength(255);
//            entity.Property(e => e.FirstName).HasMaxLength(100);
//            entity.Property(e => e.LastName).HasMaxLength(100);
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
