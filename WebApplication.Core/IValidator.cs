using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Core
{
    public interface IValidator
    {
        bool Validate(object instance, out ICollection<ValidationResult> validationErrors);
    }
}