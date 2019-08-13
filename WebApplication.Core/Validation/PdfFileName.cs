using System.ComponentModel.DataAnnotations;

namespace WebApplication.Core.Validation
{
    public class PdfFileName
        : RegularExpressionAttribute
    {
        public PdfFileName() 
            : base(@"^[\w\d\s_-]+\.pdf$")
        {
        }
    }
}