using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Homework1.Models
{
    public class StudentFilter
    {
        public int Id { get; set; }
        public string Number { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TcNo { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public string Sortby { get; set; }

        public bool IsNull()
        {
            return Id == 0 && Number == null && FirstName == null && LastName == null && TcNo == null && Gender == null && Department == null && Sortby == null;
        }
    }
}
