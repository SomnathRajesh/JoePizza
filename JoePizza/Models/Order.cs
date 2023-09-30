using System.ComponentModel.DataAnnotations;

namespace JoePizza.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }
        public int Id { get; set; }

        [Required]
        [Display(Name = "Order ID")]
        public string? OrderID { get; set; }

        [Required]
        public decimal? Amount { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual List<OrderDetails>? OrderDetails { get; set; }
    }
}
