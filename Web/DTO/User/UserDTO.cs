namespace Web.DTO.User
{
    public class UserDTO(Guid id,
                string login,
                string password,
                string name)
    {
        public Guid ID { get; set; } = id;
        public string Login { get; set; } = login;
        public string Password { get; set; } = password;
        public string Name { get; set; } = name;
    }
}
