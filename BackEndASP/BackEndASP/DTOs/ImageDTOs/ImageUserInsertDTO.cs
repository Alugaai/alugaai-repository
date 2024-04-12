using System.ComponentModel.DataAnnotations;

namespace BackEndASP.DTOs.ImageDTOs
{
    public class ImageUserInsertDTO
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
