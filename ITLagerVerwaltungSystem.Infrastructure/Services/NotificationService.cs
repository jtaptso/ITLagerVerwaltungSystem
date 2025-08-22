using System;
using System.Collections.Generic;
using ITLagerVerwaltungSystem.Core.DTOs;
using ITLagerVerwaltungSystem.Core.Services;

namespace ITLagerVerwaltungSystem.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _dbContext;

        public NotificationService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Pending notifications are those not marked as read
        public IEnumerable<NotificationDto> GetPendingNotifications()
        {
            return _dbContext.Notifications
                .Where(n => !n.IsRead)
                .Select(n => new NotificationDto
                {
                    Id = n.Id,
                    UserId = n.UserId,
                    Message = n.Message,
                    Date = n.Date,
                    IsRead = n.IsRead
                })
                .ToList();
        }

        // Approve means mark as read
        public bool ApproveNotification(int id)
        {
            var notification = _dbContext.Notifications.Find(id);
            if (notification == null) return false;
            notification.IsRead = true;
            _dbContext.SaveChanges();
            return true;
        }

        // Mark notification as unread
        public bool MarkAsUnread(int id)
        {
            var notification = _dbContext.Notifications.Find(id);
            if (notification == null) return false;
            notification.IsRead = false;
            _dbContext.SaveChanges();
            return true;
        }

        // Create a new notification
        public NotificationDto CreateNotification(string userId, string message)
        {
            var notification = new ITLagerVerwaltungSystem.Core.Domain.Notification
            {
                UserId = userId,
                Message = message,
                Date = DateTime.UtcNow,
                IsRead = false
            };
            _dbContext.Notifications.Add(notification);
            _dbContext.SaveChanges();
            return new NotificationDto
            {
                Id = notification.Id,
                UserId = notification.UserId,
                Message = notification.Message,
                Date = notification.Date,
                IsRead = notification.IsRead
            };
        }

        // Get notifications for a specific user
        public IEnumerable<NotificationDto> GetNotificationsForUser(string userId)
        {
            return _dbContext.Notifications
                .Where(n => n.UserId == userId)
                .Select(n => new NotificationDto
                {
                    Id = n.Id,
                    UserId = n.UserId,
                    Message = n.Message,
                    Date = n.Date,
                    IsRead = n.IsRead
                })
                .ToList();
        }
    }
}
