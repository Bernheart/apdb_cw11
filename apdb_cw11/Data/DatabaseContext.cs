using apdb_cw11.Models;
using Microsoft.EntityFrameworkCore;

namespace apdb_cw11.Data;

public class DatabaseContext : DbContext
{
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
        
        protected DatabaseContext() { }
        
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<Doctor>().HasData(new List<Doctor>
                {
                        new Doctor { IdDoctor = 1, FirstName = "Alice", LastName = "Smith", Email = "alice.smith@clinic.com" },
                        new Doctor { IdDoctor = 2, FirstName = "Robert", LastName = "Johnson", Email = "robert.johnson@hospital.com" },
                });

                modelBuilder.Entity<Medicament>().HasData(new List<Medicament>
                {
                        new Medicament { IdMedicament = 1, Name = "Ibuprofen", Description = "Pain reliever", Type = "Tablet" },
                        new Medicament { IdMedicament = 2, Name = "Amoxicillin", Description = "Antibiotic", Type = "Capsule" },
                        new Medicament { IdMedicament = 3, Name = "Paracetamol", Description = "Fever reducer", Type = "Syrup" },
                });

                modelBuilder.Entity<Patient>().HasData(new List<Patient>
                {
                        new Patient { IdPatient = 1, FirstName = "Emily", LastName = "Taylor", Birthdate = new DateTime(1990, 4, 12) },
                        new Patient { IdPatient = 2, FirstName = "David", LastName = "Brown", Birthdate = new DateTime(1985, 8, 22) },
                });

                modelBuilder.Entity<Prescription>().HasData(new List<Prescription>
                {
                        new Prescription { IdPrescription = 1, Date = new DateTime(2024, 5, 1), DueDate = new DateTime(2024, 5, 15), IdPatient = 1, IdDoctor = 1 },
                        new Prescription { IdPrescription = 2, Date = new DateTime(2024, 5, 3), DueDate = new DateTime(2024, 5, 17), IdPatient = 2, IdDoctor = 2 },
                });

                modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>
                {
                        new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 1, Dose = 200, Details = "Take after meals" },
                        new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 1, Dose = 500, Details = "Twice a day" },
                        new PrescriptionMedicament { IdMedicament = 3, IdPrescription = 2, Dose = 10, Details = "Before bedtime" },
                });    
        }
}