using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sfl.Models
{
    public class ParcelStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [Required]
        [StringLength(45)]
        public string? Name { get; set; }

        public ICollection<Parcel>? Parcels { get; set; }
    }
}