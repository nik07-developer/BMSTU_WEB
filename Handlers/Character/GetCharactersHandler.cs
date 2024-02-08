using Models.Character;
using Models.Character.Requests;
using Models.Character.Responses;

using DataAccess.Interfaces;
using DataAccess.DTO;

namespace Handlers.Character
{
    public class GetCharactersHandler
    {
        private readonly ICharacterRepository _repository;

        public GetCharactersHandler(ICharacterRepository repository)
        {
            _repository = repository;
        }

        public virtual GetCharactersResponse Handle(GetCharactersRequest request)
        {
            var response = new GetCharactersResponse();

            try
            {
                var list = _repository.GetAll(request.UserId);
                var characters = new List<Models.Character.Character>();

                foreach (var ch in list)
                    characters.Add(ch);

                response.Characters = characters;
                response.Code = GetCharactersResponse.OK;
            }
            catch (ArgumentException)
            {
                response.Code = GetCharactersResponse.NOT_EXISTS;
            }
            catch
            {
                response.Code = GetCharactersResponse.DB_ERROR;
            }

            return response;
        }
    }
}
