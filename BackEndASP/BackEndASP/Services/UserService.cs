using BackEndASP.DTOs;
using BackEndASP.DTOs.StudentDTOs;
using BackEndASP.Entities;
using BackEndASP.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEndASP.Services
{
    public class UserService : IUserRepository
    {

        private SystemDbContext _dbContext;

        public UserService(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<dynamic> FindUserByEmail(string email)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email.Contains(email)) ?? throw new ArgumentException("Esse email nao existe");

            if (user.GetType() == typeof(Owner))
            {
                var owner = await _dbContext.Owners
                    .Include(p => p.UserNotifications)
                    .ThenInclude(un => un.Notification)
                    .Include(p => p.Properties)
                    .Include(p => p.Image)
                    .SingleOrDefaultAsync(u => u.Email.Contains(email)) ?? throw new ArgumentException("Esse email nao existe");
                return new OwnerResponseForFindByEmail((Owner)owner);
            }
            else if (user.GetType() == typeof(Student))
            {
                var student = await _dbContext.Students
                    .Include(s => s.UserNotifications)
                    .ThenInclude(un => un.Notification)
                    .Include(s => s.Image)
                    .Include(s => s.Connections)
                    .Include(s => s.PropertiesLikes)
                    .Include(s => s.College)
                    .SingleOrDefaultAsync(u => u.Email.Contains(email)) ?? throw new ArgumentException("Esse email nao existe");
                return new StudentResponseForFindByEmail((Student)student);
            }
            

            throw new ArgumentException("Esse email nao existe");
        }

        public async Task<dynamic> FindUserById(string userId)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == userId) ??
                       throw new ArgumentException("Esse id nao existe");

            if (user.GetType() == typeof(Owner))
            {
                var owner = await _dbContext.Owners
                    .Include(p => p.UserNotifications)
                    .ThenInclude(un => un.Notification)
                    .Include(p => p.Properties)
                    .Include(p => p.Image)
                    .SingleOrDefaultAsync(u => u.Id == userId) ?? throw new ArgumentException("Esse id nao existe");
                return new OwnerResponseForFindByEmail((Owner)owner);
            }
            else if (user.GetType() == typeof(Student))
            {
                var student = await _dbContext.Students
                    .Include(s => s.UserNotifications)
                    .ThenInclude(un => un.Notification)
                    .Include(s => s.Image)
                    .Include(s => s.Connections)
                    .Include(s => s.PropertiesLikes)
                    .Include(s => s.College)
                    .SingleOrDefaultAsync(u => u.Id == userId) ?? throw new ArgumentException("Esse id nao existe");
                return new StudentResponseForFindByEmail((Student)student);
            }

            throw new ArgumentException("Esse id nao existe");
        }
    }
}
