using System.ComponentModel.DataAnnotations;

namespace Kebab.Database.Models
{
    public class Metadata
    {
        [Key]
        public string Label { get; set; }
        [Required]
        public string Value { get; set; }

    }
}