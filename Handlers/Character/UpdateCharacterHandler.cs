using Models;
using Models.Character.Requests;
using Models.Character.Responses;

using DataAccess.Interfaces;
using DataAccess.DTO;

namespace Handlers.Character
{
    public class UpdateCharacterHandler
    {
        private readonly ICharacterRepository _repository;

        public UpdateCharacterHandler(ICharacterRepository repository)
        {
            _repository = repository;
        }

        public virtual UpdateCharacterResponse Handle(UpdateCharacterRequest request)
        {
            var response = new UpdateCharacterResponse();

            try
            {
                if (request.Changes.TryGetValue("name", out var newName))
                {
                    _repository.UpdateName(request.UserId, request.CharacterId, newName);
                }

                if (request.Changes.TryGetValue("data", out var newData))
                {
                    _repository.UpdateData(request.UserId, request.CharacterId, newData);
                }

                response.Code = UpdateCharacterResponse.OK;
            }
            catch (ArgumentException)
            {
                response.Code = UpdateCharacterResponse.NOT_EXISTS;
            }
            catch
            {
                response.Code = UpdateCharacterResponse.DB_ERROR;
            }

            return response;
        }
    }
}
