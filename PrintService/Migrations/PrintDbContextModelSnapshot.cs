﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrintService.Models;

#nullable disable

namespace PrintService.Migrations
{
    [DbContext(typeof(PrintDbContext))]
    partial class PrintDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PrintService.Models.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("AccountID");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("TypeAccount")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("AccountId");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("PrintService.Models.Document", b =>
                {
                    b.Property<int>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DocumentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocumentId"));

                    b.Property<string>("FileName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FileType")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("StudentId")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("StudentID");

                    b.Property<DateTime?>("UploadedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("DocumentId")
                        .HasName("PK__Document__1ABEEF6FF0AD0E35");

                    b.HasIndex("StudentId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("PrintService.Models.EfmigrationHistory", b =>
                {
                    b.Property<string>("MigrationId")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ProductVersion")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("MigrationId");

                    b.ToTable("__EFMigrationHistory", (string)null);
                });

            modelBuilder.Entity("PrintService.Models.Printer", b =>
                {
                    b.Property<int>("PrinterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PrinterID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrinterId"));

                    b.Property<string>("Brand")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("BuildingName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CampusName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Model")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RoomNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("PrinterId")
                        .HasName("PK__Printers__D452AB21D9C48E45");

                    b.ToTable("Printers");
                });

            modelBuilder.Entity("PrintService.Models.PrintingLog", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LogID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogId"));

                    b.Property<int?>("Copies")
                        .HasColumnType("int");

                    b.Property<int?>("DocumentId")
                        .HasColumnType("int")
                        .HasColumnName("DocumentID");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime");

                    b.Property<bool?>("IsDoubleSided")
                        .HasColumnType("bit");

                    b.Property<int?>("PagesPrinted")
                        .HasColumnType("int");

                    b.Property<string>("PaperSize")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int?>("PrinterId")
                        .HasColumnType("int")
                        .HasColumnName("PrinterID");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime");

                    b.Property<string>("StudentId")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("StudentID");

                    b.HasKey("LogId")
                        .HasName("PK__Printing__5E5499A862BD064B");

                    b.HasIndex("DocumentId");

                    b.HasIndex("PrinterId");

                    b.HasIndex("StudentId");

                    b.ToTable("PrintingLog", (string)null);
                });

            modelBuilder.Entity("PrintService.Models.PurchaseHistory", b =>
                {
                    b.Property<int>("PurchaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PurchaseID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseId"));

                    b.Property<int?>("PagesPurchased")
                        .HasColumnType("int");

                    b.Property<string>("PaymentMethod")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("PurchaseDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("StudentId")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("StudentID");

                    b.HasKey("PurchaseId")
                        .HasName("PK__Purchase__6B0A6BDE2C71E164");

                    b.HasIndex("StudentId");

                    b.ToTable("PurchaseHistory", (string)null);
                });

            modelBuilder.Entity("PrintService.Models.Report", b =>
                {
                    b.Property<int>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ReportID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportId"));

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ReportDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("ReportType")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ReportId")
                        .HasName("PK__Reports__D5BD48E54E9D1821");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("PrintService.Models.Spsoconfiguration", b =>
                {
                    b.Property<int>("ConfigId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ConfigID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConfigId"));

                    b.Property<DateTime?>("DateApply")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("DefaultPagesPerSemester")
                        .HasColumnType("int");

                    b.Property<string>("DefaultPaperSize")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime?>("NextDefaultPagesDate")
                        .HasColumnType("datetime");

                    b.Property<string>("PermittedFileTypes")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ConfigId")
                        .HasName("PK__SPSOConf__C3BC333CA23CEA5E");

                    b.ToTable("SPSOConfigurations", (string)null);
                });

            modelBuilder.Entity("PrintService.Models.Student", b =>
                {
                    b.Property<string>("StudentId")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("StudentID");

                    b.Property<int?>("AccountBalance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int?>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("AccountID");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("StudentId")
                        .HasName("PK__Students__32C52A798CCF616C");

                    b.HasIndex(new[] { "AccountId" }, "IX_Students")
                        .IsUnique()
                        .HasFilter("[AccountID] IS NOT NULL");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("PrintService.Models.Document", b =>
                {
                    b.HasOne("PrintService.Models.Student", "Student")
                        .WithMany("Documents")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK__Documents__Stude__34C8D9D1");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("PrintService.Models.PrintingLog", b =>
                {
                    b.HasOne("PrintService.Models.Document", "Document")
                        .WithMany("PrintingLogs")
                        .HasForeignKey("DocumentId")
                        .HasConstraintName("FK__PrintingL__Docum__3A81B327");

                    b.HasOne("PrintService.Models.Printer", "Printer")
                        .WithMany("PrintingLogs")
                        .HasForeignKey("PrinterId")
                        .HasConstraintName("FK__PrintingL__Print__398D8EEE");

                    b.HasOne("PrintService.Models.Student", "Student")
                        .WithMany("PrintingLogs")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK__PrintingL__Stude__38996AB5");

                    b.Navigation("Document");

                    b.Navigation("Printer");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("PrintService.Models.PurchaseHistory", b =>
                {
                    b.HasOne("PrintService.Models.Student", "Student")
                        .WithMany("PurchaseHistories")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK__PurchaseH__Stude__403A8C7D");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("PrintService.Models.Student", b =>
                {
                    b.HasOne("PrintService.Models.Account", "Account")
                        .WithOne("Student")
                        .HasForeignKey("PrintService.Models.Student", "AccountId")
                        .HasConstraintName("FK_Students_Account");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("PrintService.Models.Account", b =>
                {
                    b.Navigation("Student");
                });

            modelBuilder.Entity("PrintService.Models.Document", b =>
                {
                    b.Navigation("PrintingLogs");
                });

            modelBuilder.Entity("PrintService.Models.Printer", b =>
                {
                    b.Navigation("PrintingLogs");
                });

            modelBuilder.Entity("PrintService.Models.Student", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("PrintingLogs");

                    b.Navigation("PurchaseHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
