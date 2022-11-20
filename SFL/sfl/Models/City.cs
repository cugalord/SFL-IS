using System.ComponentModel.DataAnnotations;

namespace sfl.Models
{
    public class City
    {
        [Key]
        [StringLength(20)]
        public string? Code { get; set; }
        [Required]
        [StringLength(145)]
        public string? Name { get; set; }

        public ICollection<Street>? Streets { get; set; }
    }
}