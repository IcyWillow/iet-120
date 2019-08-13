using System.ComponentModel.DataAnnotations;
using System.Linq;
using iet_120.ViewModel;

namespace iet_120.CustomValidationAttributes
{
    public class Unique : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var contains = UserViewModel.SharedViewModel().Users.Select(x => x.Email).Contains(value.ToString());
           
            if (contains)
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            
            return ValidationResult.Success;
        }
    }
}