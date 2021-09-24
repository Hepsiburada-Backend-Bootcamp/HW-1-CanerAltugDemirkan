using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Homework1.Models
{
    public class StudentDto
    {
        [Required]
        public int Id { get; set; }
        [RegularExpression("[0-9]{10}")]
        public string Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [RegularExpression("[0-9]{11}")]
        public string TcNo { get; set; }
        [GenderValidation]
        public string Gender { get; set; }
        [DepartmentValidation]
        public string Department { get; set; }
        public bool IsNull()
        {
            return Id == 0 && 
                Number == null && 
                FirstName == null && 
                LastName == null && 
                TcNo == null && 
                Gender == null && 
                Department == null;
        }
    }
}
