namespace Models.User.Requests
{
    public class UpdateUserRequest
    {
        public Guid ID;
        public Dictionary<string, string> Changes;

        public UpdateUserRequest(Guid id, Dictionary<string, string> changes)
        {
            ID = id;
            Changes = changes;
        }
    }
}
