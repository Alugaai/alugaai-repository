using BackEndASP.Entities;

namespace BackEndASP.DTOs.StudentDTOs
{
    public class StudentPropertyLikesDTO
    {
        public int? PropertyId { get; set; }


        public StudentPropertyLikesDTO()
        {

        }

        public StudentPropertyLikesDTO(PropertyStudentLikes entity)
        {
            this.PropertyId = entity.PropertyId != null ? entity.PropertyId : null;
        }
    }
}
