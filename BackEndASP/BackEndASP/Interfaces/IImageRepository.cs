using BackEndASP.DTOs.ImageDTOs;
using SixLabors.ImageSharp;

namespace BackEndASP.Interfaces
{
    public interface IImageRepository
    {
        Task InsertImageForAUser(IFormFileCollection file, string userId);
        Task InsertImageForProperty(IFormFileCollection files, string userId, int propertyId);
        Task InsertImageForCollege(IFormFileCollection files, int collegeId);    
    }
}
