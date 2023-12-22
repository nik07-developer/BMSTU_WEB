using Microsoft.AspNetCore.Mvc;

using Web.DTO.User;
using Microsoft.AspNetCore.Authorization;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MongoDB.Bson.IO;
using Microsoft.AspNetCore.Cors;

namespace Web.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(/*CreateUserHandler handler*/)
        {
            //_handler = handler;
        }

        [HttpPost("/login")]
		public IActionResult Login([FromBody] UserLoginDTO body)
		{
            // Mock only user with credentials abc:123
            if (body.Login != "abc" || body.Password != "123")
                return BadRequest();

			Console.WriteLine("Login Call");

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, body.Login), new Claim(AuthOptions.ID_CLAIM_TYPE, Guid.Empty.ToString()) };
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),  // да всё, не рендерятся твои комменты. Используй нормальную кодировку
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Возвращать HTTP response плохо, почекай потом что вернётся
            return Ok($"{{\"auth_token\":\"{token}\"}}");
        }

        [HttpPost("/logout")]
        [Authorize]
        public IActionResult Logout()
        {
            var id = this.GetID();

            Console.WriteLine($"Logout Call for {id}");

            return Ok("foo");
        }
    }
}
