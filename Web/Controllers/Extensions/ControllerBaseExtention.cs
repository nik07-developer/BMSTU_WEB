using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Web.Controllers
{
    public static class ControllerBaseExtension
    {
        public static Guid GetID(this ControllerBase controller)
        {
            var token = controller.HttpContext.Request.Headers.Authorization.ToString().Split()[1];
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var id = jwt.Claims.First(x => x.Type == AuthOptions.ID_CLAIM_TYPE).Value;
            return Guid.Parse(id);
        }
    }
}
