using CuidandoPawsApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CuidandoPawsApi.Infrastructure.Persistence.Context
{
    public class CuidandoPawsContext : DbContext
    {
        public CuidandoPawsContext(DbContextOptions<CuidandoPawsContext> options) : base(options) { }

        #region Models
        public DbSet<Adoption> Adoptions { get; set; }

        public DbSet<Appoinment> Appoinments { get; set; }

        public DbSet<MedicalRecord> MedicalRecords { get; set; }

        public DbSet<Pets> Pets { get; set; }

        public DbSet<Species> Species { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Primary Key
            modelBuilder.Entity<Adoption>()
                .HasKey(x => x.Id)
                .HasAnnotation("Npgsql:IdentityStart", 10000)
                .HasName("PK_Adoption");

            modelBuilder.Entity<Appoinment>()
                .HasKey(x => x.Id)
                .HasAnnotation("Npgsql:IdentityStart", 10000)
                .HasName("PK_Appoinment");

            modelBuilder.Entity<MedicalRecord>()
                .HasKey(x => x.Id)
                .HasAnnotation("Npgsql:IdentityStart", 10000)
                .HasName("PK_MedicalRecord");


            modelBuilder.Entity<Pets>()
                .HasKey(x => x.Id)
                .HasAnnotation("Npgsql:IdentityStart", 10000)
                .HasName("PK_Pet");

            modelBuilder.Entity<Species>()
                .HasKey(x => x.Id)
                .HasAnnotation("Npgsql:IdentityStart", 10000)
                .HasName("PK_Species");

            #endregion

            #region Relations
            modelBuilder.Entity<Pets>()
                        .HasOne(x => x.Species)
                        .WithMany(x => x.Pets)
                        .HasForeignKey(x => x.SpeciesId)
                        .IsRequired()
                        .HasConstraintName("FK_Species");

            modelBuilder.Entity<Adoption>()
                         .HasOne(x => x.Pets)
                         .WithOne(x => x.Adoption)
                         .HasForeignKey<Adoption>(x => x.IdPets)
                         .IsRequired()
                         .HasConstraintName("FK_Pets");

            modelBuilder.Entity<Pets>()
                        .HasOne(x => x.MedicalRecord)
                        .WithOne(x => x.Pet)
                        .HasForeignKey<MedicalRecord>(x => x.IdPet)
                        .IsRequired()
                        .HasConstraintName("FK_Pets"); //Change

            #endregion

            #region Medical Records Property
            modelBuilder.Entity<MedicalRecord>()
               .Property(x => x.Id)
               .IsRequired();

            modelBuilder.Entity<MedicalRecord>()
                .Property(x => x.Treatment)
                .IsRequired();

            modelBuilder.Entity<MedicalRecord>()
                .Property(x => x.Recommendations)
                .IsRequired();
            #endregion

            #region Adoption
            modelBuilder.Entity<Adoption>()
               .Property(x => x.Id)
               .IsRequired();

            modelBuilder.Entity<Adoption>()
               .Property(x => x.Notes)
               .IsRequired();

            #endregion

            #region Species Property
            modelBuilder.Entity<Species>()
                .Property(x => x.Id)
                .IsRequired();

            modelBuilder.Entity<Species>()
                .Property (x => x.Description)
                .IsRequired();
            #endregion

            #region Pets Property
            modelBuilder.Entity<Pets>()
                .Property(x => x.Id)
                .IsRequired();

            modelBuilder.Entity<Pets>()
                .Property(x => x.NamePaws)
                .IsRequired();

            modelBuilder.Entity<Pets>()
                .Property(x => x.Bred)
                .IsRequired();

            modelBuilder.Entity<Pets>()
                .Property(x => x.Notes)
                .IsRequired();

            modelBuilder.Entity<Pets>()
                .Property(x => x.Color)
                .IsRequired();

            modelBuilder.Entity<Pets>()
                .Property(x => x.NamePaws)
                .IsRequired();

            modelBuilder.Entity<Pets>()
                .Property(x => x.Gender)
                .IsRequired();


            #endregion
        }
    }
}
