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

    public virtual DbSet<PaperDetailPrinter> PaperDetailPrinters { get; set; }

    public virtual DbSet<PaperDetailStudent> PaperDetailStudents { get; set; }

    public virtual DbSet<PaperType> PaperTypes { get; set; }

    public virtual DbSet<Printer> Printers { get; set; }

    public virtual DbSet<PrintingLog> PrintingLogs { get; set; }

    public virtual DbSet<PurchaseHistory> PurchaseHistories { get; set; }

    public virtual DbSet<PurchaseHistoryDetail> PurchaseHistoryDetails { get; set; }

    public virtual DbSet<RechargeHistory> RechargeHistories { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Spsoconfiguration> Spsoconfigurations { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=35.247.182.152,1433;Database=PrintService;User Id=sa;Password=Abc@12345;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.AccountId)
                .HasMaxLength(255)
                .HasColumnName("AccountID");
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF6FA4F3DD2F");

            entity.Property(e => e.DocumentId).HasColumnName("DocumentID");
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.FileType).HasMaxLength(10);
            entity.Property(e => e.FilePath).HasMaxLength(255);
            entity.Property(e => e.StudentId)
                .HasMaxLength(255)
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

        modelBuilder.Entity<PaperDetailPrinter>(entity =>
        {
            entity.HasKey(e => new { e.PrinterId, e.PaperTypeId });

            entity.ToTable("PaperDetailPrinter");

            entity.Property(e => e.PrinterId).HasColumnName("PrinterID");
            entity.Property(e => e.PaperTypeId).HasColumnName("PaperTypeID");

            entity.HasOne(d => d.PaperType).WithMany(p => p.PaperDetailPrinters)
                .HasForeignKey(d => d.PaperTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaperDetailPrinter_PaperType");

            entity.HasOne(d => d.Printer).WithMany(p => p.PaperDetailPrinters)
                .HasForeignKey(d => d.PrinterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaperDetailPrinter_Printers");
        });

        modelBuilder.Entity<PaperDetailStudent>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.PaperTypeId });

            entity.ToTable("PaperDetailStudent");

            entity.Property(e => e.StudentId)
                .HasMaxLength(255)
                .HasColumnName("StudentID");
            entity.Property(e => e.PaperTypeId).HasColumnName("PaperTypeID");

            entity.HasOne(d => d.PaperType).WithMany(p => p.PaperDetailStudents)
                .HasForeignKey(d => d.PaperTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaperDetailStudent_PaperType");

            entity.HasOne(d => d.Student).WithMany(p => p.PaperDetailStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaperDetailStudent_Students");
        });

        modelBuilder.Entity<PaperType>(entity =>
        {
            entity.ToTable("PaperType");

            entity.Property(e => e.PaperTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PaperTypeID");
            entity.Property(e => e.PaperName).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<Printer>(entity =>
        {
            entity.HasKey(e => e.PrinterId).HasName("PK__Printers__D452AB21E9112492");

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
            entity.HasKey(e => e.LogId).HasName("PK__Printing__5E5499A854DB366E");

            entity.ToTable("PrintingLog");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.DocumentId).HasColumnName("DocumentID");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.PaperTypeId).HasColumnName("PaperTypeID");
            entity.Property(e => e.PrinterId).HasColumnName("PrinterID");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.StudentId)
                .HasMaxLength(255)
                .HasColumnName("StudentID");

            entity.HasOne(d => d.Document).WithMany(p => p.PrintingLogs)
                .HasForeignKey(d => d.DocumentId)
                .HasConstraintName("FK__PrintingL__Docum__4E88ABD4");

            entity.HasOne(d => d.PaperType).WithMany(p => p.PrintingLogs)
                .HasForeignKey(d => d.PaperTypeId)
                .HasConstraintName("FK_PrintingLog_PaperType");

            entity.HasOne(d => d.Printer).WithMany(p => p.PrintingLogs)
                .HasForeignKey(d => d.PrinterId)
                .HasConstraintName("FK__PrintingL__Print__4D94879B");

            entity.HasOne(d => d.Student).WithMany(p => p.PrintingLogs)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__PrintingL__Stude__38996AB5");
        });

        modelBuilder.Entity<PurchaseHistory>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__Purchase__6B0A6BDE9A6C83C4");

            entity.ToTable("PurchaseHistory");

            entity.Property(e => e.PurchaseId).HasColumnName("PurchaseID");
            entity.Property(e => e.PurchasedDate).HasColumnType("datetime");
            entity.Property(e => e.StudentId)
                .HasMaxLength(255)
                .HasColumnName("StudentID");
            entity.Property(e => e.TotalPurchased).HasColumnType("money");

            entity.HasOne(d => d.Student).WithMany(p => p.PurchaseHistories)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseH__Stude__403A8C7D");
        });

        modelBuilder.Entity<PurchaseHistoryDetail>(entity =>
        {
            entity.HasKey(e => new { e.PurchaseId, e.PaperTypeId });

            entity.ToTable("PurchaseHistoryDetail");

            entity.Property(e => e.PurchaseId).HasColumnName("PurchaseID");
            entity.Property(e => e.PaperTypeId).HasColumnName("PaperTypeID");
            entity.Property(e => e.PurchasedByType).HasColumnType("money");

            entity.HasOne(d => d.PaperType).WithMany(p => p.PurchaseHistoryDetails)
                .HasForeignKey(d => d.PaperTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseHistoryDetail_PaperType");

            entity.HasOne(d => d.Purchase).WithMany(p => p.PurchaseHistoryDetails)
                .HasForeignKey(d => d.PurchaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseHistoryDetail_PurchaseHistory");
        });

        modelBuilder.Entity<RechargeHistory>(entity =>
        {
            entity.HasKey(e => e.RechargeId);

            entity.ToTable("RechargeHistory");

            entity.Property(e => e.RechargeId)
                .HasColumnName("RechargeID");

            entity.Property(e => e.Amonut).HasColumnType("money");
            entity.Property(e => e.RechargeMethod).HasMaxLength(255);
            entity.Property(e => e.RechargedDate).HasColumnType("datetime");
            entity.Property(e => e.StudentId)
                .HasMaxLength(255)
                .HasColumnName("StudentID");

            entity.HasOne(d => d.Student).WithMany(p => p.RechargeHistories)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RechargeHistory_Students");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Reports__D5BD48E5ED93793E");

            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.ReportDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReportType).HasMaxLength(20);
        });

        modelBuilder.Entity<Spsoconfiguration>(entity =>
        {
            entity.HasKey(e => e.ConfigId).HasName("PK__SPSOConf__C3BC333C79E08E87");

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

            entity.Property(e => e.StudentId)
                .HasMaxLength(255)
                .HasColumnName("StudentID");
            entity.Property(e => e.AccountBalance)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);

            entity.HasOne(d => d.StudentNavigation).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_Account1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
