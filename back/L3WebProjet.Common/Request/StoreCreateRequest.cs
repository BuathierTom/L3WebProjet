namespace L3WebProjet.Common.Request
{
    public class StoreCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}