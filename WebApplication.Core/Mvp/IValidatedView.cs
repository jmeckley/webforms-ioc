using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Core.Mvp
{
    public interface IValidatedView
    {
        void SetValidationErrors(IEnumerable<ValidationResult> results);
    }
}