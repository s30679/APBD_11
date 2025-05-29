namespace APBD_11.DTOs;

public class AddPrescriptionRequestDTO
{
    public PatientRequestDTO Patient { get; set; } = null!;
    public DoctorRequestDTO Doctor { get; set; } = null!;
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<MedicamentRequestDTO> Medicaments { get; set; } = new List<MedicamentRequestDTO>();
}