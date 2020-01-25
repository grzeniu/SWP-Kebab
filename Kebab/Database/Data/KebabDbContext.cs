using System.Data.Entity;
using Kebab.Database.Models;

namespace Kebab.Database.Data
{
    internal class KebabDbContext : DbContext
    {
        public DbSet<OrderDto> OrderDtos { get; set; }
        public DbSet<Metadata> Metadatas { get; set; }

    }
}
