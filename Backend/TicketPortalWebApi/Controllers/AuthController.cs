using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace TicketPortalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("{userName}/{role}/{secretKey}")]
        public string GetToken(string userName,string role,string secretKey)
        {
            var securityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials=new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name,userName),
                new Claim(ClaimTypes.Role,role)
            };
            var token=new JwtSecurityToken(
                issuer :"https://www.CapstoneTeam2.com",
                audience:"https://www.CapstoneTeam2.com",
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials,
                claims: claims
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

         }
    }
}
