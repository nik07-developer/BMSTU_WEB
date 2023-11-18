namespace Models.User.Requests
{
    public class UpdateUserRequest
    {
        public Guid ID;

        public string Password;
        public string Name;

        public UpdateUserRequest(Guid id, string password, string name)
        {
            ID = id;
            Password = password;
            Name = name;
        }
    }
}
