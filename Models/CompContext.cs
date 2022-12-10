using Microsoft.EntityFrameworkCore;

namespace CompApi.Models
{
    public class CompContext : DbContext
    {
        public CompContext(DbContextOptions<CompContext> options) : base(options) { }
        public DbSet<CompItem> CompItems { get; set; } = null!;
    }
}
