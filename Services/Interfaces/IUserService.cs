using JwtAuthAPI.DTOs;

namespace JwtAuthAPI.Services.Interfaces;

public interface IUserService
{
    Task<int> Register(
       RegisterDto dto);

    Task<string> Login(
       LoginDto dto);
    Task<List<UserDto>> GetUsers();

    Task<UserDto?> GetUser(int id);
}