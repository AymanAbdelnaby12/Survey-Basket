using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Survey_Basket.Models;
using Survey_Basket.Persistance.EntitiesConfig;
using System.Reflection;

namespace Survey_Basket.Persistance
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations for entities
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder); 
        }
        public DbSet<Poll> Polls { get; set; }
    }
}
