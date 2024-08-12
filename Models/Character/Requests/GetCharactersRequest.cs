
namespace Models.Character.Requests
{
    public class GetCharactersRequest(Guid userId)
    {
        public Guid UserId = userId;
    }
}
