using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sfl.Models
{
    public class StaffRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [Required]
        [StringLength(65)]
        public string? Name { get; set; }

        public ICollection<Staff>? Staff { get; set; }
    }
}