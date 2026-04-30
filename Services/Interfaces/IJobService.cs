using JwtAuthAPI.DTOs;
using JwtAuthAPI.Models;

namespace JwtAuthAPI.Services.Interfaces;

public interface IJobService
{
    Task<List<Job>> GetJobs();

    Task<Job?> GetJob(int id);

    Task<Job> CreateJob(
JobDto dto,
int userId);
    Task<Job?> UpdateJob(
    int id,
    JobDto dto);

    Task DeleteJob(int id);

    Task<List<Job>> GetJobsByUser(
int userId);

    Task<List<Job>> GetJobsByDate(
DateTime date);

    Task<List<Job>> GetJobsByUserAndDate(
    int userId,
    DateTime date);
}