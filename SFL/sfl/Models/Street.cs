using System.ComponentModel.DataAnnotations;

namespace sfl.Models
{
    public class Street
    {
        // These attributes are added as primary key in the OnCreateDatabase
        [StringLength(150)]
        public string? StreetName { get; set; }
        public int StreetNumber { get; set; }

        [Required]
        public string? CityCode { get; set; }
        [Required]
        public City? City { get; set; }
        public Branch? Branch { get; set; }
        public ICollection<Parcel>? Parcels { get; set; }
    }
}