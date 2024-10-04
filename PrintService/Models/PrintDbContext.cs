using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PrintService.Models;

public partial class PrintDbContext : DbContext
{
    public PrintDbContext()
    {
    }

    public PrintDbContext(DbContextOptions<PrintDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<EfmigrationHistory> EfmigrationHistories { get; set; }

    public virtual DbSet<Printer> Printers { get; set; }

    public virtual DbSet<PrintingLog> PrintingLogs { get; set; }

    public virtual DbSet<PurchaseHistory> PurchaseHistories { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Spsoconfiguration> Spsoconfigurations { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=66.42.43.15,1433;Database=PrintService;User Id=sa;Password=Abc@12345;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.UserName).HasMaxLength(255);
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF6FF0AD0E35");

            entity.Property(e => e.DocumentId).HasColumnName("DocumentID");
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.FileType).HasMaxLength(10);
            entity.Property(e => e.StudentId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("StudentID");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Student).WithMany(p => p.Documents)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Documents__Stude__34C8D9D1");
        });

        modelBuilder.Entity<EfmigrationHistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId);

            entity.ToTable("__EFMigrationHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Printer>(entity =>
        {
            entity.HasKey(e => e.PrinterId).HasName("PK__Printers__D452AB21D9C48E45");

            entity.Property(e => e.PrinterId).HasColumnName("PrinterID");
            entity.Property(e => e.Brand).HasMaxLength(100);
            entity.Property(e => e.BuildingName).HasMaxLength(100);
            entity.Property(e => e.CampusName).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Model).HasMaxLength(100);
            entity.Property(e => e.RoomNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<PrintingLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Printing__5E5499A862BD064B");

            entity.ToTable("PrintingLog");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.DocumentId).HasColumnName("DocumentID");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.PaperSize).HasMaxLength(10);
            entity.Property(e => e.PrinterId).HasColumnName("PrinterID");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.StudentId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("StudentID");

            entity.HasOne(d => d.Document).WithMany(p => p.PrintingLogs)
                .HasForeignKey(d => d.DocumentId)
                .HasConstraintName("FK__PrintingL__Docum__3A81B327");

            entity.HasOne(d => d.Printer).WithMany(p => p.PrintingLogs)
                .HasForeignKey(d => d.PrinterId)
                .HasConstraintName("FK__PrintingL__Print__398D8EEE");

            entity.HasOne(d => d.Student).WithMany(p => p.PrintingLogs)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__PrintingL__Stude__38996AB5");
        });

        modelBuilder.Entity<PurchaseHistory>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__Purchase__6B0A6BDE2C71E164");

            entity.ToTable("PurchaseHistory");

            entity.Property(e => e.PurchaseId).HasColumnName("PurchaseID");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PurchaseDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.StudentId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("StudentID");

            entity.HasOne(d => d.Student).WithMany(p => p.PurchaseHistories)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__PurchaseH__Stude__403A8C7D");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Reports__D5BD48E54E9D1821");

            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.ReportDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReportType).HasMaxLength(20);
        });

        modelBuilder.Entity<Spsoconfiguration>(entity =>
        {
            entity.HasKey(e => e.ConfigId).HasName("PK__SPSOConf__C3BC333CA23CEA5E");

            entity.ToTable("SPSOConfigurations");

            entity.Property(e => e.ConfigId).HasColumnName("ConfigID");
            entity.Property(e => e.DateApply)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DefaultPaperSize).HasMaxLength(10);
            entity.Property(e => e.NextDefaultPagesDate).HasColumnType("datetime");
            entity.Property(e => e.PermittedFileTypes).HasMaxLength(255);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52A798CCF616C");

            entity.HasIndex(e => e.AccountId, "IX_Students").IsUnique();

            entity.Property(e => e.StudentId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("StudentID");
            entity.Property(e => e.AccountBalance).HasDefaultValue(0);
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);

            entity.HasOne(d => d.Account).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.AccountId)
                .HasConstraintName("FK_Students_Account");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
