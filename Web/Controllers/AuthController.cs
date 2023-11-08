using Microsoft.AspNetCore.Mvc;

using Web.DTO.User;
using Models.User.Requests;
using Models.User.Responses;

using Logic.Handlers.User;
using Microsoft.AspNetCore.Authorization;
using System.Net;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Web.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(CreateUserHandler handler)
        {
            //_handler = handler;
        }

        [HttpPost("/login")]
        public HttpResponseMessage Login([FromQuery] string login, [FromQuery] string password)
        {
            Console.WriteLine("Login Call");

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, login), new Claim("id", "id") };
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), // время действия 2 минуты
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            var msg = new HttpResponseMessage();
            msg.Headers.Add("jwt", token);

            return msg;
        }

        [HttpPost("/logout")]
        [Authorize]
        public ActionResult<string> Logout()
        {
            Console.WriteLine("Logout Call");
            return Ok("foo");
        }
    }
}