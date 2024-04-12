using BackEndASP.DTOs.ImageDTOs;

namespace BackEndASP.DTOs.PropertyDTOs
{
    public class PropertyDTO
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
        public ICollection<PropertyStudentLikesDTO>? StudentLikes { get; set; } = new List<PropertyStudentLikesDTO>();


        public PropertyDTO()
        {
            
        }

        public PropertyDTO(Property entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Address = entity.Address;
            this.Number = entity.Number;
            this.HomeComplement = entity.HomeComplement;
            this.Neighborhood = entity.Neighborhood;
            this.District = entity.District;
            this.State = entity.State;
            this.Images = entity.Images != null ? entity.Images.Select(img => new ImageBuidingDTO(img)).ToList() : null;
            this.Price = entity.Price;
            this.Bedrooms = entity.Bedrooms;
            this.Bathrooms = entity.Bathrooms;
            this.Description = entity.Description;
            this.StudentLikes = entity.StudentLikes.Select(sl => new PropertyStudentLikesDTO(sl)).ToList();
        }
    }
}
