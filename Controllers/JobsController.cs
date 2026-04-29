using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using JwtAuthAPI.DTOs;
using JwtAuthAPI.Services.Interfaces;

namespace JwtAuthAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/jobs")]
public class JobsController
: ControllerBase
{
    private readonly
    IJobService _service;

    public JobsController(
    IJobService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult>
    GetAll()
    {
        return Ok(await _service.GetJobs());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult>
    Get(int id)
    {
        var job =
        await _service.GetJob(id);

        if (job == null) 
            return NotFound();

        return Ok(job);
    }

    [HttpPost]
    public async Task<IActionResult>
Create(JobDto dto)
    {
        var userIdClaim =
        User.FindFirst(
        ClaimTypes.NameIdentifier
        );

        if (userIdClaim == null)
        {
            return Unauthorized(
            new
            {
                message =
              "User Id claim missing"
            });
        }

        int userId =
        int.Parse(
        userIdClaim.Value
        );

        var job =
        await _service.CreateJob(
        dto,
        userId
        );

        return Ok(job);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult>
    Update(
    int id,
    JobDto dto)
    {
        var job =
        await _service.UpdateJob(
        id, dto);

        if (job == null)
            return NotFound();

        return Ok(job);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult>
    Delete(int id)
    {
        await _service.DeleteJob(id);

        return NoContent();
    }
}