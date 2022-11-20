using sfl.Models;
using Microsoft.EntityFrameworkCore;

namespace web.Data
{
    public class CompanyContext : DbContext
    {

        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {

        }

        DbSet<Branch> Branches { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Job> Jobs { get; set; }
        DbSet<JobStatus> JobStatuses { get; set; }
        DbSet<JobType> JobTypes { get; set; }
        DbSet<Parcel> Parcels { get; set; }
        DbSet<ParcelStatus> ParcelStatuses { get; set; }
        DbSet<Staff> Staff { get; set; }
        DbSet<StaffRole> StaffRoles { get; set; }
        DbSet<Street> Streets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>().ToTable("Branch");
            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<Job>().ToTable("Job");
            modelBuilder.Entity<JobStatus>().ToTable("JobStatus");
            modelBuilder.Entity<JobType>().ToTable("JobType");
            modelBuilder.Entity<Parcel>().ToTable("Parcel");
            modelBuilder.Entity<ParcelStatus>().ToTable("ParcelStatus");
            modelBuilder.Entity<Staff>().ToTable("Staff");
            modelBuilder.Entity<StaffRole>().ToTable("StaffRole");
            modelBuilder.Entity<Street>().ToTable("Street");

            modelBuilder.Entity<Street>().HasKey(s => new { s.StreetName, s.StreetNumber, s.CityCode });

            modelBuilder.Entity<Parcel>().HasOne(p => p.SenderStreet)
                .WithMany()
                .HasForeignKey("SenderStreetName", "SenderStreetNumber", "SenderCode")
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Parcel>().HasOne(p => p.RecipientStreet)
                .WithMany(s => s.Parcels)
                .HasForeignKey("RecipientStreetName", "RecipientStreetNumber", "RecipientCode")
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Street>().HasOne(s => s.Branch)
                .WithOne(b => b.Street)
                .HasForeignKey<Branch>("StreetName", "StreetNumber", "CityCode");

            // TODO: Add value generation
            // TODO: Create controllers for Job, Staff, Parcel, Street
        }
    }
}