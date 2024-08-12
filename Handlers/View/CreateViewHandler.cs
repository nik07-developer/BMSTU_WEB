
using DataAccess.DTO;
using DataAccess.DTO.Converters;
using DataAccess.Interfaces;
using Models.View.Requests;
using Models.View.Responses;

namespace Handlers.View
{
    public class CreateViewHandler
    {
        private readonly ICharacterViewRepository _repository;

        public CreateViewHandler(ICharacterViewRepository repository)
        {
            _repository = repository;
        }

        public virtual CreateViewResponse Handle(CreateViewRequest request)
        {
            var dto = new CreateCharacterViewDTO(request.Name, WidgetViewConverter.Model2DataAccess(request.WidgetViews));
            var response = new CreateViewResponse();

            try
            {
                _repository.Create(request.UserId, request.CharacterId, dto);
                response.Code = CreateViewResponse.OK;
            }
            catch (ArgumentException)
            {
                response.Code = CreateViewResponse.ALREADY_EXISTS;
            }
            catch
            {
                response.Code = CreateViewResponse.DB_ERROR;
            }

            return response;
        }
    }
}
