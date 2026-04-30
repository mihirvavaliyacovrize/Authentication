using JwtAuthAPI.DTOs;
using JwtAuthAPI.Models;
using JwtAuthAPI.Helpers;

using JwtAuthAPI.Repositories.Interfaces;
using JwtAuthAPI.Services.Interfaces;

namespace JwtAuthAPI.Services;

public class UserService : IUserService
{
    private readonly
    IUserRepository _repo;

    private readonly
    IConfiguration _config;

    public UserService(
       IUserRepository repo,
       IConfiguration config)
    {
        _repo = repo;
        _config = config;
    }

    public async Task<int> Register(
       RegisterDto dto)
    {
        var existing =
        await _repo.GetByEmail(
        dto.Email);

        if (existing != null)
            throw new Exception(
            "User Exists");

        User user = new User
        {
            Name = dto.Name,

            Email = dto.Email,

            PasswordHash =
           BCrypt.Net.BCrypt
           .HashPassword(
           dto.Password),

            Role = dto.Role
        };

        await _repo.AddUser(user);

        await _repo.Save();

        return user.Id;
    }

    public async Task<string> Login(
       LoginDto dto)
    {
        var user =
        await _repo.GetByEmail(
        dto.Email);

        if (user == null)
            throw new ApplicationException(
            "Invalid User");

        bool valid =
        BCrypt.Net.BCrypt.Verify(
        dto.Password,
        user.PasswordHash);

        if (!valid)
            throw new ApplicationException(
            "Wrong Password");

        return JwtHelper.GenerateToken(
            user.Id,
           user.Email,
           user.Role,
           _config);
    }
    public async Task<List<UserDto>>
GetUsers()
    {
        var users =
        await _repo.GetAllUsers();

        return users.Select(x =>
        new UserDto
        {
            Id = x.Id,
            Name = x.Name,
            Email = x.Email
        }).ToList();
    }


    public async Task<UserDto?>
    GetUser(int id)
    {
        var user =
        await _repo.GetById(id);

        if (user == null)
            return null;

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}