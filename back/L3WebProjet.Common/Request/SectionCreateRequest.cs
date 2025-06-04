namespace L3WebProjet.Common.Request
{
    public class SectionCreateRequest
    {
        public string Type { get; set; } = string.Empty;
        public Guid StoreId { get; set; }
    }
}