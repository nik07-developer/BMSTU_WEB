using Models.User.Requests;
using Models.User.Responses;
using DataAccess.Interfaces;
using DataAccess.DTO;
using Models.User;

namespace Handlers.User
{
    public class GetUserHandler
    {
        private readonly IUserRepository _repository;

        public GetUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public virtual GetUserResponse Handle(GetUserRequest request)
        {
            var response = new GetUserResponse();

            try
            {
                var dto = _repository.Get(request.ID);
                response.User = new Models.User.User(dto.ID, dto.Login, dto.Password, dto.Name);
                response.Code = GetUserResponse.OK;
            }
            catch (ArgumentException)
            {
                response.Code = GetUserResponse.NOT_EXISTS;
            }
            catch
            {
                response.Code = GetUserResponse.DB_ERROR;
            }

            return response;
        }
    }
}
