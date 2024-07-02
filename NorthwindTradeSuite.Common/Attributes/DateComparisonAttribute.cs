using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Common.Attributes
{
    public class DateComparisonAttribute : ValidationAttribute
    {
        private readonly string startDatePropertyName = null!;

        public DateComparisonAttribute(string startDatePropertyName)
        {
            this.startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var endDateValue = (DateTime)value!;

            var startDatePropertyInfo = validationContext.ObjectType.GetProperty(startDatePropertyName)!;
            
            if (startDatePropertyInfo == null)
            {
                throw new ArgumentException("Invalid start date property name.");
            }

            DateTime startDateValue = (DateTime)startDatePropertyInfo.GetValue(validationContext.ObjectInstance)!;

            if (endDateValue < startDateValue)
            {
                if (string.IsNullOrEmpty(ErrorMessage))
                {
                    ErrorMessage = "End date must be after the start date";
                }

                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
