//using Microsoft.AspNetCore.Mvc;

//using JwtAuthAPI.DTOs;
//using JwtAuthAPI.Services.Interfaces;

//namespace JwtAuthAPI.Controllers;

//[ApiController]
//[Route("api/auth")]
//public class AuthController
//: ControllerBase
//{
//    private readonly
//    IUserService _service;

//    public AuthController(
//       IUserService service)
//    {
//        _service = service;
//    }

//    [HttpPost("register")]
//    public async Task<IActionResult>
//    Register(RegisterDto dto)
//    {
//        await _service.Register(dto);

//        return Ok(
//        "Registered");
//    }

//    [HttpPost("login")]
//    public async Task<IActionResult>
//    Login(LoginDto dto)
//    {
//        var token =
//        await _service.Login(dto);

//        return Ok(
//        new { Token = token });
//    }
//}

using Microsoft.AspNetCore.Mvc;

using JwtAuthAPI.DTOs;
using JwtAuthAPI.Services.Interfaces;

namespace JwtAuthAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly
    IUserService _service;

    public AuthController(
      IUserService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult>
    Register(RegisterDto dto)
    {
        try
        {
           int userId = await _service.Register(dto);

            return Ok(new
            {
                message = "Registered Successfully",
                id = userId
            });
        }

        catch (Exception ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }


    [HttpPost("login")]
    public async Task<IActionResult>
    Login(LoginDto dto)
    {
        try
        {
            var token =
            await _service.Login(dto);

            return Ok(
            new
            {
                Token = token
            });
        }

        catch (Exception ex)
        {
            return Unauthorized(new
            {
                message = ex.Message
            });
        }
    }
}