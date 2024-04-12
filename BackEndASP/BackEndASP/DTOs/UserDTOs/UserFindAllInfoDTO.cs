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
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public ImageUserDTO? ImageUser { get; set; }
        public ICollection<NotificationDTO>? Notification { get; set; }

        public UserFindAllInfoDTO()
        {
            
        }

        public UserFindAllInfoDTO(User entity)
        {
            this.Id = entity.Id;
            this.Username = entity.UserName;
            this.Email = entity.Email;
            this.BirthDate = entity.BirthDate;
            this.ImageUser = entity.Image != null ? new ImageUserDTO(entity.Image) : null;
            this.Notification = entity.UserNotifications != null ? entity.UserNotifications.Select(un => new NotificationDTO(un.Notification)).ToList() : null;
        }

    }
}
