using Handlers.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Character.Requests;
using Models.Character.Responses;
using Models.User.Requests;
using Models.User.Responses;
using Web.DTO.Character;
using Web.DTO.User;
using Web.DTO.ViewConfig;
using Web.Controllers.Extensions;

using Models.View.Requests;
using Models.View.Responses;

namespace Web.Controllers
{
    [ApiController]
    [Route("/characters/{character_id}/view-configs")]
    public class ViewConfigController : ControllerBase
    {
        private readonly CreateViewHandler _createHandler;
        private readonly DeleteViewHandler _deleteHandler;
        private readonly UpdateViewHandler _updateHandler;
        private readonly GetViewHandler _getHandler;

        public ViewConfigController(CreateViewHandler createHandler,
                                  DeleteViewHandler deleteHandler,
                                   UpdateViewHandler updateHandler,
                                    GetViewHandler getHandler)
        {
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _updateHandler = updateHandler;
            _getHandler = getHandler;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Guid> Post(Guid character_id, [FromBody] ViewConfigCreationDTO viewConfig)
        {
            var rq = new CreateViewRequest(this.GetUID(), character_id, viewConfig.Name, ConvertDTO.Convert(viewConfig.Widgets));
            var res = _createHandler.Handle(rq);

            return res.Code switch
            {
                CreateViewResponse.OK => Ok(),
                CreateViewResponse.ALREADY_EXISTS => Forbid(),
                CreateViewResponse.DB_ERROR => StatusCode(503),
                _ => BadRequest()
            };
        }

        [HttpGet()]
        [Authorize]
        public ActionResult<ViewConfigDTO> Get(Guid character_id, [FromQuery] string platform)
        {
            var rq = new GetViewRequest(this.GetUID(), character_id, platform);
            var res = _getHandler.Handle(rq);

            return res.Code switch
            {
                GetViewResponse.OK => Ok(ConvertDTO.Convert(res.CharacterView)),
                GetViewResponse.NOT_EXISTS => NotFound(),
                GetViewResponse.DB_ERROR => StatusCode(503),
                _ => BadRequest()
            };
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(Guid character_id, [FromQuery] string platform)
        {
            var rq = new DeleteViewRequest(this.GetUID(), character_id, platform);
            var res = _deleteHandler.Handle(rq);

            return res.Code switch
            {
                DeleteViewResponse.OK => Ok(),
                DeleteViewResponse.NOT_EXISTS => NotFound(),
                DeleteViewResponse.DB_ERROR => StatusCode(503),
                _ => BadRequest()
            };
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put(Guid character_id, 
                                [FromQuery] string platform, 
                                [FromBody] ViewConfigReplaceDTO viewConfig)
        {
            var rq = new UpdateViewRequest(this.GetUID(), character_id, platform, ConvertDTO.Convert(viewConfig.Widgets));
            var res = _updateHandler.Handle(rq);

            return res.Code switch
            {
                UpdateViewResponse.OK => Ok(),
                UpdateViewResponse.NOT_EXISTS => NotFound(),
                UpdateViewResponse.DB_ERROR => StatusCode(503),
                _ => BadRequest()
            };
        }
    }
}
