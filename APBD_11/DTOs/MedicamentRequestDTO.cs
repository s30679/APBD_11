namespace APBD_11.DTOs;

public class MedicamentRequestDTO
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; } = string.Empty;
}