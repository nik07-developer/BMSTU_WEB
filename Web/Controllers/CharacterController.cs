using Microsoft.AspNetCore.Mvc;

using Web.DTO.Character;

using Models.Character.Requests;
using Models.Character.Responses;

using Handlers.Character;

using System;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.InteropServices;
using Web.Controllers.Extensions;

namespace Web.Controllers
{
    [ApiController]
    [Route("/api/characters")]
    public class CharacterController : ControllerBase
    {
        private readonly CreateCharacterHandler _createHandler;
        private readonly DeleteCharacterHandler _deleteHandler;
        private readonly UpdateCharacterHandler _updateHandler;
        private readonly GetCharactersHandler _getAllHandler;
        private readonly GetCharacterHandler _getHandler;

        public CharacterController(CreateCharacterHandler createHandler,
                                   DeleteCharacterHandler deleteHandler,
                                   UpdateCharacterHandler updateHandler,
                                   GetCharactersHandler getAllHandler,
                                   GetCharacterHandler getHandler)
        {
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _updateHandler = updateHandler;
            _getAllHandler = getAllHandler;
            _getHandler = getHandler;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Guid> Post([FromBody] CharacterCreationDTO character)
        {
            var rq = new CreateCharacterRequest(this.GetUID(),
                                                character.Name,
                                                character.MaxHealth,
                                                character.Health,
                                                character.Level,
                                                character.ArmorClass,
                                                ConvertDTO.Convert(character.Attributes),
                                                ConvertDTO.Convert(character.Skills));

            var res = _createHandler.Handle(rq);

            return res.Code switch
            {
                CreateCharacterResponse.OK => Ok(res.ID),
                CreateCharacterResponse.ALREADY_EXISTS => Forbid(),
                CreateCharacterResponse.DB_ERROR => StatusCode(503),
                _ => BadRequest()
            };
        }

        [HttpGet("/api/characters")]
        [Authorize]
        public ActionResult<List<CharacterDTO>> GetAll()
        {
            var rq = new GetCharactersRequest(this.GetUID());
            var res = _getAllHandler.Handle(rq);

            return res.Code switch
            {
                GetCharactersResponse.OK => Ok(ConvertDTO.Convert(res.Characters)),
                GetCharactersResponse.NOT_EXISTS => NotFound(),
                GetCharactersResponse.DB_ERROR => StatusCode(503),
                _ => BadRequest()
            };
        }

        [HttpGet("/api/characters/{character_id}")]
        [Authorize]
        public ActionResult<CharacterDTO> Get(Guid character_id)
        {
            var rq = new GetCharacterRequest(this.GetUID(), character_id);
            var res = _getHandler.Handle(rq);

            return res.Code switch
            {
                GetCharacterResponse.OK => Ok(ConvertDTO.Convert(res.Character)),
                GetCharacterResponse.NOT_EXISTS => NotFound(),
                GetCharacterResponse.DB_ERROR => StatusCode(503),
                _ => BadRequest()
            };
        }

        [HttpDelete("/api/characters/{character_id}")]
        [Authorize]
        public IActionResult Delete(Guid character_id)
        {
            var rq = new DeleteCharacterRequest(this.GetUID(), character_id);
            var res = _deleteHandler.Handle(rq);

            return res.Code switch
            {
                DeleteCharacterResponse.OK => Ok(),
                DeleteCharacterResponse.NOT_EXISTS => NotFound(),
                DeleteCharacterResponse.DB_ERROR => StatusCode(503),
                _ => BadRequest()
            };
        }

        [HttpPatch("/api/characters/{character_id}")]
        [Authorize]
        public IActionResult Patch(Guid character_id, 
                                   [FromBody] CharacterChangeDTO changes)
        {

            var rq = new UpdateCharacterRequest(this.GetUID(),
                                                character_id,
                                                changes.Name,
                                                changes.MaxHealth,
                                                changes.Health,
                                                changes.Level,
                                                changes.ArmorClass,
                                                (changes.Attributes == null) ? null : ConvertDTO.Convert(changes.Attributes),
                                                (changes.Skills == null) ? null : ConvertDTO.Convert(changes.Skills));
                                                               
            var res = _updateHandler.Handle(rq);

            return res.Code switch
            {
                UpdateCharacterResponse.OK => Ok(),
                UpdateCharacterResponse.NOT_EXISTS => NotFound(),
                UpdateCharacterResponse.DB_ERROR => StatusCode(503),
                _ => BadRequest()
            };
        }
    }
}