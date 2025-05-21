using apdb_cw11.DTOs;
using apdb_cw11.Models;
using apdb_cw11.Services;
using Microsoft.AspNetCore.Mvc;

namespace apdb_cw11.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionsController : ControllerBase
{
    private readonly IDbService _dbService;

    public PrescriptionsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostPrescriptionDto prescriptionDto)
    {
        try
        {
            await _dbService.AddPrescription(prescriptionDto);
            return Ok("Prescription added successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to add prescription: {ex.Message}");
        }
        
    }
    
}