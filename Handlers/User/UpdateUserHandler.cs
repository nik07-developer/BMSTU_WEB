using Models.User.Requests;
using Models.User.Responses;
using DataAccess.Interfaces;
using DataAccess.DTO;
using DataAccess.DTO.User;

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
                var changes = new UpdateUserDTO(request.Password, request.Name);
                _repository.Update(request.ID, changes);
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
