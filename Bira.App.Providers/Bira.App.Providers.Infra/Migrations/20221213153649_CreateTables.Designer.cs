﻿// <auto-generated />
using System;
using Bira.App.Providers.Infra.Repositories.BaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bira.App.Providers.Infra.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20221213153649_CreateTables")]
    partial class CreateTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Bira.App.Providers.Domain.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Complement")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<Guid>("ProviderId")
                        .HasColumnType("uuid");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("varchar(8)");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId")
                        .IsUnique();

                    b.ToTable("Enderecos", (string)null);
                });

            modelBuilder.Entity("Bira.App.Providers.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("DateRegistration")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("ProviderId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.ToTable("Produtos", (string)null);
                });

            modelBuilder.Entity("Bira.App.Providers.Domain.Entities.Provider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("TypeProviders")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Fornecedores", (string)null);
                });

            modelBuilder.Entity("Bira.App.Providers.Domain.Entities.Address", b =>
                {
                    b.HasOne("Bira.App.Providers.Domain.Entities.Provider", "Provider")
                        .WithOne("Address")
                        .HasForeignKey("Bira.App.Providers.Domain.Entities.Address", "ProviderId")
                        .IsRequired();

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("Bira.App.Providers.Domain.Entities.Product", b =>
                {
                    b.HasOne("Bira.App.Providers.Domain.Entities.Provider", "Provider")
                        .WithMany("Products")
                        .HasForeignKey("ProviderId")
                        .IsRequired();

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("Bira.App.Providers.Domain.Entities.Provider", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
