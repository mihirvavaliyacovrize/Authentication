using JwtAuthAPI.Models;

namespace JwtAuthAPI.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> GetByEmail(string email);
    Task<List<User>> GetAllUsers();

    Task<User?> GetById(int id);

    Task AddUser(User user);

    Task Save();
}