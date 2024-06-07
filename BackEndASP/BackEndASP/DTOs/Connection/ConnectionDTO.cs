using BackEndASP.Entities;

namespace BackEndASP.DTOs.Connection
{
    public class ConnectionDTO
    {

        public string OtherStudentId { get; set; }

        public ConnectionDTO()
        {
            
        }

        public ConnectionDTO(UserConnection entity)
        {
            this.OtherStudentId = entity.OtherStudentId;
        }

    }
}
