using Microsoft.EntityFrameworkCore;
using SMODotNetCore.RestApi.ConnectionManager;
using SMODotNetCore.RestApi.Models;

namespace SMODotNetCore.RestApi.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }
        */
        public DbSet<BlogDto> Blogs { get; set; }   
    }
}
