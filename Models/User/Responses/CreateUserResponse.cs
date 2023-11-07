namespace Models.User.Responses
{
    public class CreateUserResponse
    {
        public int Code;
        public Guid ID;

        public CreateUserResponse(Guid id)
        {
            ID = id;
            Code = 0;
        }

        public CreateUserResponse(int returnCode)
        {
            Code = returnCode;
        }
    }
}
