using APBD_11.DAL;
using APBD_11.DTOs;
using APBD_11.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_11.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly HospitalDbContext _context;

    public PrescriptionService(HospitalDbContext context) 
    {
        _context=context;
    }

    public async Task<int> AddPrescriptionAsync(AddPrescriptionRequestDTO request) 
    {
        //1. Walidacja ilości leków
        if(request.Medicaments.Count>10)
        {
            throw new ArgumentException("Recepta może obejmować max 10 leków");
        }
        //2. Walidacja daty
        if(request.DueDate<request.Date)
        {
            throw new ArgumentException("Due Date musi być nie mniejsze niż Date");
        }
        //3. Sprawdzenie/dodanie pacjenta
        Patient pacjent;
        if(request.Patient.IdPatient.HasValue)
        {
            pacjent=await _context.Patients.FindAsync(request.Patient.IdPatient.Value);
            if(pacjent==null)
            {
                pacjent=new Patient
                {
                    FirstName=request.Patient.FirstName, 
                    LastName=request.Patient.LastName, 
                    Birthdate=request.Patient.Birthdate
                };
                _context.Patients.Add(pacjent);
                await _context.SaveChangesAsync();
            }
        }
        else
        {
            pacjent = new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                Birthdate = request.Patient.Birthdate
            }; 
            _context.Patients.Add(pacjent);
            await _context.SaveChangesAsync();
        }
        // 4. Sprawdzenie lekarza (zakładamy, że lekarz musi istnieć)
        var doktor=await _context.Doctors.FindAsync(request.Doctor.IdDoctor);
        if(doktor==null)
        {
            throw new KeyNotFoundException($"Lekarz o Id {request.Doctor.IdDoctor} nie istnieje.");
        }
        // 5. Sprawdzenie leków i dodanie recepty
        var recepta = new Prescription
        {
            Date = request.Date, 
            DueDate = request.DueDate, 
            IdPatient = pacjent.IdPatient, 
            IdDoctor = doktor.IdDoctor, 
            PrescriptionMedicaments = new List<PrescriptionMedicament>()
        };

        foreach(var medicamentRequest in request.Medicaments)
        {
            var lek = await _context.Medicaments.FindAsync(medicamentRequest.IdMedicament); 
            if(lek==null) 
            {
                throw new KeyNotFoundException($"Lek o Id {medicamentRequest.IdMedicament} nie istnieje.");
            }
            recepta.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                IdMedicament = medicamentRequest.IdMedicament, 
                Dose = medicamentRequest.Dose,
                Details = medicamentRequest.Details
            });
        }
        _context.Prescriptions.Add(recepta);
        await _context.SaveChangesAsync();
        return recepta.IdPrescription;
    }

    public async Task<PatientDetailsDTO?> GetPatientDetailsAsync(int patientId)
    {
        var pacjent=await _context.Patients.Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament).Where(p => p.IdPatient == patientId)
            .FirstOrDefaultAsync();
        if(pacjent==null)
        {
            return null;
        }
        var pacjentDTO=new PatientDetailsDTO
        {
            IdPatient=pacjent.IdPatient, 
            FirstName=pacjent.FirstName,
            LastName=pacjent.LastName,
            Birthdate=pacjent.Birthdate,
            Prescriptions=pacjent.Prescriptions.OrderByDescending(p => p.DueDate).Select(pr => new PrescriptionDetailsDTO
            {
                IdPrescription=pr.IdPrescription,
                Date=pr.Date,
                DueDate=pr.DueDate,
                Doctor=new DoctorDetailsDTO
                {
                    IdDoctor=pr.Doctor.IdDoctor,
                    FirstName=pr.Doctor.FirstName,
                    LastName=pr.Doctor.LastName
                },
                Medicaments=pr.PrescriptionMedicaments.Select(pm => new MedicamentDetailsDTO
                {
                    IdMedicament=pm.Medicament.IdMedicament,
                    Name=pm.Medicament.Name,
                    Dose=pm.Dose,
                    Details=pm.Details }).ToList()
            }).ToList()
        };
        return pacjentDTO;
    }
}