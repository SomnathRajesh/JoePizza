using JoePizza.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JoePizza.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Toppings> Toppings { get; set; }
        public DbSet<Pizza> Pizza { get; set; }

        public DbSet<Size> Sizes { get; set; }

        public DbSet<PizzasInCart> pizzasInCart { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }


    }
}