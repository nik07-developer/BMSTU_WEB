using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Web.Controllers
{
    public static class ControllerExtensions
    {
        public static Guid GetUID(this ControllerBase controller)
        {
            var token = controller.HttpContext.Request.Headers.Authorization.ToString().Split()[1];
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var id = jwt.Claims.First(x => x.Type == AuthOptions.ID_CLAIM_TYPE).Value;
            return Guid.Parse(id);
        }

        public static string CreateToken(Guid id)
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
