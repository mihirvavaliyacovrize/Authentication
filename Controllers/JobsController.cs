using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using JwtAuthAPI.DTOs;
using JwtAuthAPI.Services.Interfaces;

namespace JwtAuthAPI.Controllers;

[Authorize(Roles = "Admin,Photographer")]
[ApiController]
[Route("api/jobs")]
public class JobsController : ControllerBase
{
    private readonly IJobService _service;

    public JobsController(IJobService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult>
    GetAll([FromQuery] DateTime? date)
    {
        int userId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        );

        // ADMIN
        if (User.IsInRole("Admin"))
        {
            if (date.HasValue)
            {
                return Ok(
                    await _service.GetJobsByDate(date.Value)
                );
            }

            return Ok(await _service.GetJobs());
        }

        // PHOTOGRAPHER
        if (date.HasValue)
        {
            return Ok(
                await _service.GetJobsByUserAndDate(
                    userId,
                    date.Value
                )
            );
        }

        return Ok(
            await _service.GetJobsByUser(userId)
        );
    }



    [HttpGet("{id}")]
    public async Task<IActionResult>
    Get(int id)
    {
        var job = await _service.GetJob(id);

        if (job == null)
            return NotFound();

        int userId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        );

        if (!User.IsInRole("Admin") &&
            job.CreatedBy != userId)
        {
            return StatusCode(403, new
            {
                message = "You can view only your own jobs"
            });
        }

        return Ok(job);
    }



    // 🔥 FIX HERE → ADMIN ONLY CREATE
    [HttpPost]
    public async Task<IActionResult>
    Create(JobDto dto)
    {
        if (!User.IsInRole("Admin"))
        {
            return StatusCode(403, new
            {
                message = "Only Admin can create jobs"
            });
        }

        int userId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        );

        var job = await _service.CreateJob(dto, userId);

        return Ok(job);
    }



    [HttpPut("{id}")]
    public async Task<IActionResult>
    Update(int id, JobDto dto)
    {
        var job = await _service.GetJob(id);

        if (job == null)
            return NotFound();

        int userId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        );

        if (!User.IsInRole("Admin") &&
            job.CreatedBy != userId)
        {
            return StatusCode(403, new
            {
                message = "You can update only your own jobs"
            });
        }

        var updated = await _service.UpdateJob(id, dto);

        return Ok(updated);
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult>
    Delete(int id)
    {
        if (!User.IsInRole("Admin"))
        {
            return StatusCode(403, new
            {
                message = "Only Admin can delete jobs"
            });
        }

        await _service.DeleteJob(id);

        return NoContent();
    }
}