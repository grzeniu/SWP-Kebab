using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kebab.Database.Models
{
    public class OrderDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Meal { get; set; }

        public string Kind { get; set; } = "";
        public string Sauce { get; set; } = "";
        [Required]
        public string Price { get; set; }
    }
}
