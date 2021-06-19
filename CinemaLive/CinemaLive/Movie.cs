using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLive
{
    class Movie
    {
        public int m_id { get; set; }
        private string m_name;
        private float m_rating;
        private int m_year;
        private string m_desc, m_country, m_image, m_trailer;
        private int m_g;

        public Movie(string m_name, float m_rating, int m_year, string m_desc, 
            string m_country, string m_image, string m_trailer, int m_g)
        {
            this.m_rating = m_rating;
            this.m_name = m_name;
            this.m_year = m_year;
            this.m_desc = m_desc;
            this.m_country = m_country;
            this.m_image = m_image;
            this.m_trailer = m_trailer;
            this.m_g = m_g;
        }

        public string M_name
        {
            get { return m_name; }
            set { m_name = value; }
        }
        public float M_rating
        {
            get { return m_rating; }
            set { m_rating = value; }
        }
        public int M_year
        {
            get { return m_year; }
            set { m_year = value; }
        }
        public string M_desc
        {
            get { return m_desc; }
            set { m_desc = value; }
        }
        public string M_country
        {
            get { return m_country; }
            set { m_country = value; }
        }
        public string M_image
        {
            get { return m_image; }
            set { m_image = value; }
        }
        public string M_trailer
        {
            get { return m_trailer; }
            set { m_trailer = value; }
        }
        public int M_g
        {
            get { return m_g; }
            set { m_g = value; }
        }

    }
}
