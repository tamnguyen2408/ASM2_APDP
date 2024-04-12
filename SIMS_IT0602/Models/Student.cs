﻿using System.ComponentModel.DataAnnotations;
using static SIMS_IT0602.Controllers.StudentController;

namespace SIMS_IT0602.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime DoB { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailValidation(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        [Required(ErrorMessage ="Major is required.")]
        public string Major { get; set; }
        public Student()
        {
        }
    }
}
