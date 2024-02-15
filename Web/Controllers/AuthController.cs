using Microsoft.AspNetCore.Mvc;

using Web.DTO.User;
using Microsoft.AspNetCore.Authorization;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MongoDB.Bson.IO;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json.Linq;
using DataAccess.Interfaces;
using Handlers.User;
using Models.User.Requests;
using Models.User.Responses;
using Web.Controllers.Extensions;

namespace Web.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private GetUserHandler _getHandler;

        public AuthController(GetUserHandler authHandler) 
        {
            _getHandler = authHandler;
        }

        [HttpPost("/login")]
		public IActionResult Login([FromBody] UserLoginDTO body)
		{

            var rq = new GetUserRequest(body.Login, body.Password);
            var res = _getHandler.Handle(rq);

            string msg = "";
            if (res.User != null)
            {
                var token = ControllerExtensions.CreateToken(res.User.ID);
                msg = $"{{\"auth_token\":\"{token}\"}}";
            }

            return res.Code switch
            {
                GetUserResponse.OK => Ok(msg),
                GetUserResponse.NOT_EXISTS => NotFound(),
                GetUserResponse.DB_ERROR => StatusCode(503),
                _ => BadRequest()
            };
        }

        [HttpPost("/refresh")]
        [Authorize]
        public IActionResult Refresh()
        {
            var id = this.GetUID();
            var token = ControllerExtensions.CreateToken(id);

            return Ok($"{{\"auth_token\":\"{token}\"}}");
        }
    }
}
