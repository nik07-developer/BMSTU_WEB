using Models.User.Requests;
using Models.User.Responses;
using DataAccess.Interfaces;
using DataAccess.DTO;

namespace Handlers.User
{
    public class DeleteUserHandler
    {
        private readonly IUserRepository _repository;

        public DeleteUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public virtual DeleteUserResponse Handle(DeleteUserRequest request)
        {
            var response = new DeleteUserResponse();

            try
            {
                _repository.Delete(request.ID);
                response.Code = DeleteUserResponse.OK;
            }
            catch (ArgumentOutOfRangeException)
            {
                response.Code = DeleteUserResponse.NOT_EXISTS;
            }
            catch
            {
                response.Code = DeleteUserResponse.DB_ERROR;
            }

            return response;
        }
    }
}
