using Microsoft.EntityFrameworkCore;


// DbContext är en representation av en session med databasen som möjliggör
// att man interagera och spara instanser av databasen


namespace wpr201_assignment2_asp.Models
{
    // PizzaDBContext ärver från DbContext vilken är en del av Entity Framework
    public class PizzaDbContext : DbContext
    {
        // Konstruktorn tar DbContextOptions som en parameter för att kunna använda
        // dess funktioner
        public PizzaDbContext(DbContextOptions<PizzaDbContext> options) : base(options)
        {
        }

        // DbSet representerar 'Pizzas' databastabellen i databasen
        // Möjliggör interaktion med pizza-tabellen i databasen
        public DbSet<Pizza> Pizzas { get; set; }

        // Möjliggör interaktion med order-tabellen
        public DbSet<Order> Orders { get; set; }
    }
}
