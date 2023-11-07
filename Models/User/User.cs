namespace Models.User
{
    public class User
    {
        public Guid ID { get; }
        public string Login { get; }
        public string Password { get; set; }
        public string Name { get; set; }

        public User(Guid id, string login, string password, string name)
        {
            ID = id;
            Login = login;
            Password = password;
            Name = name;
        }
    }
}