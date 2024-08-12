using Models;
using Models.User.Requests;
using Models.User.Responses;

using DataAccess.Interfaces;
using DataAccess.DTO;

namespace Handlers.User
{
    public class CreateUserHandler
    {
        private readonly IUserRepository _repository;

        public CreateUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public virtual CreateUserResponse Handle(CreateUserRequest request)
        {
            var dto = new CreateUserDTO(request.Login, request.Password, request.Name);
            var response = new CreateUserResponse(Guid.Empty);

            try
            {
                response.ID = _repository.Create(dto);
                response.Code = CreateUserResponse.OK;
            }
            catch(ArgumentOutOfRangeException)
            {
                response.Code = CreateUserResponse.ALREADY_EXISTS;
            }
            catch
            {
                response.Code = CreateUserResponse.DB_ERROR;
            }

            return response;
        }
    }
}
