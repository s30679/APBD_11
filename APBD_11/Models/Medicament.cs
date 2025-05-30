using System.ComponentModel.DataAnnotations;

namespace APBD_11.Models;

public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}