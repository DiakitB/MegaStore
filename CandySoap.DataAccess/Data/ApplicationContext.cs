using CandySoap.Models;
using Microsoft.EntityFrameworkCore;

namespace CandySoap.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> option) : base(option) { }
        public DbSet<Covertypes> categories { get; set; }
       public DbSet<Covertype> covertypes { get; set; }
        public DbSet<Product> products { get; set; }  
    }
}
