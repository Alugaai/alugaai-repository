using BackEndASP.DTOs.BuildingDTOs;
using System.ComponentModel.DataAnnotations;

namespace BackEndASP.DTOs.PropertyDTOs
{
    public class PropertyInsertDTO : BuildingInsertDTO
    {
        [Required]
        public string HomeComplement { get; set; }
        [Required]
        public string Bedrooms { get; set; }
        [Required]
        public string Bathrooms { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }

    }
}
