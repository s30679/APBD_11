namespace APBD_11.Models;

public class Doctor
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}