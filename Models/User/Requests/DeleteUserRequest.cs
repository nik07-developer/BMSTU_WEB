namespace Models.User.Requests
{
    public class DeleteUserRequest
    {
        public Guid ID;

        public DeleteUserRequest(Guid id)
        {
            ID = id;
        }
    }
}
