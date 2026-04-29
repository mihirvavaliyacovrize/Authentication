using JwtAuthAPI.Data;
using JwtAuthAPI.Models;
using JwtAuthAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(
       AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetByEmail(
       string email)
    {
        return await _context.Users
        .FirstOrDefaultAsync(
        x => x.Email == email);
    }

    public async Task AddUser(
       User user)
    {
        await _context.Users
        .AddAsync(user);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
    public async Task<List<User>>
GetAllUsers()
    {
        return await _context.Users
        .ToListAsync();
    }

    public async Task<User?>
    GetById(int id)
    {
        return await _context.Users
        .FirstOrDefaultAsync(
        x => x.Id == id);
    }
}