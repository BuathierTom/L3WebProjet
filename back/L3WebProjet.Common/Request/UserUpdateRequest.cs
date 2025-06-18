namespace L3WebProjet.Common.Request
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        public string Pseudo { get; set; } = string.Empty;
    }
}