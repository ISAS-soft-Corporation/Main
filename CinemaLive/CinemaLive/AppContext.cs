using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CinemaLive
{
    class AppContext : DbContext
    {
        public DbSet<User> users { get; set; }

        public AppContext() : base("DefaultConnection") { }

    }
}
