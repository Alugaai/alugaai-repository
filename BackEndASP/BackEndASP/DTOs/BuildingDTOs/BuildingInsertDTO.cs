using System.ComponentModel.DataAnnotations;

namespace BackEndASP.DTOs.BuildingDTOs
{
    public class BuildingInsertDTO
    {

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(250)]
        public string Address { get; set; }
        [Required]
        [MaxLength(100)]
        public string Neighborhood { get; set; }
        [Required]
        [MaxLength(100)]
        public string District { get; set; }
        [Required]
        [MaxLength(50)]
        public string State { get; set; }
        [Required]
        public string? Number { get; set; }

    }
}
