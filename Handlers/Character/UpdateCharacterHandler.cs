using Models.Character.Requests;
using Models.Character.Responses;

using DataAccess.Interfaces;

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
                UpdateCharacter(request);
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

        private void UpdateCharacter(UpdateCharacterRequest rq)
        {
            if (rq.Name != null)
            {
                _repository.UpdateName(rq.UserId, rq.CharacterId, rq.Name);
            }

            if  (rq.MaxHealth != null)
            {
                _repository.UpdateMaxHealth(rq.UserId, rq.CharacterId, (int) rq.MaxHealth);
            }

            if (rq.Health != null)
            {
                _repository.UpdateHealth(rq.UserId, rq.CharacterId, (int) rq.Health);
            }

            if (rq.Level != null)
            {
                _repository.UpdateLevel(rq.UserId, rq.CharacterId, (int)rq.Level);
            }

            if (rq.ArmorClass != null)
            {
                _repository.UpdateArmorClass(rq.UserId, rq.CharacterId, (int)rq.ArmorClass);
            }

            if (rq.Attributes != null)
            {
                _repository.UpdateAttributes(rq.UserId, rq.CharacterId, rq.Attributes);
            }

            if (rq.Skills != null)
            {
                _repository.UpdateSkills(rq.UserId, rq.CharacterId, rq.Skills);
            }
        }
    }
}
