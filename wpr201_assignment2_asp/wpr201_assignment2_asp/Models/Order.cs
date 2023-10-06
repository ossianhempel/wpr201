using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wpr201_assignment2_asp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Pizza")]
        public int PizzaId { get; set; }

        public DateTime OrderDate { get; set; }

        public string CustomerName { get; set; }

        public string Address { get; set; }

        public int Quantity { get; set; }

        public Pizza Pizza { get; set; }
    }
}

