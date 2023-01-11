using CandySoap.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CandySoap.Data
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> option) : base(option) { }
        public DbSet<Category> categories { get; set; }
       public DbSet<Covertype> covertypes { get; set; }
        public DbSet<Product> products { get; set; }  
    }
}
