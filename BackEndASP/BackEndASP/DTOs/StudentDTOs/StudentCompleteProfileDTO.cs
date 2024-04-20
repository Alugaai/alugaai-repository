using BackEndASP.DTOs.UserDTOs;

namespace BackEndASP.DTOs.StudentDTOs
{
    public class StudentCompleteProfileDTO : UserCompleteProfileDTO
    {

        public List<string> Hobbies { get; set; }
        public List<string> Personalitys { get; set; }
        public int CollegeId { get; set; }

    }
}
