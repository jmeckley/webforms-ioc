using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers
{
    public class MyEntityViewModel
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, Range(0, 150)]
        public int Age { get; set; }
    }
}