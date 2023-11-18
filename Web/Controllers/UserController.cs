using Microsoft.AspNetCore.Mvc;

using Web.DTO.User;
using Models.User.Requests;
using Models.User.Responses;
using Handlers.User;

namespace Web.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UserController : ControllerBase
    {
        private readonly CreateUserHandler _createHandler;
        private readonly UpdateUserHandler _updateHandler;
        private readonly DeleteUserHandler _deleteHandler;
        private readonly GetUserHandler _getHandler;

        public UserController(CreateUserHandler createHandler,
                              UpdateUserHandler updateHandler,
                              DeleteUserHandler deleteHandler,
                              GetUserHandler getHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getHandler = getHandler;
        }

        [HttpPost]
        public ActionResult<Guid> Post([FromBody] UserCreationDTO user)
        {
            Console.WriteLine($"Hello {user.Name} aka {user.Login}:) your password: $#%{user.Password}^&*");

            var rq = new CreateUserRequest(user.Login, user.Password, user.Name);
            var res = _createHandler.Handle(rq);

            return res.Code switch
            {
                CreateUserResponse.OK => Ok(res.ID),
                CreateUserResponse.ALREADY_EXISTS => BadRequest(),
                _ => BadRequest(),
            };
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