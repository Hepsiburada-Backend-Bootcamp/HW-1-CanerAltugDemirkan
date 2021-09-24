using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Homework1.Models
{
    public class DepartmentValidationAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string[] departments =
            {    
                "Architect",
                "ComputerEngineer",
                "ElectronicalEngineer"
            };
            if (departments.Contains(value))
                return true;

            ErrorMessage = "Accepted Departments are =";
            for(int i = 0; i<departments.Length; i++)
            {
                ErrorMessage = ErrorMessage + $" {departments[i]},";
            }
            return false;
        }
    }
}
