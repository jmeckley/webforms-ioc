using System.ComponentModel.DataAnnotations;

namespace WebApplication.Core.Model
{
    public class MyEntity
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Range(0, 150)]
        public int Age { get; set; }
    }
}