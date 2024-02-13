
namespace Models.Character.Requests
{
    public class GetCharactersRequest
    {
        public Guid UserId;

        public GetCharactersRequest(Guid userId)
        {
            UserId = userId;
        }
    }
}
