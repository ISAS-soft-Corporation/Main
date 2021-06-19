using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLive
{
    class Casting
    {
        public int m_id { get; set; }
        public int p_id { get; set; }
        private string p_role;

        public Casting(string p_role)
        {
            this.p_role = p_role;
        }
        public string P_role
        {
            get { return p_role; }
            set { p_role = value; }
        }
    }
}
