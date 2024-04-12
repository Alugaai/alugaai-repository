using BackEndASP.DTOs.CordinatesDTOs;
using BackEndASP.DTOs.ImageDTOs;
using BackEndASP.DTOs.StudentDTOs;

namespace BackEndASP.DTOs.CollegeDTOs
{
    public class CollegeResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? Number { get; set; }
        public string Neighborhood { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public CordinatesDTO Position { get; set; }
        public ICollection<ImageBuidingDTO>? Images { get; set; }

        public CollegeResponseDTO()
        {
            
        }

        public CollegeResponseDTO(College entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Address = entity.Address;
            this.Number = entity.Number;
            this.Neighborhood = entity.Neighborhood;
            this.District = entity.District;
            this.State = entity.State;
            this.Position = new CordinatesDTO(entity.Lat, entity.Long);
        }

    }
}
