using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebApplication.Core.Implementation
{
    public class DataAnnotationValidator
        : IValidator
    {
        private readonly IServiceProvider _provider;

        public DataAnnotationValidator(IServiceProvider provider)
        {
            _provider = provider;
        }

        public bool Validate(object instance, out ICollection<ValidationResult> validationErrors)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(instance, _provider, new Dictionary<object, object>());
            var isValid = Validator.TryValidateObject(instance, context, results,true);
            validationErrors = results.Where(result => result != ValidationResult.Success).ToList();

            return isValid;
        }
    }
}