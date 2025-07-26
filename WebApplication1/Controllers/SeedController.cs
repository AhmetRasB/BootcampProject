using Business.Services.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly SeedDataService _seedDataService;

    public SeedController(SeedDataService seedDataService)
    {
        _seedDataService = seedDataService;
    }

    [HttpPost]
    public async Task<IActionResult> SeedData()
    {
        try
        {
            await _seedDataService.SeedDataAsync();
            return Ok(new { message = "Test data seeded successfully!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Error seeding data: {ex.Message}" });
        }
    }
} 