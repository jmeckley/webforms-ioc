using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace WebApplication.Core.Validation
{
    public class NotEmptyAttribute
        : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is IEnumerable collection) return collection.Cast<object>().Any();
            if (value is Stream stream) return stream.Length > 0;
            return false;
        }

        public override string FormatErrorMessage(string name) => $"{name} cannot be empty.";

        public override bool RequiresValidationContext => true;

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if(value == null) return ValidationResult.Success;

            var displayName = context.DisplayName;
            if (value is IEnumerable || value is Stream)
            {
                return IsValid(value) 
                    ? ValidationResult.Success 
                    : new ValidationResult(FormatErrorMessage(displayName), new []{ context.MemberName });
            }

            var errorMessage = $"Cannot convert {context.ObjectType.FullName}.{displayName} from {value.GetType().FullName} to {typeof(IEnumerable).FullName}.";
            throw new ValidationException(errorMessage);
        }
    }
}