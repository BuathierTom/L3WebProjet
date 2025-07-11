using L3WebProjet.Common.DAO;
using L3WebProjet.Common.DTO;
using Microsoft.EntityFrameworkCore;

namespace L3WebProjet.DataAccess
{
    public class VideoclubDbContext : DbContext
    {
        public VideoclubDbContext(DbContextOptions<VideoclubDbContext> options)
            : base(options) {}

        public DbSet<UserDao> Users { get; set; }
        public DbSet<StoreDao> Stores { get; set; }
        public DbSet<SectionDao> Sections { get; set; }
        public DbSet<ResourceDao> Resources { get; set; }
        public DbSet<WarehouseDao> Warehouses { get; set; }
    }
}