using Microsoft.AspNetCore.Mvc;

using Web.DTO.User;
using Models.User.Requests;
using Models.User.Responses;
using Handlers.User;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.InteropServices;

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
            //Console.WriteLine($"Hello {user.Name} aka {user.Login}:) your password: $#%{user.Password}^&*");

            var rq = new CreateUserRequest(user.Login, user.Password, user.Name);
            var res = _createHandler.Handle(rq);

            return res.Code switch
            {
                CreateUserResponse.OK => Ok(res.ID),
                CreateUserResponse.ALREADY_EXISTS => BadRequest(),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Authorize]
        public ActionResult<User> Get([FromQuery(Name = "user_id")] Guid userId)
        {
            var rq = new GetUserRequest(userId);
            var res = _getHandler.Handle(rq);

            return res.Code switch
            {
                GetUserResponse.OK => Ok(new User() { ID = res.ID, Login = res.Login, Name = res.Name, Password = res.Password }),
                GetUserResponse.NOT_EXISTS => NotFound(),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Authorize]
        public IActionResult Patch([FromQuery(Name ="user_id")] Guid userId,
                                   [FromBody] Dictionary<string, string> userChanges)
        {
            var rq = new UpdateUserRequest(userId, userChanges);
            var res = _updateHandler.Handle(rq);

            return res.Code switch
            {
                UpdateUserResponse.OK => Ok(),
                UpdateUserResponse.NOT_EXISTS => NotFound(),
                UpdateUserResponse.DB_ERROR => BadRequest(),
                _ => BadRequest()
            };
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete([FromQuery(Name = "user_id")] Guid userId)
        {
            var rq = new DeleteUserRequest(userId);
            var res = _deleteHandler.Handle(rq);

            return res.Code switch
            {
                DeleteUserResponse.OK => Ok(),
                DeleteUserResponse.NOT_EXISTS => NotFound(),
                DeleteUserResponse.DB_ERROR => BadRequest(),
                _ => BadRequest()
            };
        }
    }
}