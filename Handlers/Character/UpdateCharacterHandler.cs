using Models.Character.Requests;
using Models.Character.Responses;

using DataAccess.Interfaces;
using DataAccess.DTO.Character;

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
                var changes = new UpdateCharacterDTO(request.Name,
                                                     request.MaxHealth,
                                                     request.Health,
                                                     request.Level,
                                                     request.ArmorClass,
                                                     request.Attributes,
                                                     request.Skills);

                _repository.Update(request.UserId, request.CharacterId, changes);
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
