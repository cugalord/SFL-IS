using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sfl.Models
{
    public class Parcel
    {
        [Key]
        [StringLength(8)]
        public string? ID { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public int Width { get; set; }
        [Required]
        public int Depth { get; set; }

        [Required]
        public ParcelStatus? ParcelStatus { get; set; }

        [ForeignKey("RecipientStreet")]
        [Required]
        public string? RecipientCode { get; set; }
        [Required]
        public string? RecipientStreetName { get; set; }
        [Required]
        public int RecipientStreetNumber { get; set; }
        [Required]
        public virtual Street? RecipientStreet { get; set; }

        [ForeignKey("SenderStreet")]
        [Required]
        public string? SenderCode { get; set; }
        [Required]
        public string? SenderStreetName { get; set; }
        [Required]
        public int SenderStreetNumber { get; set; }
        [Required]
        public virtual Street? SenderStreet { get; set; }

        [Required]
        public ICollection<Job>? Jobs { get; set; }
    }
}