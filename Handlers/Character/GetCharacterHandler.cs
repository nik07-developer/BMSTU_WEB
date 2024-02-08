using Models;
using Models.Character.Requests;
using Models.Character.Responses;

using DataAccess.Interfaces;
using DataAccess.DTO;

namespace Handlers.Character
{
    public class GetCharacterHandler
    {
        private readonly ICharacterRepository _repository;

        public GetCharacterHandler(ICharacterRepository repository)
        {
            _repository = repository;
        }

        public virtual GetCharacterResponse Handle(GetCharacterRequest request)
        {
            var response = new GetCharacterResponse();

            try
            {
                response.Character = _repository.Get(request.UserId, request.CharacterId);
                response.Code = GetCharacterResponse.OK;
            }
            catch (ArgumentException)
            {
                response.Code = GetCharacterResponse.NOT_EXISTS;
            }
            catch
            {
                response.Code = GetCharacterResponse.DB_ERROR;
            }

            return response;
        }
    }
}
