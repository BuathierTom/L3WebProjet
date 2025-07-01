using L3WebProjet.Common.DTO;
using Microsoft.EntityFrameworkCore;

namespace L3WebProjet.DataAccess
{
    public class VideoclubDbContext : DbContext
    {
        public VideoclubDbContext(DbContextOptions<VideoclubDbContext> options)
            : base(options) {}

        public DbSet<UserDto> Users { get; set; }
        public DbSet<StoreDto> Stores { get; set; }
        public DbSet<SectionDto> Sections { get; set; }
        public DbSet<ResourceDto> Resources { get; set; }
        public DbSet<WarehouseDto> Warehouses { get; set; }
    }
}