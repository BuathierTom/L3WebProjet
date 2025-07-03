using System.ComponentModel.DataAnnotations.Schema;

namespace L3WebProjet.Common.DTO;

public class WarehouseDto
{
    public Guid Id { get; set; }
    public Guid StoreId { get; set; }
    public int Level { get; set; }
    public int Capacity => Level * 10000;
    public int UpgradeCost => (int)((Level + 1) * 10000 * 0.2);
}