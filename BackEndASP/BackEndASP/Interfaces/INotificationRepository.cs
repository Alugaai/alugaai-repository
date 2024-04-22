using BackEndASP.DTOs;

namespace BackEndASP.Interfaces
{
    public interface INotificationRepository
    {
        Task<IEnumerable<NotificationDTO>> GetNotifications(string userId);
        Task ReadNotification(int notificationId);
    }
}
