using System;
using System.ComponentModel.DataAnnotations;
namespace SIMS_IT0602.Models
{
	public class Teacher
	{
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime DoB { get; set; }

        [Required(ErrorMessage = "Course is required.")]
        public string Course { get; set; }
        public Teacher()
        {
        }
    }
}

