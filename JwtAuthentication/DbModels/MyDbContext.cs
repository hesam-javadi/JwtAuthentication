using Microsoft.EntityFrameworkCore;

namespace JwtAuthentication.DbModels
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public string DbPath { get; set; }
        public MyDbContext()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DbPath = Path.Join(path, "MyDB.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}
