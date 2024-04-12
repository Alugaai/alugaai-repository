using BackEndASP.DTOs.BuildingDTOs;
using BackEndASP.DTOs.Connection;
using BackEndASP.DTOs.PropertyDTOs;

namespace BackEndASP.DTOs.StudentDTOs
{
    public class StudentResponseForFindByEmail : UserFindAllInfoDTO
    {

        public ICollection<string>? Personalitys { get; set; }
        public ICollection<string>? Hobbies { get; set; }
        public ICollection<ConnectionDTO>? Connections { get; set; }
        public ICollection<string>? PendentsConnectionsId { get; set; }
        public BuildingResponseDTO? College { get; set; }
        public ICollection<StudentPropertyLikesDTO>? PropertiesLikes { get; set; }

        public StudentResponseForFindByEmail() { }

        public StudentResponseForFindByEmail(Student entity) : base(entity)
        {
            
            this.Personalitys = entity.Personalitys != null ? entity.Personalitys : null;
            this.Hobbies = entity.Hobbies!= null ? entity.Hobbies : null;
            this.Connections = entity.Connections != null ? entity.Connections.Select(c => new ConnectionDTO(c)).ToList() : null;
            this.PendentsConnectionsId = entity.PendentsConnectionsId != null ? entity.PendentsConnectionsId : null;
            this.College = entity.College != null ? new BuildingResponseDTO(entity.College) : null;
            this.PropertiesLikes = entity.PropertiesLikes != null ? entity.PropertiesLikes.Select(pl => new StudentPropertyLikesDTO(pl)).ToList() : null;


        }
    }
}
