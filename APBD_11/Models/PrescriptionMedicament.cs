using System.ComponentModel.DataAnnotations;

namespace APBD_11.Models;

public class PrescriptionMedicament
{
    [Key]
    public int IdMedicament { get; set; }
    public int IdPrescription { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; } = string.Empty;
    public Medicament Medicament { get; set; } = null!;
    public Prescription Prescription { get; set; } = null!;
}