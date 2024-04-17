using System;
using System.ComponentModel.DataAnnotations;
using static SIMS_IT0602.Controllers.StudentController;

namespace SIMS_IT0602.Models
{
	public class Course
	{
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Class name is required.")]
        [StringLength(50, ErrorMessage = "Class name must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Class { get; set; }

        [Required(ErrorMessage = "Major is required.")]
        public string Major { get; set; }

        [Required(ErrorMessage = "Lecturer name is required.")]
        [StringLength(50, ErrorMessage = "Lecturer name must be between {2} and {1} characters long.", MinimumLength = 3)] public string Lecturer { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }


        public Course()
        {
        }
    }
}

