using Microsoft.EntityFrameworkCore;
using TheaterApp.Model;

namespace TheaterApp.Data
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options)
        : base(options)
        {
        }
        
        public DbSet<TheaterEvent?> Events { get; set; }
        
        public DbSet<Ticket?> Tickets { get; set; }
    }
}