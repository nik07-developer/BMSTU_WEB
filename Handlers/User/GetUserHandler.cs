using Models.User.Requests;
using Models.User.Responses;
using DataAccess.Interfaces;
using DataAccess.DTO;

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
            GetUserResponse response;

            try
            {
                var dto = _repository.Get(request.ID);
                response = new GetUserResponse(dto.ID, dto.Login, dto.Password, dto.Name);
            }
            catch (ArgumentException)
            {
                response = new GetUserResponse(GetUserResponse.NOT_EXISTS);
            }
            catch
            {
                response = new GetUserResponse(GetUserResponse.DB_ERROR);
            }

            return response;
        }
    }
}
