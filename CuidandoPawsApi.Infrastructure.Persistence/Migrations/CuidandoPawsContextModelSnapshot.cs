﻿// <auto-generated />
using System;
using CuidandoPawsApi.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CuidandoPawsApi.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(CuidandoPawsContext))]
    partial class CuidandoPawsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CuidandoPawsApi.Domain.Models.Appoinment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("IdServiceCatalog")
                        .HasColumnType("integer");

                    b.Property<string>("Notes")
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id")
                        .HasName("PK_Appoinment")
                        .HasAnnotation("Npgsql:IdentityStart", 20000);

                    b.HasIndex("IdServiceCatalog");

                    b.ToTable("Appoinment", (string)null);
                });

            modelBuilder.Entity("CuidandoPawsApi.Domain.Models.MedicalRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("IdPet")
                        .HasColumnType("integer");

                    b.Property<string>("Recommendations")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("Treatment")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id")
                        .HasName("PK_MedicalRecord")
                        .HasAnnotation("Npgsql:IdentityStart", 10000);

                    b.HasIndex("IdPet")
                        .IsUnique();

                    b.ToTable("MedicalRecord", (string)null);
                });

            modelBuilder.Entity("CuidandoPawsApi.Domain.Models.Pets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("AdoptionStatus")
                        .HasColumnType("boolean");

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Bred")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfEntry")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<string>("NamePaws")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("SpeciesId")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("PK_Pet")
                        .HasAnnotation("Npgsql:IdentityStart", 10000);

                    b.HasIndex("SpeciesId");

                    b.ToTable("Pets", (string)null);
                });

            modelBuilder.Entity("CuidandoPawsApi.Domain.Models.ServiceCatalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("Text");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAvaible")
                        .HasColumnType("boolean");

                    b.Property<string>("NameService")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Type")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.HasKey("Id")
                        .HasName("PK_ServiceCatalog")
                        .HasAnnotation("Npgsql:IdentityStart", 10000);

                    b.HasIndex("NameService")
                        .IsUnique();

                    b.ToTable("ServiceCatalog", (string)null);
                });

            modelBuilder.Entity("CuidandoPawsApi.Domain.Models.Species", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<DateTime?>("EntryOfSpecie")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id")
                        .HasName("PK_Species")
                        .HasAnnotation("Npgsql:IdentityStart", 10000);

                    b.ToTable("Species", (string)null);
                });

            modelBuilder.Entity("CuidandoPawsApi.Domain.Models.Appoinment", b =>
                {
                    b.HasOne("CuidandoPawsApi.Domain.Models.ServiceCatalog", "ServiceCatalog")
                        .WithMany("Appoinment")
                        .HasForeignKey("IdServiceCatalog")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ServiceCatalog");

                    b.Navigation("ServiceCatalog");
                });

            modelBuilder.Entity("CuidandoPawsApi.Domain.Models.MedicalRecord", b =>
                {
                    b.HasOne("CuidandoPawsApi.Domain.Models.Pets", "Pet")
                        .WithOne("MedicalRecord")
                        .HasForeignKey("CuidandoPawsApi.Domain.Models.MedicalRecord", "IdPet")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Pets");

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("CuidandoPawsApi.Domain.Models.Pets", b =>
                {
                    b.HasOne("CuidandoPawsApi.Domain.Models.Species", "Species")
                        .WithMany("Pets")
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Species");

                    b.Navigation("Species");
                });

            modelBuilder.Entity("CuidandoPawsApi.Domain.Models.Pets", b =>
                {
                    b.Navigation("MedicalRecord");
                });

            modelBuilder.Entity("CuidandoPawsApi.Domain.Models.ServiceCatalog", b =>
                {
                    b.Navigation("Appoinment");
                });

            modelBuilder.Entity("CuidandoPawsApi.Domain.Models.Species", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
