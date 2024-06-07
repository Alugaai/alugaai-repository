using BackEndASP.DTOs.ImageDTOs;
using BackEndASP.Entities;

namespace BackEndASP.DTOs.PropertyDTOs
{
    public class FindPropertyDetailsByIdDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? Number { get; set; }
        public string? HomeComplement { get; set; }
        public string Neighborhood { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public ICollection<ImageBuidingDTO>? Images { get; set; }
        public double Price { get; set; }
        public string Bedrooms { get; set; }
        public string Bathrooms { get; set; }
        public string Description { get; set; }
        public OwnerDTO Owner { get; set; }

        public ICollection<PropertyStudentLikesDTO>? StudentLikes { get; set; } = new List<PropertyStudentLikesDTO>();


        public FindPropertyDetailsByIdDTO()
        {
            
        }

        public FindPropertyDetailsByIdDTO(Property entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Address = entity.Address;
            this.Number = entity.Number;
            this.HomeComplement = entity.HomeComplement;
            this.Neighborhood = entity.Neighborhood;
            this.District = entity.District;
            this.State = entity.State;
            this.Images = entity.Images.Count > 0 ? entity.Images.Select(img => new ImageBuidingDTO(img)).ToList() : null;
            this.Price = entity.Price;
            this.Bedrooms = entity.Bedrooms;
            this.Bathrooms = entity.Bathrooms;
            this.Description = entity.Description;
            this.Owner = new OwnerDTO(entity.Owner);
            this.StudentLikes = entity.StudentLikes.Select(sl => new PropertyStudentLikesDTO(sl)).ToList();

        }



    }
}
