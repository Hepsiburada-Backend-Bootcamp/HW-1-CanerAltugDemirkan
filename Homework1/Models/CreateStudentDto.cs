using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Homework1.Models
{
    public class CreateStudentDto
    {
        [RegularExpression("[0-9]{10}")]
        [Required]
        public string Number { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("[0-9]{11}")]
        public string TcNo { get; set; }
        [Required]
        [GenderValidation]
        public string Gender { get; set; }
        [Required]
        [DepartmentValidation]
        public string Department { get; set; }

        public bool IsNull()
        {
            return Number == null && FirstName == null && LastName == null && TcNo == null && Gender == null && Department == null;
        }
    }
}
