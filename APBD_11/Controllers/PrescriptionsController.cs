using APBD_11.DTOs;
using APBD_11.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionsController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionRequestDTO request)
    {
        try
        {
            var receptaID = await _prescriptionService.AddPrescriptionAsync(request);
            return CreatedAtAction(nameof(GetPatientDetails), new { patientId = request.Patient.IdPatient }, receptaID);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet("{patientId}")]
    public async Task<IActionResult> GetPatientDetails(int patientId)
    {
        var pacjent=await _prescriptionService.GetPatientDetailsAsync(patientId);
        if(pacjent==null)
        {
            return NotFound($"Pacjent o Id {patientId} nie istnieje");
        }
        return Ok(pacjent);
    }
}