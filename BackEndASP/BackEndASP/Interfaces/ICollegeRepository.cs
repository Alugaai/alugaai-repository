using BackEndASP.DTOs.BuildingDTOs;
using BackEndASP.DTOs.CollegeDTOs;

namespace BackEndASP.Interfaces
{
    public interface ICollegeRepository
    {
        Task<CollegeResponseDTO> FindCollegeByIdAsync(int id);
        Task<IEnumerable<CollegeResponseDTO>> FindAllCollegesAsync();
        Task InsertCollege(BuildingInsertDTO dto);
        Task AddUserToCollege(int collegeId, string userId);
    }
}
