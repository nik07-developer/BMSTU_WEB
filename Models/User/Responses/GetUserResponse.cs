namespace Models.User.Responses
{
    public class UpdateUserResponse
    {
        public int Code;
        public Guid ID;

        public UpdateUserResponse(Guid id)
        {
            ID = id;
            Code = 0;
        }

        public UpdateUserResponse(int returnCode)
        {
            Code = returnCode;
        }
    }
}
