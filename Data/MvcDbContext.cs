using AspNetMvcCrud.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcCrud.Data
{

    public class MvcDbContext : DbContext
    {
        public MvcDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
