
using DataAccess.Interfaces;
using Models.View.Requests;
using Models.View.Responses;

namespace Handlers.View
{
    public class DeleteViewHandler
    {
        private readonly ICharacterViewRepository _repository;

        public DeleteViewHandler(ICharacterViewRepository repository)
        {
            _repository = repository;
        }

        public virtual DeleteViewResponse Handle(DeleteViewRequest request)
        {
            var response = new DeleteViewResponse();

            try
            {
                _repository.Delete(request.UserId, request.CharacterId, request.Name);
                response.Code = DeleteViewResponse.OK;
            }
            catch (ArgumentOutOfRangeException)
            {
                response.Code = DeleteViewResponse.NOT_EXISTS;
            }
            catch
            {
                response.Code = DeleteViewResponse.DB_ERROR;
            }

            return response;
        }
    }
}
