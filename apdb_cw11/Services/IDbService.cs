using apdb_cw11.DTOs;

namespace apdb_cw11.Services;

public interface IDbService
{
    Task<GetPatientDto> GetPatient(int id);
    
    Task AddPrescription(PostPrescriptionDto prescription);
}