using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLive
{
    class Genre
    {
        public int GenreId { get; set; }
        private string g_name;
        public Genre() { }

        public Genre(string g_name)
        {
            this.g_name = g_name;
        }

        public string G_name
        {
            get { return g_name; }
            set { g_name = value; }
        }

    }
}
