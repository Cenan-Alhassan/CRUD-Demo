using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)    // a derived class from Dbcontext
    {
        public DbSet<Name> Names {get; set;}    // a property of Dbset type with AppUser as entity, properties of AppUser will become columns
    }
}