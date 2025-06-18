namespace L3WebProjet.Common.Request
{
    public class ResourceCreateRequest
    {
        public string Type { get; set; } = string.Empty;
        public int Amount { get; set; }
        public Guid StoreId { get; set; }
    }
}