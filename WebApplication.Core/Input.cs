
using System.ComponentModel.DataAnnotations;
using System.IO;
using WebApplication.Core.Validation;

namespace WebApplication.Core
{
    public class Input
    {
        [Required]
        [MaxLength(256)]
        [PdfFileName]
        public string FileName { get; set; }

        [Required]
        [NotEmpty]
        public Stream Content { get; set; }
    }
}