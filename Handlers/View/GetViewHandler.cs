
using DataAccess.Interfaces;
using Models.View.Requests;
using Models.View.Responses;

namespace Handlers.View
{
    public class GetViewHandler
    {
        private readonly ICharacterViewRepository _repository;

        public GetViewHandler(ICharacterViewRepository repository)
        {
            _repository = repository;
        }

        public virtual GetViewResponse Handle(GetViewRequest request)
        {
            var response = new GetViewResponse();

            try
            {
                response.CharacterView = _repository.Get(request.UserId, request.CharacterId, request.Name);
                response.Code = GetViewResponse.OK;
            }
            catch (ArgumentException)
            {
                response.Code = GetViewResponse.NOT_EXISTS;
            }
            catch
            {
                response.Code = GetViewResponse.DB_ERROR;
            }

            return response;
        }
    }
}
