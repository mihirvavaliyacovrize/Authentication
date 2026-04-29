using JwtAuthAPI.Models;

namespace JwtAuthAPI.Repositories.Interfaces;

public interface IJobRepository
{
    Task<List<Job>> GetAll();

    Task<Job?> GetById(int id);

    Task AddJob(Job job);

    void Update(Job job);

    void Delete(Job job);

    Task Save();
}