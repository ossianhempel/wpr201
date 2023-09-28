using Microsoft.EntityFrameworkCore;

namespace wpr201_assignment2_asp.Models
{
    public class PizzaDbContext : DbContext
    {
        public PizzaDbContext(DbContextOptions<PizzaDbContext> options) : base(options)
        {
        }

        public DbSet<Pizza> Pizzas { get; set; }
    }
}
