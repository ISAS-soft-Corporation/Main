using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLive
{
    class Casting
    {
        public int c_id { get; set; }
        private int m_id;
        private int p_id;
        private string p_role;

        public Casting(int m_id, int p_id, string p_role)
        {
            this.m_id = m_id;
            this.p_id = p_id;
            this.p_role = p_role;
        }
        public int M_id
        {
            get { return m_id; }
            set { m_id = value; }
        }
        public int P_id
        {
            get { return p_id; }
            set { p_id = value; }
        }
        public string P_role
        {
            get { return p_role; }
            set { p_role = value; }
        }
    }
}
