using Microsoft.AspNetCore.Mvc;

using Web.DTO.User;
using Microsoft.AspNetCore.Authorization;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MongoDB.Bson.IO;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json.Linq;

namespace Web.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController() { }

        [HttpPost("/login")]
		public IActionResult Login([FromBody] UserLoginDTO body)
		{
            // Mock only user with credentials abc:123
            if (body.Login != "abc" || body.Password != "123")
                return Forbid();

            var id = Guid.Empty;
            var token = CreateToken(id);

            return Ok($"{{\"auth_token\":\"{token}\"}}");
        }

        [HttpPost("/refresh")]
        [Authorize]
        public IActionResult Refresh()
        {
            var id = this.GetID();
            var token = CreateToken(id);

            return Ok($"{{\"auth_token\":\"{token}\"}}");
        }

        private static string CreateToken(Guid id)
        {
            var claims = new List<Claim> { new(AuthOptions.ID_CLAIM_TYPE, id.ToString()) };
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),  // да всё, не рендерятся твои комменты. Используй нормальную кодировку
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }
    }
}
