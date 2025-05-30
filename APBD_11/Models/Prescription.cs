using System.ComponentModel.DataAnnotations;

namespace APBD_11.Models;

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}