
using BackEndASP.DTOs.ImageDTOs;
using BackEndASP.Entities;

namespace BackEndASP.DTOs
{
    public class OwnerDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ImageUserDTO Image { get; set; }

        public OwnerDTO()
        {
            
        }


        public OwnerDTO(Owner entity)
        {
            this.Id = entity.Id;
            this.Name = entity.UserName;
            this.Email = entity.Email;
            this.PhoneNumber = entity.PhoneNumber;
            this.Image = entity.Image != null ? new ImageUserDTO(entity.Image) : null;
        }
    }
}
