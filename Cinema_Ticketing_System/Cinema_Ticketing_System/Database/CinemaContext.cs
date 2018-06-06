using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema_Ticketing_System.Models;
using MySql.Data.Entity;

namespace Cinema_Ticketing_System.Database
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class CinemaContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Film> Films { get; set; }

        public DbSet<Screening> Screenings { get; set; }

        public DbSet<Screen> Screens { get; set; }

        public CinemaContext()
        {
            
        }

    }
}
