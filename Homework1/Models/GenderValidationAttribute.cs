using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Homework1.Models
{
    public class GenderValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (String.Equals(value, "M") || String.Equals(value, "F") || String.Equals(value, null))
                return true;
            ErrorMessage = "Accepted genders are: M, F";
            return false;
        }
    }
}
