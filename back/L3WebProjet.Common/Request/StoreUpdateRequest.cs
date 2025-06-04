namespace L3WebProjet.Common.Request
{
    public class StoreUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}