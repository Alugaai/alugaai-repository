using BackEndASP.DTOs.UserDTOs;
using BackEndASP.Entities;
using BackEndASP.Interfaces;

namespace BackEndASP.Services
{
    public class OwnerService : IOwnerRepository
    {

        private readonly SystemDbContext _dbContext;

        public OwnerService(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CompleteProfileOwner(string userId, UserCompleteProfileDTO dto)
        {
            var owner = await _dbContext.Owners.FindAsync(userId)
                ?? throw new ArgumentException($"User with id {userId} does not exist");

            InsertDTOToOwnerAsync(dto, owner);

            _dbContext.Owners.Update(owner);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private void InsertDTOToOwnerAsync(UserCompleteProfileDTO dto, Owner owner)
        {
            if (dto.Username != null)
            {
                owner.UserName = dto.Username;
            }

            if (dto.Gender != null)
            {
                owner.Gender = dto.Gender;
            }

            if (dto.BirthDate != null)
            {
                owner.BirthDate = (DateTimeOffset)dto.BirthDate;
            }

            if (dto.PhoneNumber != null)
            {
                owner.PhoneNumber = dto.PhoneNumber;
            }

        }
    }
}
