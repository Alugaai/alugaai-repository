using BackEndASP.DTOs;

namespace BackEndASP.Interfaces
{
    public interface INotificationRepository
    {
        Task<IEnumerable<NotificationDTO>> GetNotifications(string userId);
        Task ReadNotification(int notificationId);
        Task<int> CountNotificationNotRead(string userId);
        Task<NotificationDTO> FindNotificationByUserId(string userId, string userWhoSendConnection);
    }
}
