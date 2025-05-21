using apdb_cw11.Models;

namespace apdb_cw11.DTOs;

public class PostPrescriptionDto
{
    public PostPatientDto Patient { get; set; }
    
    public PostDoctorDto Doctor { get; set; }
    public List<PostMedicamentDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}

public class PostPatientDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
}

public class PostDoctorDto
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}

public class PostMedicamentDto
{
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    public string Description { get; set; }
}