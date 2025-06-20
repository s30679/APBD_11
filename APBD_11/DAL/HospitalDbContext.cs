﻿using APBD_11.Models;
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
    }
}