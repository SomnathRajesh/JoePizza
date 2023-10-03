﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JoePizza.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }

        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Display(Name = "Pizza")]
        public int PizzaId { get; set; }

        public int Quantity { get; set; }
        
        public decimal? Price { get; set; }

        [Display(Name ="Topping")]
        public int ToppingId { get; set; }

        [Display(Name = "Size")]
        public int SizeId { get; set; }

        [ForeignKey("OrderId")]
        public Order? Order { get; set; }

        [ForeignKey("ToppingId")]
        public Toppings? Toppings { get; set; }

        [ForeignKey("SizeId")]
        public Size? Size { get; set; }

        [ForeignKey("PizzaId")]
        public Pizza? Pizzas { get; set; }

    }
}
