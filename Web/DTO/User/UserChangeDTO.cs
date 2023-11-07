namespace Web.DTO.User
{
    public class UserChangeDTO
    {
        public string Password { get; set; }
        public bool IsPasswordUpdated { get; set; }

        public string Name { get; set; }
        public bool IsNameUpdated { get; set; }
    }
}
