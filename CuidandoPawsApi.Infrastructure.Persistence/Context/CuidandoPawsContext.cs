using CuidandoPawsApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CuidandoPawsApi.Infrastructure.Persistence.Context
{
    public class CuidandoPawsContext : DbContext
    {
        public CuidandoPawsContext(DbContextOptions<CuidandoPawsContext> options) : base(options) { }

        #region Models

        public DbSet<Appoinment> Appoinments { get; set; }

        public DbSet<MedicalRecord> MedicalRecords { get; set; }

        public DbSet<Pets> Pets { get; set; }

        public DbSet<Species> Species { get; set; }
        
        public DbSet<ServiceCatalog> ServicesCatalog { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            # region Tables
            modelBuilder.Entity<Appoinment>()
                        .ToTable("Appoinment");
            
            modelBuilder.Entity<MedicalRecord>()
                .ToTable("MedicalRecord");
            
            modelBuilder.Entity<Pets>()
                .ToTable("Pets");
            
            modelBuilder.Entity<ServiceCatalog>()
                .ToTable("ServiceCatalog");
            
            modelBuilder.Entity<Species>()
                .ToTable("Species");
            #endregion
            
            #region Primary Key
            modelBuilder.Entity<Appoinment>()
                .HasKey(x => x.Id)
                .HasName("PK_Appoinment");
            
            modelBuilder.Entity<ServiceCatalog>()
                .HasKey(x => x.Id)
                .HasName("PK_ServiceCatalog");

            modelBuilder.Entity<MedicalRecord>()
                .HasKey(x => x.Id)
                .HasName("PK_MedicalRecord");


            modelBuilder.Entity<Pets>()
                .HasKey(x => x.Id)
                .HasName("PK_Pet");

            modelBuilder.Entity<Species>()
                .HasKey(x => x.Id)
                .HasName("PK_Species");

            #endregion

            #region Relationships
            modelBuilder.Entity<Pets>()
                        .HasOne(x => x.Species)
                        .WithMany(x => x.Pets)
                        .HasForeignKey(x => x.SpeciesId)
                        .IsRequired()
                        .HasConstraintName("FkSpecies");

            modelBuilder.Entity<Pets>()
                        .HasOne(x => x.MedicalRecord)
                        .WithOne(x => x.Pet)
                        .HasForeignKey<MedicalRecord>(x => x.IdPet)
                        .IsRequired()
                        .HasConstraintName("FkMedicalRecord");

            modelBuilder.Entity<Appoinment>()
                        .HasOne(x => x.ServiceCatalog)
                        .WithMany(x => x.Appoinment)
                        .HasForeignKey(x => x.IdServiceCatalog)
                        .IsRequired()
                        .HasConstraintName("FkServiceCatalog");
            #endregion

            #region Medical Records Property
            modelBuilder.Entity<MedicalRecord>()
               .Property(x => x.Id)
               .HasAnnotation("Npgsql:IdentityStart", 10000)
               .ValueGeneratedOnAdd()
               .IsRequired();

            modelBuilder.Entity<MedicalRecord>()
                .Property(x => x.Treatment)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<MedicalRecord>()
                .Property(x => x.Recommendations)
                .HasMaxLength(150)
                .IsRequired();
            
            modelBuilder.Entity<MedicalRecord>()
                        .Property(x => x.IdPet)
                        .IsRequired();
            #endregion

            #region Service Catalog Property
            modelBuilder.Entity<ServiceCatalog>()
                .Property(x => x.Id)
                .HasAnnotation("Npgsql:IdentityStart", 10000)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<ServiceCatalog>()
                .Property(x => x.NameService)
                .HasMaxLength(50)
                .IsRequired();
            
            modelBuilder.Entity<ServiceCatalog>()
                .Property(x => x.Description)
                .HasColumnType("Text")
                .IsRequired();

            modelBuilder.Entity<ServiceCatalog>()
                .Property(x => x.Price)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<ServiceCatalog>()
                .Property(x => x.Type)
                .HasMaxLength(15);
            
            modelBuilder.Entity<ServiceCatalog>()
                        .HasIndex(x => x.NameService)
                        .IsUnique();
            
            modelBuilder.Entity<ServiceCatalog>()
                        .Property(x => x.Duration)
                        .IsRequired();
            modelBuilder.Entity<ServiceCatalog>()
                .Property(x => x.IsAvaible);
            #endregion

            #region Species Property
            modelBuilder.Entity<Species>()
                .Property(x => x.Id)
                .HasAnnotation("Npgsql:IdentityStart", 10000)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<Species>()
                .Property (x => x.Description)
                .HasMaxLength(150)
                .IsRequired();            
            #endregion

            #region Pets Property
            modelBuilder.Entity<Pets>()
                .Property(x => x.Id)
                .HasAnnotation("Npgsql:IdentityStart", 10000)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<Pets>()
                .Property(x => x.NamePaws)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Pets>()
                .Property(x => x.Bred)
                .HasMaxLength(75)
                .IsRequired();

            modelBuilder.Entity<Pets>()
                .Property(x => x.Notes)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Pets>()
                .Property(x => x.Color)
                .HasMaxLength(25)
                .IsRequired();

            modelBuilder.Entity<Pets>()
                .Property(x => x.Gender)
                .HasMaxLength(25)
                .IsRequired();
            
            modelBuilder.Entity<Pets>()
                        .Property(x => x.SpeciesId)
                        .IsRequired();
            #endregion

            #region Appoinment
            
            modelBuilder.Entity<Appoinment>()
                        .Property(x => x.Id)
                        .HasAnnotation("Npgsql:IdentityStart", 10000)
                        .ValueGeneratedOnAdd()
                        .IsRequired();
            
            modelBuilder.Entity<Appoinment>()
                        .Property(x => x.ReservationDate)
                        .IsRequired();

            modelBuilder.Entity<Appoinment>()
                .Property(x => x.Notes)
                .HasMaxLength(75);
                        
            modelBuilder.Entity<Appoinment>()
                        .Property(x => x.IdServiceCatalog)
                        .IsRequired();
            #endregion
        }
    }
}
