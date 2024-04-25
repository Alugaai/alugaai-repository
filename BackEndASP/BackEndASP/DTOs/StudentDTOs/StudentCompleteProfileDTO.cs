using BackEndASP.DTOs.UserDTOs;

namespace BackEndASP.DTOs.StudentDTOs
{
    public class StudentCompleteProfileDTO : UserCompleteProfileDTO
    {

        public int CollegeId { get; set; }

    }
}
