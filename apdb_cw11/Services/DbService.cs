using apdb_cw11.Data;
using apdb_cw11.DTOs;
using apdb_cw11.Models;
using Microsoft.EntityFrameworkCore;

namespace apdb_cw11.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task AddPrescription(PostPrescriptionDto dto)
    {
        if (dto.DueDate < dto.Date)
            throw new ArgumentException("DueDate must be after Date.");

        var patient = await _context.Patients.FindAsync(dto.Patient.IdPatient);
        
        var doctor = await _context.Doctors.FindAsync(dto.Doctor.IdDoctor);

        if (doctor == null)
            throw new ArgumentException("Doctor does not exist.");
        
        var medicamentIds = dto.Medicaments.Select(m => m.IdMedicament).ToList();
        var existingMedicamentIds = await _context.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();

        if (medicamentIds.Except(existingMedicamentIds).Any())
            throw new ArgumentException("One or more medicaments do not exist.");
        
        if (medicamentIds.Count > 10)
            throw new ArgumentException("Too many medicaments.");
        
        if (patient == null)
        {
            patient = new Patient
            {
                IdPatient = dto.Patient.IdPatient,
                FirstName = dto.Patient.FirstName,
                LastName = dto.Patient.LastName,
                Birthdate = dto.Patient.Birthdate
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        // Create Prescription
        var prescription = new Prescription
        {
            Date = dto.Date,
            DueDate = dto.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = doctor.IdDoctor,
            PrescriptionMedicaments = dto.Medicaments.Select(m => new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Description
            }).ToList()
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
    }

    public async Task<GetPatientDto> GetPatient(int id)
    {
       var patient = await _context.Patients.Select(e =>
           new GetPatientDto
           {
               IdPatient = e.IdPatient,
               FirstName = e.FirstName,
               LastName = e.LastName,
               Birthdate = e.Birthdate,
               Prescriptions = e.Prescriptions.Select(a =>
                   new GetPrescriptionDto
                   {
                       IdPrescription = a.IdPrescription,
                       Date = a.Date,
                       DueDate = a.DueDate,
                       Medicaments = a.PrescriptionMedicaments.Select(m => 
                           new GetMedicamantDto
                           {
                               IdMedicament = m.IdMedicament,
                               Name = m.Medicament.Name,
                               Dose = m.Dose,
                               Description = m.Details
                           }).ToList(),
                       Doctor = new GetDoctorDto
                       {
                           IdDoctor = a.IdDoctor,
                           FirstName = a.Doctor.FirstName,
                           LastName = a.Doctor.LastName,
                           Email = a.Doctor.Email,
                       }
               }).OrderBy(a => a.DueDate).ToList()
           }).Where(e => e.IdPatient == id).FirstAsync();
       
       return patient;
    }
}