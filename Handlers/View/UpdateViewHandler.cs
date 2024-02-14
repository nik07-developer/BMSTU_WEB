
using DataAccess.Interfaces;
using Models.View.Responses;
using Models.View.Requests;
using DataAccess.DTO.Converters;

namespace Handlers.View
{
    public class UpdateViewHandler
    {
        private readonly ICharacterViewRepository _repository;

        public UpdateViewHandler(ICharacterViewRepository repository)
        {
            _repository = repository;
        }

        public virtual UpdateViewResponse Handle(UpdateViewRequest request)
        {
            var response = new UpdateViewResponse();

            try
            {
                var dto = request.NewWidgets.Model2DataAccess();
                _repository.Update(request.UserId, request.CharacterId, request.Name, dto);
                
                response.Code = UpdateViewResponse.OK;
            }
            catch (ArgumentException)
            {
                response.Code = UpdateViewResponse.NOT_EXISTS;
            }
            catch
            {
                response.Code = UpdateViewResponse.DB_ERROR;
            }

            return response;
        }
    }
}
