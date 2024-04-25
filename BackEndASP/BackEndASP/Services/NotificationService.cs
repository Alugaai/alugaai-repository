using AngleSharp.Attributes;
using BackEndASP.DTOs;
using BackEndASP.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEndASP.Services
{
    public class NotificationService : INotificationRepository
    {

        private readonly SystemDbContext _dbContext;

        public NotificationService(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<NotificationDTO>> GetNotifications(string userId)
        {
            var notifications = await _dbContext.Notifications.Include(n => n.User).Where(n => n.User.Id == userId).AsNoTracking().ToListAsync();
            return notifications.Select(n => new NotificationDTO(n)).ToList();
        }

        public async Task ReadNotification(int notificationId)
        {
            var notification = await _dbContext.Notifications.AsNoTracking().FirstOrDefaultAsync(n => n.Id == notificationId) ?? throw new ArgumentException("This notification id does not exists");
            notification.Read = true;
            _dbContext.Notifications.Update(notification);
        }

        public async Task<int> CountNotificationNotRead(string userId)
        {
            var count = await _dbContext.Notifications
                .Include(n => n.User)
                .Where(n => n.User.Id == userId && !n.Read)
                .CountAsync();

            return count;

        }


        public async Task<NotificationDTO> FindNotificationByUserId(string userId, string userWhoSendConnection)
        {
            var notification = await _dbContext.Notifications.Include(n => n.User).FirstOrDefaultAsync(n => n.User.Id == userId && !n.Read && n.UserIdWhoSendNotification == userWhoSendConnection);
            return new NotificationDTO(notification);
        }
    }
}
