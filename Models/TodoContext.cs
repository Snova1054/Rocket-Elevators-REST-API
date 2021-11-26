using Microsoft.EntityFrameworkCore;

namespace RocketElevatorsRESTAPI.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        // Add a new Dbset<> before create a new controllers.
        public DbSet<Building> buildings { get; set; }
        public DbSet<Battery> batteries { get; set; }
        public DbSet<Elevator> elevators { get; set; }
        public DbSet<Column> columns { get; set; }
        public DbSet<Lead> leads { get; set; }
        public DbSet<Customer> customers { get; set; }
    }
}