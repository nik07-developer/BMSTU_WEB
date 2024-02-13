using Models;
using Models.Character.Requests;
using Models.Character.Responses;

using DataAccess.Interfaces;
using DataAccess.DTO;

namespace Handlers.Character
{
    public class DeleteCharacterHandler
    {
        private readonly ICharacterRepository _repository;

        public DeleteCharacterHandler(ICharacterRepository repository)
        {
            _repository = repository;
        }

        public virtual DeleteCharacterResponse Handle(DeleteCharacterRequest request)
        {
            var response = new DeleteCharacterResponse();

            try
            {
                _repository.Delete(request.UserId, request.CharacterId);
                response.Code = DeleteCharacterResponse.OK;
            }
            catch (ArgumentException)
            {
                response.Code = DeleteCharacterResponse.NOT_EXISTS;
            }
            catch
            {
                response.Code = DeleteCharacterResponse.DB_ERROR;
            }

            return response;
        }
    }
}
