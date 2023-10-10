using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wpr201_assignment2_asp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; } // Unik identifierare 

        [ForeignKey("Pizza")]
        public int PizzaId { get; set; } // Foreign nyckel som kopplar ordern till en specifik pizza

        public DateTime OrderDate { get; set; } // Datum och tid när ordern gjordes

        public string CustomerName { get; set; } // Kundens namn

        public string Address { get; set; } // Kundens adress

        public int Quantity { get; set; } // Antal pizzor i ordern

        public Pizza Pizza { get; set; } // Egenskap för att nå data kopplad till ordern
    }
}
