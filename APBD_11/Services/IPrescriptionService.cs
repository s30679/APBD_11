using APBD_11.DTOs;

namespace APBD_11.Services;

public interface IPrescriptionService
{
    Task<int> AddPrescriptionAsync(AddPrescriptionRequestDTO request);
    Task<PatientDetailsDTO?> GetPatientDetailsAsync(int patientId);
}