namespace APBD_11.DTOs;

public class MedicamentDetailsDTO
{
    public int IdMedicament { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Dose { get; set; }
    public string Details { get; set; } = string.Empty;
}