using apdb_cw11.Data;
using apdb_cw11.Services;
using Microsoft.AspNetCore.Mvc;

namespace apdb_cw11.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IDbService _dbService;

    public PatientsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var patient = await _dbService.GetPatient(id);
        return Ok(patient);
    }
    
}