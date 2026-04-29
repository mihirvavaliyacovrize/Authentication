using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using JwtAuthAPI.Services.Interfaces;

namespace JwtAuthAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UsersController
: ControllerBase
{
    private readonly
    IUserService _service;

    public UsersController(
      IUserService service)
    {
        _service = service;
    }


    // GET api/users
    [HttpGet]
    public async Task<IActionResult>
    GetAllUsers()
    {
        var users =
        await _service.GetUsers();

        return Ok(users);
    }


    // GET api/users/1
    [HttpGet("{id}")]
    public async Task<IActionResult>
    GetUserById(int id)
    {
        var user =
        await _service.GetUser(id);

        if (user == null)
            return NotFound(
            new
            {
                message =
             "User Not Found"
            });

        return Ok(user);
    }
}