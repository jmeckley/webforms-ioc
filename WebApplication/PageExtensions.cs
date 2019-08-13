using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Core;

namespace WebApplication
{
    public static class PageExtensions
    {
        public static void AddValidationErrors(this ValidatorCollection validator, IEnumerable<ValidationResult> results) =>
            results
                .Select(result => new CustomValidator { IsValid = false, ErrorMessage = result.ErrorMessage })
                .Each(validator.Add);
    }
}