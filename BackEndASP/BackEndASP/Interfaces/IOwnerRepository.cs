using BackEndASP.DTOs.StudentDTOs;
using BackEndASP.DTOs.UserDTOs;

namespace BackEndASP.Interfaces
{
    public interface IOwnerRepository
    {
        Task<bool> CompleteProfileOwner(string userId, UserCompleteProfileDTO dto);
    }
}
