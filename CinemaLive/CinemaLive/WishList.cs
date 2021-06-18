using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLive
{
    class WishList
    {
        public int w_id { get; set; }
        private int u_id, m_id;
        public WishList(int u_id, int m_id)
        {
            this.u_id = u_id;
            this.m_id = m_id;
        }

        public int U_id
        {
            get { return u_id; }
            set { u_id = value; }
        }
        public int M_id
        {
            get { return m_id; }
            set { m_id = value; }
        }

    }
}
