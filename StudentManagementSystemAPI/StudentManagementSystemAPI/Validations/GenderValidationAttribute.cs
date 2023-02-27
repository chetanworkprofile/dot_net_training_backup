using StudentManagementSystemAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystemAPI.Validations
{
    public class GenderValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Console.Write(value);
            if((string?)value != "Male"  && (string?)value != "Female" && (string?)value != "Other")
            {
                return new ValidationResult("Gender input not in proper format");
            }
            return ValidationResult.Success;
        }
    }
}
