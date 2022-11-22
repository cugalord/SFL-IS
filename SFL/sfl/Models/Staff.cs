using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sfl.Models
{
    public class Staff
    {
        [Key]
        [StringLength(20)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? Username { get; set; }
        [Required]
        [StringLength(85)]
        public string? Name { get; set; }
        [Required]
        [StringLength(85)]
        public string? Surname { get; set; }

        [Required]
        [ForeignKey("BranchID")]
        public int BranchID { get; set; }
        [Required]
        public virtual Branch? Branch { get; set; }
        public virtual ICollection<Job>? Jobs { get; set; }
        [Required]
        [ForeignKey(name: "RoleID")]
        public int RoleID { get; set; }
        [Required]
        public virtual StaffRole? Role { get; set; }
    }
}