using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLive
{
    class Person
    {
        public int p_id { get; set; }
        private string p_name;

        public Person(string p_name)
        {
            this.p_name = p_name;
        }
        public string P_name
        {
            get { return p_name; }
            set { p_name = value; }
        }
    }
}
