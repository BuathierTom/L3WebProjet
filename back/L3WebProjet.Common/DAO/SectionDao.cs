namespace L3WebProjet.Common.DAO;

public class SectionDao
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public int Level { get; set; }
    public int UpgradePrice => (int)(100 * Math.Pow(Level, 1.6));
    public bool IsUnderConstruction { get; set; }
    public DateTime? ConstructionEnd { get; set; }
    public Guid StoreId { get; set; }
}