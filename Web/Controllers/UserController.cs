using Microsoft.AspNetCore.Mvc;

using Web.DTO.User;
using Models.User.Requests;
using Models.User.Responses;
using Logic.Handlers.User;

namespace Web.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UserController : ControllerBase
    {
        private readonly CreateUserHandler _handler;

        public UserController(CreateUserHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public ActionResult<Guid> Post([FromBody] UserCreationDTO user)
        {
            Console.WriteLine($"Hello {user.Name} aka {user.Login}:) your password: $#%{user.Password}^&*");

            var rq = new CreateUserRequest(user.Login, user.Password, user.Name);
            var res = _handler.Handle(rq);

            if (res.Code != 0)
                return BadRequest();

            return Ok(res.ID);
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            Console.WriteLine("GET");

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            //response.Headers.Add("Access-Control-Allow-Origin", "*");
            return response;
        }

        /*[HttpPatch]
        public ActionResult<Guid> Patch([FromBody] UserChangeDTO user)
        {
            Console.WriteLine($"Hello {user.Name} aka {user.Login}:) your password: $#%{user.Password}^&*");

            var rq = new CreateUserRequest(user.Login, user.Password, user.Name);
            var res = _handler.Handle(rq);

            if (res.Code != 0)
                return BadRequest();

            //return Ok(res.ID);
        }*/

    }
}