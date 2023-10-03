using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoePizza.Models
{
    public class PizzasInCart
    {
        public int Id { get; set; }


        [Display(Name = "Pizza")]
        public int PizzaId { get; set; }

        public int Quantity { get; set; }

        [Display(Name = "Topping")]
        public int ToppingId { get; set; }

        [Display(Name = "Size")]
        public int SizeId { get; set; }
        [ForeignKey("ToppingId")]
        public Toppings? Toppings { get; set; }

        [ForeignKey("SizeId")]
        public Size? Size { get; set; }

        [ForeignKey("PizzaId")]
        public Pizza? Pizzas { get; set; }
    }
}
