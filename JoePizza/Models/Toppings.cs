using System.ComponentModel.DataAnnotations;

namespace JoePizza.Models
{
    public class Toppings
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Topping Name")]
        public string? ToppingsName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        public virtual List<Pizza>? Pizzas { get; set; }
    }
}
