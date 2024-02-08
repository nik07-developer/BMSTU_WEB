namespace Web.DTO.User
{
    public class User(Guid id,
                string login,
                string password,
                string name)
    {
        public Guid ID = id;
        public string Login = login;
        public string Password = password;
        public string Name = name;
    }
}
