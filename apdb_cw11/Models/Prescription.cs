using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apdb_cw11.Models;

[PrimaryKey(nameof(IdPrescription))]
[Table(nameof(Prescription))]
public class Prescription
{
    public int IdPrescription { get; set; }
    [Column(TypeName = "date")]
    public DateTime Date { get; set; }
    [Column(TypeName = "date")]
    public DateTime DueDate { get; set; }
    
    [ForeignKey(nameof(Patient))]
    public int IdPatient { get; set; }
    [ForeignKey(nameof(Doctor))]
    public int IdDoctor { get; set; }
    
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
    
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
}