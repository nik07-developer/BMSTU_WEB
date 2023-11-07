namespace Models.User.Responses
{
    public class DeleteUserResponse
    {
        public int Code;
        public Guid ID;

        public DeleteUserResponse(Guid id)
        {
            ID = id;
            Code = 0;
        }

        public DeleteUserResponse(int returnCode)
        {
            Code = returnCode;
        }
    }
}
