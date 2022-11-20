using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sfl.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        // Can be null.
        public DateTime DateCompleted { get; set; }

        [Required]
        public JobStatus? JobStatus { get; set; }
        [Required]
        public JobType? JobType { get; set; }
        [Required]
        public Staff? Staff { get; set; }
        [Required]
        public ICollection<Parcel>? Parcels { get; set; }
    }
}