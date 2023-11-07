using Models.User.Requests;
using Models.User.Responses;

namespace Logic.Handlers.User
{
    public class CreateUserHandler
    {
        public virtual CreateUserResponse Handle(CreateUserRequest request)
        {
            Console.WriteLine("qq");

            return new CreateUserResponse(1);
        }
    }
}
