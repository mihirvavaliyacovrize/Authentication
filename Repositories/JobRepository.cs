using JwtAuthAPI.Data;
using JwtAuthAPI.Models;
using JwtAuthAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthAPI.Repositories;

public class JobRepository : IJobRepository
{
    private readonly AppDbContext _context;

    public JobRepository(
    AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Job>> GetAll()
    {
        return await _context.Jobs.ToListAsync();
    }

    public async Task<Job?> GetById(int id)
    {
        return await _context.Jobs
        .FirstOrDefaultAsync(
        x => x.JobId == id);
    }

    public async Task AddJob(Job job)
    {
        await _context.Jobs.AddAsync(job);
    }

    public void Update(Job job)
    {
        _context.Jobs.Update(job);
    }

    public void Delete(Job job)
    {
        _context.Jobs.Remove(job);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}