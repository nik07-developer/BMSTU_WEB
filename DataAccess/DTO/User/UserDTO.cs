using Models.User;

namespace DataAccess.DTO
{
    public class UserDTO : User
    {
        public UserDTO(Guid id, string login, string password, string name) 
            : base(id, login, password, name)
        {
        }
    }
}
