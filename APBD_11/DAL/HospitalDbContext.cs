using APBD_11.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_11.DAL;

public class HospitalDbContext : DbContext
{
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }

    protected HospitalDbContext()
    {
    }

    public HospitalDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrescriptionMedicament>().HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });
        modelBuilder.Entity<PrescriptionMedicament>().HasOne(pm => pm.Medicament).WithMany(m => m.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdMedicament);
        modelBuilder.Entity<PrescriptionMedicament>().HasOne(pm => pm.Prescription).WithMany(p => p.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdPrescription);
        modelBuilder.Entity<Prescription>().HasOne(p => p.Patient).WithMany(pat => pat.Prescriptions)
            .HasForeignKey(p => p.IdPatient);
        modelBuilder.Entity<Prescription>().HasOne(p => p.Doctor).WithMany(d => d.Prescriptions)
            .HasForeignKey(p => p.IdDoctor);
        
        modelBuilder.Entity<Medicament>().HasData(
            new Medicament { IdMedicament = 1, Name = "Paracetamol", Description = "Ból i gorączka", Type = "Tabletki" },
            new Medicament { IdMedicament = 2, Name = "Apap", Description = "Ból głowy", Type = "Tabletki" });

        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { IdDoctor = 1, FirstName = "Anna", LastName = "Kowalska", Email = "anna.kowalska@example.com" },
            new Doctor { IdDoctor = 2, FirstName = "Jan", LastName = "Nowak", Email = "jan.nowak@example.com" });

        modelBuilder.Entity<Patient>().HasData(
            new Patient { IdPatient = 1, FirstName = "Adam", LastName = "Mickiewicz", Birthdate = new DateTime(1900, 1, 1) },
            new Patient { IdPatient = 2, FirstName = "Ewa", LastName = "Konopka", Birthdate = new DateTime(1990, 5, 10) });
    }
}