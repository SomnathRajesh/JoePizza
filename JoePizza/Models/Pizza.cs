using System.ComponentModel.DataAnnotations;

namespace JoePizza.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Pizza Name")]
        public string? PizzaName { get; set; }

        public string? Image { get; set; }

        //[Required]
        //[RegularExpression(@"large|medium|small")]
        //public string? Size { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        public virtual List<Toppings>? Toppings { get; set; }

        public virtual List<Size> Sizes { get; set; }
    }
}
