using Models.Character.Requests;
using Models.Character.Responses;

using DataAccess.Interfaces;
using DataAccess.DTO;

namespace Handlers.Character
{
    public class CreateCharacterHandler
    {
        private readonly ICharacterRepository _repository;

        public CreateCharacterHandler(ICharacterRepository repository)
        {
            _repository = repository;
        }

        public virtual CreateCharacterResponse Handle(CreateCharacterRequest request)
        {
            var dto = new CreateCharacterDTO(request.Name, request.Data);
            var response = new CreateCharacterResponse();

            try
            {
                response.ID = _repository.Create(request.UserId, dto);
                response.Code = CreateCharacterResponse.OK;
            }
            catch (ArgumentException)
            {
                response.Code = CreateCharacterResponse.ALREADY_EXISTS;
            }
            catch
            {
                response.Code = CreateCharacterResponse.DB_ERROR;
            }

            return response;
        }
    }
}
