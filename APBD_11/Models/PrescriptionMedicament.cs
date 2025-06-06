using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_11.Models;

public class PrescriptionMedicament
{
    [Key,ForeignKey("IdMedicament")]
    public int IdMedicament { get; set; }
    [Key,ForeignKey("IdPrescription")]
    public int IdPrescription { get; set; }
    public int? Dose { get; set; }
    [Required,MaxLength(100)]
    public string Details { get; set; } = string.Empty;
    public Medicament Medicament { get; set; } = null!;
    public Prescription Prescription { get; set; } = null!;
}