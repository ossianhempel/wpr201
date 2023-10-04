using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wpr201_assignment2_asp.Models
{
    public class Pizza
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Unik identifierare för varje pizza

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } // Pizzans namn

        [Required]
        public string Description { get; set; } // Pizzans beskrivning

        [Required]
        public double Price { get; set; } // Pizzans pris

        public ICollection<Order> Orders { get; set; }

    }
}


