﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using miniprojeto_samsys.DAL;

#nullable disable

namespace miniprojeto_samsys.API.Migrations
{
    [DbContext(typeof(DDDSample1DbContext))]
    partial class DDDSample1DbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("miniprojeto_samsys.Infrastructure.Entities.Authors.Author", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("miniprojeto_samsys.Infrastructure.Entities.Books.Book", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BookAuthorID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit")
                        .HasColumnName("isActive");

                    b.HasKey("Id");

                    b.HasIndex("BookAuthorID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("miniprojeto_samsys.Infrastructure.Entities.Authors.Author", b =>
                {
                    b.OwnsOne("miniprojeto_samsys.Infrastructure.Entities.Authors.AuthorName", "AuthorName", b1 =>
                        {
                            b1.Property<string>("AuthorId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("_AuthorName")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("AuthorId");

                            b1.ToTable("Authors");

                            b1.WithOwner()
                                .HasForeignKey("AuthorId");
                        });

                    b.Navigation("AuthorName");
                });

            modelBuilder.Entity("miniprojeto_samsys.Infrastructure.Entities.Books.Book", b =>
                {
                    b.HasOne("miniprojeto_samsys.Infrastructure.Entities.Authors.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("BookAuthorID");

                    b.OwnsOne("miniprojeto_samsys.Infrastructure.Entities.Books.BookName", "BookName", b1 =>
                        {
                            b1.Property<string>("BookId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("_BookName")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BookId");

                            b1.ToTable("Books");

                            b1.WithOwner()
                                .HasForeignKey("BookId");
                        });

                    b.OwnsOne("miniprojeto_samsys.Infrastructure.Entities.Books.BookPrice", "BookPrice", b1 =>
                        {
                            b1.Property<string>("BookId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("_BookPrice")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BookId");

                            b1.ToTable("Books");

                            b1.WithOwner()
                                .HasForeignKey("BookId");
                        });

                    b.Navigation("Author");

                    b.Navigation("BookName");

                    b.Navigation("BookPrice");
                });

            modelBuilder.Entity("miniprojeto_samsys.Infrastructure.Entities.Authors.Author", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}