using System.ComponentModel.DataAnnotations;

namespace JoePizza.Models
{
    public class Size
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Pizza Size")]
        public string? PizzaSize { get; set; }
    }
}
