namespace Models.User.Responses
{
    public class GetUserResponse
    {
        public int Code;
        public Guid ID;

        public GetUserResponse(Guid id)
        {
            ID = id;
            Code = 0;
        }

        public GetUserResponse(int returnCode)
        {
            Code = returnCode;
        }
    }
}
