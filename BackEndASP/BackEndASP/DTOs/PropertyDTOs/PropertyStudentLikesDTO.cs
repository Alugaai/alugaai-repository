using BackEndASP.Entities;

namespace BackEndASP.DTOs.PropertyDTOs
{
    public class PropertyStudentLikesDTO
    {

        public string StudentId { get; set; }


        public PropertyStudentLikesDTO()
        {

        }

        public PropertyStudentLikesDTO(PropertyStudentLikes entity)
        {
            StudentId = entity.StudentId;
        }

    }
}
