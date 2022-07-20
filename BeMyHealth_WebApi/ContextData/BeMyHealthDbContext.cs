using BeMyHealth_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeMyHealth_WebApi.ContextData
{
    public class BeMyHealthDbContext : DbContext
    {
        public BeMyHealthDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<CustomSubscription> CustomSubscriptions { get; set; }
        public DbSet<CustomDietPlan> CustomDietPlans { get; set; }
     }
}
