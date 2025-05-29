namespace APBD_11.DTOs;

public class PatientRequestDTO
{
    public int? IdPatient { get; set; } 
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime Birthdate { get; set; }
}