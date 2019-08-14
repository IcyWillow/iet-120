using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using iet_120.Model;

namespace iet_120.Notification
{
    internal class GenericValidationRule<T> : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) 
        {
            string result = string.Empty;
            BindingGroup bindingGroup = (BindingGroup)value;
            foreach (var item in bindingGroup.Items.OfType<T>()) 
            {
                Type type = item.GetType();
                // First we find all properties that have attributes.
                foreach (var pi in type.GetProperties()) 
                {
                    foreach (var attrib in Attribute.GetCustomAttributes(pi, true)) 
                    {
                        // We continue if we found a data annotation
                        if (attrib is System.ComponentModel.DataAnnotations.ValidationAttribute validationAttribute)
                        {
                            if (bindingGroup.TryGetValue(item, pi.Name, out object val))
                            {
                                if (val == DependencyProperty.UnsetValue)
                                    val = null;
                                // We set the error message if the validation of the property fails.
                                if (!validationAttribute.IsValid(val)) 
                                {
                                    if (!string.IsNullOrWhiteSpace(result))
                                        result += Environment.NewLine;
                                    if (string.IsNullOrEmpty(validationAttribute.ErrorMessage))
                                        result += string.Format("Validation on {0} failed!", pi.Name);
                                    else
                                        result += validationAttribute.ErrorMessage;
                                }
                            }
                        }
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(result))
                return new ValidationResult(false, result);
            else
                return ValidationResult.ValidResult;
        }
    }
    
    internal class UserValidationRule : GenericValidationRule<User> { }
}