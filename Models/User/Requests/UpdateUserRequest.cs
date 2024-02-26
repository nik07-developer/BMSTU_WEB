namespace Models.User.Requests
{
    public class UpdateUserRequest
    {
        public Guid ID;
        public string? Name { get; set; }
        public string? Password { get; set; }

        public UpdateUserRequest(Guid id, string? name = null, string? password = null)
        {
            ID = id;
            Name = name;
            Password = password;
        }
    }
}
