using Models.User.Requests;
using Models.User.Responses;
using DataAccess.Interfaces;
using DataAccess.DTO;

namespace Handlers.User
{
    public class UpdateUserHandler
    {
        private readonly IUserRepository _repository;

        public UpdateUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public virtual UpdateUserResponse Handle(UpdateUserRequest request)
        {
            var response = new UpdateUserResponse();

            try
            {
                if (request.Name != null)
                {
                    _repository.UpdateName(request.ID, request.Name);
                }

                if (request.Password != null)
                {
                    _repository.UpdateName(request.ID, request.Password);
                }

                response.Code = UpdateUserResponse.OK;
            }
            catch (ArgumentOutOfRangeException)
            {
                response.Code = UpdateUserResponse.NOT_EXISTS;
            }
            catch
            {
                response.Code = UpdateUserResponse.DB_ERROR;
            }

            return response;
        }
    }
}
