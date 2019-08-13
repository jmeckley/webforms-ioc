using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers
{
    public class ViewModel
    {
        [Required]
        [MaxLength(256)]
        public string Value { get; set; }
    }
}