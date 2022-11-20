using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sfl.Models
{
    public class Branch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [StringLength(45)]
        public string? Name { get; set; }

        /*[Required]
        public string? CityCode { get; set; }
        [Required]
        public string? StreetName { get; set; }
        [Required]
        public int StreetNumber { get; set; }*/
        [Required]
        public Street? Street { get; set; }
        [Required]
        public ICollection<Staff>? Staff { get; set; }
    }
}