namespace Models.User.Requests
{
    public class GetUserRequest
    {
        public Guid ID;

        public GetUserRequest(Guid id)
        {
            ID = id;
        }
    }
}
