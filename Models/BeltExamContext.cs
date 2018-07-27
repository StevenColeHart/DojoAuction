using Microsoft.EntityFrameworkCore;
 
namespace beltexam.Models
{
    public class BeltExamContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public BeltExamContext(DbContextOptions<BeltExamContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products {get; set;}
        public DbSet<Bid> Bids {get; set;}
    }
}