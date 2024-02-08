using Microsoft.AspNetCore.Mvc;

using Web.DTO.Character;

using Models.Character.Requests;
using Models.Character.Responses;

using Handlers.Character;

using System;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.InteropServices;

namespace Web.Controllers
{
    [ApiController]
    [Route("/characters")]
    public class CharacterController : ControllerBase
    {
        private readonly CreateCharacterHandler _createHandler;

        public CharacterController(CreateCharacterHandler createHandler)
        {
            _createHandler = createHandler;
        }

        [HttpPost]
        public ActionResult<Guid> Post([FromBody] CharacterCreationDTO character)
        {
            //Console.WriteLine($"Hello {user.Name} aka {user.Login}:) your password: $#%{user.Password}^&*");

            var rq = new CreateCharacterRequest(this.GetID(), character.Name, character.Data);
            var res = _createHandler.Handle(rq);

            return res.Code switch
            {
                CreateCharacterResponse.OK => Ok(res.ID),
                CreateCharacterResponse.ALREADY_EXISTS => Forbid(),
                CreateCharacterResponse.DB_ERROR => StatusCode(503),
                _ => BadRequest()
            };
        }
    }
}