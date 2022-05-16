using Microsoft.EntityFrameworkCore;

namespace TodoWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<TodoWeb> TodoWebs { get; set; }
    }
}
