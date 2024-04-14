using System;
using System.ComponentModel.DataAnnotations;

namespace SIMS_IT0602.Models
{
    public class Class
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Class name is required.")]
        [StringLength(50, ErrorMessage = "Class name must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string ClassName { get; set; }

        [Required(ErrorMessage = "Major is required.")]
        public string Major { get; set; }

        [Required(ErrorMessage = "Lecturer name is required.")]
        [StringLength(50, ErrorMessage = "Lecturer name must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Lecturer { get; set; }
    }
}
