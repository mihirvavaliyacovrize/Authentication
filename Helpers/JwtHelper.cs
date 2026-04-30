using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;

namespace JwtAuthAPI.Helpers;

public static class JwtHelper
{
    public static string GenerateToken(
    int userId,
    string email,
    string role,
    IConfiguration config)
    {
        var claims = new[]
        {
      new Claim(
       ClaimTypes.NameIdentifier,
       userId.ToString()
      ),

      new Claim(
       ClaimTypes.Email,
       email
      ),

      new Claim(
       ClaimTypes.Role,
       role
      )
   };

        var key =
        new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(
        config["Jwt:Key"]!
        ));

        var creds =
        new SigningCredentials(
           key,
           SecurityAlgorithms.HmacSha256
        );

        var token =
        new JwtSecurityToken(
           issuer:
           config["Jwt:Issuer"],

           audience:
           config["Jwt:Audience"],

           claims: claims,

           expires:
           DateTime.Now.AddHours(1),

           signingCredentials:
           creds
        );

        return new JwtSecurityTokenHandler()
        .WriteToken(token);
    }
}