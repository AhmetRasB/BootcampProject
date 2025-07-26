using Business.Services.Abstracts;
using DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlacklistController : ControllerBase
{
    private readonly IBlacklistService _blacklistService;

    public BlacklistController(IBlacklistService blacklistService)
    {
        _blacklistService = blacklistService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBlacklistRequest request)
    {
        try
        {
            var result = await _blacklistService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _blacklistService.GetByIdAsync(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _blacklistService.GetAllAsync();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _blacklistService.DeleteAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
} 