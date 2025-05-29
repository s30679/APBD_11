namespace APBD_11.DTOs;

public class PrescriptionDetailsDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<MedicamentDetailsDTO> Medicaments { get; set; } = new List<MedicamentDetailsDTO>();
    public DoctorDetailsDTO Doctor { get; set; } = null!;
}