using JwtAuthAPI.DTOs;
using JwtAuthAPI.Models;
using JwtAuthAPI.Repositories.Interfaces;
using JwtAuthAPI.Services.Interfaces;

namespace JwtAuthAPI.Services;

public class JobService : IJobService
{
    private readonly IJobRepository _repo;

    public JobService(IJobRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Job>> GetJobs()
    {
        return await _repo.GetAll();
    }

    public async Task<Job?> GetJob(int id)
    {
        return await _repo.GetById(id);
    }

    public async Task<Job> CreateJob(JobDto dto, int userId)
    {
        Job job = new Job
        {
            ShootId = dto.ShootId,
            PhotographerId = dto.PhotographerId,
            ShootDate = dto.ShootDate,
            EventType = dto.EventType,
            ShootCategory = dto.ShootCategory,
            SaleType = dto.SaleType,
            Status = dto.Status,
            CreatedBy = userId
        };

        await _repo.AddJob(job);
        await _repo.Save();

        return job;
    }

    public async Task<Job?> UpdateJob(int id, JobDto dto)
    {
        var job = await _repo.GetById(id);

        if (job == null)
            return null;

        job.ShootId = dto.ShootId;
        job.ShootDate = dto.ShootDate;
        job.EventType = dto.EventType;
        job.ShootCategory = dto.ShootCategory;
        job.SaleType = dto.SaleType;
        job.Status = dto.Status;

        _repo.Update(job);
        await _repo.Save();

        return job;
    }

    public async Task DeleteJob(int id)
    {
        var job = await _repo.GetById(id);

        if (job != null)
        {
            _repo.Delete(job);
            await _repo.Save();
        }
    }

    // 🔥 ONLY OWN JOBS
    public async Task<List<Job>> GetJobsByUser(int userId)
    {
        var jobs = await _repo.GetAll();

        return jobs
            .Where(x => x.PhotographerId == userId)
            .ToList();
    }

    // 🔥 ALL JOBS BY DATE (FIXED)
    public async Task<List<Job>> GetJobsByDate(DateTime date)
    {
        var jobs = await _repo.GetAll();

        DateTime start = date.Date;
        DateTime end = start.AddDays(1);

        return jobs
            .Where(x =>
                x.ShootDate >= start &&
                x.ShootDate < end
            )
            .ToList();
    }
}