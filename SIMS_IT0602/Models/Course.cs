using System;
namespace SIMS_IT0602.Models
{
	public class Course
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string Major { get; set; }
        public string Lecturer { get; set; }
        public string Status { get; set; }
        public string Teacher { get; internal set; }

        public Course()
        {
        }
    }
}

