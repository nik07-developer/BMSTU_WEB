using Models.User;

namespace DataAccess.DTO
{
    public class UserDTO : Models.User.User
    {
        public UserDTO(Guid id, string login, string password, string name) 
            : base(id, login, password, name)
        {
        }
    }
}
