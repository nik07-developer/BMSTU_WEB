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
                var dto = new UpdateUserDTO(); // ToDo
                _repository.Update(request.ID, dto);
            }
            catch (ArgumentException)
            {
                response = new UpdateUserResponse(UpdateUserResponse.NOT_EXISTS);
            }
            catch
            {
                response = new UpdateUserResponse(UpdateUserResponse.DB_ERROR);
            }

            return response;
        }
    }
}
