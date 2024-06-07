using BackEndASP.DTOs.ImageDTOs;
using BackEndASP.DTOs;
using BackEndASP.DTOs.BuildingDTOs;
using BackEndASP.DTOs.Connection;
using BackEndASP.DTOs.PropertyDTOs;
using BackEndASP.Entities;

namespace BackEndASP.DTOs
{
    public class UserFindAllInfoDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public ImageUserDTO? ImageUser { get; set; }
        public ICollection<NotificationDTO>? Notification { get; set; }

        public UserFindAllInfoDTO()
        {
            
        }

        public UserFindAllInfoDTO(User entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Email = entity.Email;
            this.BirthDate = entity.BirthDate;
            this.ImageUser = entity.Image != null ? new ImageUserDTO(entity.Image) : null;
            this.Notification = entity.Notifications != null ? entity.Notifications.Select(n => new NotificationDTO(n)).ToList() : null;
            this.Gender = entity.Gender != null ? entity.Gender : null;
        }

    }
}
