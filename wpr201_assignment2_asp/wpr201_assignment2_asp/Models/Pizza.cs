using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wpr201_assignment2_asp.Models
{
    public class Pizza
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Unique identifier for each pizza

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } // Name of the pizza

        [Required]
        public string Description { get; set; } // Description of the pizza

        [Required]
        public double Price { get; set; } // Price of the pizza

        // Additional fields can go here, like image URLs, ingredients, etc.
    }
}


