using System;
using dz3FlightsAPI.DB;
using Microsoft.EntityFrameworkCore;

namespace dz3FlightsAPI.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<Flight> Flight { get; set; }
        public DbSet<Route> Route { get; set; }
        public DbSet<Airport> Airport { get; set; }


        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
