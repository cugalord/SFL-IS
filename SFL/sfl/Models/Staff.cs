using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sfl.Models
{
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(20)]
        public string? Username { get; set; }
        [Required]
        [StringLength(85)]
        public string? Name { get; set; }
        [Required]
        [StringLength(85)]
        public string? Surname { get; set; }

        [Required]
        public Branch? Branch { get; set; }
        public ICollection<Job>? Jobs { get; set; }
        [Required]
        public StaffRole? Role { get; set; }
    }
}