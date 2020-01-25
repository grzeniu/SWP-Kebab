using System.ComponentModel.DataAnnotations;

namespace Kebab.Database.Models
{
    public class OrderDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Meal { get; set; }

        public string Kind { get; set; } = "";
        public string Sauce { get; set; } = "";
        [Required]
        public string Price { get; set; }
    }
}
